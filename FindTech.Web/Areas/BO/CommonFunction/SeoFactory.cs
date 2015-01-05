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
            var dictionary = new Dictionary<string, string> { { "&", "va" }, { "$", "dola" }, { "%", "phan-tram" }, {"*", "sao"}, { "#", "thang" } };
            var seoTitle = title.RemoveDiacritics().ToLower();
            seoTitle = dictionary.Aggregate(seoTitle, (current, d) => new StringBuilder(current).Replace(d.Key, d.Value).ToString());
            seoTitle = Regex.Replace(seoTitle, @"[^a-z0-9\s-]", "");
            seoTitle = Regex.Replace(seoTitle, @"\s+", " ").Trim();
            //str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            seoTitle = Regex.Replace(seoTitle, @"\s", "-");
            return seoTitle;
        }

        public static string RemoveDiacritics(this string title)
        {
            var normalizedString = title.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();
            foreach (var c in normalizedString.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark))
            {
                stringBuilder.Append(c);
            }
            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}