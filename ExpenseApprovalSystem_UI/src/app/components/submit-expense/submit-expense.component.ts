import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { ExpenseService } from '../../services/expense.service';

@Component({
  selector: 'app-submit-expense',
  standalone: true,
  imports: [
    CommonModule, 
    ReactiveFormsModule, 
    MatFormFieldModule, 
    MatInputModule, 
    MatSelectModule, 
    MatButtonModule,
    MatIconModule,
    MatSnackBarModule
  ],
  templateUrl: './submit-expense.component.html',
  styleUrl: './submit-expense.component.scss'
})
export class SubmitExpenseComponent {
form: FormGroup;

  constructor(
    private fb: FormBuilder, 
    private service: ExpenseService, 
    private router: Router,
    private snackBar: MatSnackBar
  ) {
    this.form = fb.group({
      amount: ['', Validators.required],
      category: ['', Validators.required],
      description: ['', Validators.required],
      file: [null]
    });
  }

  onFileChange(event: any) {
    this.form.patchValue({ file: event.target.files[0] });
  }

  submit() {
    const formData = new FormData();
    formData.append('Amount', this.form.value.amount);
    formData.append('Category', this.form.value.category);
    formData.append('Description', this.form.value.description);
    if (this.form.value.file) formData.append('File', this.form.value.file);

    this.service.submitExpense(formData).subscribe({
      next: res => {
        console.log('Submitted successfully');
        this.snackBar.open('Expense submitted successfully!', 'Close', {
          duration: 3000,
          horizontalPosition: 'end',
          verticalPosition: 'top'
        });
        this.form.reset();
      },
      error: err => {
        if (err.status === 403) {
          this.router.navigate(['/forbidden']);
        } else {
          console.error('Error submitting expense:', err);
          this.snackBar.open('Failed to submit expense. Please try again.', 'Close', {
            duration: 3000,
            horizontalPosition: 'end',
            verticalPosition: 'top'
          });
        }
      }
    });
  }
}
