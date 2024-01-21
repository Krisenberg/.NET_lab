using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace List_10.Areas.Identity
{
    public class NotAdminRequirement : AuthorizationHandler<NotAdminRequirement>, IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, NotAdminRequirement requirement)
        {
            var roles = new[] { "Admin" };
            var userIsInRole = roles.Any(role => context.User.IsInRole(role));
            if (userIsInRole)
            {
                context.Fail();
                return Task.FromResult(false);
            }

            context.Succeed(requirement);
            return Task.FromResult(true);
        }
    }
}
