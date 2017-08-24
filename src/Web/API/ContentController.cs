using Microsoft.AspNetCore.Mvc;
using Timelase.Core;
using Timelase.Web.Model;

namespace Timelase.Web.API
{
    [Route("api/[controller]")]
    public class ContentController : Controller
    {
        private readonly ITfsRepository _tfsRepository;

        public ContentController(ITfsRepository tfsRepository)
        {
            _tfsRepository = tfsRepository;
        }

        [HttpPost]
        public string Get([FromBody]GetContentRequest request)
        {
            var t = _tfsRepository.GetContent(request.Path, request.ChangeSetId).Result;
            return t;
        }
    }
}
