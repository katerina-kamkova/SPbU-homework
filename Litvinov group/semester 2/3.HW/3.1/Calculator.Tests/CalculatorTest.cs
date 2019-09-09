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
        /// Count some expressions; shall return 9 (Stack)
        /// Try each type of stack
        /// </summary>
        [TestMethod]
        public void CountExpressionShallReturn9()
        {
            input = "3 5 + 9 * 7 9 * -";
            calculator.Calculate(input.Split());
            Assert.AreEqual(9, calculator.PrintAnswer());
        }

        /// <summary>
        /// Count some expressions; shall return -17 (Stack)
        /// Try each type of stack
        /// </summary>
        [TestMethod]
        public void CountExpressionShallReturnMinus17()
        {
            input = "56 35 - -8 * 7 / 43 -9 4 * + +";
            calculator.Calculate(input.Split());
            Assert.AreEqual(-17, calculator.PrintAnswer());
        }

        /// <summary>
        /// Count some expressions; shall return 3 (Stack)
        /// Try each type of stack
        /// </summary>
        [TestMethod]
        public void CountExpressionShallReturn3()
        {
            input = "17 5 /";
            calculator.Calculate(input.Split());
            Assert.AreEqual(3, calculator.PrintAnswer());
        }

        /// <summary>
        /// Try different wrong inputs (Stack)
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(WrongInputStringException))]
        public void WrongInput1()
        {
            input = "2 +";
            calculator.Calculate(input.Split());
        }

        /// <summary>
        /// Try different wrong inputs (Stack)
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(WrongInputStringException))]
        public void WrongInput2()
        {
            input = "+";
            calculator.Calculate(input.Split());
        }

        /// <summary>
        /// Try different wrong inputs (Stack)
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(WrongInputStringException))]
        public void WrongInput3()
        {
            input = "a b";
            calculator.Calculate(input.Split());
        }

        /// <summary>
        /// Try different wrong inputs (Stack)
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(WrongInputStringException))]
        public void WrongInput4()
        {
            input = "1 d +";
            calculator.Calculate(input.Split());
        }

        /// <summary>
        /// Count some expressions; shall return 9 (ArrayStack)
        /// Try each type of stack
        /// </summary>
        [TestMethod]
        public void CountExpressionShallReturn9AArrayStack()
        {
            input = "3 5 + 9 * 7 9 * -";
            calculatorArray.Calculate(input.Split());
            Assert.AreEqual(9, calculatorArray.PrintAnswer());
        }

        /// <summary>
        /// Count some expressions; shall return -17 (ArrayStack)
        /// Try each type of stack
        /// </summary>
        [TestMethod]
        public void CountExpressionShallReturnMinus17ArrayStack()
        {
            input = "56 35 - -8 * 7 / 43 -9 4 * + +";
            calculatorArray.Calculate(input.Split());
            Assert.AreEqual(-17, calculatorArray.PrintAnswer());
        }

        /// <summary>
        /// Count some expressions; shall return 3 (ArrayStack)
        /// Try each type of stack
        /// </summary>
        [TestMethod]
        public void CountExpressionShallReturn3ArrayStack()
        {
            input = "17 5 /";
            calculatorArray.Calculate(input.Split());
            Assert.AreEqual(3, calculatorArray.PrintAnswer());
        }

        /// <summary>
        /// Try different wrong inputs (ArrayStack)
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(WrongInputStringException))]
        public void WrongInput1ArrayStack()
        {
            input = "2 +";
            calculatorArray.Calculate(input.Split());
        }

        /// <summary>
        /// Try different wrong inputs (ArrayStack)
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(WrongInputStringException))]
        public void WrongInput2ArrayStack()
        {
            input = "+";
            calculatorArray.Calculate(input.Split());
        }

        /// <summary>
        /// Try different wrong inputs (ArrayStack)
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(WrongInputStringException))]
        public void WrongInput3ArrayStack()
        {
            input = "a b";
            calculatorArray.Calculate(input.Split());
        }

        /// <summary>
        /// Try different wrong inputs (ArrayStack)
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(WrongInputStringException))]
        public void WrongInput4ArrayStack()
        {
            input = "1 d +";
            calculatorArray.Calculate(input.Split());
        }
    }
}
