using System;
namespace MyExpenses.Models
{
    public class DebitEntry : Entry
    {
        
        public DebitEntry()
        {
        }

        public long BankAccountId
        {
            get;
            set;
        }
    }
}
