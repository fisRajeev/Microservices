import { Component, OnInit } from '@angular/core';
import { SubscriptionService } from './../core/subscription.service';
import { Subscription } from './../model/Subscription';

@Component({
  selector: 'app-subscription-service',
  templateUrl: './subscription-service.component.html',
  styleUrls: ['./subscription-service.component.css']
})
export class SubscriptionServiceComponent implements OnInit {

  subs: Subscription[] = [];
  constructor(private subservice: SubscriptionService) { }

  ngOnInit() {
    this.subservice.getSubscriptions().subscribe(s =>
      this.subs = s
    )};

}
