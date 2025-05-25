using LoanApp.Data;
using LoanApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LoanApp.Service.ILoanService;



namespace LoanApp.Service;


public class LoanService : ILoanService
{
    private readonly LoanContext _context;

    public LoanService(LoanContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Loan>> GetLoansAsync()
    {
        return await _context.Loans.ToListAsync();
    }

    public async Task<Loan?> GetLoanAsync(long id)
    {

        return await _context.Loans.FindAsync(id);
    }

    public async Task<Loan> CreateLoanAsync(Loan loan)
    {
        if (loan.Amount < 1000 || loan.Amount > 1000000)
            throw new ArgumentException("Loan amount must be between 1000 and 1,000,000.");
        if (loan.InterestRate < 0.01m || loan.InterestRate > 20.0m)
            throw new ArgumentException("Interest rate must be between 0.01% and 20%.");

        loan.ApplicationDate = DateTime.UtcNow;
        _context.Loans.Add(loan);
        await _context.SaveChangesAsync();
        return loan;
    }
    public async Task<bool> UpdateLoanAsync(long id, Loan loan)
    {
        if (id != loan.Id)
            return false;

        var existingLoan = await _context.Loans.FindAsync(id);
        if (existingLoan == null)
            return false;

        existingLoan.BorrowerName = loan.BorrowerName;
        existingLoan.Amount = loan.Amount;
        existingLoan.InterestRate = loan.InterestRate;
        existingLoan.Status = loan.Status;

        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            return false;
        }
    }

    public async Task<bool> DeleteLoanAsync(long id)
    {
        var loan = await _context.Loans.FindAsync(id);
        if (loan == null)
            return false;

        _context.Loans.Remove(loan);
        await _context.SaveChangesAsync();
        return true;
    }

}