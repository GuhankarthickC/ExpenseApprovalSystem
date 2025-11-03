-- Get all expenses pending for a given user (userId param)
SELECT e.* FROM Expenses e
WHERE e.Status = 'Pending' AND 
(
    SELECT COUNT(*) FROM ApprovalHistory h WHERE h.ExpenseId = e.Id AND h.Action = 'Approve'
) + 1 = 
(
    SELECT l.Sequence FROM ApprovalRuleLevels l
    INNER JOIN ApprovalRules r ON l.RuleId = r.Id
    WHERE e.Amount >= r.MinAmount AND (r.MaxAmount IS NULL OR e.Amount <= r.MaxAmount)
    AND (
        (l.ApproverType = 'Manager' AND @userId = (SELECT ManagerId FROM Users WHERE Id = e.SubmitterId))
        OR
        (l.ApproverType = 'DepartmentHead' AND @userId = (SELECT d.HeadId FROM Departments d INNER JOIN Users u ON u.DepartmentId = d.Id WHERE u.Id = e.SubmitterId))
        OR
        (l.ApproverType = 'CFO' AND @userId = (SELECT Id FROM Users WHERE RoleId = (SELECT Id FROM Roles WHERE Name = 'CFO')))
    )
);