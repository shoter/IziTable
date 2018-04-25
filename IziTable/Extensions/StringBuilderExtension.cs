using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IziTable.Extensions
{
    internal static class StringBuilderExtension
    {
        public static StringBuilder Append(this StringBuilder stringBuilder, StringBuilder secondBuilder)
        {
            if (secondBuilder == null)
                return stringBuilder;

            stringBuilder.EnsureCapacity(stringBuilder.Length + secondBuilder.Length);
            for (int i = 0; i < secondBuilder.Length; ++i)
                stringBuilder.Append(secondBuilder[i]);

            stringBuilder.Append(secondBuilder);
            return stringBuilder;
        }
    }
}
