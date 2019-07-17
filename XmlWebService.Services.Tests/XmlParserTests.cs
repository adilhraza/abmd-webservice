using System.Xml;
using NUnit.Framework;
using XmlWebService.Services.Impl;

namespace XmlWebService.Services.Tests
{
    [TestFixture]
    public class XmlParserTests
    {
        [Test]
        public void ParsePayload_NoPayloadGiven_ReturnEmptyString()
        {
            var parser = new XmlParser();

            var res = parser.ParsePayload("");
            
            Assert.IsEmpty(res);
        }

        [Test]
        public void ParsePayload_InvalidCommandGiven_ReturnMinus1()
        {
            var parser = new XmlParser();

            var payload = @"<InputDocument>
                <DeclarationList>
                <Declaration Command=""WRONG"" Version=""5.13"">
                <DeclarationHeader>
                <Jurisdiction>IE</Jurisdiction>
                <CWProcedure>IMPORT</CWProcedure>
                <DeclarationDestination>CUSTOMSWAREIE</DeclarationDestination>
                <DocumentRef>71Q0019681</DocumentRef>
                <SiteID>DUB</SiteID>
                <AccountCode>G0779837</AccountCode>
                </DeclarationHeader>
                </Declaration>
                </DeclarationList>
                </InputDocument>
                ";
            
            var res = parser.ParsePayload(payload);
            
            Assert.AreEqual(res, "-1");
        }

        [Test]
        public void ParsePayload_InvalidSiteID_ReturnMinus2()
        {
            var parser = new XmlParser();

            var payload = @"<InputDocument>
                <DeclarationList>
                <Declaration Command=""DEFAULT"" Version=""5.13"">
                <DeclarationHeader>
                <Jurisdiction>IE</Jurisdiction>
                <CWProcedure>IMPORT</CWProcedure>
                <DeclarationDestination>CUSTOMSWAREIE</DeclarationDestination>
                <DocumentRef>71Q0019681</DocumentRef>
                <SiteID>LD</SiteID>
                <AccountCode>G0779837</AccountCode>
                </DeclarationHeader>
                </Declaration>
                </DeclarationList>
                </InputDocument>
                ";
            
            var res = parser.ParsePayload(payload);
            
            Assert.AreEqual(res, "-2");
        }
        
        [Test]
        public void ParsePayload_InvalidXml_ThrowXmlException()
        {
            var parser = new XmlParser();

            var payload = @"<InputDocument>
                <DeclarationList>
                <Declaration Command=""DEFAULT"" Version=""5.13"">
                <DeclarationHeader>
                <Jurisdiction>IE</Jurisdiction>
                <CWProcedure>IMPORT</CWProcedure>
                <DeclarationDestination>CUSTOMSWAREIE</DeclarationDestination>
                <DocumentRef>71Q0019681</DocumentRef>
                <SiteID>LD</SiteID>
                <AccountCode>G0779837</AccountCode>
                </DeclarationHeader>

                </DeclarationList>
                </InputDocument>
                ";
            
            Assert.Throws<XmlException>(() => parser.ParsePayload(payload));
        }

        [Test]
        public void ParsePayload_AllGoesWell_Return0()
        {
            var parser = new XmlParser();

            var payload = @"<InputDocument>
                <DeclarationList>
                <Declaration Command=""DEFAULT"" Version=""5.13"">
                <DeclarationHeader>
                <Jurisdiction>IE</Jurisdiction>
                <CWProcedure>IMPORT</CWProcedure>
                <DeclarationDestination>CUSTOMSWAREIE</DeclarationDestination>
                <DocumentRef>71Q0019681</DocumentRef>
                <SiteID>DUB</SiteID>
                <AccountCode>G0779837</AccountCode>
                </DeclarationHeader>
                </Declaration>
                </DeclarationList>
                </InputDocument>
                ";
            
            var res = parser.ParsePayload(payload);
            
            Assert.AreEqual(res, "0");
        }

    }
}