namespace LoanApp.Service;

public interface ILoanService
{
    Task<IEnumerable<Loan>> GetLoansAsync();
    Task<Loan?> GetLoanByIdAsync(long id);

    Task<Loan> CreateLoanAsync(Loan loan);
    Task<Loan> UpdateLoanAsync(long id, Loan loan);
    Task<bool> DeleteLoanAsync(long id);
}