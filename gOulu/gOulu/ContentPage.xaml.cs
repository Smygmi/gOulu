using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
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
using Windows.Web.Http;
using Windows.UI.Xaml.Media.Imaging;
using Windows.System;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Globalization;

namespace gOulu
{

    public class Event
    {
        public string name;
        public string location;
        public string streetAddress;
        public string postalCode;
        public string city;
        public string startDateTime;
        public string endDateTime;
        public double price;
        public string notes;
        public string ageLimit;
        public string websiteURL;
        public string pictureURL;
        public string entryAdded;
        public int adType;

        public TextBlock GridEventName = new TextBlock();
        public TextBlock GridEventDateTime = new TextBlock();
        public TextBlock GridEventLocation = new TextBlock();
        public TextBlock GridEventPrice = new TextBlock();
        public TextBlock GridEventAgeLimit = new TextBlock();

        public Event() { }

    }

    public sealed partial class ContentPage : Page
    {
        //Category
        public string category;
        Uri featured =      new Uri("http://www.students.oamk.fi/~t4hevi00/mobileWinApi/getFeaturedTable.php");
        Uri musiikki =      new Uri("http://www.students.oamk.fi/~t4hevi00/mobileWinApi/getMusiikkiTable.php");
        Uri teatteri =      new Uri("http://www.students.oamk.fi/~t4hevi00/mobileWinApi/getTeatteriTable.php");
        Uri urheilu =       new Uri("http://www.students.oamk.fi/~t4hevi00/mobileWinApi/getUrheiluTable.php");
        Uri hengelliset =   new Uri("http://www.students.oamk.fi/~t4hevi00/mobileWinApi/getHengellisetTable.php");
        Uri poliittiset =   new Uri("http://www.students.oamk.fi/~t4hevi00/mobileWinApi/getPoliittisetTable.php");
        Uri yoelama =       new Uri("http://www.students.oamk.fi/~t4hevi00/mobileWinApi/getYoelamaTable.php");
        Uri koulutus =      new Uri("http://www.students.oamk.fi/~t4hevi00/mobileWinApi/getKoulutusTable.php");
        Uri harrastajat =   new Uri("http://www.students.oamk.fi/~t4hevi00/mobileWinApi/getHarrastusTable.php");
        Uri messut =        new Uri("http://www.students.oamk.fi/~t4hevi00/mobileWinApi/getMessutTable.php");
        Uri muut =          new Uri("http://www.students.oamk.fi/~t4hevi00/mobileWinApi/getMuuTable.php");

        //Change these colors to theme later
        SolidColorBrush white = new SolidColorBrush(new Windows.UI.Color() { A = 255, R = 255, G = 255, B = 255 });
        SolidColorBrush red = new SolidColorBrush(new Windows.UI.Color() { A = 255, R = 0, G = 255, B = 0 });
        SolidColorBrush black = new SolidColorBrush(new Windows.UI.Color() { A = 255, R = 0, G = 0, B = 0 });
        SolidColorBrush grey = new SolidColorBrush(new Windows.UI.Color() { A = 255, R = 230, G = 230, B = 230 });
        SolidColorBrush transparent = new SolidColorBrush(new Windows.UI.Color() { A = 0, R = 0, G = 0, B = 0 });

        //EventGrid
        Grid EventGrid = new Grid();
        RowDefinition[] Rows;
        RelativePanel border = new RelativePanel();
        ColumnDefinition[] Cols = new ColumnDefinition[2];
        int rowIndex;
        int colIndex;
        TextBlock[] GridTexts = new TextBlock[5];
        StackPanel[] EventInnerPanels;
        RelativePanel[] EventOuterPanels;

        //For event parsing
        string eventDataAsString;
        Event[] eventObjects;
        char delimiterChar;

        //blaa 

        Style eventbg = (Style)Application.Current.Resources["EventBgStyle"];

        Style eventstyle = (Style)Application.Current.Resources["EventBaseTextStyle"];
        Style event0style = (Style)Application.Current.Resources["Event0TextStyle"];
        Style event1style = (Style)Application.Current.Resources["Event1TextStyle"];
        Style event2style = (Style)Application.Current.Resources["Event2TextStyle"];

        Style outerpanelstyle = (Style)Application.Current.Resources["EventOuterPanelStyle"];
        Style innerpanelstyle = (Style)Application.Current.Resources["EventInnerPanelStyle"];

        
        public ContentPage()
        {
            this.InitializeComponent();

            //Event grid dimensions
            
            Content.Width = 450;
            EventGrid.Width = 450;

            //Row and column definitions
            rowIndex = 0;
            colIndex = 0;
            Cols[0] = new ColumnDefinition();
            Cols[1] = new ColumnDefinition();
            Cols[0].Width = new GridLength(160);
            Cols[1].Width = new GridLength(160);
            EventGrid.ColumnDefinitions.Add(Cols[0]);
            EventGrid.ColumnDefinitions.Add(Cols[1]);

            Content.Background = transparent;
            Content.Children.Add(EventGrid);
        }

        //Connects to database server
        async void DatabaseConnect(string category)
        {
            LoadingIndicator.IsActive = true;

            //Initialize Http call
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage httpResponse = new HttpResponseMessage();

            //API URI = http://www.students.oamk.fi/~t4hevi00/mobileWinApi/

            Uri requestUri = featured;

            switch (category)
            {
                case "featured":
                    requestUri = featured;
                    break;
                case "musiikki":
                    requestUri = musiikki;
                    break;
                case "teatteri":
                    requestUri = teatteri;
                    break;
                case "urheilu":
                    requestUri = urheilu;
                    break;
                case "hengelliset":
                    requestUri = hengelliset;
                    break;
                case "poliittiset":
                    requestUri = poliittiset;
                    break;
                case "yoelama":
                    requestUri = yoelama;
                    break;
                case "koulutus":
                    requestUri = koulutus;
                    break;
                case "harrastajat":
                    requestUri = harrastajat;
                    break;
                case "messut":
                    requestUri = messut;
                    break;
                case "muut":
                    requestUri = muut;
                    break;
            }
                
            string httpResponseBody = "";

            try
            {
                //Send the GET request
                httpResponse = await httpClient.GetAsync(requestUri);
                httpResponse.EnsureSuccessStatusCode();
                httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                eventDataAsString = httpResponseBody;
                LoadingIndicator.IsActive = false;
                GenerateGridData();
            }

            catch (Exception ex)
            {
                httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
            }
        }

        //Fetches events from received string
        void GenerateGridData()
        {
            int index = 0;
            JsonTextReader reader = new JsonTextReader(new StringReader(eventDataAsString));

            //Read value fields from Json
            List<string> eventDataStrings = new List<string>();

            while (reader.Read()) {
                if (reader.Value != null)
                    eventDataStrings.Add(reader.Value.ToString());
                
                else if (reader.TokenType == JsonToken.StartObject){
                    index++;
                }
            }

            //Create event object arrays
            eventObjects = new Event[index];


            //Filter values from value keys        
            List<string> filteredList = new List<string>();

            for (int i = 0; i < eventDataStrings.Count; i++)
            {
                i++;

                if (eventDataStrings[i - 1] == "Start_Date_Time" || eventDataStrings[i - 1] == "End_Date_Time")
                {

                    char[] delimiterChars = { '-', ':', ' ' };
                    string[] dateTimeString = eventDataStrings[i].Split(delimiterChars);

                    string formattedDateTime = dateTimeString[2] + "." + dateTimeString[1] + "." + dateTimeString[0] + " " + dateTimeString[3] + ":" + dateTimeString[4];
                    filteredList.Add(formattedDateTime);
                }
               else if (eventDataStrings[i - 1] == "Rating")
                {
                    filteredList.Add("" + eventDataStrings[i]);
                    filteredList.Add("EndOfObject");
                }
                else
                {
                    filteredList.Add("" + eventDataStrings[i]);
                }
                
             }

            index = 0;

            //Create enough panels for events
            EventInnerPanels = new StackPanel[eventObjects.Length];
            EventOuterPanels = new RelativePanel[eventObjects.Length];
            Rows = new RowDefinition[eventObjects.Length + 100];
            

                for (int i = 0; i < filteredList.Count; i++)
                { 
                    if (filteredList[i] == "EndOfObject")
                    {
                    eventObjects[index] = new Event()
                        {
                        name = filteredList[i - 14],
                        location = filteredList[i - 13],
                        streetAddress = filteredList[i - 12],
                        postalCode = filteredList[i - 11],
                        city = filteredList[i - 10],
                        startDateTime = filteredList[i - 9],
                        endDateTime = filteredList[i - 8],

                        notes = filteredList[i - 6],
                        ageLimit = filteredList[i - 5],
                        websiteURL = filteredList[i - 4],
                        pictureURL = filteredList[i - 3],
                        entryAdded = filteredList[i - 2],
                        adType = int.Parse(filteredList[i - 1])
                        };
                    double.TryParse(filteredList[i - 7], out eventObjects[index].price);
                    GenerateGridEntry(index);
                    index++;
                    }
                    
                }
        }


        //Generates Grid Block for the current event
        void GenerateGridEntry(int index)
        {

            
            //Print event name
            eventObjects[index].GridEventName.Text = eventObjects[index].name;

            //If end datetime is same as start print only starting time
            if (eventObjects[index].startDateTime == eventObjects[index].endDateTime) eventObjects[index].GridEventDateTime.Text = eventObjects[index].startDateTime;
            else eventObjects[index].GridEventDateTime.Text = eventObjects[index].startDateTime + " - " + eventObjects[index].endDateTime;

            //Print event location
            eventObjects[index].GridEventLocation.Text = eventObjects[index].location + " " + eventObjects[index].streetAddress + " " + eventObjects[index].postalCode + " " + eventObjects[index].city;

            //Print price. If price is null, free entry
            if (eventObjects[index].price != 0.0) eventObjects[index].GridEventPrice.Text = eventObjects[index].price.ToString() + "€";
            else eventObjects[index].GridEventPrice.Text = "Vapaa pääsy";

            //If no age limit, don't print
            if (eventObjects[index].ageLimit != "") eventObjects[index].GridEventAgeLimit.Text = "K" + eventObjects[index].ageLimit.ToString();
            else eventObjects[index].GridEventAgeLimit.Text = "";

            //Generate new elements

            Rows[rowIndex] = new RowDefinition();
            EventGrid.RowDefinitions.Add(Rows[rowIndex]);


            //Background picture
           Image bg = new Image();
            if (eventObjects[index].pictureURL != "Default")
            {
                bg.Source = new BitmapImage(new Uri(eventObjects[index].pictureURL, UriKind.Absolute));
                //bg.Style = eventbg;
                //bg.Opacity = .6;
            }


            else EventInnerPanels[index].Background = white;

            Debug.WriteLine("success");

            //Outer panel
            EventOuterPanels[index] = new RelativePanel();
            EventOuterPanels[index].Style = outerpanelstyle;
            //EventOuterPanels[index].Opacity = 1;
            //EventOuterPanels[index].Margin = new Thickness(10);
            EventGrid.Children.Add(EventOuterPanels[index]);

            //Inner panel
            EventInnerPanels[index] = new StackPanel();
            //EventInnerPanels[index].HorizontalAlignment = HorizontalAlignment.Center;
            //EventInnerPanels[index].VerticalAlignment = VerticalAlignment.Center;
           // EventInnerPanels[index].Opacity = 1;

            
            EventOuterPanels[index].Children.Add(bg);
            EventOuterPanels[index].Children.Add(EventInnerPanels[index]);

            //Text elements
            GridTexts[0] = eventObjects[index].GridEventName;
            GridTexts[1] = eventObjects[index].GridEventDateTime;
            GridTexts[2] = eventObjects[index].GridEventLocation;
            GridTexts[3] = eventObjects[index].GridEventPrice;
            GridTexts[4] = eventObjects[index].GridEventAgeLimit;

            // Loop properties for text elements
            for (int i = 0; i < GridTexts.Length; i++)
            {

                //Adds base style for all text
                GridTexts[i].Style = eventstyle;

                if (i == 0)
                {
                    GridTexts[i].SetValue(TextBlock.FontWeightProperty, FontWeights.Bold);
                    GridTexts[0].Style = event0style;
                }
                if (eventObjects[index].adType == 1)
                {
                    GridTexts[0].Style = event1style;                    
                }
                else if (eventObjects[index].adType == 2)
                {
                    GridTexts[0].Style = event2style;
                }

                
                EventInnerPanels[index].Children.Add(GridTexts[i]);
                
            }



            Grid.SetRow(EventOuterPanels[index], rowIndex);

            //Setting column index for 1x1 ad size
            if (eventObjects[index].adType == 0)
            {
                //EventOuterPanels[index].Padding = new Thickness(5);
                //EventInnerPanels[index].Padding = new Thickness(10);
                //bg.Stretch = Stretch.Fill;

                Grid.SetColumn(EventOuterPanels[index], colIndex);
                EventInnerPanels[index].Width = 140;
                EventInnerPanels[index].Height = 140;
                Rows[rowIndex].Height = new GridLength(170);
                

                EventGrid.Height = EventGrid.Height + 200;


            }

            //Setting properties for 2x1 ad size
            else if (eventObjects[index].adType == 1)
            {
                //EventOuterPanels[index].Padding = new Thickness(5);
                //EventInnerPanels[index].Padding = new Thickness(15);
                //bg.Stretch = Stretch.UniformToFill;

                EventInnerPanels[index].Width = 300;
                EventInnerPanels[index].Height = 170;
                
                
                Grid.SetColumn(EventOuterPanels[index], 0);
                Grid.SetColumnSpan(EventOuterPanels[index], 2);
                Rows[rowIndex].Height = new GridLength(170);
               
                Grid.SetColumnSpan(bg, 2);
                

                colIndex = 1;
                EventGrid.Height = EventGrid.Height + 170;
            }
            
            //Setting properties for 2x2 ad size
            else if (eventObjects[index].adType == 2)
            {
                // EventOuterPanels[index].Padding = new Thickness(10);
                //EventInnerPanels[index].HorizontalAlignment = HorizontalAlignment.Center;
                //EventInnerPanels[index].Padding = new Thickness(10, 50, 0, 0);
                //EventInnerPanels[index].VerticalAlignment = VerticalAlignment.Center;
                //bg.Stretch = Stretch.Fill;

                EventInnerPanels[index].Width = 300;
                EventInnerPanels[index].Height = 300;
                
                Grid.SetColumn(EventOuterPanels[index], 0);
                Grid.SetColumnSpan(EventOuterPanels[index], 2);
                Rows[rowIndex].Height = new GridLength(300);
                colIndex = 1;
                
                EventGrid.Height = EventGrid.Height + 350;

            }
            

            //Increase rows after second column
            if (colIndex == 1) rowIndex++;

            
            //Variate between columns 0/1
            if (colIndex == 0) colIndex = 1;
            else if (colIndex == 1) colIndex = 0;

            MainPage.content.Height = 900;

            EventOuterPanels[index].Tapped += (sender, e) => eventTapped(sender, e, index);


           
            //EventInnerPanels[index].Style = innerpanelstyle;
        }

        private async void eventTapped(object sender, RoutedEventArgs e, int index)
        {
            bool success = await Launcher.LaunchUriAsync(new Uri(eventObjects[index].websiteURL));
        }


        //Designates database table category with receiving parameter
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            category = e.Parameter as string;
            DatabaseConnect(category);
        }
    }

   
}
