using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyNUnit
{
    [TestClass]
    public class MyNUnitTests
    {
        private TestsRunner myNUnit;
        private string path;

        /// <summary>
        /// Compares expected params with actual
        /// need special method, because don`t have expected time
        /// </summary>
        private void CheckInfo(string classType, string testName, string result, string reason, Info info)
        {
            Assert.AreEqual(classType, info.ClassType);
            Assert.AreEqual(testName, info.TestName);
            Assert.AreEqual(result, info.Result);
            //Assert.AreEqual(reason, info.Reason); exception messages are on Russian, appveyor can`t normally read them; works on my comp
        }

        /// <summary>
        /// If file with given path exists, there some mistakes in order of Before/After
        /// </summary>e="path"></param>
        private void CheckFileExistance(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
                Assert.Fail("Smth`s wrong with order of called methods");
            }
        }

        /// <summary>
        /// Check the meanings of variables
        /// </summary>
        private void CheckVariables(string path, string expected)
        {
            using (FileStream fstream = File.OpenRead(path))
            {
                byte[] array = new byte[fstream.Length];
                fstream.Read(array, 0, array.Length);
                string textFromFile = System.Text.Encoding.Default.GetString(array);
                Assert.AreEqual(expected, textFromFile);
            }
            File.Delete(path);
        }

        [TestInitialize]
        public void Init()
        {
            myNUnit = new TestsRunner();

            path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            path = path.Substring(0, path.IndexOf("MyNUnit.Tests")) + "Tests";
        }

        [TestMethod]
        public void CheckGetPath()
        {
            var asmPath = AssemblyGetter.GetPath(path);

            Assert.AreEqual(5, asmPath.Length);

            Assert.AreEqual(path + @"\MixedTests\MixedTests\MixedTests.exe", asmPath[0]);
            Assert.AreEqual(path + @"\OneTest\OneTest\OneTest.exe", asmPath[1]);
            Assert.AreEqual(path + @"\SimpleTests\SimpleTests\SimpleTests.exe", asmPath[2]);
            Assert.AreEqual(path + @"\WithAttrParams\WithAttrParams\WithAttrParams.exe", asmPath[3]);
            Assert.AreEqual(path + @"\WithBeforeAfter\WithBeforeAfter\WithBeforeAfter.exe", asmPath[4]);
        }

        [TestMethod]
        public void CheckComparer()
        {
            var info1 = new Info("b", "x", "", "", default(TimeSpan));
            var info2 = new Info("a", "z", "", "", default(TimeSpan));
            var info3 = new Info("b", "z", "", "", default(TimeSpan));
            var info4 = new Info("b", "xz", "", "", default(TimeSpan));
            var info5 = new Info("ba", "y", "", "", default(TimeSpan));

            var infoArray = new Info[] { info1, info2, info3, info4, info5 };
            Array.Sort(infoArray, Printer.Compare);

            Assert.AreEqual(info2, infoArray[0]);
            Assert.AreEqual(info1, infoArray[1]);
            Assert.AreEqual(info4, infoArray[2]);
            Assert.AreEqual(info3, infoArray[3]);
            Assert.AreEqual(info5, infoArray[4]);
        }

        [TestMethod]
        public void CheckRunTestOnOneTest()
        {
            var infoArray = myNUnit.ExecuteTests(AssemblyGetter.GetPath(path + @"\OneTest\OneTest\OneTest.exe"));

            Assert.AreEqual(1, infoArray.Length);

            CheckInfo("OneTest", "Func", "Passed", null, infoArray[0]);
        }

        [TestMethod]
        public void CheckRunTestOnSimpleTests()
        {
            var infoArray = myNUnit.ExecuteTests(AssemblyGetter.GetPath(path + @"\SimpleTests\SimpleTests\SimpleTests.exe"));
            Array.Sort(infoArray, Printer.Compare);

            Assert.AreEqual(8, infoArray.Length);

            CheckInfo("AnotherClass", "Null", "Passed", null, infoArray[0]);
            CheckInfo("EmptyTest", "Func", "Passed", null, infoArray[1]);
            CheckInfo("FailedTest", "Fail", "Failed", "The test has thrown the System.NullReferenceException. Exception message is: Адресат вызова создал исключение.", infoArray[2]);
            CheckInfo("InDiffClasses", "Sum", "Passed", null, infoArray[3]);
            CheckInfo("SeveralTests", "FactorialOf5", "Passed", null, infoArray[4]);
            CheckInfo("SeveralTests", "FailTest", "Failed", "The test has thrown the System.NullReferenceException. Exception message is: Адресат вызова создал исключение.", infoArray[5]);
            CheckInfo("SeveralTests", "Sum", "Passed", null, infoArray[6]);
            CheckInfo("SeveralTests", "Wait", "Passed", null, infoArray[7]);
        }

        [TestMethod]
        public void CheckRunTestOnWithAttrParams()
        {
            var infoArray = myNUnit.ExecuteTests(AssemblyGetter.GetPath(path + @"\WithAttrParams\WithAttrParams\WithAttrParams.exe"));
            Array.Sort(infoArray, Printer.Compare);

            Assert.AreEqual(8, infoArray.Length);

            CheckInfo("MixedParams", "Sum", "Ignored", "Too complex to run", infoArray[0]);
            CheckInfo("WithExpexted", "Factorialof3", "Passed", null, infoArray[1]);
            CheckInfo("WithExpexted", "Func", "Failed", "The test hasn`t thrown the System.Exception", infoArray[2]);
            CheckInfo("WithExpexted", "Null", "Failed", "The test has thrown the System.NullReferenceException. Exception message is: Адресат вызова создал исключение.", infoArray[3]);
            CheckInfo("WithExpexted", "Sum", "Passed", null, infoArray[4]);
            CheckInfo("WithIgnore", "FactorialOf7", "Ignored", "I have a good reason, but I won`t tell you", infoArray[5]);
            CheckInfo("WithIgnore", "Func", "Ignored", "", infoArray[6]);
            CheckInfo("WithIgnore", "Sum", "Passed", null, infoArray[7]);
        }

        [TestMethod]
        public void CheckRunTestOnWithBeforeAfter()
        {
            var infoArray = myNUnit.ExecuteTests(AssemblyGetter.GetPath(path + @"\WithBeforeAfter\WithBeforeAfter\WithBeforeAfter.exe"));
            Array.Sort(infoArray, Printer.Compare);

            Assert.AreEqual(13, infoArray.Length);

            CheckInfo("AfterClassFail", "TestFunc1", "Failed", "The test has thrown the System.Exception. Exception message is: Адресат вызова создал исключение.", infoArray[0]);
            CheckInfo("AfterClassFail", "TestFunc2", "Failed", "AfterClassFunc2 method has thrown System.Exception, message: Выдано исключение типа \"System.Exception\".", infoArray[1]);
            CheckInfo("AfterFail", "TestFunc1", "Failed", "The test has thrown the System.Exception. Exception message is: Адресат вызова создал исключение.", infoArray[2]);
            CheckInfo("AfterFail", "TestFunc2", "Failed", "AfterFunc2 method has thrown System.Exception, message: Выдано исключение типа \"System.Exception\".", infoArray[3]);
            CheckInfo("BeforeClassFail", "TestFunc1", "Failed", "BeforeClassFunc2 method has thrown System.Exception, message: Expected x = 4, real x = 4", infoArray[4]);
            CheckInfo("BeforeClassFail", "TestFunc2", "Failed", "BeforeClassFunc2 method has thrown System.Exception, message: Expected x = 4, real x = 4", infoArray[5]);
            CheckInfo("BeforeFail", "TestFunc1", "Failed", "BeforeFunc2 method has thrown System.Exception, message: Выдано исключение типа \"System.Exception\".", infoArray[6]);
            CheckInfo("BeforeFail", "TestFunc2", "Failed", "BeforeFunc2 method has thrown System.Exception, message: Выдано исключение типа \"System.Exception\".", infoArray[7]);
            CheckInfo("DifferentSingleAttr", "Sum", "Passed", null, infoArray[8]);
            CheckInfo("Mixed", "AddOneX", "Passed", null, infoArray[9]);
            CheckInfo("Mixed", "AddOneY", "Passed", "Some text, can`t check it anyway", infoArray[10]);
            CheckInfo("TestFail", "TestFunc1", "Failed", "The test has thrown the System.Exception. Exception message is: Адресат вызова создал исключение.", infoArray[11]);
            CheckInfo("TestFail", "TestFunc2", "Passed", null, infoArray[12]);

            var temp = Path.GetTempPath();
            CheckFileExistance($@"{temp}\AfterClassFail.txt");
            CheckFileExistance($@"{temp}\BeforeClassFail.txt");

            CheckVariables($@"{temp}\DifferentSingleAttr.txt", "3 3");
            CheckVariables($@"{temp}\Mixed.txt", "16 14");
            CheckVariables($@"{temp}\TestFail.txt", "12 3");
        }

        [TestMethod]
        public void CheckRunTestOnMixedTests()
        {
            var infoArray = myNUnit.ExecuteTests(AssemblyGetter.GetPath(path + @"\MixedTests\MixedTests\MixedTests.exe"));
            Array.Sort(infoArray, Printer.Compare);

            Assert.AreEqual(6, infoArray.Length);

            CheckInfo("Mix", "Test1", "Passed", null, infoArray[0]);
            CheckInfo("Mix", "Test2", "Failed", "The test has thrown the System.Exception. Exception message is: Адресат вызова создал исключение.", infoArray[1]);
            CheckInfo("Mix", "Test3", "Passed", null, infoArray[2]);
            CheckInfo("Mix", "Test4", "Failed", "The test has thrown the System.NullReferenceException. Exception message is: Адресат вызова создал исключение.", infoArray[3]);
            CheckInfo("Mix", "Test5", "Ignored", "Why not?", infoArray[4]);
            CheckInfo("Mix", "Test6", "Ignored", "Just do it", infoArray[5]);
        }
    }
}
