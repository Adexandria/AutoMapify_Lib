using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automapify.Services.Utilities
{
    public class Property
    {
        public Property(string name, Type destinationType)
        {
            Name = name;
            DestinationType = destinationType;
        }
        public string Name { get; set; }
        public Type DestinationType { get; set; }
    }
}
