using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParseTree.Tests
{
    [TestClass]
    public class ParseTreeTest
    {
        /// <summary>
        /// Count expression, shall return 4
        /// </summary>
        [TestMethod]
        public void SomeTestsShallReturn4()
        {
            string input = "(* (+ 1 1) 2)";
            Tree tree1 = new Tree();
            tree1.FillTree(input.Split());
            Assert.AreEqual(4, tree1.CalculateAnswer());
        }

        /// <summary>
        /// Count expression, shall return -17
        /// </summary>
        [TestMethod]
        public void SomeTestsShallReturnMinus17()
        {
            string input = "(+ (/ (* (- 56 35) -8) 7) (+ (* -9 4) 43))";
            Tree tree2 = new Tree();
            tree2.FillTree(input.Split());
            Assert.AreEqual(-17, tree2.CalculateAnswer());
        }

        /// <summary>
        /// Count expression, shall return 3
        /// </summary>
        [TestMethod]
        public void SomeTestsShallReturnd3()
        {
            string input = "(/ 17 5)";
            Tree tree3 = new Tree();
            tree3.FillTree(input.Split());
            Assert.AreEqual(3, tree3.CalculateAnswer());
        }

        /// <summary>
        /// Count expression, shall return 9
        /// </summary>
        [TestMethod]
        public void SomeTestsShallReturnd9()
        {
            string input = "(/ (+ (/ (+ (- 3 (* 9 5)) 6) 6) 33) 3)";
            Tree tree3 = new Tree();
            tree3.FillTree(input.Split());
            Assert.AreEqual(9, tree3.CalculateAnswer());
        }

        /// <summary>
        /// Enter expression without gaps; expected exception WrongInputException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(WrongInputException))]
        public void EnterExpressionWithoutGapsExpectedException()
        {
            string input = "(*(+11)2)";
            Tree tree3 = new Tree();
            tree3.FillTree(input.Split());
            Assert.AreEqual(3, tree3.CalculateAnswer());
        }

        /// <summary>
        /// Enter expression with letters; expected WrongInputException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(WrongInputException))]
        public void EnterExpressionWithLettersExpectedWrongInputException()
        {
            string input = "(/ 17 a)";
            Tree tree3 = new Tree();
            tree3.FillTree(input.Split());
        }
    }
}
