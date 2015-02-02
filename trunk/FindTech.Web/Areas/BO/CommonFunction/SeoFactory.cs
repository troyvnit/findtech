using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace FindTech.Web.Areas.BO.CommonFunction
{
    public static class SeoFactory
    {
        public static string GenerateSeoTitle(this string title)
        {
            var normalizedString = title.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder(title.Length);
            foreach (var c in normalizedString.ToCharArray())
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    switch (c.ToString())
                    {
                        case "&":
                            stringBuilder.Append("va");
                            break;
                        case "$":
                            stringBuilder.Append("dola");
                            break;
                        case "%":
                            stringBuilder.Append("phan-tram");
                            break;
                        case " ":
                            stringBuilder.Append("-");
                            break;
                        case "/":
                            stringBuilder.Append("-");
                            break;
                        case "\\":
                            stringBuilder.Append("-");
                            break;
                        default:
                            stringBuilder.Append(c);
                            break;
                    }
                }
            }
            return stringBuilder.ToString();
        }
    }
}