CREATE PROCEDURE GetExpenseStatusAndNextApprover
    @expenseId INT
AS
BEGIN
    SELECT Status FROM Expenses WHERE Id = @expenseId;

    DECLARE @approvalsCount INT = (SELECT COUNT(*) FROM ApprovalHistory WHERE ExpenseId = @expenseId AND Action = 'Approve');
    DECLARE @nextLevel INT = @approvalsCount + 1;

    DECLARE @amount DECIMAL(18,2) = (SELECT Amount FROM Expenses WHERE Id = @expenseId);
    DECLARE @ruleId INT = (SELECT Id FROM ApprovalRules WHERE @amount >= MinAmount AND (@amount <= MaxAmount OR MaxAmount IS NULL));

    DECLARE @approverType VARCHAR(50) = (SELECT ApproverType FROM ApprovalRuleLevels WHERE RuleId = @ruleId AND Sequence = @nextLevel);

    DECLARE @nextApproverId INT;

    IF @approverType = 'Manager'
        SET @nextApproverId = (SELECT ManagerId FROM Users u INNER JOIN Expenses e ON e.SubmitterId = u.Id WHERE e.Id = @expenseId);
    ELSE IF @approverType = 'DepartmentHead'
        SET @nextApproverId = (SELECT d.HeadId FROM Departments d INNER JOIN Users u ON u.DepartmentId = d.Id INNER JOIN Expenses e ON e.SubmitterId = u.Id WHERE e.Id = @expenseId);
    ELSE IF @approverType = 'CFO'
        SET @nextApproverId = (SELECT Id FROM Users WHERE RoleId = (SELECT Id FROM Roles WHERE Name = 'CFO'));

    SELECT @nextApproverId AS NextApproverId;
END