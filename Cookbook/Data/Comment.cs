using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace Cookbook.Data
{
    public class Comment
    {
        public void AddComment(Sitecore.Data.Items.Item product, string userName, string email, string comment)
        {
            string host = "sitecorecookbook", database = "master";
            string url = String.Format("http://{0}/-/item/v1/?sc_itemid={1}&template={2}&name={3}&sc_database={4}&payload=content",
                                        host, product.ID.ToString(),
                                        "Cookbook/Comment",
                                        Guid.NewGuid().ToString(), 
                                        database);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            SetRequestHeaders(request);
            SendFieldValues(request, userName, email, comment);
        }

        private static void SendFieldValues(HttpWebRequest request, string userName, string email, string comment)
        {
            string fields = string.Format("Username={0}&Email={1}&comment={2}&CommentedOn={3}",
                                        userName, email, comment, DateTime.Now.ToString("yyyyMMddTHHmmss"));

            byte[] data = Encoding.ASCII.GetBytes(fields);
            request.ContentLength = data.Length;
            request.ContentType = "application/x-www-formurlencoded";
            request.Method = "POST";
            request.KeepAlive = false;

            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
        }

        private static void SetRequestHeaders(HttpWebRequest request)
        {
            request.Headers.Add("X-Scitemwebapi-Username", "sitecore\\comment_creator");
            request.Headers.Add("X-Scitemwebapi-Password", "b");
        }
    }
}