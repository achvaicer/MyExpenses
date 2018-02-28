using System;
namespace MyExpenses.Repository
{
    public class ConnectionString
    {
        public static string AppHarbor = System.Configuration.ConfigurationManager.ConnectionStrings["MyExpensesConnectionString"].ConnectionString;
    }
}
