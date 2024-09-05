using Automapify.Models;
using System.Linq.Expressions;

namespace Automapify.Services
{
    public class MapifyConfigurationBuilder<TSource, TDestination>
    {
        public MapifyConfigurationBuilder()
        {
            mapifyTuples = new List<MapifyTuple>();
        }

        /// <summary>
        /// Stores how the source expressions is going to be mapped to the destination object
        /// </summary>
        /// <param name="destinationPredicate">Destination expression</param>
        /// <param name="sourcePredicate">Source expression</param>
        /// <returns></returns>
        public MapifyConfigurationBuilder<TSource, TDestination> Map<DestinationMember, SourceMember>
            (Expression<Func<TDestination, DestinationMember>> destinationPredicate, Expression<Func<TSource, SourceMember>> sourcePredicate)
        {
            CreateTuple(destinationPredicate, sourcePredicate);
            return this;
        }



        /// <summary>
        /// Ignores property
        /// </summary>
        /// <typeparam name="DestinationMember"> destination member type</typeparam>
        /// <param name="destinationPredicate">Destination expression</param>
        /// <returns></returns>
        public MapifyConfigurationBuilder<TSource,TDestination> Ignore<DestinationMember>
            (Expression<Func<TDestination, DestinationMember>> destinationPredicate)
        {
            var members = GetMemberExpressionNames(destinationPredicate);

            mapifyTuples.Add(new MapifyTuple(members));

            return this;
        }


        /// <summary>
        /// Get expression names from member
        /// </summary>
        /// <typeparam name="T">Type of object to convert to or from</typeparam>
        /// <typeparam name="TObject">Data type</typeparam>
        /// <param name="desExp">Expression</param>
        /// <returns>Names of the member</returns>
        private void CreateTuple<DestinationMember, SourceMember>
            (Expression<Func<TDestination, DestinationMember>> destinationPredicate, Expression<Func<TSource, SourceMember>> sourcePredicate, bool isIgnored = false)
        {
            MemberExpression memberExpression = destinationPredicate.Body as MemberExpression;

            var currentMemberExpression = memberExpression ?? (destinationPredicate.Body is UnaryExpression unary ? unary.Operand as MemberExpression : null);

            LambdaExpression newSourceExpression = null;

            var sourceName = GetSourceMemberExpressionName(sourcePredicate);

            MapifyTuple mapifyTuple = null;

            if (currentMemberExpression != null)
            {
                if (sourcePredicate.Body is MethodCallExpression sourceExpression)
                {
                    newSourceExpression = sourcePredicate.Compile().Invoke(Activator.CreateInstance<TSource>()) as LambdaExpression;
                }
                mapifyTuple = new MapifyTuple(currentMemberExpression?.Member?.Name, sourceName, newSourceExpression ?? sourcePredicate);
            }
            else
            {
                var destinationName = GetMemberExpressionNames(destinationPredicate);

                newSourceExpression = sourcePredicate.Compile().Invoke(Activator.CreateInstance<TSource>()) as LambdaExpression;

                mapifyTuple = new MapifyTuple(destinationName, sourceName, newSourceExpression);
            }

            if (isIgnored)
            {
                mapifyTuple.Ignore();
            }

            mapifyTuples.Add(mapifyTuple);
        }

        /// <summary>
        /// Get expression names from member
        /// </summary>
        /// <typeparam name="T">Type of object to convert to or from</typeparam>
        /// <typeparam name="TObject">Data type</typeparam>
        /// <param name="exp">Expression</param>
        /// <returns>Names of the member</returns>
        private string GetSourceMemberExpressionName<T, TObject>(Expression<Func<T, TObject>> exp)
        {
            if (exp.Body is not MethodCallExpression methodExpression)
            {
                if (exp.Body is MemberExpression e)
                {
                    return e.Member?.Name;
                }
            }
            else
            {
                var expression = methodExpression.Arguments.FirstOrDefault();
                var memberExpression = expression as MemberExpression;
                var currentMemberExpression = memberExpression ?? (expression is UnaryExpression un ? (un.Operand is LambdaExpression body ? body.Body as MemberExpression : null) : null);

                if (currentMemberExpression != null)
                {
                    return currentMemberExpression?.Member?.Name;
                }
            }

            return default;
        }


        /// <summary>
        /// Get expression names from member
        /// </summary>
        /// <typeparam name="T">Type of object to convert to or from</typeparam>
        /// <typeparam name="TObject">Data type</typeparam>
        /// <param name="exp">Expression</param>
        /// <returns>Names of the member</returns>
        private List<string> GetMemberExpressionNames<T, TObject>(Expression<Func<T, TObject>> exp)
        {
            var members = new List<string>();

            if (exp.Body is not MethodCallExpression methodExpression)
            {
                if (exp.Body is MemberExpression e)
                {
                    members.Add(e.Member?.Name);
                }
            }
            else
            {
                var expressions = methodExpression.Arguments.Reverse().ToList();
                foreach (var expression in expressions)
                {
                    var memberExpression = expression as MemberExpression;
                    var currentMemberExpression = memberExpression ?? (expression is UnaryExpression un ? (un.Operand is LambdaExpression body ? body.Body as MemberExpression : null) : null);

                    if (currentMemberExpression != null)
                    {
                        members.Add(currentMemberExpression?.Member?.Name);
                    }
                }
            }

            return members;
        }

        public MapifyConfiguration CreateConfig()
        {
            return new MapifyConfiguration(mapifyTuples);
        }

        private IList<MapifyTuple> mapifyTuples;
    }
}
