using ExpenseApprovalSystem.DTOs;
using ExpenseApprovalSystem.Models;
using ExpenseApprovalSystem.Repositories.Contracts;
using ExpenseApprovalSystem.Services.Contracts;

namespace ExpenseApprovalSystem.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _repo;
        public ExpenseService(IExpenseRepository repo) { _repo = repo; }

        public async Task<Expense> SubmitExpenseAsync(ExpenseDto dto, int userId)
        {
            var expense = new Expense
            {
                SubmitterId = userId,
                Amount = dto.Amount,
                Category = dto.Category,
                Description = dto.Description,
                FileData = dto.File != null ? await FileToByteArrayAsync(dto.File) : []
            };
            var added = await _repo.AddExpenseAsync(expense);

            return added;
        }

        private async Task<byte[]> FileToByteArrayAsync(IFormFile file)
        {
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            return ms.ToArray();
        }

        public async Task<List<Expense>> GetExpensesAsync(int userId, string status)
        {
            return await _repo.GetExpensesByUserAsync(userId, status);
        }

        public async Task ApproveRejectAsync(int expenseId, string action, string comments, int approverId)
        {
            var expense = await _repo.GetExpenseByIdAsync(expenseId);
            if (expense.Status != "Pending") throw new Exception("Not pending");

            var approvalHistory = await _repo.GetApprovalHistoryAsync(expenseId);
            var approvalsCount = approvalHistory.Count(h => h.Action == "Approve");
            var nextApproverId = await _repo.GetNextApproverIdAsync(expenseId, approvalsCount + 1);
            if (nextApproverId != approverId) throw new Exception("Not your turn to approve");

            var history = new ApprovalHistory { ExpenseId = expenseId, ApproverId = approverId, Action = action, Comments = comments };
            await _repo.AddApprovalHistoryAsync(history);

            if (action == "Reject")
            {
                expense.Status = "Rejected";
            }
            else
            {
                var rule = await _repo.GetRuleForAmountAsync(expense.Amount);
                if (approvalsCount + 1 == rule.Levels.Count)
                {
                    expense.Status = "Approved";
                }
            }
            await _repo.UpdateExpenseAsync(expense);
        }
    }
}
