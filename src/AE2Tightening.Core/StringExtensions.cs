using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AE2Tightening
{
    public static class StringExtensions
    {
        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="indexs">The indexs.</param>
        /// <returns></returns>
        public static string GetString(this string value, params int[] indexs) => 
            string.Join("", indexs.Select(x => value[x]));
    }
}
