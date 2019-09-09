using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace _6._1
{
    [TestClass]
    public class CalculatorTests
    {
        private ICalculator calculator = new Calculator();

        /// <summary>
        /// Check function Calculate, shall return 2
        /// Test Add func
        /// </summary>
        [TestMethod]
        public void CalculateExpressionCheckAddShallReturn2()
        {
            List<string> input = new List<string>() { "1", "+", "1" };
            Assert.AreEqual(2, calculator.Calculate(input));
        }

        /// <summary>
        /// Check function Calculate, shall return 1
        /// Test Subtrack func
        /// </summary>
        [TestMethod]
        public void CalculateExpressionSubtrackCheckShallReturn1()
        {
            List<string> input = new List<string>() { "2", "-", "1" };
            Assert.AreEqual(1, calculator.Calculate(input));
        }

        /// <summary>
        /// Check function Calculate, shall return 24
        /// Test Multiply func
        /// </summary>
        [TestMethod]
        public void CalculateExpressionMultiplyCheckShallReturn24()
        {
            List<string> input = new List<string>() { "3", "*", "8" };
            Assert.AreEqual(24, calculator.Calculate(input));
        }

        /// <summary>
        /// Check function Calculate, shall return 0.3
        /// Test Divide func
        /// </summary>
        [TestMethod]
        public void CalculateExpressionDivideCheck()
        {
            List<string> input = new List<string>() { "3", "/", "9" };
            Assert.AreEqual(0.333, calculator.Calculate(input), 0.01);
        }

        /// <summary>
        /// Check function Calculate, shall return 18
        /// </summary>
        [TestMethod]
        public void CalculateExpressionShallReturn18()
        {
            List<string> input = new List<string>() { "9", "+", "3", "*", "(", "6", "-", "5", ")", "/", "3", "-",
                                                      "(", "8", "*", "(", "-6", "+", "5", ")", ")" };
            Assert.AreEqual(18, calculator.Calculate(input));
        }

        /// <summary>
        /// Check function Calculate, shall return 6,2
        /// </summary>
        [TestMethod]
        public void CalculateExpression()
        {
            List<string> input = new List<string>() { "(", "9", "-", "6", "*", "8,2", ")", "-", "3", "*", "-8,8", "+", "20" };
            Assert.AreEqual(6.2, calculator.Calculate(input), 0.01);
        }

        /// <summary>
        /// Check two different expressions one by one
        /// </summary>
        [TestMethod]
        public void CheckTwoExpressionsOneByOne()
        {
            List<string> input = new List<string>() { "9", "+", "3", "*", "(", "6", "-", "5", ")", "/", "3", "-",
                                                      "(", "8", "*", "(", "-6", "+", "5", ")", ")" };
            Assert.AreEqual(18, calculator.Calculate(input));

            input = new List<string>() { "(", "9", "-", "6", "*", "8,2", ")", "-", "3", "*", "-8,8", "+", "20" };
            Assert.AreEqual(6.2, calculator.Calculate(input), 0.01);
        }
        
        /// <summary>
        /// Just one more expression, shall return 99
        /// </summary>
        [TestMethod]
        public void OneMoreExpressionShallReturn99()
        {
            List<string> input = new List<string>() { "6", "-", "(", "3", "*", "7", "-", "28,5", "*", "(", "3", "-",
                                                      "1", "*", "(", "7", "-", "8", ")", ")", ")" };
            Assert.AreEqual(99, calculator.Calculate(input));
        }

        /// <summary>
        /// Check whether priorities work right, shall return 3
        /// </summary>
        [TestMethod]
        public void RightWorkOfPrioritiesShallReturn3()
        {
            List<string> input = new List<string>() { "3", "-", "8", "*", "5", "/", "2", "+", "10", "*", "2" };
            Assert.AreEqual(3, calculator.Calculate(input));
        }
    }
}
