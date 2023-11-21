using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automapify.Services.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public abstract class MapFieldAttribute:Attribute
    {
        public string FieldName { get; set; }
        public Type DestinationType { get; set; }
    }
}
