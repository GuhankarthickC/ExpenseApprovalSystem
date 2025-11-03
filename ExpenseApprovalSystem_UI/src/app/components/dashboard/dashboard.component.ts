import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTabsModule } from '@angular/material/tabs';
import { AuthService } from '../../services/auth.service';
import { SubmitExpenseComponent } from '../submit-expense/submit-expense.component';
import { ViewSubmittedComponent } from '../view-submitted/view-submitted.component';
import { ViewPendingComponent } from '../view-pending/view-pending.component';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule, 
    MatToolbarModule, 
    MatButtonModule, 
    MatIconModule, 
    MatTabsModule,
    SubmitExpenseComponent, 
    ViewSubmittedComponent, 
    ViewPendingComponent
  ],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent {
  activeTab: string = 'view-submitted';
  userRole: string | null = null;

  constructor(private authService: AuthService, private router: Router) {
    this.userRole = this.authService.getUserRole();
  }

  setActiveTab(tab: string) {
    this.activeTab = tab;
  }

  logout() {
    this.authService.logout();
  }

  isEmployee(): boolean {
    return this.userRole === 'Employee';
  }

  isApprover(): boolean {
    return this.userRole === 'Manager' || this.userRole === 'DepartmentHead' || this.userRole === 'CFO';
  }
}
