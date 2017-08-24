using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Timelase.Core;
using Timelase.Core.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Timelase.Web.API
{
    [Route("api/[controller]")]
    public class ChangesetsController : Controller
    {
        private readonly ITfsRepository _tfsRepository;

        public ChangesetsController(ITfsRepository tfsRepository)
        {
            _tfsRepository = tfsRepository;
        }

        [HttpGet]
        public IEnumerable<TfsChangeset> Get(string path)
        {
            var t = _tfsRepository.GetItemChangesets(path).Result;
            return t;
        }
    }
}
