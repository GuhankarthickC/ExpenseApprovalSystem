using ExpenseApprovalSystem.DTOs;
using ExpenseApprovalSystem.Models;
using ExpenseApprovalSystem.Repositories.Contracts;
using ExpenseApprovalSystem.Services;
using Moq;
using Xunit;

namespace Backend.Tests;

public class ExpenseServiceTest
{
    [Fact]
    public async Task SubmitExpense_ShouldAddToRepo()
    {
        var mockRepo = new Mock<IExpenseRepository>();
        var service = new ExpenseService(mockRepo.Object);
        var dto = new ExpenseDto { Amount = 1000 };
        await service.SubmitExpenseAsync(dto, 1);
        mockRepo.Verify(r => r.AddExpenseAsync(It.IsAny<Expense>()), Times.Once);
    }
}
