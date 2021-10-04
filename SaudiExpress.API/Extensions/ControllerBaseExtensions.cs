using Microsoft.AspNetCore.Mvc;
using SaudiExpress.Business.Models;
using System.Net;

namespace SaudiExpress.API.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static ObjectResult FailedResponseResult(this ControllerBase controller, ResponseModel response)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            switch (response.Status)
            {
                case (ResponseStatus.Unauthorized): statusCode = HttpStatusCode.Unauthorized; break;
                case (ResponseStatus.NotFound): statusCode = HttpStatusCode.NotFound; break;
                case (ResponseStatus.BadRequest):
                case (ResponseStatus.Failed): statusCode = HttpStatusCode.BadRequest; break;
            }
            return controller.StatusCode((int)statusCode, response.Message);
        }

        public static StatusCodeResult Created(this ControllerBase controller)
        {
            return controller.StatusCode((int)HttpStatusCode.Created);
        }
    }
}
