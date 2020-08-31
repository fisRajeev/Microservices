using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubscriptionService.Models
{
    public class Subscription
    {
        public string subscriberName { get; set; }
 
        public DateTime dateSubscribed { get; set; }

        public DateTime dateReturned { get; set; }

        public int bookId { get; set; }
    }
}
