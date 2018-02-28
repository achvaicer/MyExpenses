using System;
namespace MyExpenses.Models
{
    public class CreditCard : NamedEntity
    {
        public CreditCard()
        {
        }

        public string Number
        {
            get;
            set;
        }
    }
}
