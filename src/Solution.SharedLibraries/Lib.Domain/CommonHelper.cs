using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace Lib.Domain
{
    public static class CommonHelper
    {
        public static string ReFormatAppHtmlContent(string content, int imgSize = 0)
        {
            if (!string.IsNullOrWhiteSpace(content))
            {
                content = RemoveTagEmptyDataFromHtml(content, "p");
                return string.Format("<!DOCTYPE html><meta name=\"viewport\" content=\"initial-scale=1.0\" /><div style=\"color:#444;font-family:arial,helvetica,sans-serif;\">{0}</div>", ResizeImageFromHtml(Regex.Replace(content, "times new roman,times,serif", "arial,helvetica,sans-serif", RegexOptions.IgnoreCase), imgSize));
            }
            else
                return string.Empty;
        }
        public static string ReFormatAppHtmlContentWithoutImage(string content)
        {
            if (!string.IsNullOrWhiteSpace(content))
            {
                var formatedHtml = RemoveImageFromHtml(Regex.Replace(content, "times new roman,times,serif", "arial,helvetica,sans-serif", RegexOptions.IgnoreCase));
                formatedHtml = RemoveParagraphFromHtml(formatedHtml);
                return string.Format("<meta name=\"viewport\" content=\"initial-scale=1.0\" /><div style=\"color:#444;font-family:arial,helvetica,sans-serif;\">{0}</div>", formatedHtml);
            }
            else
                return string.Empty;
        }

        public static string RemoveTagEmptyDataFromHtml(string html, string tagName)
        {
            if (!string.IsNullOrEmpty(html))
            {
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(html);
                var lstElement = htmlDoc.DocumentNode.SelectNodes(string.Format("//{0}", tagName));

                if (lstElement != null && lstElement.Count > 0)
                {
                    foreach (var item in lstElement)
                    {
                        if (string.IsNullOrWhiteSpace(item.InnerText) && string.IsNullOrWhiteSpace(item.InnerHtml))
                            item.Remove();
                    }
                }

                return htmlDoc.DocumentNode.InnerHtml;
            }
            return html;
        }
        public static string RemoveImageFromHtml(string html)
        {
            if (!string.IsNullOrEmpty(html))
            {
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(html);
                var lstElement = htmlDoc.DocumentNode.SelectNodes("//img");

                if (lstElement != null && lstElement.Count > 0)
                {
                    foreach (var item in lstElement)
                    {
                        item.Remove();
                    }
                }

                return htmlDoc.DocumentNode.InnerHtml;
            }
            return html;
        }
        public static string RemoveParagraphFromHtml(string html)
        {
            if (!string.IsNullOrEmpty(html))
            {
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(html);
                var lstElement = htmlDoc.DocumentNode.SelectNodes("//p");

                if (lstElement != null && lstElement.Count > 0)
                {
                    foreach (var item in lstElement)
                    {
                        if (string.IsNullOrWhiteSpace(item.InnerHtml))
                            item.Remove();
                    }
                }

                return htmlDoc.DocumentNode.InnerHtml;
            }
            return html;
        }
        public static string ResizeImageFromHtml(string html, int maxWidth)
        {
            if (!string.IsNullOrEmpty(html))
            {
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(html);
                var lstElement = htmlDoc.DocumentNode.SelectNodes("//img");

                if (lstElement != null && lstElement.Count > 0)
                {
                    foreach (var item in lstElement)
                    {
                        var style = item.Attributes["style"];
                        //if (style == null)
                        //{
                        if (maxWidth == 0)
                        {
                            item.SetAttributeValue("style", "width:100%;");
                        }
                        else
                        {
                            item.SetAttributeValue("style", string.Format("width:{0}px;", maxWidth));
                        }
                        var divNode = item.ParentNode;

                        while (divNode != null && divNode.OriginalName != "div")
                        {
                            divNode = divNode.ParentNode;
                        }
                        if (divNode != null)
                            divNode.SetAttributeValue("style", "text-align:center");
                    }
                }

                return htmlDoc.DocumentNode.InnerHtml;
            }
            return html;
        }
        public static string RemoveTagFromHtml(string html, string tagName)
        {
            if (!string.IsNullOrEmpty(html))
            {
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(html);
                var lstElement = htmlDoc.DocumentNode.SelectNodes(string.Format("//{0}", tagName));

                if (lstElement != null && lstElement.Count > 0)
                {
                    foreach (var item in lstElement)
                    {
                        item.Remove();
                    }
                }

                return htmlDoc.DocumentNode.InnerHtml;
            }
            return html;
        }
        public static string GetFullHtmlFieldNameWithoutPrefix(string fullFieldName)
        {
            string result = string.Empty;

            int flag = fullFieldName.LastIndexOf(".");

            if (flag > 0)
            {
                int startIndex = flag + 1;
                result = fullFieldName.Substring(startIndex, fullFieldName.Length - startIndex);
            }
            else
            {
                result = fullFieldName;
            }

            return result;
        }

        public static string FirstToUpper(string input)
        {
            return input.First().ToString().ToUpper() + input.Substring(1);
        }
    }
}
