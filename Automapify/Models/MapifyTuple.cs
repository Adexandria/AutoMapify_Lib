using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automapify.Models
{
    internal class MapifyTuple<TSource,TDestination>
    {
        public MapifyTuple(TSource source, TDestination destination)
        {
            Source = source.GetType();
            Destination = destination.GetType();
        }

        public Type Source { get; set; }
        public Type Destination { get; set; }
    }
}
