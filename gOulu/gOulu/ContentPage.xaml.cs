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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

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
        public int price;
        public string notes;
        public int ageLimit;
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
        Uri musiikki = new Uri("http://www.students.oamk.fi/~t4toan00/feikki_musiikki.php");
        Uri teatteri = new Uri("http://www.students.oamk.fi/~t4toan00/feikki_teatteri.php");

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
        char delimiterChar;
        string[] events;
        Event[] eventObjects;
        string[] dataPairs;

        


        public ContentPage()
        {
            this.InitializeComponent();

            //Event grid dimensions
            EventGrid.Height = 760;
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

            dataPairs = new String[7];

            Content.Background = transparent;
            Content.Children.Add(EventGrid);
        }


        async void DatabaseConnect(string category)
        {
            LoadingIndicator.IsActive = true;

            //Initialize Http call
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage httpResponse = new HttpResponseMessage();

            //Designate requestUri by categoryy
            //API URI = http://www.students.oamk.fi/~t4hevi00/mobileWinApi/

            Uri requestUri = new Uri("http://www.students.oamk.fi/~t4toan00/");

            switch (category)
            { case "musiikki":
                    requestUri = musiikki;
                    break;
                case "teatteri":
                    requestUri = teatteri;
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


        void GenerateGridData()
        {
            //Get datapairs from events[] at line break
            int index = -1;
            delimiterChar = '\n';
            events = eventDataAsString.Split(delimiterChar);
            eventObjects = new Event[events.Length];
            EventInnerPanels = new StackPanel[eventObjects.Length];
            EventOuterPanels = new RelativePanel[eventObjects.Length];
            Rows = new RowDefinition[eventObjects.Length + 100];

            foreach (string e in events)
            {
                index++;
                eventObjects[index] = new Event() { };
                SetValues(index, e);
                GenerateGridEntry(index);
            }
        }


        void SetValues(int index, string e)
        {
            //Get datapairs from events[] at | separator
            delimiterChar = '|';
            dataPairs = e.Split(delimiterChar);

            foreach (string pair in dataPairs)
            {
                //Get datapairs from events[] at tab separator
                delimiterChar = '\t';
                string[] pairString = new string[2];
                pairString = pair.Split(delimiterChar);

                //Roll values to variables
                foreach (string value in pairString)
                {
                    switch (pairString[0])
                    {
                        case "Name":
                            eventObjects[index].name = pairString[1];
                            break;
                        case "Place_Name":
                            eventObjects[index].location = pairString[1];
                            break;
                        case "Street_Address":
                            eventObjects[index].location = pairString[1];
                            break;
                        case "Postal_Code":
                            eventObjects[index].location = pairString[1];
                            break;
                        case "City":
                            eventObjects[index].location = pairString[1];
                            break;
                        case "Start_Date_Time":
                            eventObjects[index].startDateTime = pairString[1];
                            break;
                        case "End_Date_Time":
                            eventObjects[index].endDateTime = pairString[1];
                            break;
                        case "Price":
                            eventObjects[index].price = int.Parse(pairString[1]);
                            break;
                        case "Notes":
                            eventObjects[index].notes = pairString[1];
                            break;
                        case "Age_Rating":
                            eventObjects[index].ageLimit = int.Parse(pairString[1]);
                            break;
                        case "Website_Url":
                            eventObjects[index].websiteURL = pairString[1];
                            break;
                        case "Picture_Url":
                            eventObjects[index].pictureURL = pairString[1];
                            break;
                        case "Entry_Added":
                            eventObjects[index].entryAdded = pairString[1];
                            break;
                        case "Rating":
                            eventObjects[index].adType = int.Parse(pairString[1]);

                            break;
                    }
                }
            }


        }

        void GenerateGridEntry(int index)
        {

            //Print event name
            eventObjects[index].GridEventName.Text = eventObjects[index].name;


            //If end datetime is null print only starting time
            if (eventObjects[index].endDateTime == "") eventObjects[index].GridEventDateTime.Text = eventObjects[index].startDateTime;
            else eventObjects[index].GridEventDateTime.Text = eventObjects[index].startDateTime + " - " + eventObjects[index].endDateTime;

            //Print event location
            eventObjects[index].GridEventLocation.Text = eventObjects[index].location + eventObjects[index].streetAddress + eventObjects[index].postalCode + eventObjects[index].city;

            //Print price. If price is null, free entry
            if (eventObjects[index].price != 0) eventObjects[index].GridEventPrice.Text = eventObjects[index].price.ToString() + "€";
            else eventObjects[index].GridEventPrice.Text = "Vapaa pääsy";

            //If no age limit, don't print
            if (eventObjects[index].ageLimit != 0) eventObjects[index].GridEventAgeLimit.Text = "K" + eventObjects[index].ageLimit.ToString();
            else eventObjects[index].GridEventAgeLimit.Text = "";

            //Generate new elements

            Rows[rowIndex] = new RowDefinition();
            EventGrid.RowDefinitions.Add(Rows[rowIndex]);


            //Background picture
           Image bg = new Image();
            if (eventObjects[index].pictureURL != "Default")
            {
                bg.Source = new BitmapImage(new Uri(eventObjects[index].pictureURL, UriKind.Absolute));
                bg.Opacity = .6;

            }
            else EventInnerPanels[index].Background = white;

            //Outer panel
            EventOuterPanels[index] = new RelativePanel();
            EventOuterPanels[index].Background = grey;
            EventOuterPanels[index].Opacity = 1;
            EventOuterPanels[index].Margin = new Thickness(10);
            EventGrid.Children.Add(EventOuterPanels[index]);

            //Inner panel
            EventInnerPanels[index] = new StackPanel();
            EventInnerPanels[index].HorizontalAlignment = HorizontalAlignment.Center;
            EventInnerPanels[index].VerticalAlignment = VerticalAlignment.Center;
            EventInnerPanels[index].Opacity = 1;


            EventOuterPanels[index].Children.Add(bg);
            EventOuterPanels[index].Children.Add(EventInnerPanels[index]);

            //Text elements
            GridTexts[0] = eventObjects[index].GridEventName;
            GridTexts[1] = eventObjects[index].GridEventDateTime;
            GridTexts[2] = eventObjects[index].GridEventLocation;
            GridTexts[3] = eventObjects[index].GridEventPrice;
            GridTexts[4] = eventObjects[index].GridEventAgeLimit;


            // Pyydä ATlta muotoilutiedostoa tekstien muotoiluun
            // Loop properties for text elements
            for (int i = 0; i < GridTexts.Length; i++)
            {

                GridTexts[i].TextWrapping = TextWrapping.Wrap;
                GridTexts[i].TextAlignment = TextAlignment.Center;
                GridTexts[i].VerticalAlignment = VerticalAlignment.Center;
                GridTexts[i].HorizontalAlignment = HorizontalAlignment.Center;
                GridTexts[i].FontSize = 13.0;
                GridTexts[i].Foreground = black;

                
                if (i == 0) GridTexts[i].SetValue(TextBlock.FontWeightProperty, FontWeights.Bold);
                if (eventObjects[index].adType == 1)
                {
                    GridTexts[0].FontSize = 30;
                    GridTexts[1].FontSize = 18;
                    GridTexts[i].Foreground = black;
                    
                }
                else if (eventObjects[index].adType == 2)
                {
                    GridTexts[0].FontSize = 40;
                    GridTexts[1].FontSize = 24;
                }

                
                EventInnerPanels[index].Children.Add(GridTexts[i]);
                
            }



            Grid.SetRow(EventOuterPanels[index], rowIndex);

            //Setting column index for 1x1 ad size
            if (eventObjects[index].adType == 0)
            {
                EventOuterPanels[index].Padding = new Thickness(5);
                Grid.SetColumn(EventOuterPanels[index], colIndex);
              
                EventInnerPanels[index].Width = 140;
                EventInnerPanels[index].Height = 170;
               
                EventInnerPanels[index].Padding = new Thickness(20);

                Rows[rowIndex].Height = new GridLength(170);
                bg.Stretch = Stretch.Fill;
                
            }

            //Setting properties for 2x1 ad size
            else if (eventObjects[index].adType == 1)
            {
                EventOuterPanels[index].Padding = new Thickness(5);

                EventInnerPanels[index].Width = 300;
                EventInnerPanels[index].Height = 170;
                EventInnerPanels[index].Padding = new Thickness(15);
                Grid.SetColumn(EventOuterPanels[index], 0);
                Grid.SetColumnSpan(EventOuterPanels[index], 2);
                Rows[rowIndex].Height = new GridLength(170);
                colIndex = 1;
                Grid.SetColumnSpan(bg, 2);
                bg.Stretch = Stretch.UniformToFill;
            }
            //Setting properties for 2x2 ad size
            else if (eventObjects[index].adType == 2)
            {
                EventOuterPanels[index].Padding = new Thickness(10);
                EventInnerPanels[index].Width = 300;
                EventInnerPanels[index].Height = 300;
                EventInnerPanels[index].HorizontalAlignment = HorizontalAlignment.Center;
                EventInnerPanels[index].Padding = new Thickness(10, 50, 0, 0);
                EventInnerPanels[index].VerticalAlignment = VerticalAlignment.Center;
                Grid.SetColumn(EventOuterPanels[index], 0);
                Grid.SetColumnSpan(EventOuterPanels[index], 2);
                Rows[rowIndex].Height = new GridLength(300);
                colIndex = 1;
                bg.Stretch = Stretch.Fill;
                rowIndex++;
            }

            

            //Increase rows after second column
            if (colIndex == 1) rowIndex++;

            //Variate between columns 0/1
            if (colIndex == 0) colIndex = 1;
            else if (colIndex == 1) colIndex = 0;


        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            category = e.Parameter as string;
            DatabaseConnect(category);
        }
    }

   
}
