using System.Windows;
using System.Windows.Controls;

namespace GUIforFTP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Start the process, bind with model and list
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            var model = new ViewModel();
            DataContext = model;

            model.HaveMessage += ShowMessage;
        }
        
        /// <summary>
        /// Reaction to the click on Choose button, 
        /// starts the connection
        /// </summary>
        private void ChooseClick(object sender, RoutedEventArgs e)
        {
            (DataContext as ViewModel).CreateClient();
        }

        /// <summary>
        /// Reaction to the click on Open button, 
        /// sets root directory
        /// </summary>
        private async void OpenClick(object sender, RoutedEventArgs e)
        {
            await (DataContext as ViewModel).Open();
        }

        /// <summary>
        /// Reaction to the choosing one of list elements
        /// opens inside list of the chosen folder
        /// </summary>
        private async void ElementIsChosen(object sender, SelectionChangedEventArgs e)
        {
            await (DataContext as ViewModel).TryOpenFolder((IListElement)listBox.SelectedItem);
        }

        /// <summary>
        /// Reaction to the click on Back,
        /// opens parent folder
        /// </summary>
        private async void BackClick(object sender, RoutedEventArgs e)
        {
            await (DataContext as ViewModel).GoBack();
        }

        /// <summary>
        /// Reaction to the click on Download button,
        /// downloads wanted file to the wanted place
        /// </summary>
        private async void DownloadClick(object sender, RoutedEventArgs e)
        {
            await (DataContext as ViewModel).Download();
        }

        /// <summary>
        /// Show the message in extra window
        /// </summary>
        /// <param name="message">The message</param>
        private void ShowMessage(object sender, string message)
        {
            MessageBox.Show(message);
        }
    }
}
