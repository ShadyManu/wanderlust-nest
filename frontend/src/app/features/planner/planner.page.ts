import { Component } from '@angular/core';
import { IonContent } from '@ionic/angular/standalone';
import { HeaderComponent } from '../../shared/components/header/header.component';

@Component({
    selector: 'app-planner',
    templateUrl: './planner.page.html',
    styleUrls: ['./planner.page.scss'],
    imports: [IonContent, HeaderComponent]
})
export class PlannerPage {}
