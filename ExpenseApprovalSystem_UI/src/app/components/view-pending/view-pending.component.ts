import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatChipsModule } from '@angular/material/chips';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule } from '@angular/material/dialog';
import { ExpenseService } from '../../services/expense.service';
import { ApproveRejectComponent } from '../approve-reject/approve-reject.component';

@Component({
  selector: 'app-view-pending',
  standalone: true,
  imports: [
    CommonModule, 
    MatTableModule, 
    MatChipsModule, 
    MatButtonModule, 
    MatIconModule,
    MatDialogModule,
    ApproveRejectComponent
  ],
  templateUrl: './view-pending.component.html',
  styleUrl: './view-pending.component.scss'
})
export class ViewPendingComponent {
  constructor(private expenseService: ExpenseService, private router: Router){}
  expenses: any[] = [];
  selectedExpenseId: number | null = null;
  displayedColumns: string[] = ['amount', 'category', 'description', 'status', 'actions'];

  ngOnInit() {
    this.loadExpenses();
  }

  loadExpenses() {
    this.expenseService.getExpenses('Pending').subscribe({
      next: data => this.expenses = data,
      error: err => {
        if (err.status === 403) {
          this.router.navigate(['/forbidden']);
        } else {
          console.error('Error loading expenses:', err);
        }
      }
    }); 
  }

  selectExpense(expenseId: number) {
    this.selectedExpenseId = expenseId;
  }

  onActionComplete() {
    this.selectedExpenseId = null;
    this.loadExpenses();
  }
}
