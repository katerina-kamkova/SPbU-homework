using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace GUIforFTP
{
    /// <summary>
    /// Class that describes reactions to different activity from MainWindow.xaml
    /// </summary>
    public class ViewModel
    {
        private int port;
        private Client client;
        private Object lockObject = new Object();

        /// <summary>
        /// Check wheather connection`s on
        /// </summary>
        private bool connectionOn;

        /// <summary>
        /// Make basic values to make the start quicker
        /// </summary>
        public ViewModel()
        {
            port = 4242;
            ServerAddress = "localHost";
            OriginalPath = "ServerExample/Directory";
            NewPath = "ServerExample/Dir";
            RootFolder = "ServerExample";
            CurrentFolder = null;
            connectionOn = false;
            List = new ObservableCollection<IListElement>();
            DownloadingList = new ObservableCollection<IListElement>();
        }

        /// <summary>
        /// Get/set port, convert string to int
        /// TO DO: WIndow in case of wrong input
        /// </summary>
        public string Port
        {
            get => Convert.ToString(port);
            set { port = Convert.ToInt32(value); }
        }

        /// <summary>
        /// Get/set serverAddress
        /// </summary>
        public string ServerAddress { get; set; }

        /// <summary>
        /// Get/set current folder
        /// </summary>
        public string RootFolder { get; set; }
        
        /// <summary>
        /// Get/set current folder
        /// </summary>
        public string CurrentFolder { get; set; }
        
        /// <summary>
        /// Get/set the path of the wanted file/folder
        /// </summary>
        public string OriginalPath { get; set; }

        /// <summary>
        /// Get/set new path for the file/folder to be downloaded
        /// </summary>
        public string NewPath { get; set; }

        /// <summary>
        /// Header for the list representing Folder system
        /// </summary>
        public string ListHeader
        {
            get
            {
                if (List.Count == 0)
                {
                    return "Folder manager";
                }

                if (CurrentFolder == null)
                {
                    return RootFolder;
                }

                return CurrentFolder;
            }
        }

        /// <summary>
        /// File List that sends elements to the MainWindow.xaml
        /// </summary>
        public ObservableCollection<IListElement> List { get; set; }

        /// <summary>
        /// File List that sends elements to the MainWindow.xaml
        /// </summary>
        public ObservableCollection<IListElement> DownloadingList { get; set; }

        /// <summary>
        /// Send when there`s something to be told to the user
        /// </summary>
        public event EventHandler<string> HaveMessage;
        
        /// <summary>
        /// Create new client, connect with server
        /// </summary>
        public void CreateClient()
        {
            client = new Client(port, ServerAddress);
            connectionOn = true;

            HaveMessage?.Invoke(this, "Server`s chosen");
        }

        /// <summary>
        /// Open root folder
        /// </summary>
        public async Task Open()
        {
            if (!connectionOn)
            {
                HaveMessage?.Invoke(this, "Port and server address are not chosen");
                return;
            }

            CurrentFolder = null;
            List.Clear();

            var check = new List<(string, bool)>();
            try
            {
                check = await client.List(RootFolder);
            }
            catch (SocketException)
            {
                HaveMessage?.Invoke(this, "Server`s not found");
                return;
            }

            if (check != null)
            {
                string name = null;
                for (var i = RootFolder.LastIndexOf("/") + 1; i < RootFolder.Length; i++)
                {
                    name += RootFolder[i];
                }

                List.Add(new ListElement(name, true));
                return;
            }

            HaveMessage?.Invoke(this, "Root folder isn`t found");
        }

        /// <summary>
        /// GEt list of inside files of the wanted folder
        /// Create new list in GUI
        /// </summary>
        /// <param name="chosenElement">The folder to be opened</param>
        public async Task TryOpenFolder(IListElement chosenElement)
        {
            if (!connectionOn)
            {
                HaveMessage?.Invoke(this, "Port and server address are not chosen");
                return;
            }
            
            if (chosenElement != null && chosenElement.Type)
            {
                if (CurrentFolder == null)
                {
                    CurrentFolder = RootFolder;
                }
                else
                {
                    CurrentFolder = CurrentFolder + "/" + chosenElement.Title;
                }

                var sourceList = new List<(string title, bool type)>();
                try
                {
                    sourceList = await client.List(CurrentFolder);
                }
                catch (SocketException)
                {
                    HaveMessage?.Invoke(this, "Server`s not found");
                    return;
                }

                if (sourceList != null)
                {
                    List.Clear();

                    foreach (var element in sourceList)
                    {
                        List.Add(new ListElement(element.title, element.type));
                    }
                }

                return;
            }
        }

        /// <summary>
        /// Go to the parent folder
        /// </summary>
        public async Task GoBack()
        {
            if (!connectionOn)
            {
                HaveMessage?.Invoke(this, "Port and server address are not chosen");
                return;
            }

            if (CurrentFolder == null)
            {
                HaveMessage?.Invoke(this, "It`s a root folder, you can`t go back");
                return;
            }

            if (!Equals(CurrentFolder, RootFolder))
            {
                string temp = null;
                for (var i = 0; i < CurrentFolder.LastIndexOf("/"); i++)
                {
                    temp += CurrentFolder[i];
                }

                CurrentFolder = null;
                for (var i = 0; i < temp.LastIndexOf("/"); i++)
                {
                    CurrentFolder += temp[i];
                }

                string name = null;
                for (var i = temp.LastIndexOf("/") + 1; i < temp.Length; i++)
                {
                    name += temp[i];
                }

                await TryOpenFolder(new ListElement(name, true));
            }
            else
            {
                await Open();
            }
        }

        /// <summary>
        /// Download wanted file/folder and save it in wanted place
        /// </summary>
        /// <param name="originalP">The source, null if first call of the func</param>
        /// <param name="newP">Place where save copy, null if first call of the func</param>
        public async Task Download(string originalP = null, string newP = null)
        {
            if (!connectionOn)
            {
                HaveMessage?.Invoke(this, "Port and server address are not chosen");
                return;
            }

            if (OriginalPath == "")
            {
                HaveMessage?.Invoke(this, "Set the original path");
                return;
            }

            if (NewPath == "")
            {
                HaveMessage?.Invoke(this, "Set the new path");
                return;
            }

            var wantedFile = false;
            DownloadListElement element = null;

            if (originalP == null)              // Если вызывается из интерфейса
            {
                originalP = OriginalPath;
                newP = NewPath;

                lock (lockObject)
                {
                    element = new DownloadListElement(originalP);
                    DownloadingList.Add(element);
                }
                wantedFile = true;
            }

            var clientList = new List<(string title, bool type)>();
            try
            {
                clientList = await client.List(originalP);
            }
            catch (SocketException)
            {
                HaveMessage?.Invoke(this, "Server`s not found");
                return;
            }

            if (clientList == null)
            {
                try
                {
                    await Task.Run(async () => await client.Get(originalP, newP));
                }
                catch (SocketException)
                {
                    HaveMessage?.Invoke(this, "Server`s not found");
                    return;
                }

                if (wantedFile)
                {
                    lock (lockObject)
                    {
                        var index = DownloadingList.IndexOf(element);
                        element.Type = true;
                        DownloadingList.RemoveAt(index);
                        DownloadingList.Insert(index, element);
                    }
                }
                return;
            }

            Directory.CreateDirectory(newP);
            foreach (var el in clientList)
            {
                try
                {
                    await Task.Run(async () => await Download(originalP + "/" + el.title, newP + "/" + el.title));
                }
                catch (SocketException)
                {
                    HaveMessage?.Invoke(this, "Server`s not found");
                    return;
                }
            }

            lock (lockObject)
            {
                if (wantedFile)
                {
                    var index = DownloadingList.IndexOf(element);
                    element.Type = true;
                    DownloadingList.RemoveAt(index);
                    DownloadingList.Insert(index, element);
                }
            }
        }

        /// <summary>
        /// The element for the list;
        /// Keeps title, bool, adjusts proper picture
        /// </summary>
        private class ListElement : IListElement
        {
            private string title;
            private bool type;
            private string imagePath;

            public ListElement(string title, bool type)
            {
                this.title = title;
                this.type = type;

                imagePath = type ? "Pictures/Folder.png" : "Pictures/File.png";
            }

            public string Title => title;
            public bool Type { get => type; set => type = value; }
            public string ImagePath { get => imagePath; }
        }

        /// <summary>
        /// Class for Downloaded list
        /// </summary>
        private class DownloadListElement : IListElement
        {
            private string title;
            private bool type;
            private string imagePath;

            public DownloadListElement(string title)
            {
                this.title = title;
                type = false;
                imagePath = "Pictures/Downloading.png";
            }

            public string Title { get => title; }
            public bool Type
            {
                get => type;
                set
                {
                    type = value;
                    if (value)
                    {
                        imagePath = "Pictures/Downloaded.png";
                    }
                    else
                    {
                        imagePath = "Pictures/Downloading.png";
                    }
                }
            }
            public string ImagePath { get => imagePath; }
        }
    }
}
