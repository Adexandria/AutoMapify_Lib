using System.Linq.Expressions;

namespace Automapify.Models
{
    public class MapifyTuple<TSource,TDestination>
    {
        public MapifyTuple()
        {

        }
        public MapifyTuple(Expression<Func<TSource,object>> _sourcePredicate, Expression<Func<TDestination, object>> _destinationpredicate)
        {
            SourcePredicate = _sourcePredicate;
            DestinationPredicate = (MemberExpression)_destinationpredicate.Body;
        }

        public MemberExpression DestinationPredicate { get; set; }

        public Expression<Func<TSource, object>> SourcePredicate { get; set; }

    }
}
