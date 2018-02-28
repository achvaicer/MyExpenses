using System;
namespace MyExpenses.Models
{
    public class NamedEntity : BaseEntity
    {
        public string Name
        {
            get;
            set;
        }
        public NamedEntity()
        {
        }
    }
}
