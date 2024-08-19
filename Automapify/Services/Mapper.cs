using Automapify.Models;
using Automapify.Services;
using Automapify.Services.Attributes;
using Automapify.Services.Utilities;
using System.Collections;
using System.Reflection;

namespace Automappify.Services
{
    /// <summary>
    /// A mapping service to map values from a source object to a destination object
    /// </summary>
    public static class Mapper
    {

        /// <summary>
        /// Map values from a source object to an existing destination object
        /// </summary>
        /// <typeparam name="TSource">Source object</typeparam>
        /// <typeparam name="TDestination">Destination object</typeparam>
        /// <param name="destinationObj">Object to store gathered data</param>
        /// <param name="sourceObj">Object to gather information or data from</param>
        public static void Map<TSource,TDestination>(this TDestination destinationObj, TSource sourceObj, MapifyConfiguration mapifyConfiguration = null)
        {
            try
            {
                var destinationType = destinationObj.GetType();

                var isEnumerable = IsEnumerable(destinationType);

                if (isEnumerable)
                {
                    destinationType = destinationType.GetGenericArguments().FirstOrDefault();

                    var destination = destinationObj as IList;

                    MapCollection<TSource, TDestination>(sourceObj, destination, destinationType, mapifyConfiguration);

                    destinationObj = (TDestination)destination;

                    return;
                }   

                var destinationProperties = destinationType.GetProperties();

                var sourceProperties = sourceObj.GetType().GetProperties();
            
                var mappingAttributes = destinationType.GetAttributes<MapPropertyAttribute>();

                MapProperties(destinationProperties, sourceProperties,
                    mapifyConfiguration, sourceObj, destinationObj,
                    mappingAttributes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }


        /// <summary>
        /// Map values from a source object to a new destination object.
        /// </summary>
        /// <typeparam name="TSource">Source object</typeparam>
        /// <typeparam name="TDestination">Destination object</typeparam>
        /// <param name="sourceObj">Object to gather information or data from</param>
        /// <returns>Destination object</returns>
        public static TDestination Map<TSource, TDestination>(this TSource sourceObj, MapifyConfiguration mapifyConfiguration = null)
        {
            try
            {
                var destinationType = typeof(TDestination);

                var isEnumerable = IsEnumerable(destinationType);

                if (isEnumerable)
                {
                    destinationType = destinationType.GetGenericArguments().FirstOrDefault();

                    Type openType = typeof(List<>);

                    Type target = openType.MakeGenericType(new[]{ destinationType });

                    var destination = (TDestination)Activator.CreateInstance(target) as IList;

                    MapCollection<TSource,TDestination>(sourceObj, destination, destinationType, mapifyConfiguration);

                    return (TDestination)destination;
                }

                TDestination destinationObj = Activator.CreateInstance<TDestination>();

                var destinationProperties = destinationType.GetProperties();

                var sourceProperties = sourceObj.GetType().GetProperties();

                var mappingAttributes = destinationType.GetAttributes<MapPropertyAttribute>();

                MapProperties(destinationProperties, sourceProperties,
                    mapifyConfiguration,sourceObj,destinationObj, 
                    mappingAttributes);

                return destinationObj;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        private static void MapCollection<TSource,TDestination>(TSource sourceObj, IList destinationObj ,Type destinationType, MapifyConfiguration mapifyConfiguration = null)
        {
            var sourceType = typeof(TSource);

            var destinationProperties = destinationType.GetProperties();

            var mappingAttributes = destinationType.GetAttributes<MapPropertyAttribute>();

            PropertyInfo[] sourceProperties;

            dynamic destination = Activator.CreateInstance(destinationType);

            if (sourceObj is ICollection sourceCollection)
            {
                sourceProperties = sourceType.GetGenericArguments().FirstOrDefault().GetProperties();

                foreach (var source in sourceCollection)
                {
                    destination = Activator.CreateInstance(destinationType);

                    MapProperties(destinationProperties, sourceProperties,
                    mapifyConfiguration, source, destination,
                    mappingAttributes);

                    destinationObj.Add(destination);
                }
            }
            else
            {
                sourceProperties = sourceType.GetProperties();

                MapProperties(destinationProperties, sourceProperties,
                mapifyConfiguration, sourceObj, destination,
                mappingAttributes);

                destinationObj.Add(destination);
            }
        }

        private static void MapProperties<TSource,TDestination>(PropertyInfo[] destinationProperties,
            PropertyInfo[] sourceProperties,MapifyConfiguration mapifyConfiguration,
            TSource sourceObj, TDestination destinationObj,Dictionary<string, MapPropertyAttribute[]> mappingAttributes)
        {
            foreach (var destinationProperty in destinationProperties)
            {
                object propertyValue = null;
                var propertyExpression = GetSourceExpression(mapifyConfiguration?.MapifyTuples, destinationProperty.Name);
                if (propertyExpression != null)
                {
                    propertyValue = propertyExpression.DynamicInvoke(sourceObj);
                }
                else
                {
                    var sourceProperty = GetProperyInfo<TSource>(mappingAttributes, sourceProperties, destinationProperty);
                    if (sourceProperty != null)
                    {
                        propertyValue = sourceProperty.GetValue(sourceObj);
                    }
                }

                if (propertyValue != null)
                {
                    destinationProperty.SetValue(destinationObj, propertyValue);
                }
            }
        }

        private static PropertyInfo GetProperyInfo<TSource>(Dictionary<string, MapPropertyAttribute[]> mappingAttributes, PropertyInfo[] sourceProperties, PropertyInfo destinationProperty)
        {
            PropertyInfo sourceProperty = null;

            mappingAttributes.TryGetValue(destinationProperty.Name, out MapPropertyAttribute[] attributes);
            if (attributes != null)
            {
                var currentAttribute = attributes.Where(s => s.SourceType == typeof(TSource)).FirstOrDefault();
                if (currentAttribute == null)
                {
                    var attribute = attributes.FirstOrDefault();
                    sourceProperty = sourceProperties.Where(s => s.Name == attribute.PropertyName).FirstOrDefault();
                }
                else
                {
                    sourceProperty = sourceProperties.Where(s => s.Name == currentAttribute.PropertyName).FirstOrDefault();
                }
            }
            else
            {
                sourceProperty = sourceProperties.Where(s => s.Name == destinationProperty.Name).FirstOrDefault();
            }
            return sourceProperty;
        }


        private static Delegate GetSourceExpression(IList<MapifyTuple> mapifyTuples, string propertyName)
        {
            if (mapifyTuples == null)
                return default;

            var memberExpr = mapifyTuples.Where(s => s.DestinationMemberName == propertyName).FirstOrDefault();
            if(memberExpr != null)
            {
                return memberExpr.SourceExpression;
            }

            return default;
        }

        private static bool IsEnumerable(Type type)
        {
            return (type.GetInterface(nameof(IEnumerable)) != null);
        }

        private static bool IsEnumerable<type>()
        {
            return typeof(type).GetInterface(nameof(IEnumerable)) != null;
        }
    }
}
