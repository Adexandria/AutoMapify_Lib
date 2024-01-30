using Automapify.Models;

namespace Automapify.Services
{
    /// <summary>
    /// Stores the source and destination expressions
    /// </summary>
    /// <typeparam name="TSource">Source object</typeparam>
    /// <typeparam name="TDestination">Destination object</typeparam>
    public class MapifyConfiguration
    {
        /// <summary>
        /// A constructor
        /// </summary>
        public MapifyConfiguration(IList<MapifyTuple> mapifyTuples)
        {
            MapifyTuples = mapifyTuples;
        }

        /// <summary>
        /// Stores the all source and destination expressions
        /// </summary>
        public IList<MapifyTuple> MapifyTuples { get; set; }
    }
}
