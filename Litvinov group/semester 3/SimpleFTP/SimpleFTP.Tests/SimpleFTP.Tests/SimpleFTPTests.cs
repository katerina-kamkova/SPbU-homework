using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleFTP
{
    [TestClass]
    public class ServerTests
    {
        private const int port = 4242;
        private Server server;
        private Client client;
        private List<(string, bool)> list;

        [TestInitialize]
        public void Initialize()
        {
            server = new Server(port);
            client = new Client(port);
        }

        [TestMethod]
        public void ServerCanBeClosed()
        {
            Task.Run(async () => await server.ServerWork());

            Thread.Sleep(3000);
            server.Stop();
        }

        [TestMethod]
        public void ListExistingDirectory()
        {
            Task.Run(async () => await server.ServerWork());

            list = client.List("Example").Result;
            Assert.AreEqual(4, list.Count);
            Assert.AreEqual(("Directory", true), list[0]);
            Assert.AreEqual(("group_theory.pdf", false), list[1]);
            Assert.AreEqual(("Геометрическая прогрессия.docx", false), list[2]);
            Assert.AreEqual(("Дом, в котором....jpg", false), list[3]);

            server.Stop();
        }

        [TestMethod]
        public void ListNonexistentDirectory()
        {
            Task.Run(async () => await server.ServerWork());

            list = client.List("Example/group_theory.pdf").Result;
            Assert.IsNull(list);

            server.Stop();
        }

        [TestMethod]
        public void GetExistingFile()
        {
            Task.Run(async () => await server.ServerWork());

            client.Get("Example/Геометрическая прогрессия.docx", "Example/Directory/NewFile.docx").Wait();
            Assert.AreEqual(true, File.Exists("Example/Directory/NewFile.docx"));
            File.Delete("Example/Directory/NewFile.docx");

            server.Stop();
        }

        [TestMethod]
        public void GetNonexistentFile()
        {
            Task.Run(async () => await server.ServerWork());

            client.Get("Example/AAA.docx", "Example/Directory/NewNewFile.docx").Wait();
            Assert.AreEqual(false, File.Exists("Example/Directory/NewNewFile.docx"));

            server.Stop();
        }

        [TestMethod]
        public void ServerClosedButClientDoesNotThrowException()
        {
            list = client.List("Example/group_theory.pdf").Result;
        }
    }
}
