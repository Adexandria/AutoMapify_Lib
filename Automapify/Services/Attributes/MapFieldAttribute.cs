namespace Automapify.Services.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public abstract class MapFieldAttribute:Attribute
    {
        public string FieldName { get; set; }
        public Type SourceType { get; set; }
    }
}
