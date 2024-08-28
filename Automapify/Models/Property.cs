using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automapify.Models
{
    public class Property
    {
        public Property(string propertyName)
        {
            PropertyName = propertyName;    
        }

        public string SourceName { get; set; }
        public string PropertyName { get; set; }
    }
}
