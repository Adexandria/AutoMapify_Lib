using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automapify.Services
{
    public static class SettingConfiguration<TSource,TDestination>
    {
      public static MapifyConfiguration<TSource,TDestination> CreateConfig()
      {
         return new MapifyConfiguration<TSource, TDestination>();
      }
    }
}
