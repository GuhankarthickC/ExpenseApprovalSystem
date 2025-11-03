namespace ExpenseApprovalSystem.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public int SubmitterId { get; set; }
        public User Submitter { get; set; }
        public decimal Amount { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public byte[] FileData { get; set; }
        public DateTime SubmittedDate { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Pending";
    }
}
