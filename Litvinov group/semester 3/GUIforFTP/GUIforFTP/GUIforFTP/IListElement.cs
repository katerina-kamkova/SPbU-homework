namespace GUIforFTP
{
    /// <summary>
    /// Interface for list elements, can return title, type and image of the element
    /// </summary>
    public interface IListElement
    {
        /// <summary>
        /// Returns the name of the file/folder
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Returns the type of the element: true - folder, false - file
        /// </summary>
        bool Type { get; set; }

        /// <summary>
        /// Returns the path to the image
        /// </summary>
        string ImagePath { get; }
    }
}
