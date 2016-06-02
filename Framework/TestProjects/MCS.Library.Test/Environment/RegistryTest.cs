using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;
using MCS.Library.Core;

namespace MCS.Library.Test.Environment
{
    [TestClass]
    public class RegistryTest
    {
        public const string RegistrySubKey = "SOFTWARE\\AK47\\Variables";
        public const string RegistryKey = "MCS2016TestDir";
        public const string RegistryValue = "X:\\AK47-2016\\Config";

        //[TestMethod]
        public void ReadNotExistsKey()
        {
            string wrongVariableName = "%MCS2016TestDir1%";
            string filePath = wrongVariableName + "\\MCS.Framework.config";

            string result = EnvironmentHelper.ReplaceRegistryVariablesInString(filePath);

            Console.WriteLine(result);

            Assert.IsTrue(result.IndexOf(wrongVariableName) >= 0);
        }

        //[TestMethod]
        public void ReadExistsKey()
        {
            string filePath = "%MCS2016TestDir%\\MCS.Framework.config";

            string result = EnvironmentHelper.ReplaceRegistryVariablesInString(filePath);

            Console.WriteLine(result);

            Assert.IsTrue(result.IndexOf("%MCS2016TestDir%") < 0);
        }

        //[TestMethod]
        //public void ReadExistsKeyTest()
        //{
        //    using (RegistryKey subKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\AK47\\Variables"))
        //    {

        //    }
        //        string filePath = "%MCS2016XueDaLocalTestDir%\\MCS.Framework.config";

        //    string result = EnvironmentHelper.ReplaceRegistryVariablesInString(filePath);

        //    Console.WriteLine(result);

        //    Assert.IsTrue(result.IndexOf("%MCS2016TestDir%") < 0);
        //}
    }
}
