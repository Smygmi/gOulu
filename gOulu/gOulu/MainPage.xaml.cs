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
            categoryTitle.Text = "Featured";
        }

        private void button_categoryMusic_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(ContentPage), "musiikki");
            categoryTitle.Text = "Musiikki";
        }

        private void button_categoryTheater_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(ContentPage), "teatteri");
            categoryTitle.Text = "Teatteri";
        }

        private void button_categorySport_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(ContentPage), "urheilu");
            categoryTitle.Text = "Urheilu";
        }

        private void button_categorySpiritual_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(ContentPage), "hengelliset");
            categoryTitle.Text = "Hengelliset";
        }

        private void button_categoryPolitical_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(ContentPage), "poliittiset");
            categoryTitle.Text = "Poliittiset";
        }

        private void button_categoryNightlife_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(ContentPage), "yoelama");
            categoryTitle.Text = "Yöelämä";
        }
        private void button_categoryEducation_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(ContentPage), "koulutus");
            categoryTitle.Text = "Koulutus";
        }

        private void button_categoryHobbies_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(ContentPage), "harrastajat");
            categoryTitle.Text = "Harrastajat";
        }

        private void button_categoryExpo_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(ContentPage), "messut");
            categoryTitle.Text = "Messut";
        }

        private void button_categoryOthers_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(ContentPage), "muut");
            categoryTitle.Text = "Muut";
        }

        private void logo_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(ContentPage), "featured");
            categoryTitle.Text = "Featured";
        }
    }
}
