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
        public static StackPanel content;
        

        public MainPage()
        {
            this.InitializeComponent();

            content = this.ContentPanel;
            ContentFrame.Navigate(typeof(ContentPage), "featured");
        }

        private void button_categoryMusic_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(ContentPage), "musiikki");    
        }

        private void button_categoryTheater_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(ContentPage), "teatteri");
        }

        private void button_categorySport_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(ContentPage), "urheilu");
        }

        private void button_categorySpiritual_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(ContentPage), "hengelliset");
        }

        private void button_categoryPolitical_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(ContentPage), "poliittiset");
        }

        private void button_categoryNightlife_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(ContentPage), "yoelama");
        }
        private void button_categoryEducation_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(ContentPage), "koulutus");
        }

        private void button_categoryHobbies_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(ContentPage), "harrastajat");
        }

        private void button_categoryExpo_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(ContentPage), "messut");
        }

        private void button_categoryOthers_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(ContentPage), "muut");
        }

        private void logo_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(ContentPage), "featured");
        }
    }
}
