import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ExpenseService {

  private apiUrl = 'https://localhost:7057/api/expenses'; 

  constructor(private http: HttpClient) {}

  submitExpense(formData: FormData): Observable<any> {
    return this.http.post(`${this.apiUrl}/submit`, formData, { headers: this.getAuthHeader() });
  }

  getExpenses(status?: string): Observable<any[]> {
    let url = this.apiUrl;
    if (status) url += `?status=${status}`;
    return this.http.get<any[]>(url, { headers: this.getAuthHeader() });
  }

  actionExpense(id: number, ActionDto: { Action: string, Comments: string }): Observable<any> {
    return this.http.post(`${this.apiUrl}/${id}/action`, ActionDto, { headers: this.getAuthHeader() });
  }

  private getAuthHeader() {
    const token = localStorage.getItem('token'); 
    return new HttpHeaders({ Authorization: `Bearer ${token}` });
  }
}
