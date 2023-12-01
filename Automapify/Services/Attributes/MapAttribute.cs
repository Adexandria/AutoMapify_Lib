namespace Automapify.Services.Attributes
{
    /// <summary>
    /// Property attributes used to store property name and source type
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class MapAttribute:Attribute
    {
        /// <summary>
        /// Property name
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// Source type
        /// </summary>
        public Type SourceType { get; set; }
    }
}
