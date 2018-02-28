using System;
namespace MyExpenses.Models
{
    public class Category : NamedEntity
    {
        public long ClusterId
        {
            get;
            set;
        }
        public Category()
        {
        }
    }
}
