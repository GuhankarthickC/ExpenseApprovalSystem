namespace ExpenseApprovalSystem.DTOs
{
    public class ExpenseDto
    {
        public decimal Amount { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public IFormFile? File { get; set; }
    }
}
