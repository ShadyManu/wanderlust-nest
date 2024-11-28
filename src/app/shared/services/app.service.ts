import { Injectable, signal } from '@angular/core';
import { AppPage } from '../types/layout.types';

@Injectable({
  providedIn: 'root',
})
export class AppService {
  private currentPage = signal<AppPage>(AppPage.Home);

  get getCurrentPage() {
    return this.currentPage;
  }

  setCurrentPage(page: AppPage): void {
    this.currentPage.set(page);
  }
}
