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
using Windows.UI.Xaml.Media.Imaging;

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
            //categoryTitle.Text = "Featured";
            TaustaKuva.Source = new BitmapImage(new Uri(this.BaseUri, "/Assets/Bgs/othersBG.png"));
        }

        private void button_categoryMusic_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(ContentPage), "musiikki");
            TaustaKuva.Source = new BitmapImage(new Uri(this.BaseUri, "/Assets/Bgs/musicBG.png"));
            //categoryTitle.Text = "Musiikki";
        }

        private void button_categoryTheater_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(ContentPage), "teatteri");
            //categoryTitle.Text = "Teatteri";
            TaustaKuva.Source = new BitmapImage(new Uri(this.BaseUri, "/Assets/Bgs/theaterBG.png"));
        }

        private void button_categorySport_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(ContentPage), "urheilu");
            TaustaKuva.Source = new BitmapImage(new Uri(this.BaseUri, "/Assets/Bgs/sportsBG.png"));
            //categoryTitle.Text = "Urheilu";
        }

        private void button_categorySpiritual_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(ContentPage), "hengelliset");
            TaustaKuva.Source = new BitmapImage(new Uri(this.BaseUri, "/Assets/Bgs/spiritualBG.png"));
            //categoryTitle.Text = "Hengelliset";
        }

        private void button_categoryPolitical_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(ContentPage), "poliittiset");
            TaustaKuva.Source = new BitmapImage(new Uri(this.BaseUri, "/Assets/Bgs/politicalBG.png"));
        }

        private void button_categoryNightlife_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(ContentPage), "yoelama");
            TaustaKuva.Source = new BitmapImage(new Uri(this.BaseUri, "/Assets/Bgs/nightlifeBG.png"));
            //categoryTitle.Text = "Yöelämä";
        }
        private void button_categoryEducation_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(ContentPage), "koulutus");
            TaustaKuva.Source = new BitmapImage(new Uri(this.BaseUri, "/Assets/Bgs/educationBG.png"));
            //categoryTitle.Text = "Koulutus";
        }

        private void button_categoryHobbies_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(ContentPage), "harrastajat");
            TaustaKuva.Source = new BitmapImage(new Uri(this.BaseUri, "/Assets/Bgs/hobbiesBG.png"));
        }

        private void button_categoryExpo_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(ContentPage), "messut");
            TaustaKuva.Source = new BitmapImage(new Uri(this.BaseUri, "/Assets/Bgs/expoBG.png"));
        }

        private void button_categoryOthers_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(ContentPage), "muut");
            TaustaKuva.Source = new BitmapImage(new Uri(this.BaseUri, "/Assets/Bgs/othersBG.png"));
            //categoryTitle.Text = "Muut";
        }

        private void logo_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(ContentPage), "featured");
            //categoryTitle.Text = "Featured";
        }
    }
}
