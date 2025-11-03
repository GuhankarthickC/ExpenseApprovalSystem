import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatChipsModule } from '@angular/material/chips';
import { ExpenseService } from '../../services/expense.service';

@Component({
  selector: 'app-view-submitted',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatChipsModule],
  templateUrl: './view-submitted.component.html',
  styleUrl: './view-submitted.component.scss'
})
export class ViewSubmittedComponent {

constructor(private expenseService: ExpenseService, private router: Router){}
expenses: any[] = [];
displayedColumns: string[] = ['amount', 'category', 'description', 'status'];

ngOnInit() {
  this.expenseService.getExpenses('all').subscribe({
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
}
