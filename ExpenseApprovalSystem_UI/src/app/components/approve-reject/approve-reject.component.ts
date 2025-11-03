import { Component, Input, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { ExpenseService } from '../../services/expense.service';

@Component({
  selector: 'app-approve-reject',
  standalone: true,
  imports: [
    ReactiveFormsModule, 
    MatFormFieldModule, 
    MatInputModule, 
    MatButtonModule, 
    MatIconModule,
    MatSnackBarModule
  ],
  templateUrl: './approve-reject.component.html',
  styleUrl: './approve-reject.component.scss'
})
export class ApproveRejectComponent {

form: FormGroup;
@Input() expenseId!: number;
@Output() actionComplete = new EventEmitter<void>();

constructor(
  private fb: FormBuilder, 
  private service: ExpenseService, 
  private router: Router,
  private snackBar: MatSnackBar
){
  this.form = this.fb.group({ comments: [''] });
}


approve() {
  this.service.actionExpense(this.expenseId, {Action: 'Approve', Comments: this.form.value.comments}).subscribe({
    next: res => {
      console.log('Expense approved');
      this.snackBar.open('Expense approved successfully!', 'Close', {
        duration: 3000,
        horizontalPosition: 'end',
        verticalPosition: 'top'
      });
      this.form.reset();
      this.actionComplete.emit();
    },
    error: err => {
      if (err.status === 403) {
        this.router.navigate(['/forbidden']);
      } else {
        console.error('Error approving expense:', err);
        this.snackBar.open('Failed to approve expense. Please try again.', 'Close', {
          duration: 3000,
          horizontalPosition: 'end',
          verticalPosition: 'top'
        });
      }
    }
  });
}

reject() {
  this.service.actionExpense(this.expenseId, {Action: 'Reject', Comments: this.form.value.comments}).subscribe({
    next: res => {
      console.log('Expense rejected');
      this.snackBar.open('Expense rejected successfully!', 'Close', {
        duration: 3000,
        horizontalPosition: 'end',
        verticalPosition: 'top'
      });
      this.form.reset();
      this.actionComplete.emit();
    },
    error: err => {
      if (err.status === 403) {
        this.router.navigate(['/forbidden']);
      } else {
        console.error('Error rejecting expense:', err);
        this.snackBar.open('Failed to reject expense. Please try again.', 'Close', {
          duration: 3000,
          horizontalPosition: 'end',
          verticalPosition: 'top'
        });
      }
    }
  });
}
}
