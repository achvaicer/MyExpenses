using System;
namespace MyExpenses.Models
{
    public class CreditEntry : Entry
    {
        public CreditEntry()
        {
        }

        public string Currency
        {
            get;
            set;
        }

        public long CreditCardId
        {
            get;
            set;
        }
    }
}
