using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Serilog;
using Serilog.Context;

namespace CashManager.Daily.Api.Filter
{
    public class LogActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var method = context.HttpContext.Request.Method; 
            var path = context.HttpContext.Request.Path;
            var request = $"Request - {method}/ Path: {path}";

            using (LogContext.PushProperty("Method", method))
            using (LogContext.PushProperty("Path", path))
            {
                Log.Logger.Information(request);
                base.OnActionExecuting(context);
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var method = context.HttpContext.Request.Method;
            var path = context.HttpContext.Request.Path;
            var result = JsonConvert.SerializeObject(context.Result);
            var queryString = context.HttpContext.Request.QueryString.ToString();
            var response = $"Response - {method}/ Path: {path}";

            using (LogContext.PushProperty("Method", method))
            using (LogContext.PushProperty("Path", path))
            using (LogContext.PushProperty("Result", result))
            using (LogContext.PushProperty("QueryString", queryString))
            {
                Log.Logger.Information(response);
                base.OnActionExecuted(context);
            }
        }
    }
}