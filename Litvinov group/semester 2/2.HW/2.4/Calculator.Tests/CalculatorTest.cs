using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StackCalculator
{
    /// <summary>
    /// Tests for class Calculator
    /// </summary>
    [TestClass]
    public class CalculatorTests
    {
        private Calculator calculatorArray;
        private Calculator calculator;
        private string input;

        /// <summary>
        /// Initialize calculator
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            calculatorArray = new Calculator(new ArrayStack());
            calculator = new Calculator(new Stack());
        }

        /// <summary>
        /// Count some expressions; shall return 9, -17 and 3
        /// Try each type of stack
        /// </summary>
        [TestMethod]
        public void CountExpressionShallReturn9AndMinus17And3()
        {
            input = "3 5 + 9 * 7 9 * -";
            calculator.Calculate(input.Split());
            Assert.AreEqual(9, calculator.PrintAnswer());
            calculatorArray.Calculate(input.Split());
            Assert.AreEqual(9, calculatorArray.PrintAnswer());

            input = "56 35 - -8 * 7 / 43 -9 4 * + +";
            calculator.Calculate(input.Split());
            Assert.AreEqual(-17, calculator.PrintAnswer());
            calculatorArray.Calculate(input.Split());
            Assert.AreEqual(-17, calculatorArray.PrintAnswer());

            input = "17 5 /";
            calculator.Calculate(input.Split());
            Assert.AreEqual(3, calculator.PrintAnswer());
            calculatorArray.Calculate(input.Split());
            Assert.AreEqual(3, calculatorArray.PrintAnswer());
        }

        /// <summary>
        /// Try different wrong inputs
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(WrongInputStringException))]
        public void WrongInput1()
        {
            input = "2 +";
            calculator.Calculate(input.Split());
            calculatorArray.Calculate(input.Split());
        }

        /// <summary>
        /// Try different wrong inputs
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(WrongInputStringException))]
        public void WrongInput2()
        {
            input = "+";
            calculator.Calculate(input.Split());
            calculatorArray.Calculate(input.Split());
        }

        /// <summary>
        /// Try different wrong inputs
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(WrongInputStringException))]
        public void WrongInput3()
        {
            input = "a b";
            calculator.Calculate(input.Split());
            calculatorArray.Calculate(input.Split());
        }

        /// <summary>
        /// Try different wrong inputs
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(WrongInputStringException))]
        public void WrongInput4()
        {
            input = "1 d +";
            calculator.Calculate(input.Split());
            calculatorArray.Calculate(input.Split());
        }
    }
}
