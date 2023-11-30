using Automapify.Models;
using System.Linq.Expressions;

namespace Automapify.Services
{
    public class MapifyConfiguration<TSource,TDestination> 
    {
        public MapifyConfiguration()
        {
            MapifyTuples = new List<MapifyTuple<TSource,TDestination>>();
        }

        public MapifyConfiguration<TSource,TDestination> Map(Expression<Func<TDestination,object>> destinationPredicate, Expression<Func<TSource, object>> sourcePredicate)
        {
            MapifyTuples.Add(new MapifyTuple<TSource, TDestination>(sourcePredicate, destinationPredicate));
            return this;
        }
        public IList<MapifyTuple<TSource,TDestination>> MapifyTuples { get; set; }

        public Type Source { get; set; } = typeof(TSource);
        public Type Destination { get; set; } = typeof (TDestination);
    }
}
