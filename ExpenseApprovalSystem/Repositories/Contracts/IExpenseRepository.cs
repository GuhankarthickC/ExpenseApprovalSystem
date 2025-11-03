using ExpenseApprovalSystem.Models;

namespace ExpenseApprovalSystem.Repositories.Contracts
{
    public interface IExpenseRepository
    {
        Task<Expense> AddExpenseAsync(Expense expense);
        Task<List<Expense>> GetExpensesByUserAsync(int userId, string status);
        Task<Expense> GetExpenseByIdAsync(int id);
        Task AddApprovalHistoryAsync(ApprovalHistory history);
        Task<List<ApprovalHistory>> GetApprovalHistoryAsync(int expenseId);
        Task<ApprovalRule> GetRuleForAmountAsync(decimal amount);
        Task<int> GetNextApproverIdAsync(int expenseId, int currentLevel);

        Task<List<Expense>> GetPendingForUserAsync(int userId);
        Task UpdateExpenseAsync(Expense expense);
    }
}
