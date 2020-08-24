using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace Cadastro.Controllers.Base
{
    public class BaseController : Controller
    {
        protected IActionResult ReturnPackage(Func<dynamic> procedure, HttpStatusCode status = HttpStatusCode.OK, string message = null)
        {
            try
            {
                var result = procedure();

                if (message == null)
                    return StatusCode((int)status, result);

                return StatusCode((int)status, new { mensagem = message });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = ex.Message });
            }
        }
    }
}
