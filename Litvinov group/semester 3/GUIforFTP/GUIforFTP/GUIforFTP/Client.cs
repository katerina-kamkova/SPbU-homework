using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace GUIforFTP
{
    /// <summary>
    /// Client
    /// Can ask for a name list of files inside given directory
    /// Can download wanted file
    /// </summary>
    public class Client
    {
        private readonly int port;
        private readonly string server;

        /// <summary>
        /// Get the port number and the name of the server
        /// </summary>
        /// <param name="port">The number of the port</param>
        /// <param name="server">Server`s name</param>
        public Client(int port, string server = "localHost")
        {
            this.port = port;
            this.server = server;
        }

        /// <summary>
        /// Asks and gets the name list of inside files of given directory
        /// </summary>
        /// <param name="path">Path to the given directory</param>
        /// <returns>List filled with names and types</returns>
        public async Task<List<(string title, bool type)>> List(string path)
        {
            var client = new TcpClient();
            await client.ConnectAsync(server, port);
     
            using (var stream = client.GetStream())
            {
                var writer = new StreamWriter(stream) { AutoFlush = true };

                await writer.WriteLineAsync("1");
                await writer.WriteLineAsync(path);

                var reader = new StreamReader(stream);
                var fileDirAmount = Convert.ToInt32(await reader.ReadLineAsync());

                var list = new List<(string name, bool isDirectory)>();
                if (fileDirAmount == -1)
                {
                    Console.WriteLine("Such directory doesn`t exist");
                    return null;
                }

                for (var i = 0; i < fileDirAmount; i++)
                {
                    var name = await reader.ReadLineAsync();
                    var isDir = Convert.ToBoolean(await reader.ReadLineAsync());

                    list.Add((name, isDir));
                }

                return list;
            }
        }

        /// <summary>
        /// Ask for the wanted file, get it and create on given new path
        /// </summary>
        /// <param name="path">Path to the wanted file</param>
        /// <param name="newPath">Path for the copy to be created</param>
        public async Task Get(string path, string newPath)
        {
            var client = new TcpClient();
            await client.ConnectAsync(server, port);

            using (var stream = client.GetStream())
            {
                var writer = new StreamWriter(stream) { AutoFlush = true };

                await writer.WriteLineAsync("2");
                await writer.WriteLineAsync(path);

                var reader = new StreamReader(stream);
                var fileSize = long.Parse(await reader.ReadLineAsync());
                if (fileSize == -1)
                {
                    Console.WriteLine("Such file doesn`t exist");
                    return;
                }

                var fileResult = new FileStream(newPath, FileMode.Create);
                await reader.BaseStream.CopyToAsync(fileResult);

                fileResult.Flush();
                fileResult.Close();
                return;
            }
        }
    }
}
