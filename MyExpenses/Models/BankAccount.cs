using System;
namespace MyExpenses.Models
{
    public class BankAccount
    {
        public BankAccount()
        {
        }

        public long BankId
        {
            get;
            set;
        }

        public string AgencyNumber
        {
            get;
            set;
        }

        public string AccountNumber
        {
            get;
            set;
        }
    }
}
