using Automapify.Models;
using Automapify.Services;
using Automapify.Services.Attributes;
using Automapify.Services.Utilities;
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
        public static void Map<TSource,TDestination>(this TDestination destinationObj, TSource sourceObj, MapifyConfiguration<TSource,TDestination> mapifyConfiguration = null)
        {
            var destinationType = destinationObj.GetType();

            var destinationProperties = destinationType.GetProperties();

            var sourceProperties = sourceObj.GetType().GetProperties();
            try
            {
                var mappingAttributes = destinationType.GetAttributes<MapPropertyAttribute>();

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
        public static TDestination Map<TSource, TDestination>(this TSource sourceObj, MapifyConfiguration<TSource, TDestination> mapifyConfiguration = null)
        {
            var destinationObj = Activator.CreateInstance<TDestination>();

            var destinationType = destinationObj.GetType();

            var destinationProperties = destinationType.GetProperties();

            var sourceProperties = sourceObj.GetType().GetProperties();

            try
            {
                var mappingAttributes = destinationType.GetAttributes<MapPropertyAttribute>();

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
                return destinationObj;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
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
    }
}
