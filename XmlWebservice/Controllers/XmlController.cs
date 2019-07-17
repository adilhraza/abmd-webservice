using System.Threading.Tasks;
using System.Web.Http;
using XmlWebService.Services;

namespace XmlWebservice.Controllers
{
    public class XmlController : ApiController
    {
        private readonly IXmlParser _parser;

        public XmlController(IXmlParser parser)
        {
            _parser = parser;
        }
        
        [HttpPost]
        public async Task<string> Post()
        {
            var payload = await Request.Content.ReadAsStringAsync();

            var x = _parser.ParsePayload(payload);
            return x;
        }
    }
}
