using Automapify.Models;
using System.Linq.Expressions;

namespace Automapify.Services
{
    /// <summary>
    /// Stores the source and destination expressions
    /// </summary>
    /// <typeparam name="TSource">Source object</typeparam>
    /// <typeparam name="TDestination">Destination object</typeparam>
    public class MapifyConfiguration<TSource,TDestination> 
    {
        /// <summary>
        /// A parameterless constructor
        /// </summary>
        public MapifyConfiguration()
        {
            MapifyTuples = new List<MapifyTuple<TSource,TDestination>>();
        }

        /// <summary>
        /// Stores how the source expressions is going to be mapped to the destination object
        /// </summary>
        /// <param name="destinationPredicate">Destination expression</param>
        /// <param name="sourcePredicate">Source expression</param>
        /// <returns></returns>
        public MapifyConfiguration<TSource,TDestination> Map(Expression<Func<TDestination,object>> destinationPredicate, Expression<Func<TSource, object>> sourcePredicate)
        {
            MapifyTuples.Add(new MapifyTuple<TSource, TDestination>(sourcePredicate, destinationPredicate));
            return this;
        }

        /// <summary>
        /// Stores the all source and destination expressions
        /// </summary>
        public IList<MapifyTuple<TSource,TDestination>> MapifyTuples { get; set; }
    }
}
