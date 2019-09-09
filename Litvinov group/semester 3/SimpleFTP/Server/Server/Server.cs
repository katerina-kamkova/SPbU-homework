using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleFTP
{
    /// <summary>
    /// Server
    /// Can do List and Get functions
    /// </summary>
    public class Server
    {
        private readonly int port;
        private TcpListener listener;
        private CancellationTokenSource cts = new CancellationTokenSource();
        private Object lockObject = new Object();
        private int tasksInProcess;
        private AutoResetEvent tryStopListener = new AutoResetEvent(false);

        /// <summary>
        /// Get the port number, create listener
        /// </summary>
        /// <param name="port">Port number</param>
        public Server(int port)
        {
            this.port = port;

            listener = new TcpListener(IPAddress.Any, port);
        }

        /// <summary>
        /// Starts the listener and
        /// starts the Reader, so it can get and fulfil user`s requests
        /// </summary>
        public async Task ServerWork()
        {
            listener.Start();

            while (!cts.Token.IsCancellationRequested)
            {
                ThreadPool.QueueUserWorkItem(Reader, await listener.AcceptTcpClientAsync());
            }
        }

        /// <summary>
        /// Get client`s request, starts needed method
        /// </summary>
        /// <param name="client"></param>
        private async void Reader(Object client)
        {
            try
            {
                lock (lockObject)
                {
                    tasksInProcess++;
                }

                using (var stream = (client as TcpClient).GetStream())
                {
                    var reader = new StreamReader(stream);

                    var requestNumber = await reader.ReadLineAsync();
                    var path = await reader.ReadLineAsync();

                    var writer = new StreamWriter(stream) { AutoFlush = true };
                    switch (requestNumber)
                    {
                        case "1":
                            {
                                await List(path, writer);
                                break;
                            }
                        case "2":
                            {
                                await Get(path, writer);
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Wrong request, try again");
                                break;
                            }
                    }

                    (client as TcpClient).Close();
                }

                lock (lockObject)
                {
                   tasksInProcess--;
                }
                tryStopListener.Set();
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                (client as TcpClient).Close();

                lock (lockObject)
                {
                    tasksInProcess--;
                }
                tryStopListener.Set();
            }
        }

        /// <summary>
        /// Send to the client amount of the files inside given directory
        /// </summary>
        /// <param name="path">The path to wanted directory</param>
        /// <param name="writer">Allows to write to client</param>
        private async Task List(string path, StreamWriter writer)
        {
            if (!Directory.Exists(path))
            {
                await writer.WriteLineAsync("-1");
                return;
            }

            var directory = new DirectoryInfo(path);
            var dirList = directory.GetDirectories();
            var fileList = directory.GetFiles();

            await writer.WriteLineAsync($"{dirList.Length + fileList.Length}");

            foreach (var dir in dirList)
            {
                await writer.WriteLineAsync(dir.Name);
                await writer.WriteLineAsync("true");
            }

            foreach (var file in fileList)
            {
                await writer.WriteLineAsync(file.Name);
                await writer.WriteLineAsync("false");
            }
        }

        /// <summary>
        /// Send length of the wanted file and than send wanted file
        /// </summary>
        /// <param name="path">Path to the wanted file</param>
        /// <param name="writer">Allows to write to client</param>
        private async Task Get(string path, StreamWriter writer)
        {
            if (!File.Exists(path))
            {
                await writer.WriteLineAsync("-1");
                return;
            }

            await writer.WriteLineAsync($"{(new FileInfo(path)).Length}");

            using (FileStream file = File.OpenRead(path))
            {
                await file.CopyToAsync(writer.BaseStream);
            }
        }

        /// <summary>
        /// Stop the server
        /// </summary>
        public void Stop()
        {
            cts.Cancel();

            while (true)
            {
                lock (lockObject)
                {
                    if (tasksInProcess == 0)
                    {
                        break;
                    }
                }
                tryStopListener.WaitOne();
            }

            listener.Stop();
        }
    }
}
