namespace ExpenseApprovalSystem.Models
{
    public class ApprovalRuleLevel
    {
        public int Id { get; set; }
        public int RuleId { get; set; }
        public ApprovalRule Rule { get; set; }
        public int Sequence { get; set; }
        public string ApproverType { get; set; }
    }
}
