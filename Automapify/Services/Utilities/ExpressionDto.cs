using System.Linq.Expressions;

namespace Automapify.Services.Utilities
{
    public class ExpressionDto<TSource>
    {
      public ExpressionDto(string name, Func<TSource, object> expression)
      {
            Expression = expression;
      }
       public string Name { get; set; }
       public Func<TSource, object> Expression { get; set; }
    }
}
