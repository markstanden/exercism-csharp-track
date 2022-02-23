using System;

static class SavingsAccount
{
    public static float InterestRate(decimal balance)
        => balance switch
        {
            < 0                => 3.213f,
            >= 0 and < 1000    => 0.50f,
            >= 1000 and < 5000 => 1.621f,
            >= 5000            => 2.475f,
        };


    public static decimal Interest(decimal balance)
        => balance * (decimal) (InterestRate(balance) / 100);


    public static decimal AnnualBalanceUpdate(decimal balance)
        => balance + Interest(balance);


    public static int YearsBeforeDesiredBalance(decimal balance, decimal targetBalance)
    {
        int year = 0;
        decimal newBalance = balance;

        while (newBalance < targetBalance) {
            newBalance = AnnualBalanceUpdate(newBalance);
            year++;
        }

        return year;
    }
}