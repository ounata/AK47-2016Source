using MCS.Library.Validation;
using PPTS.Data.Products.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Products.ViewModels.ServiceFees
{
    public class EditExpensesModel
    {
        [ObjectValidator]
        public Expense expense { set; get; }

        [ObjectValidator]
        public string[] ExpenseIds { set; get; }
    }
}