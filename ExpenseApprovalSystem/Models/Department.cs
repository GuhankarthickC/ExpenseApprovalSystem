namespace ExpenseApprovalSystem.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? HeadId { get; set; }
        public User? Head { get; set; }
    }
}
