using ExpenseApprovalSystem.DTOs;
using ExpenseApprovalSystem.Models;

namespace ExpenseApprovalSystem.Services.Contracts
{
    public interface IExpenseService
    {
        Task<Expense> SubmitExpenseAsync(ExpenseDto dto, int userId);
        Task<List<Expense>> GetExpensesAsync(int userId, string status);
        Task ApproveRejectAsync(int expenseId, string action, string comments, int approverId);
    }
}
