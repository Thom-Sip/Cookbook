using Sitecore.Pipelines.Upload;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;

namespace Cookbook.Pipelines
{
    public class UploadRestrictions : Sitecore.Pipelines.Upload.UploadProcessor
    {
        private List<string> restrictedContentType = new List<string>();
        private List<string> restrictedExtensions = new List<string>();

        public void Process(UploadArgs args)
        {
            foreach (string fileKey in args.Files)
            {
                string fileName = args.Files[fileKey].FileName;
                string contentType = args.Files[fileKey].ContentType;
                string extension = Path.GetExtension(fileName);
                if (IsRestrictedExtension(extension) || IsRestrictedContentType(contentType))
                {
                    args.ErrorText = "Upload of this file restricted.";
                    args.AbortPipeline();
                    break;
                }
            }
        }

        protected virtual void AddRestrictedContentType(XmlNode configNode)
        {
            restrictedContentType.Add(configNode.InnerText);
        }

        protected virtual void AddRestrictedExtension(XmlNode configNode)
        {
            string[] extensions = configNode.InnerText.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string extension in extensions)
                restrictedExtensions.Add("." + extension);
        }

        private bool IsRestrictedExtension(string extension)
        {
            return restrictedExtensions.Exists(ext => string.Equals(ext, extension, StringComparison.CurrentCultureIgnoreCase));
        }

        private bool IsRestrictedContentType(string contentType)
        {
            return restrictedContentType.Exists(type => string.Equals(type, contentType, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}