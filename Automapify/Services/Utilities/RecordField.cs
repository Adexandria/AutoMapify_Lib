using Automapify.Services.Attributes;

namespace Automapify.Services.Utilities
{
    public static class RecordField
    {
        /// <summary>
        ///  Get the property name and attribute name
        /// </summary>
        /// <typeparam name="T">Attribute field</typeparam>
        /// <param name="t">Type of the object to read</param>
        /// <returns>A dictionary</returns>
        /// <exception cref="NullReferenceException"></exception>
        public static Dictionary<string,string> GetAttributes<T>(this Type t) where T : MapFieldAttribute
        {
            var propertyInfo = t.GetProperties(); 
            if (propertyInfo.Length == 0)
                throw new NullReferenceException("properties not found");

            var properties = new Dictionary<string, string>();
            foreach (var info in propertyInfo)
            {
                var attributes = info.GetCustomAttributes(true);
                if(attributes.Length == 0)
                    continue;
                var attribute = (T)attributes[0];
                properties.Add(info.Name, attribute.FieldName);
            }
            return properties;
        }
    }
}
