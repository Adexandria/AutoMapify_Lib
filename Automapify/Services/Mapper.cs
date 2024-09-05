using Automapify.Models;
using Automapify.Services;
using Automapify.Services.Attributes;
using Automapify.Services.Extensions;
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

                var sourceType = typeof(TSource);

                var isEnumerable = IsEnumerable(destinationType);

                if (isEnumerable)
                {
                    destinationType = destinationType.GetGenericArguments().FirstOrDefault();

                    var destination = destinationObj as IList;

                    MapCollection(sourceObj, destination, destinationType, sourceType,mapifyConfiguration);

                    destinationObj = (TDestination)destination;

                    return;
                }   

                var destinationProperties = destinationType.GetProperties();

                var sourceProperties = sourceType.GetProperties();
            
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

                var sourceType = typeof(TSource);

                var isEnumerable = IsEnumerable(destinationType);

                if (isEnumerable)
                {
                    destinationType = destinationType.GetGenericArguments().FirstOrDefault();

                    Type openType = typeof(List<>);

                    Type target = openType.MakeGenericType(new[]{ destinationType });

                    var destination = (TDestination)Activator.CreateInstance(target) as IList;

                    MapCollection(sourceObj, destination, destinationType,sourceType ,mapifyConfiguration);

                    return (TDestination)destination;
                }

                TDestination destinationObj = Activator.CreateInstance<TDestination>();

                var destinationProperties = destinationType.GetProperties();

                var sourceProperties = sourceType.GetProperties();

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

        /// <summary>
        /// Handles mapping for collection
        /// </summary>
        /// <typeparam name="TSource">Type of the source</typeparam>
        /// <param name="sourceObj">Source object</param>
        /// <param name="destinationObj">Destination object</param>
        /// <param name="destinationType">Destination type</param>
        /// <param name="sourceType">Source Type</param>
        /// <param name="mapifyConfiguration">Mapify configuration</param>
        private static void MapCollection<TSource>(TSource sourceObj, IList destinationObj ,Type destinationType,
            Type sourceType,
            MapifyConfiguration mapifyConfiguration = null)
        {
            if(sourceObj == null)
            {
                return;
            }
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

        /// <summary>
        /// Maps system collection
        /// </summary>
        /// <param name="sourceObj">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <param name="destinationName">Destination name</param>
        /// <param name="mapifyConfiguration">Mapify configuration</param>

        private static void MapCollection(object? sourceObj, IList destination,string destinationName,
            MapifyConfiguration mapifyConfiguration = null)
        {
             if( sourceObj is ICollection sourceCollection)
             {
                var propertyExpression = GetSourceExpression(mapifyConfiguration?.MapifyTuples, destinationName);
                foreach (var source in sourceCollection)
                {
                    var result = propertyExpression?.DynamicInvoke(source);

                    if (result != null)
                        destination.Add(result);
                }
             }
        }

        private static void MapProperties<TSource,TDestination>(PropertyInfo[] destinationProperties,
            PropertyInfo[] sourceProperties,MapifyConfiguration mapifyConfiguration,
            TSource sourceObj, TDestination destinationObj,Dictionary<string, MapPropertyAttribute[]> mappingAttributes)
        {
            foreach (var destinationProperty in destinationProperties)
            {
                object propertyValue = null;

                if (destinationProperty.GetCustomAttribute<IgnoreAttribute>() != null)
                    continue;

                var isIgnored = mapifyConfiguration?.MapifyTuples.Where(s => s.IsIgnored && s.DestinationMemberNames.Contains(destinationProperty.Name)).Select(s => s.IsIgnored).FirstOrDefault();
                if (isIgnored.GetValueOrDefault())
                {
                    continue;
                }


                if (destinationProperty.PropertyType.GetGenericArguments().FirstOrDefault() is Type type && mapifyConfiguration != null)
                {
                    Type openType = typeof(List<>);

                    Type target = openType.MakeGenericType(new[] { type });

                    var destination = Activator.CreateInstance(target) as IList;

                    var config = GetSourceConfiguration(mapifyConfiguration?.MapifyTuples, destinationProperty.Name, sourceProperties);

                    var sourceProperty = sourceProperties.Where(s => config.MapifyTuples.Any(x => x.SourceMemberName == s.Name)).FirstOrDefault();

                    var sourcePropertyValue = sourceProperty.GetValue(sourceObj);

                    if (type.Namespace.StartsWith("System"))
                    {
                        MapCollection(sourcePropertyValue, destination, destinationProperty.Name, config);
                    }
                    else
                    {
                        MapCollection(sourcePropertyValue, destination, type, sourcePropertyValue?.GetType(), config);
                    }

                    propertyValue = destination;
                }

                else if (GetProperyInfo<TSource>(mappingAttributes, sourceProperties, destinationProperty) is PropertyInfo propertyInfo)
                {
                    propertyValue = propertyInfo.GetValue(sourceObj);
                }
                else if (GetSourceExpression(mapifyConfiguration?.MapifyTuples, destinationProperty.Name) is Delegate propertyExpression)
                {
                    propertyValue = propertyExpression?.DynamicInvoke(sourceObj);
                }
                else
                {
                    continue;
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
            if (attributes == null)
            {
                return sourceProperties.Where(s => s.Name == destinationProperty.Name && s.PropertyType == destinationProperty.PropertyType).FirstOrDefault();
            }
           
            var currentAttribute = attributes.Where(s => s.SourceType == destinationProperty.PropertyType).FirstOrDefault();
            if (currentAttribute != null)
            {
                sourceProperty = sourceProperties.Where(s => s.Name == currentAttribute.PropertyName).FirstOrDefault();
            }
            else
            {
                currentAttribute = attributes.FirstOrDefault();
                sourceProperty = sourceProperties.Where(s => s.Name == currentAttribute.PropertyName).FirstOrDefault();
            }

            return sourceProperty;
        }


        private static Delegate GetSourceExpression(IList<MapifyTuple> mapifyTuples, string propertyName)
        {
            if (mapifyTuples == null)
            {
                return default;
            }
                
            MapifyTuple mapifyTuple = mapifyTuples.Where(s => s.DestinationMemberNames.Contains(propertyName)).FirstOrDefault();
            if(mapifyTuple != null)
            {
                return mapifyTuple.SourceExpression;
            }

            return default;
        }

        private static MapifyConfiguration GetSourceConfiguration(IList<MapifyTuple> mapifyTuples,string sourcePropertyName, PropertyInfo[] properties)
        {

            if (mapifyTuples == null)
                return default;

            var newMapifyTuples = mapifyTuples.Where(s => s.DestinationMemberNames.Contains(sourcePropertyName) && properties.Any(p => s.SourceMemberName.Contains(p.Name))).ToList();
            if (newMapifyTuples.Count > 0)
            {
                return new MapifyConfiguration(newMapifyTuples);
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
