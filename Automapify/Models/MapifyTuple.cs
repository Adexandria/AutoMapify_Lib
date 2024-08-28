using System.Linq.Expressions;

namespace Automapify.Models
{
    public class MapifyTuple
    {
        public MapifyTuple(List<string> destinationMembers, string sourceName ,LambdaExpression sourceExpression)
        {
            DestinationMemberNames = destinationMembers;
            SourceMemberName = sourceName;
            SourceExpression = sourceExpression.Compile();
        }
        public MapifyTuple(string destinationMember, string sourceName, LambdaExpression sourceExpression)
        {
            DestinationMemberNames.Add(destinationMember);
            SourceMemberName = sourceName;
            SourceExpression = sourceExpression.Compile();
        }


        public List<string> DestinationMemberNames { get; set; } = new List<string>();

        public string SourceMemberName { get; set; }
        public Delegate SourceExpression { get; set; }
    }
}
