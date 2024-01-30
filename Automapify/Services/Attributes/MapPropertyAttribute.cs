namespace Automapify.Services.Attributes
{
    /// <summary>
    /// Property attributes used to store property name and source type
    /// </summary>
    [AttributeUsage(AttributeTargets.Property,AllowMultiple = true)]
    public class MapPropertyAttribute : MapAttribute
    {
        /// <summary>
        /// A constructor
        /// </summary>
        /// <param name="type">Type of the source</param>
        /// <param name="propertyName">Property name</param>
        /// <exception cref="ArgumentNullException">If property name is null or empty</exception>
        public MapPropertyAttribute(Type type,string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName) || string.IsNullOrWhiteSpace(propertyName))
                throw new ArgumentNullException($"{propertyName}", "property name is invalid");

            PropertyName = propertyName;

            SourceType = type ?? throw new ArgumentNullException($"{type}", "Invalid type");
        }

        /// <summary>
        /// A constructor
        /// </summary>
        /// <param name="propertyName">Property name</param>
        /// <exception cref="ArgumentNullException">If property name is null or empty</exception>
        public MapPropertyAttribute(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName) || string.IsNullOrWhiteSpace(propertyName))
                throw new ArgumentNullException($"{propertyName}", "property name is invalid");

            PropertyName = propertyName;
        }
    }
}
