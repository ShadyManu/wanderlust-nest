import { Component, inject, OnInit } from '@angular/core';
import { IonContent, IonRouterOutlet } from '@ionic/angular/standalone';
import { HeaderComponent } from '../../shared/components/header/header.component';

@Component({
  selector: 'app-planner',
  templateUrl: './planner.page.html',
  styleUrls: ['./planner.page.scss'],
  imports: [IonContent, HeaderComponent, IonRouterOutlet],
})
export class PlannerPage implements OnInit {
  private readonly routerOutlet = inject(IonRouterOutlet);

  ngOnInit(): void {
    this.routerOutlet.swipeGesture = true;
  }
}
