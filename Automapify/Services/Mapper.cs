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
        public static void Map<TSource,TDestination>(this TDestination destinationObj, TSource sourceObj)
        {
            var destinationType = destinationObj.GetType();

            var destinationProperties = destinationType.GetProperties();

            var sourceProperties = sourceObj.GetType().GetProperties();

            var mappingAttributes = destinationType.GetAttributes<MapPropertyAttribute>();

            foreach (var destinationProperty in destinationProperties)
            {
                PropertyInfo sourceProperty;
                mappingAttributes.TryGetValue(destinationProperty.Name, out string property);
                if (property == null)
                {
                    sourceProperty = sourceProperties.Where(s => s.Name == destinationProperty.Name).FirstOrDefault();
                }
                else
                {
                    sourceProperty = sourceProperties.Where(s => s.Name == property).FirstOrDefault();
                }
                var propertyValue = sourceProperty.GetValue(sourceObj);
                destinationProperty.SetValue(destinationObj, propertyValue);
            }
        }


        /// <summary>
        /// Map values from a source object to a new destination object.
        /// </summary>
        /// <typeparam name="TSource">Source object</typeparam>
        /// <typeparam name="TDestination">Destination object</typeparam>
        /// <param name="sourceObj">Object to gather information or data from</param>
        /// <returns>Destination object</returns>
        public static TDestination Map<TSource,TDestination>(this TSource sourceObj)
        {
            var destinationObj = Activator.CreateInstance<TDestination>();

            var destinationType = destinationObj.GetType();

            var destinationProperties = destinationType.GetProperties();

            var sourceProperties = sourceObj.GetType().GetProperties();

            var mappingAttributes = destinationType.GetAttributes<MapPropertyAttribute>();

            foreach (var destinationProperty in destinationProperties)
            {
                PropertyInfo sourceProperty;
                mappingAttributes.TryGetValue(destinationProperty.Name,out string property);
                if (property == null)
                {
                    sourceProperty = sourceProperties.Where(s => s.Name == destinationProperty.Name).FirstOrDefault();
                }
                else
                {
                    sourceProperty = sourceProperties.Where(s => s.Name == property).FirstOrDefault();
                }
                var propertyValue = sourceProperty.GetValue(sourceObj);
                destinationProperty.SetValue(destinationObj, propertyValue);
            }
            return destinationObj;
        }

    }
}
