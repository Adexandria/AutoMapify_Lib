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

        public MapifyTuple(List<string> destinationMembers)
        {
            DestinationMemberNames = destinationMembers;

            IsIgnored = true;
        }

        public void Ignore()
        {
            IsIgnored = true;
        }

        public List<string> DestinationMemberNames { get; set; } = new List<string>();

        public string SourceMemberName { get; set; } = string.Empty;
        public Delegate SourceExpression { get; set; }
        public bool IsIgnored { get; set; } = false;
    }
}
