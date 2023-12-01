using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automapify.Client.Extensions
{
    public static class ConverterExtensions
    {
        /// <summary>
        /// Convert date of birth to the user's age
        /// </summary>
        /// <param name="dateOfBirth"></param>
        /// <returns></returns>
        public static int ToAge(this DateTime dateOfBirth)
        {
            var difference = DateTime.Today.Subtract(dateOfBirth);

            int age = (int)(difference.TotalDays/365.25);
           
            return age;
        }
    }
}
