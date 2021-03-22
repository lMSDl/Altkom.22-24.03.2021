using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class HtmlBuilder
    {
        private readonly StringBuilder _html = new StringBuilder();

        public void Append(string @string)
        {
            _html.Append(@string);
        }

        public void AppendBold(string @string)
        {
            Append($"<b>{@string}</b>");
        }

        public override string ToString()
        {
            return _html.ToString();
        }

        public void AppendAnchor(string href, string label)
        {
            Uri uri;
            if (!(CheckUrl(href, out uri)))
                throw new InvalidOperationException();

            Append($"<a href=\"{uri.AbsoluteUri}\">{label}</a>");
        }

        private static bool CheckUrl(string href, out Uri uri)
        {
            return Uri.TryCreate(href, UriKind.Absolute, out uri) && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
        }
    }
}
