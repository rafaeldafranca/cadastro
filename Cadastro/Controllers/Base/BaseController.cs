using Cadastro.Domain.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace Cadastro.Controllers.Base
{
    public class BaseController : Controller
    {
        protected IActionResult ReturnPackage(Func<dynamic> procedure, HttpStatusCode status = HttpStatusCode.OK,
            string message = null)
        {
            string chamada = $"{HttpContext.Request.Method} -  {HttpContext.Request.Path.Value} ";
            string RemoteConnection = HttpContext.Connection.RemoteIpAddress.ToString();

            Serilog.Log.Information($"*** Iniciando {chamada} para {RemoteConnection} ***");

            try
            {
                var result = procedure();

                if (result is ObjectResult)
                    return result;

                if (message == null)
                    return StatusCode((int)status, result);

                return StatusCode((int)status, new { mensagem = message });

            }
            catch (DomainException dEx) //erros gerados pelo domain.
            {
                var msgError = string.Join(", ", dEx.Errors.ToArray());
                Serilog.Log.Fatal(msgError, $"Server error {chamada} para {RemoteConnection}");
                return StatusCode(500, new { mensagem = msgError });
            }
            catch (Exception ex)
            {
                Serilog.Log.Fatal(ex, $"Server error {chamada} para {RemoteConnection}");
                return StatusCode(500, new { mensagem = ex.Message });
            }

        }
    }
}
