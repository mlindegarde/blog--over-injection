namespace FullBoar.Examples.OverInjection.RefactoringDemo.Model
{
    public interface IAccount
    {
        ProcessResult Process(Deposit deposit);
        ProcessResult Process(Withdrawal withdrawal);
        ProcessResult Process(Check check);
        ProcessResult Process(Fee fee);

        int GetBalance();
    }
}