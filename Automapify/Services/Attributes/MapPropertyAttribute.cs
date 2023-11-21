using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automapify.Services.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field,AllowMultiple = true)]
    public class MapPropertyAttribute : MapFieldAttribute
    {
        public MapPropertyAttribute(Type type,string fieldName)
        {
            if (string.IsNullOrEmpty(fieldName) || string.IsNullOrWhiteSpace(fieldName))
                throw new ArgumentNullException("field name", "Field name is invalid");

            FieldName = fieldName;

            DestinationType = type ?? throw new ArgumentNullException("field name", "Field name is invalid");
        }

        public MapPropertyAttribute(string fieldName)
        {
            if (string.IsNullOrEmpty(fieldName) || string.IsNullOrWhiteSpace(fieldName))
                throw new ArgumentNullException("field name", "Field name is invalid");

            FieldName = fieldName;
        }
    }
}
