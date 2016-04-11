using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Text;

namespace gOulu
{
    public sealed partial class MainPage : Page
    {

        public string category;

        public static StackPanel content;
        

        public MainPage()
        {
            this.InitializeComponent();

            content = this.ContentPanel;
            //ContentFrame.Navigate(typeof(ContentPage), "");
        }

        void OpenInBrowser()
        {
            //Add browser redirection code here
        }

        private void button_categoryMusic_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(ContentPage), "musiikki");
            
        }

        private void button_categoryTheater_Click(object sender, RoutedEventArgs e)
        {
            //Tähän gettipyyntö

            //Set event info
            ContentFrame.Navigate(typeof(ContentPage), "teatteri");
            
        }

        private void button_categorySport_Click(object sender, RoutedEventArgs e)
        {
            //Tähän gettipyyntö

            //Set event info
            ContentFrame.Navigate(typeof(ContentPage), "urheilu");

        }
    }
}
