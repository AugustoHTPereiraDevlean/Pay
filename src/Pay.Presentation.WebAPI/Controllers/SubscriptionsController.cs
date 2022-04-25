using Microsoft.AspNetCore.Mvc;
using Pay.Core.Abstractions.Services;
using Pay.Presentation.WebAPI.Controllers.Base;
using Pay.Presentation.WebAPI.Requests;

namespace Pay.Presentation.WebAPI.Controllers
{
    public class SubscriptionsController : APIController
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionsController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpPost]
        public async Task<IActionResult> OnPostAsync([FromBody] SubscribeRequest request)
        {
            return CreateResult(await _subscriptionService.SubscribeAsync(request.PlanId, request.UserId, request.PaymentMethod, request.CouponId?.ToString()));
        }
    }
}