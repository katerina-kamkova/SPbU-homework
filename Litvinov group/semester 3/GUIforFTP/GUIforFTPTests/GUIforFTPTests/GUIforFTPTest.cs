using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GUIforFTP
{
    [TestClass]
    public class GUITests
    {
        private int port = 4242;
        private Server server;
        private ViewModel model;

        [TestInitialize]
        public void Initialize()
        {
            server = new Server(port);
            model = new ViewModel();
            Task.Run(async () => await server.ServerWork());
            model.CreateClient();
            model.Open().Wait();
        }

        [TestMethod]
        public void CheckOpen()
        {
            Assert.IsNull(model.CurrentFolder);
            Assert.AreEqual("ServerExample", model.List[0].Title);
            Assert.IsTrue(model.List[0].Type);
            Assert.AreEqual(1, model.List.Count);

            server.Stop();
        }

        [TestMethod]
        public void CheckTryOpen()
        {
            model.TryOpenFolder(new ListElement("ServerExample", true)).Wait();

            Assert.AreEqual("ServerExample", model.CurrentFolder);
            Assert.AreEqual(4, model.List.Count);

            Assert.AreEqual("Directory", model.List[0].Title);
            Assert.AreEqual("Дом, в котором", model.List[1].Title);
            Assert.AreEqual("group_theory.pdf", model.List[2].Title);
            Assert.AreEqual("Геометрическая прогрессия.docx", model.List[3].Title);

            Assert.IsTrue(model.List[0].Type);
            Assert.IsTrue(model.List[1].Type);
            Assert.IsFalse(model.List[2].Type);
            Assert.IsFalse(model.List[3].Type);

            server.Stop();
        }

        [TestMethod]
        public void CheckBack()
        {
            model.TryOpenFolder(new ListElement("ServerExample", true)).Wait();
            model.TryOpenFolder(new ListElement("Directory", true)).Wait();
            model.GoBack().Wait();

            Assert.AreEqual("ServerExample", model.CurrentFolder);
            Assert.AreEqual(4, model.List.Count);

            Assert.AreEqual("Directory", model.List[0].Title);
            Assert.AreEqual("Дом, в котором", model.List[1].Title);
            Assert.AreEqual("group_theory.pdf", model.List[2].Title);
            Assert.AreEqual("Геометрическая прогрессия.docx", model.List[3].Title);

            Assert.IsTrue(model.List[0].Type);
            Assert.IsTrue(model.List[1].Type);
            Assert.IsFalse(model.List[2].Type);
            Assert.IsFalse(model.List[3].Type);

            server.Stop();
        }

        /// <summary>
        /// Check download method
        /// </summary>
        [TestMethod]
        public void Download()
        {
            model.RootFolder = "ServerExample";
            model.Open().Wait();
            model.OriginalPath = "ServerExample/Directory/_6P6n3qdjOk.jpg";
            model.NewPath = "ServerExample/picture.jpg";
            model.Download().Wait();
            Assert.IsTrue(File.Exists("ServerExample/picture.jpg"));
            File.Delete("ServerExample/picture.jpg");

            server.Stop();
        }
    }
}
