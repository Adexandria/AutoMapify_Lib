using System.Linq.Expressions;

namespace Automapify.Services.Extensions
{
    public static class MappingExtension
    {
        public static Expression<Func<TSource, SourceMember>> MapFrom<TSource, SourceMember>(this IEnumerable<TSource> source, Expression<Func<TSource, SourceMember>> sourcePredicate)
        {
            return sourcePredicate;
        }

        public static Expression<Func<TSource,SourceMember>> MapTo<TSource, SourceMember>(this IEnumerable<TSource> destination, Expression<Func<TSource, SourceMember>> sourcePredicate)
        {
            return sourcePredicate;
        }

    }
}
