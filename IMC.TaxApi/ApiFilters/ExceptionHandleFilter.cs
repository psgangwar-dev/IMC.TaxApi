using ICM.TaxApi.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace IMC.TaxApi.Host.ApiFilters
{
    public class ExceptionHandleFilter : IExceptionFilter
    {
        /// <summary>
        /// Error Exception Filter :  Catch all errors and returns Appropriate Error response
        /// </summary>
        private readonly ILogger<ExceptionHandleFilter> _logger;
        public ExceptionHandleFilter(ILogger<ExceptionHandleFilter> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Catch Exception and returns response
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {

                case InvalidOperationException ex:
                    context.Result = CreateExceptionInfoResponse(context, new List<string> { ex.Message }, "Forbidden", HttpStatusCode.BadRequest);
                    break;

                case Exception ex:
                    context.Result = CreateExceptionInfoResponse(context, new List<string> { "UnexpectedError" }, "InteralServerError", HttpStatusCode.InternalServerError);
                    break;

            }
        }


        /// <summary>
        /// Creates Exception Info Response
        /// </summary>
        /// <param name="context"></param>
        /// <param name="errors"></param>
        /// <param name="errorConstant"></param>
        /// <param name="httpStatusCode"></param>
        /// <returns></returns>
        private IActionResult CreateExceptionInfoResponse(ActionContext context, IEnumerable<string> errors, string errorConstant, HttpStatusCode httpStatusCode)
        {
            if (errors == null || errors.Any(x => String.IsNullOrWhiteSpace(x)))
            {
                errors = new List<string> { errorConstant };
            }

            var errorInfo = new ErrorInfo() { ErrorMessages = errors };

            // TODO: Refactor this code
            //foreach (var error in errors)
            //{
            //    var errorMessage = new ErrorMessage()
            //    {
            //        Code = $"TaxApi.{error}",
            //        Message = $"TaxApi.{error}"
            //    };

            //    errorInfo.Messages.Add(errorMessage);
            //}

            var result = new ObjectResult(errorInfo) { StatusCode = (int)httpStatusCode };
            _logger.LogError($"Error = {result.StatusCode}");

            return result;
        }
    }

}
