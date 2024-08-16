﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Automapify.Services.Extensions
{
    internal static class StringExtensions
    {
        public static bool NotContains(this string source, string parameter)
        {
            if(string.IsNullOrEmpty(source))
                return true;

            return !source.Contains(parameter);
        }

        public static bool NotStartsWith(this string source, string parameter)
        {
            if (string.IsNullOrEmpty(source))
                return true;

            return !source.StartsWith(parameter);
        }
    }
}
