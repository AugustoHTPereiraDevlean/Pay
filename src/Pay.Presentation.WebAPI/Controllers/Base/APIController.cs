using Microsoft.AspNetCore.Mvc;
using Pay.Core.Base;

namespace Pay.Presentation.WebAPI.Controllers.Base
{
    [ApiController]
    [Route("api/[controller]")]
    public class APIController : ControllerBase
    {
        protected IActionResult CreateResult(ServiceResponse serviceResponse)
        {
            if (!serviceResponse.IsSuccessfully)
                return BadRequest(serviceResponse);

            return Ok(serviceResponse);
        }
    }
}