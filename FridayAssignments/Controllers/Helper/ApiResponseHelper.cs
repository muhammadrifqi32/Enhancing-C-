using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FridayAssignments.Controllers.Helper
{
    public class ApiResponseHelper
    {
        public static IActionResult ApiResponse(HttpStatusCode status, string message, object? data = null)
        {
            return new ObjectResult(new
            {
                status = (int)status,
                message = message,
                data
            })
            {
                StatusCode = (int)status
            };
        }
    }
}
