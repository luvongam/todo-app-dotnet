using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LoanApp.Data;
using LoanApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoanApp.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Authorize]
    public class LoansController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoansController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        // GET: api/v1/Loans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Loan>>> GetLoans()
        {
            var loans = await _loanService.GetLoansAsync();
            return Ok(loans);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Loan>> GetLoan(long id)
        {
            var loan = await _loanService.GetLoanAsync(id);
            if (loan == null)
            {
                return NotFound();
            }
            return Ok(loan);
        }
        // POST: api/v1/Loans
        [HttpPost]
        public async Task<ActionResult<Loan>> CreateLoan(Loan loan)
        {
            try
            {
                var createdLoan = await _loanService.CreateLoanAsync(loan);
                return CreatedAtAction(nameof(GetLoan), new { id = createdLoan.Id }, createdLoan);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // PUT: api/v1/Loans/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLoan(long id, Loan loan)
        {
            var updated = await _loanService.UpdateLoanAsync(id, loan);
            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE: api/v1/Loans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoan(long id)
        {
            var loan = await _context.Loans.FindAsync(id);
            if (loan == null)
            {
                return NotFound();
            }

            _context.Loans.Remove(loan);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        // DELETE: api/v1/Loans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoan(long id)
        {
            var deleted = await _loanService.DeleteLoanAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}