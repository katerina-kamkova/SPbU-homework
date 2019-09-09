using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUIforFTP
{
    /// <summary>
    /// Copy of the class ListElement in GUIforFTP/ViewModel.cs;
    /// Done only for tests
    /// </summary>
    public class ListElement : IListElement
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

        public string Title { get => title; }
        public bool Type { get => type; set => type = value; }
        public string ImagePath { get => imagePath; }
    }
}
