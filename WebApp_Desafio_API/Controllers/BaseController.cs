using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WebApp_Desafio_API.Controllers
{
    /// <summary>
    /// BaseController
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : Controller
    {
        protected IActionResult HandleException(Exception ex)
        {
            if (ex is ArgumentException)
            {
                return BadRequest(ex.Message);
            }
            else if (ex is ApplicationException)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex.Message);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
