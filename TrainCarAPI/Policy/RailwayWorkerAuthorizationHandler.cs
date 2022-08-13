using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace TrainCarAPI.Policy
{
    public class RailwayWorkerAuthorizationHandler : AuthorizationHandler<RailwayWorkerPolicy>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RailwayWorkerPolicy requirement)
        {
            var railwayWorkerClaim = context.User.FindFirst(
                        c => c.Type == ClaimTypes.Actor);
            if (railwayWorkerClaim is null) { 
                return Task.CompletedTask;
            }
            if (bool.Parse(railwayWorkerClaim.Value))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
