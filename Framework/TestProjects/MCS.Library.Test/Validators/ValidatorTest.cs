using MCS.Library.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace MCS.Library.Test.Validators
{
    [TestClass]
    public class ValidatorTest
    {
        [TestMethod]
        public void SimpleCustomerTest()
        {
            Customer customer = new Customer();

            Validator validator = ValidationFactory.CreateValidator(typeof(Customer));

            ValidationResults results = validator.Validate(customer);

            OutputValidationResults(results);

            Assert.IsTrue(results.ResultCount > 0);
        }

        [TestMethod]
        public void CompositeObjectTest()
        {
            CompositeObject contanier = new CompositeObject();

            contanier.Customer = new Customer();
            contanier.Order = new Order();

            Validator validator = ValidationFactory.CreateValidator(typeof(CompositeObject));

            ValidationResults results = validator.Validate(contanier);

            OutputValidationResults(results);
        }

        private static void OutputValidationResults(IEnumerable<ValidationResult> innerResults)
        {
            foreach (ValidationResult innerResult in innerResults)
                Console.WriteLine(innerResult.Message);
        }
    }
}
