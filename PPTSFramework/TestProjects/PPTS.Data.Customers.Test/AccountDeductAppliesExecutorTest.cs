using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers.Executors;
using PPTS.WebAPI.Customers.Executors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Test
{
    [TestClass]
    public class AccountDeductAppliesExecutorTest
    {
        [TestMethod]
        public void Deduct()
        {
            List<CustomerExpenseRelation> expenses = new List<CustomerExpenseRelation>();
            CustomerExpenseRelation expense = new CustomerExpenseRelation();
            expense.AccountID = "241440";
            expense.CustomerID = "3990022";
            expense.ExpenseID = "1";
            expense.ExpenseType = "0";
            expense.ExpenseMoney = 200;
            expenses.Add(expense);
            expense.AccountID = "241440";
            expense.CustomerID = "3990022";
            expense.ExpenseID = "2";
            expense.ExpenseType = "1";
            expense.ExpenseMoney = 300;
            //expenses.Add(expense);

            AccountDeductAppliesModel model = new AccountDeductAppliesModel(expenses);
            model.Prepare();
            new AccountDeductAppliesExecutor(model).Execute();
        }
    }
}
