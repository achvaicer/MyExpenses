using System;
namespace MyExpenses.Models
{
    public abstract class Entry
    {
        public Entry()
        {
        }

        public decimal Value
        {
            get;
            set;
        }

        public DateTime Date
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }
    }
}
