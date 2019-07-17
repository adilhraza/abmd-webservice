using System;
using System.Xml;

namespace XmlWebService.Services.Impl
{
    public class XmlParser : IXmlParser
    {
        private readonly string SITE_ID = "DUB";
        private readonly string INVALID_SITE_SPECIFIED = "-2";
        private readonly string DOCUMENT_STRUCTURED_CORRECTLY = "0";
        private readonly string INVALID_COMMAND_SPECIFIED = "-1";
        private readonly string COMMAND_VALUE = "DEFAULT";
        private readonly string ATTRIBUTE_NAME = "Command";

        public string ParsePayload(string payLoad)
        {
            if (string.IsNullOrWhiteSpace(payLoad))
            {
                return string.Empty;
            }

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(payLoad);

                var decPath = "InputDocument/DeclarationList/Declaration";

                var decNode = xmlDoc.SelectSingleNode(decPath);

                if (decNode.Attributes.GetNamedItem(ATTRIBUTE_NAME).Value != COMMAND_VALUE)
                {
                    return INVALID_COMMAND_SPECIFIED;
                }
                
                var sitePath = "InputDocument/DeclarationList/Declaration/DeclarationHeader/SiteID";
                
                var siteNode = xmlDoc.SelectSingleNode(sitePath);
                if (siteNode?.InnerText != SITE_ID)
                {
                    return INVALID_SITE_SPECIFIED;
                }
            }
            catch (XmlException e)
            {
                // log to logger utility
                Console.WriteLine(e);
                throw;
            }
            
            return DOCUMENT_STRUCTURED_CORRECTLY;
        }
    }
}