using ExpenseApprovalSystem.Data;
using ExpenseApprovalSystem.Models;
using ExpenseApprovalSystem.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ExpenseApprovalSystem.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly AppDbContext _context;
        public ExpenseRepository(AppDbContext context) { _context = context; }

        public async Task<Expense> AddExpenseAsync(Expense expense)
        {
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();
            return expense;
        }

        public async Task<ApprovalRule> GetRuleForAmountAsync(decimal amount)
        {
            return await _context.ApprovalRules
                .Include(r => r.Levels)
                .FirstOrDefaultAsync(r => amount >= r.MinAmount && (r.MaxAmount == null || amount <= r.MaxAmount));
        }

        public async Task<int> GetNextApproverIdAsync(int expenseId, int currentLevel)
        {
            var expense = await _context.Expenses.Include(e => e.Submitter).ThenInclude(u => u.Department).ThenInclude(d => d.Head).FirstOrDefaultAsync(e => e.Id == expenseId);
            var rule = await GetRuleForAmountAsync(expense.Amount);
            var level = rule.Levels.Find(l => l.Sequence == currentLevel);
            if (level == null) return -1;

            switch (level.ApproverType)
            {
                case "Manager": return expense.Submitter.ManagerId ?? -1;
                case "DepartmentHead": return expense.Submitter.Department.HeadId ?? -1;
                case "CFO": return await _context.Users.Where(u => u.Role.Name == "CFO").Select(u => u.Id).FirstOrDefaultAsync();
                default: return -1;
            }
        }

        public async Task<List<Expense>> GetPendingForUserAsync(int userId)
        {
            var pendings = await _context.Expenses.Where(e => e.Status == "Pending").ToListAsync();
            var result = new List<Expense>();
            foreach (var exp in pendings)
            {
                var approvalsCount = await _context.ApprovalHistories.CountAsync(h => h.ExpenseId == exp.Id && h.Action == "Approve");
                var nextId = await GetNextApproverIdAsync(exp.Id, approvalsCount + 1);
                if (nextId == userId) result.Add(exp);
            }
            return result;
        }

        public async Task<List<Expense>> GetExpensesByUserAsync(int userId, string status)
        {
            var query = _context.Expenses
            .AsNoTracking()
            .Include(e => e.Submitter)
            .Where(e => e.SubmitterId == userId);

            if (!string.IsNullOrEmpty(status) && status.ToLower() != "all")
            {
                query = query.Where(e => e.Status == status);
            }

            return await query.ToListAsync();
        }

        public async Task<Expense> GetExpenseByIdAsync(int id)
        {
            return await _context.Expenses
            .AsNoTracking()
            .Include(e => e.Submitter)
            .ThenInclude(u => u.Department)
            .ThenInclude(d => d.Head)
            .Include(e => e.Submitter)
            .ThenInclude(u => u.Manager)
            .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task AddApprovalHistoryAsync(ApprovalHistory history)
        {
            _context.ApprovalHistories.Add(history);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ApprovalHistory>> GetApprovalHistoryAsync(int expenseId)
        {
            return await _context.ApprovalHistories
            .AsNoTracking()
            .Include(h => h.Approver)
            .Where(h => h.ExpenseId == expenseId)
            .OrderBy(h => h.ApprovalDate)
            .ToListAsync();
        }

        public async Task UpdateExpenseAsync(Expense expense)
        {
            if (expense == null) throw new ArgumentNullException(nameof(expense));

            var entry = _context.Entry(expense);
            if (entry.State == EntityState.Detached)
            {
                _context.Expenses.Update(expense);
            }
            else
            {
                entry.State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();
        }
    }
}
