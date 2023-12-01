using System.Linq.Expressions;

namespace Automapify.Models
{
    /// <summary>
    /// Stores the lambda expression on how the source object will be mapped to a destination object
    /// </summary>
    /// <typeparam name="TSource">Source object</typeparam>
    /// <typeparam name="TDestination">Destination object</typeparam>
    public class MapifyTuple<TSource,TDestination>
    {
        /// <summary>
        /// A parameterless constructor
        /// </summary>
        public MapifyTuple()
        {

        }

        /// <summary>
        /// A constructor
        /// </summary>
        /// <param name="_sourcePredicate">Expression of the source object</param>
        /// <param name="_destinationpredicate">Expression of the destination object</param>
        public MapifyTuple(Expression<Func<TSource,object>> _sourcePredicate, Expression<Func<TDestination, object>> _destinationpredicate)
        {
            SourcePredicate = _sourcePredicate;
            DestinationPredicate = GetMemberExpression(_destinationpredicate);
        }

        /// <summary>
        /// Member expression of the destination object
        /// </summary>
        public MemberExpression DestinationPredicate { get; set; }

        /// <summary>
        /// Expression of the source object
        /// </summary>
        public Expression<Func<TSource, object>> SourcePredicate { get; set; }


        private MemberExpression GetMemberExpression<T>(Expression<Func<T, object>> exp)
        {
            var member = exp.Body as MemberExpression;
            var unary = exp.Body as UnaryExpression;
            return member ?? (unary != null ? unary.Operand as MemberExpression : null);
        }
    }
}
