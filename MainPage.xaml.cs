using Microsoft.Maui.Controls;

namespace LTXTools
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            MainContent.Content = new Views.FileCopyPage();
        }

        private void btnFileCopy_Clicked(object sender, EventArgs e) => MainContent.Content = new Views.FileCopyPage();
        private void btnActivate_Clicked(object sender, EventArgs e) => MainContent.Content = new Views.ActivatePage();
        private void btnProcess_Clicked(object sender, EventArgs e) => MainContent.Content = new Views.ProcessPage();
        private void btnProcessGUI_Clicked(object sender, EventArgs e) => MainContent.Content = new Views.ProcessGUIPage();
        private void btnFormat_Clicked(object sender, EventArgs e) => MainContent.Content = new Views.FormatPage();
        private void btnShutdown_Clicked(object sender, EventArgs e) => MainContent.Content = new Views.ShutdownPage();
    }
}
