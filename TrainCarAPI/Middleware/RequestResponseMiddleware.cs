using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Security.Claims;
using TrainCarAPI.Model.DTO;
using TrainCarAPI.Model.Entity;
using TrainCarAPI.Services;

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
        /// Request and Response modifying Middleware Task 12, Task 15:f
        /// Append unique ID for Request and change Response body using the excepted format.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context, IRollingStockService rollingStockService, UserManager<ApplicationUser> userManager)
        {
            setRequestId(context);

            var originBody = context.Response.Body;
            try
            {
                using (var responseBody = new MemoryStream())
                {
                    //Create new stream for response
                    var response = context.Response;
                    response.Body = responseBody;
                    await _next(context);
                    //Get authenticated user username
                    var username = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                    //Get the response body
                    string responseBodyContent = IsRollingStockRequest(context, username) ? await GetSerializedFilteredRollingStocks(context, username, rollingStockService, userManager) : await GetResponseBodyContent(context.Response);
                    //Set the reponse format
                    await SetExpectedFormatForResponse(context, JsonConvert.DeserializeObject(responseBodyContent), response, originBody);
                }
            }
            catch (Exception ex)
            {
                using (var responseBody = new MemoryStream())
                {
                    var response = context.Response;
                    response.Body = responseBody;
                    response.StatusCode = 500;
                    await SetExpectedFormatForResponse(context, ex.Message, response, originBody);
                }
            }
        }

        /// <summary>
        /// Get the user company filtered rollingstocks
        /// </summary>
        /// <param name="context"></param>
        /// <param name="username"></param>
        /// <param name="rollingStockService"></param>
        /// <param name="userManager"></param>
        /// <returns></returns>
        private async Task<string> GetSerializedFilteredRollingStocks(HttpContext context, string username, IRollingStockService rollingStockService, UserManager<ApplicationUser> userManager)
        {
            var rollingStockStr = await GetResponseBodyContent(context.Response);
            var rollingStocks = JsonConvert.DeserializeObject<IList<RollingStock>>(rollingStockStr);
            var user = await userManager.FindByNameAsync(username);
            rollingStocks = rollingStocks.Where(rollingStock =>
            {
                var foundRollingStock = rollingStockService.GetById(rollingStock.Id);
                return foundRollingStock.Owner.Name == user.RailwayCompanyName;
            }).ToList();
            return JsonConvert.SerializeObject(rollingStocks);
        }

        private async Task SetExpectedFormatForResponse(HttpContext context, object serializedObject, HttpResponse response, Stream originBody)
        {
            var identity = context.Request.Headers["ID"];
            var statusCode = context.Response.StatusCode;
            var requestBody = new MiddlewareReturnDTO(statusCode.ToString(), serializedObject, identity);
            var modifiedResponseStream = new MemoryStream();
            var sw = new StreamWriter(modifiedResponseStream);
            sw.Write(JsonConvert.SerializeObject(requestBody));
            sw.Flush();
            modifiedResponseStream.Position = 0;
            response.ContentType = "application/json";
            await modifiedResponseStream.CopyToAsync(originBody);
        }


        private bool IsRollingStockRequest(HttpContext context, string username)
        {

            return context.Request.Path.Value.Contains("/api/RollingStock") &&
                !context.Request.Path.Value.Contains("GetAggergatedRollingStocks") &&
                context.Request.Method == "GET" && username != null;
        }

        /// <summary>
        /// Setting a unique ID for Request
        /// </summary>
        /// <param name="context"></param>
        private void setRequestId(HttpContext context)
        {
            context.Request.Headers.Append("ID", Guid.NewGuid().ToString());
        }

        private async Task<string> GetResponseBodyContent(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            string bodyText = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return bodyText;
        }


    }
}
