using System.Linq.Expressions;

namespace Automapify.Services.Utilities
{
    public class ExpressionDto
    {
      public ExpressionDto(string name, Expression expression)
      {
            Name = name;
            Expression = expression;
      }
       public string Name { get; set; }
       public Expression Expression { get; set; }
    }
}
