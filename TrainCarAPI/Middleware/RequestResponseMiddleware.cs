using Newtonsoft.Json;
using TrainCarAPI.Model.DTO;

namespace TrainCarAPI.Middleware
{
    public class RequestResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Request and Response modifying Middleware
        /// Append unique ID for Request and change Response body using the excepted format.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            setRequestId(context);

            var originBody = context.Response.Body;
            try
            {
                await setReponseBodyForSuccess(context, originBody);
            }
            catch (Exception ex)
            {
                await setResponseBodyForException(context, ex, originBody);
            }
        }
        /// <summary>
        /// Setting a unique ID for Request
        /// </summary>
        /// <param name="context"></param>
        private void setRequestId(HttpContext context)
        {
            context.Request.Headers.Append("ID", Guid.NewGuid().ToString());
        }

        /// <summary>
        /// Setting the excepted Body for Response
        /// </summary>
        /// <param name="context"></param>
        /// <param name="originBody"></param>
        /// <returns></returns>
        private async Task setReponseBodyForSuccess(HttpContext context, Stream originBody)
        {
            //Assign new stream for Response
            var newResponseStream = new MemoryStream();
            context.Response.Body = newResponseStream;
            await _next(context);

            //Read current response
            newResponseStream.Position = 0;
            var responseBody = new StreamReader(newResponseStream).ReadToEnd();
            var statusCode = context.Response.StatusCode;
            var identity = context.Request.Headers["ID"];
            var serializedRequestBody = JsonConvert.DeserializeObject(responseBody);
            var requestBody = new MiddlewareReturnDTO(statusCode.ToString(), serializedRequestBody, identity);

            //Change Response Body
            var modifiedResponseStream = new MemoryStream();
            var sw = new StreamWriter(modifiedResponseStream);
            sw.Write(JsonConvert.SerializeObject(requestBody));
            sw.Flush();
            modifiedResponseStream.Position = 0;
            context.Response.Headers.ContentType = "application/json; charset=utf-8";
            await modifiedResponseStream.CopyToAsync(originBody);
        }

        /// <summary>
        /// Setting the excepted Body for Response if an Exception happened
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ex"></param>
        /// <param name="originBody"></param>
        /// <returns></returns>
        private async Task setResponseBodyForException(HttpContext context, Exception ex, Stream originBody)
        {
            var statusCode = 500;
            var identity = context.Request.Headers["ID"];
            var responseBody = new MiddlewareReturnDTO(statusCode.ToString(), ex.Message, identity);
            context.Response.StatusCode = 500;
            context.Response.Headers.ContentType = "application/json; charset=utf-8";
            var modifiedResponseStream = new MemoryStream();
            var sw = new StreamWriter(modifiedResponseStream);
            sw.Write(JsonConvert.SerializeObject(responseBody));
            sw.Flush();
            modifiedResponseStream.Position = 0;

            await modifiedResponseStream.CopyToAsync(originBody).ConfigureAwait(false);
        }
    }
}
