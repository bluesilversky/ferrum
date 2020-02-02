using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ferrum.FakeBank.Models;
using Ferrum.Core.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ferrum.FakeBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthoriseController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(new { Get = "Authorise Controller", Success = true });
        }
        
        [HttpPost]
        public AuthoriseResponse Post([FromBody] AuthoriseRequest request)
        {
            if (request.SecurityCode == 200 && request.CardStruct.IsValid)
                return new AuthoriseResponse(AuthStatus.Authorised);

            if (request.SecurityCode == 404)
                return new AuthoriseResponse(AuthStatus.Unknown);

            if (request.SecurityCode == 500)
                throw new Exception();

            if (request.SecurityCode == 501)
                return new AuthoriseResponse(AuthStatus.Error);

            return new AuthoriseResponse(AuthStatus.Declined);
        }        
    }
}
