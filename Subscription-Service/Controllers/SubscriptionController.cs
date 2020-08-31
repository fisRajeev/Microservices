using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SubscriptionService.Models;
using Microsoft.AspNetCore.Mvc;

namespace SubscriptionService.Controllers
{
    [Route("subscription")]
    public class SubscriptionController : ControllerBase
    {
        // GET /subscription
        [HttpGet]
        public Subscription[] Get()
        {
            Subscriptions obj = new Subscriptions();
            return obj.GetSubscriptions();
        }

        // GET subscription/Alice
        [HttpGet("{id}")]
        public Subscription Get(string id)
        {
            Subscriptions obj = new Subscriptions();
            Subscription sub = obj.GetSubscriptionByName(id);

            if (sub.subscriberName != null && sub.subscriberName.Length > 0)
            {
                return sub;
            }
            else
            {
                return null;
            }
        }
    }
}
