using System.IO;
using System.Linq;

namespace MyNUnit
{
    /// <summary>
    /// Gets all pathes to assemblies on given path
    /// </summary>
    public static class AssemblyGetter
    {
        /// <summary>
        /// Get array of assemblies` paths in given folder/file 
        /// else throw WrongPathException
        /// </summary>
        /// <param name="path">given path</param>
        /// <returns>array of assemblies` paths</returns>
        public static string[] GetPath(string path)
        {
            if (Directory.Exists(path))
            {
                return Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories)
                    .Concat(Directory.GetFiles(path, "*.exe", SearchOption.AllDirectories)).ToArray();
            }

            var extension = new FileInfo(path).Extension;
            if (extension == ".dll" || extension == ".exe")
            {
                return new string[1] { path };
            }

            return new string[0];
        }
    }
}