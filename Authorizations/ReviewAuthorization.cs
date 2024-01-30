using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UserMicroservice.Interfaces;

namespace UserMicroservice.Authorizations
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class ReviewAuthorization : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewAuthorization(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userId = context.HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.NameId);
            var routeData = context.RouteData;
            if (routeData.Values.TryGetValue("reviewId", out var reviewIdObj) && int.TryParse(reviewIdObj.ToString(), out var reviewId))
            {
                if (!_reviewRepository.IsUserReviewOwner(reviewId, userId)) context.Result = new ForbidResult();
            }
            else context.Result = new BadRequestResult();
        }
    }
}
