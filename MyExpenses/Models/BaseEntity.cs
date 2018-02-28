using System;
namespace MyExpenses.Models
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            DateCreated = DateLastUpdated = DateTime.Now;
        }

        public long Id
        {
            get;
            set;
        }

        public DateTime DateCreated
        {
            get;
            set;
        }

        public DateTime DateLastUpdated
        {
            get;
            set;
        }
    }
}
