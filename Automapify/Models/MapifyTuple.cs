using System.Linq.Expressions;

namespace Automapify.Models
{
    public class MapifyTuple
    {
        public MapifyTuple(string destinationMemberName, LambdaExpression expression)
        {
            DestinationMemberName = destinationMemberName;
            SourceExpression = expression.Compile();
        }
        public string DestinationMemberName { get; set; }
        public Delegate SourceExpression { get; set; }
    }
}
