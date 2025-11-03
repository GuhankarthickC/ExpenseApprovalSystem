namespace ExpenseApprovalSystem.Models
{
    public class ApprovalRule
    {
        public int Id { get; set; }
        public decimal MinAmount { get; set; }
        public decimal? MaxAmount { get; set; }
        public List<ApprovalRuleLevel> Levels { get; set; }
    }
}
