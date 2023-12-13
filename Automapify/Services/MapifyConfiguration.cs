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
            MapifyTuples = new List<MapifyTuple>();
        }

        /// <summary>
        /// Stores how the source expressions is going to be mapped to the destination object
        /// </summary>
        /// <param name="destinationPredicate">Destination expression</param>
        /// <param name="sourcePredicate">Source expression</param>
        /// <returns></returns>
        public MapifyConfiguration<TSource,TDestination> Map<DestinationMember,SourceMember>(Expression<Func<TDestination, DestinationMember>> destinationPredicate, Expression<Func<TSource, SourceMember>> sourcePredicate)
        {
            MapifyTuples.Add(new MapifyTuple(GetMemberExpressionName(destinationPredicate), sourcePredicate));
            return this;
        }


        /// <summary>
        /// Get expression name from member
        /// </summary>
        /// <typeparam name="T">Type of object to convert to or from</typeparam>
        /// <typeparam name="TObject">Data type</typeparam>
        /// <param name="exp">Expression</param>
        /// <returns>Name of the member</returns>
        private string GetMemberExpressionName<T, TObject>(Expression<Func<T, TObject>> exp)
        {
            MemberExpression member = exp.Body as MemberExpression;
            var currentMember = member ?? (exp.Body is UnaryExpression unary ? unary.Operand as MemberExpression : null);
            if(currentMember != null)
                return currentMember.Member?.Name;

            return default;
        }

        /// <summary>
        /// Stores the all source and destination expressions
        /// </summary>
        public IList<MapifyTuple> MapifyTuples { get; set; }
    }
}
