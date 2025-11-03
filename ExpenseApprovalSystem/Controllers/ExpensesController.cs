using ExpenseApprovalSystem.DTOs;
using ExpenseApprovalSystem.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExpenseApprovalSystem.Controllers
{
    [Route("api/expenses")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpenseService _service;
        public ExpensesController(IExpenseService service) { _service = service; }

        [HttpPost("submit")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> Submit([FromForm] ExpenseDto dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var expense = await _service.SubmitExpenseAsync(dto, userId);
            return Ok(expense);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get(string status = null)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var expenses = await _service.GetExpensesAsync(userId, status);
            return Ok(expenses);
        }

        [HttpPost("{id}/action")]
        [Authorize(Roles = "Manager,DepartmentHead,CFO")]
        public async Task<IActionResult> Action(int id, [FromBody] ActionDto dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _service.ApproveRejectAsync(id, dto.Action, dto.Comments, userId);
            return Ok();
        }
    }
}
