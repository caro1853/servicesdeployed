using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Scheduling.API.Filters
{
	public class FilterException : ExceptionFilterAttribute
    {
        private readonly ILogger<FilterException> logger;

        public FilterException(ILogger<FilterException> logger)
		{
            this.logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {

            var message = new
            {
                message = context.Exception.Message
            };
            context.Result = new BadRequestObjectResult(message);
            this.logger.LogError(context.Exception, context.Exception.Message);
            base.OnException(context);
        }
    }
}

