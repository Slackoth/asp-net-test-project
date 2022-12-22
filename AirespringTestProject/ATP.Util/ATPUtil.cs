using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP.Util
{
    public static class ATPUtil
    {
        public static string RemoveDashAndParenthesis(string Input)
        {
            StringBuilder builder = new(Input);

            builder.Replace("(", ""); builder.Replace(")", "");
            builder.Replace("-", ""); builder.Replace(" ", "");

            return builder.ToString().ToLower();
        }
    }
}
