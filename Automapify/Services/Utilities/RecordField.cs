using Automapify.Services.Attributes;

namespace Automapify.Services.Utilities
{
    public static class RecordField
    {
        /// <summary>
        /// Gets the property name and attribute name
        /// </summary>
        /// <typeparam name="T">Attribute field</typeparam>
        /// <param name="t">Type of the object to read</param>
        /// <returns>A dictionary</returns>
        /// <exception cref="NullReferenceException">If there are no properties</exception>
        public static Dictionary<string,T[]> GetAttributes<T>(this Type t) where T : MapAttribute
        {
            var propertyInfo = t.GetProperties(); 
            if (propertyInfo.Length == 0)
                throw new NullReferenceException("properties not found");

            var properties = new Dictionary<string,T[]>();
            foreach (var info in propertyInfo)
            {
                // Gets ignore attribute
                var ignoreAttribute = info.GetCustomAttributes(typeof(IgnoreAttribute), true);
                if (ignoreAttribute.Length != 0)
                     continue;

                // Gets Map attribute
                var attributes = (T[])info.GetCustomAttributes(typeof(T),true);
                if(attributes.Length == 0)
                    continue;

                properties.Add(info.Name,attributes);
            }

            return properties;
        }
    }
}
