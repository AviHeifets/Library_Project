using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Helpers.Validations;

namespace UnitTests
{
    [TestClass()]
    public class ValidationHelperTests
    {
        ValidationHelper validator = ValidationHelper.Init;

        [TestMethod()]
        public void ValidateIsbnTest()
        {
            Assert.IsFalse(validator.ValidateIsbn(string.Empty, false));
            Assert.IsTrue(validator.ValidateIsbn(string.Empty, true));
            Assert.IsTrue(validator.ValidateIsbn("123", true));
            Assert.IsTrue(validator.ValidateIsbn("123", false));
        }

        [TestMethod()]
        public void ValidateDoubleTest()
        {
            Assert.IsFalse(validator.ValidateDouble(string.Empty, "100", false));
            Assert.IsTrue(validator.ValidateDouble("95.02", "100", true));
            Assert.IsTrue(validator.ValidateDouble("95.02", "100", false));
            Assert.IsTrue(validator.ValidateDouble("95.02", "100", true));
            Assert.IsFalse(validator.ValidateDouble("925.02", "100", true));
            Assert.IsFalse(validator.ValidateDouble("925.02", "100", false));
            Assert.IsFalse(validator.ValidateDouble("925.02", "100", true));

        }

        [TestMethod()]
        public void ValidateIntTest()
        {
            Assert.IsFalse(validator.ValidateDouble(string.Empty, "100", false));
            Assert.IsTrue(validator.ValidateDouble("95", "100", true));
            Assert.IsTrue(validator.ValidateDouble("95", "100", false));
            Assert.IsTrue(validator.ValidateDouble("95", "100", true));
            Assert.IsFalse(validator.ValidateDouble("925", "100", true));
            Assert.IsFalse(validator.ValidateDouble("925", "100", false));
            Assert.IsFalse(validator.ValidateDouble("925", "100", true));
        }

        [TestMethod()]
        public void ValidateDateTest()
        {
            Assert.IsTrue(validator.ValidateDate(DateTime.Now));
            Assert.IsFalse(validator.ValidateDate(DateTime.Now.AddDays(1)));
        }

        [TestMethod()]
        public void ValidateStringTest()
        {
            Assert.IsFalse(validator.ValidateString(string.Empty, false));
            Assert.IsTrue(validator.ValidateString(string.Empty, true));
            Assert.IsTrue(validator.ValidateString("not empty", false));
            Assert.IsTrue(validator.ValidateString("not empty", true));
        }
    }
}