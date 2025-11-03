namespace ExpenseApprovalSystem.Models
{
    public class ApprovalHistory
    {
        public int Id { get; set; }
        public int ExpenseId { get; set; }
        public Expense Expense { get; set; }
        public int ApproverId { get; set; }
        public User Approver { get; set; }
        public string Action { get; set; }
        public string Comments { get; set; }
        public DateTime ApprovalDate { get; set; } = DateTime.UtcNow;
    }
}
