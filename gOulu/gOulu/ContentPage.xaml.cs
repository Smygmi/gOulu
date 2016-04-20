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
using Windows.UI.Xaml.Shapes;

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
        public string category;

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
        Uri featured =      new Uri("http://www.students.oamk.fi/~t4hevi00/mobileWinApi/getFeaturedTable.php", UriKind.Absolute);
        Uri musiikki =      new Uri("http://www.students.oamk.fi/~t4hevi00/mobileWinApi/getMusiikkiTable.php", UriKind.Absolute);
        Uri teatteri =      new Uri("http://www.students.oamk.fi/~t4hevi00/mobileWinApi/getTeatteriTable.php", UriKind.Absolute);
        Uri urheilu =       new Uri("http://www.students.oamk.fi/~t4hevi00/mobileWinApi/getUrheiluTable.php", UriKind.Absolute);
        Uri hengelliset =   new Uri("http://www.students.oamk.fi/~t4hevi00/mobileWinApi/getHengellisetTable.php", UriKind.Absolute);
        Uri poliittiset =   new Uri("http://www.students.oamk.fi/~t4hevi00/mobileWinApi/getPoliittisetTable.php", UriKind.Absolute);
        Uri yoelama =       new Uri("http://www.students.oamk.fi/~t4hevi00/mobileWinApi/getYoelamaTable.php", UriKind.Absolute);
        Uri koulutus =      new Uri("http://www.students.oamk.fi/~t4hevi00/mobileWinApi/getKoulutusTable.php", UriKind.Absolute);
        Uri harrastajat =   new Uri("http://www.students.oamk.fi/~t4hevi00/mobileWinApi/getHarrastusTable.php", UriKind.Absolute);
        Uri messut =        new Uri("http://www.students.oamk.fi/~t4hevi00/mobileWinApi/getMessutTable.php", UriKind.Absolute);
        Uri muut =          new Uri("http://www.students.oamk.fi/~t4hevi00/mobileWinApi/getMuuTable.php", UriKind.Absolute);

        //Category icon paths

        string musiikkiIconUrl =    "/Assets/Icons/musiikki_ikoni.png";
        string teatteriIconUrl = "/Assets/Icons/teatteri_ikoni.png";
        string urheiluIconUrl = "/Assets/Icons/urheilu_ikoni.png";
        string hengellisetIconUrl = "/Assets/Icons/hengelliset_ikoni.png";
        string poliittisetIconUrl = "/Assets/Icons/poliittiset_ikoni.png";
        string yoelamaIconUrl = "/Assets/Icons/yoelama_ikoni.png";
        string koulutusIconUrl = "/Assets/Icons/koulutus_ikoni.png";
        string harrastajatIconUrl = "/Assets/Icons/harrastajat_ikoni.png";
        string messutIconUrl = "/Assets/Icons/messut_ikoni.png";
        string muutIconUrl = "/Assets/Icons/muut_ikoni.png";



        //Change these colors to theme later
        SolidColorBrush white = new SolidColorBrush(new Windows.UI.Color() { A = 255, R = 255, G = 255, B = 255 });
        SolidColorBrush red = new SolidColorBrush(new Windows.UI.Color() { A = 255, R = 0, G = 255, B = 0 });
        SolidColorBrush black = new SolidColorBrush(new Windows.UI.Color() { A = 255, R = 0, G = 0, B = 0 });
        SolidColorBrush grey = new SolidColorBrush(new Windows.UI.Color() { A = 255, R = 230, G = 230, B = 230 });
        SolidColorBrush transparent = new SolidColorBrush(new Windows.UI.Color() { A = 0, R = 0, G = 0, B = 0 });

        SolidColorBrush teatteriBrush = new SolidColorBrush(new Windows.UI.Color() { A = 255, R = 141, G = 30, B = 55 });
        SolidColorBrush koulutusBrush = new SolidColorBrush(new Windows.UI.Color() { A = 255, R = 192, G = 14, B = 45 });
        SolidColorBrush harrastajatBrush = new SolidColorBrush(new Windows.UI.Color() { A = 255, R = 201, G = 75, B = 33 });
        SolidColorBrush urheiluBrush = new SolidColorBrush(new Windows.UI.Color() { A = 255, R = 215, G = 135, B = 32 });
        SolidColorBrush messutBrush = new SolidColorBrush(new Windows.UI.Color() { A = 255, R = 234, G = 190, B = 129 });
        SolidColorBrush yoelamaBrush = new SolidColorBrush(new Windows.UI.Color() { A = 255, R = 137, G = 30, B = 111 });
        SolidColorBrush musiikkiBrush = new SolidColorBrush(new Windows.UI.Color() { A = 255, R = 145, G = 60, B = 123 });
        SolidColorBrush poliittisetBrush = new SolidColorBrush(new Windows.UI.Color() { A = 255, R = 85, G = 42, B = 127 });
        SolidColorBrush hengellisetBrush = new SolidColorBrush(new Windows.UI.Color() { A = 255, R = 127, G = 95, B = 160 });
        SolidColorBrush muutBrush = new SolidColorBrush(new Windows.UI.Color() { A = 255, R = 184, G = 157, B = 194 });

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

        TextBlock[] notes;
        StackPanel[] linkPanel;
        TextBlock[] link;
        bool[] hidden;

        //For event parsing
        string eventDataAsString;
        Event[] eventObjects;

        //blaa 

        Style eventbg = (Style)Application.Current.Resources["EventBgStyle"];
        Style eventbguniform = (Style)Application.Current.Resources["EventBgUniformStyle"];

        Style eventstyle = (Style)Application.Current.Resources["EventBaseTextStyle"];
        Style event0style = (Style)Application.Current.Resources["Event0TextStyle"];
        Style event1style = (Style)Application.Current.Resources["Event1TextStyle"];
        Style event2style = (Style)Application.Current.Resources["Event2TextStyle"];

        Style outerpanelstyle = (Style)Application.Current.Resources["EventOuterPanelStyle"];
        Style innerpanelstyle = (Style)Application.Current.Resources["EventInnerPanel0Style"];

        
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



            //THIS IF ELSE FUNCTION + MANUALDEMOGENERATE() IS ONLY FOR DEMOING THE 
            //FEATURE PAGE WHICH SHOWS THE HIGHEST PAID ADS FROM OTHER CATEGORIES
            if (category == "featured")
            {
                LoadingIndicator.IsActive = false;
                ManualDemoGenerate();
            }
            else {

                switch (category)
                {
                    case "featured":
                        requestUri = featured;

                        break;
                    //break;
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
                    Debug.WriteLine(httpResponseBody);
                }

                catch (Exception ex)
                {
                    httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
                }


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

                else if (reader.TokenType == JsonToken.StartObject) {
                    index++;
                }
            }

            //Create event object arrays
            eventObjects = new Event[index + 1];


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
            EventInnerPanels = new StackPanel[eventObjects.Length + 1];
            EventOuterPanels = new RelativePanel[eventObjects.Length + 1];
            Rows = new RowDefinition[eventObjects.Length + 100];
            hidden = new bool[eventObjects.Length];
            notes = new TextBlock[eventObjects.Length];
            linkPanel = new StackPanel[eventObjects.Length];
            link = new TextBlock[eventObjects.Length];

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
                            adType = int.Parse(filteredList[i - 1]),
                            category = category
                            };
                        double.TryParse(filteredList[i - 7], out eventObjects[index].price);
                   
                        GenerateGridEntry(index);
                        index++;
                  }   
              }

            CreateEmpty(index);
      }


        //This function creates an empty entry so half-gridwidth entries show in full in the end of the page
        void CreateEmpty(int index)
        {
            eventObjects[index] = new Event()
            {
                name = "",
                location = "",
                streetAddress = "",
                postalCode = "",
                city = "",
                startDateTime = "",
                endDateTime = "",

                notes = "",
                ageLimit = "",
                websiteURL = "http://www.google.com",
                pictureURL = "http://www.google.com",
                entryAdded = "",
                adType = 0,
                category = "empty"
            };
            double.TryParse("0", out eventObjects[index].price);

            GenerateGridEntry(index);
            index++;
        }

        void ManualDemoGenerate()
        {
            
            int index = 0;
            eventObjects = new Event[4];

            //Create enough panels for events
            EventInnerPanels = new StackPanel[eventObjects.Length];
            EventOuterPanels = new RelativePanel[eventObjects.Length];
            Rows = new RowDefinition[eventObjects.Length + 100];
            hidden = new bool[eventObjects.Length];
            notes = new TextBlock[eventObjects.Length];
            linkPanel = new StackPanel[eventObjects.Length];
            link = new TextBlock[eventObjects.Length];

                    eventObjects[index] = new Event()
                    {
                        name = "Laulajien konsertti",
                        location = "",
                        streetAddress = "Törmäntie 15",
                        postalCode = "90830",
                        city = "Haukipudas",
                        startDateTime = "20.4.2016 18:00",
                        endDateTime = "20.4.2016 18:00",

                        notes = "Yhteislaulua Esko Holapan säestyksellä. Kahvia ja arpoja myytävänä. järj. Haukiputaan eläkeläisyhdistysten ja rintamaveteraanien virkistystoimikunta",
                        ageLimit = "0",
                        websiteURL = "http://www.google.com",
                        pictureURL = "http://www.google.com",
                        entryAdded = "20.4.2016",
                        adType = 2,
                        category = "musiikki"
                    };
                    double.TryParse("0", out eventObjects[index].price);

                    GenerateGridEntry(index);
                    index++;

            eventObjects[index] = new Event()
            {
                name = "Juoksuliike: Yhteistreenit",
                location = "",
                streetAddress = "Raatintie 2",
                postalCode = "90100",
                city = "Oulu",
                startDateTime = "20.4.2016 18:00",
                endDateTime = "29.5.2016",

                notes = "Yhteisjuoksuja ja Juoksukouluja ympäri Suomen järjestävä Juoksuliike nyt myös Oulusa!",
                ageLimit = "0",
                websiteURL = "http://www.google.com",
                pictureURL = "/Assets/eventBGexample.jpg",
                entryAdded = "20.4.2016",
                adType = 1,
                category = "urheilu"
            };
            double.TryParse("110", out eventObjects[index].price);

            GenerateGridEntry(index);
            index++;

            eventObjects[index] = new Event()
            {
                name = "Gorilla Gorilla",
                location = "",
                streetAddress = "Kaarlenväylä 2",
                postalCode = "90100",
                city = "Oulu",
                startDateTime = "20.4.2016 10:00, 18:00",
                endDateTime = "28.4.2016",

                notes = "Viidakon valtaamassa ostoskeskuksessa asuu lauma poliisiksi pukeutuneita gorilloja. Ne ovat pehmeitä, virkaintoisia ja ilahtuvat kovasti törmätessään uuteen asiaan. Banaanien lisäksi niitä kiinnostavat esimerkiksi autot, nenänkaivuu, rannalla löhöily, ihmiset, evoluutio ja liikennesäännöt. Gorillapoliisit ovat käsistään käteviä ja äärimmäisen älykkäitä. Samaan aikaan kun joku niistä kiipeää puuhun, saattaa toinen keksiä vaikkapa avaruusraketin. Ja vaikka joskus käy hassusti, tulee kolari tai ydinjäte ei katoakaan maton alle, niin onneksi asiat unohtuvat nopeasti, heti seuraavan kivan jutun myötä!",
                ageLimit = "0",
                websiteURL = "http://www.google.com",
                pictureURL = "http://www.google.com",
                entryAdded = "20.4.2016",
                adType = 0,
                category = "teatteri"
            };
            double.TryParse("8 / 10", out eventObjects[index].price);
            GenerateGridEntry(index);
            index++;
            CreateEmpty(index);

        }

        //Generates Grid Block for the current event
        void GenerateGridEntry(int index)
        {

            
            //Print event name
            eventObjects[index].GridEventName.Text = eventObjects[index].name;

            //If end datetime is same as start print only starting time
            if (eventObjects[index].startDateTime == eventObjects[index].endDateTime || eventObjects[index].endDateTime == " ") eventObjects[index].GridEventDateTime.Text = eventObjects[index].startDateTime;
            else eventObjects[index].GridEventDateTime.Text = eventObjects[index].startDateTime + " - " + eventObjects[index].endDateTime;

            //Print event location
            eventObjects[index].GridEventLocation.Text = eventObjects[index].location + " " + eventObjects[index].streetAddress + " " + eventObjects[index].postalCode + " " + eventObjects[index].city;

            //Print price. If price is null, free entry
            if (eventObjects[index].price != 0.0) eventObjects[index].GridEventPrice.Text = eventObjects[index].price.ToString() + "€";
            else eventObjects[index].GridEventPrice.Text = "Vapaa pääsy";

            //If no age limit, don't print
            if (eventObjects[index].ageLimit != " " && eventObjects[index].ageLimit != "0") eventObjects[index].GridEventAgeLimit.Text = "K" + eventObjects[index].ageLimit.ToString();
            else eventObjects[index].GridEventAgeLimit.Text = "";


            /////


            //Generate new elements
            Rows[rowIndex] = new RowDefinition();
            EventGrid.RowDefinitions.Add(Rows[rowIndex]);


            /////


            //Background picture
           Image bg = new Image();
            bg.Style = eventbguniform;
            string bgurl;
            bgurl = eventObjects[index].pictureURL;
            if (eventObjects[index].pictureURL != "Default")
            {
                try {      
                    bg.Source = new BitmapImage(new Uri(this.BaseUri, bgurl));
                }
                finally
                {
                
                }
            }
            bg.Margin = new Thickness(0, 0, 8, 0);

            /////

            //Outer panel
            EventOuterPanels[index] = new RelativePanel();
            EventOuterPanels[index].Style = outerpanelstyle;
            EventGrid.Children.Add(EventOuterPanels[index]);

            //Inner panel
            EventInnerPanels[index] = new StackPanel();

            EventOuterPanels[index].Children.Add(bg);
            EventOuterPanels[index].Children.Add(EventInnerPanels[index]);



            // Kategory icon and color

            Ellipse ellipse = new Ellipse();
            ellipse.Fill = red;
            

            Image ikoni = new Image();

            string url = "";

            switch (eventObjects[index].category)
            {
                case "featured":
                    url = musiikkiIconUrl;
                    break;
                case "musiikki":
                    url = musiikkiIconUrl;
                    ellipse.Fill = musiikkiBrush;
                    break;
                case "teatteri":
                    url = teatteriIconUrl;
                    ellipse.Fill = teatteriBrush;
                    break;
                case "urheilu":
                    url = urheiluIconUrl;
                    ellipse.Fill = urheiluBrush;
                    break;
                case "hengelliset":
                    url = hengellisetIconUrl;
                    ellipse.Fill = hengellisetBrush;
                    break;
                case "poliittiset":
                    url = poliittisetIconUrl;
                    ellipse.Fill = poliittisetBrush;
                    break;
                case "yoelama":
                    url = yoelamaIconUrl;
                    ellipse.Fill = yoelamaBrush;
                    break;
                case "koulutus":
                    url = koulutusIconUrl;
                    ellipse.Fill = koulutusBrush;
                    break;
                case "harrastajat":
                    url = harrastajatIconUrl;
                    ellipse.Fill = harrastajatBrush;
                    break;
                case "messut":
                    url = messutIconUrl;
                    ellipse.Fill = messutBrush;
                    break;
                case "muut":
                    url = muutIconUrl;
                    ellipse.Fill = muutBrush;
                    break;

            }
            

            try
            {
                ikoni.Source = new BitmapImage(new Uri(this.BaseUri, url));
                ikoni.Width = 50;
                ikoni.Height = 50;
            }
            finally
            {

            }
            ellipse.Width = 50;
            ellipse.Height = 50;

            RelativePanel iconPanel = new RelativePanel();
            iconPanel.Children.Add(ellipse);
            iconPanel.Children.Add(ikoni);
            iconPanel.Margin = new Thickness(-20, -20, 0, 0);
            EventOuterPanels[index].Children.Add(iconPanel);

            ////


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
                    GridTexts[1].Style = event0style;
                    GridTexts[2].Style = event0style;

                }
                else if (eventObjects[index].adType == 2)
                {
                    GridTexts[0].Style = event2style;
                    GridTexts[1].Style = event1style;
                    GridTexts[2].Style = event1style;
                    GridTexts[3].Style = event0style;
                    GridTexts[4].Style = event0style;
                }

                EventInnerPanels[index].Children.Add(GridTexts[i]);
            }


            //Setting row index for all ad sizes
            Grid.SetRow(EventOuterPanels[index], rowIndex);


            /*notes = new TextBlock[12];
            eventObjects[index].notes = "Tämä on paras tapahtuma vittu ikinä maailmassa";
           // notes[index].Text = eventObjects[index].notes;
            Debug.WriteLine(notes[index]);
            /*
            notes[index].TextWrapping = TextWrapping.Wrap;
            


            link[index].Text = "Linkki";
            link[index].VerticalAlignment = VerticalAlignment.Bottom;

            linkPanel[index].Children.Add(notes[index]);
            linkPanel[index].Children.Add(link[index]);

            linkPanel[index].Visibility = Visibility.Collapsed;
            EventInnerPanels[index].Children.Add(linkPanel[index]);

            hidden[index] = true;
            */



            //Setting column index for 1x1 ad size
            if (eventObjects[index].adType == 0)
            {
                Grid.SetColumn(EventOuterPanels[index], colIndex);
                EventInnerPanels[index].Width = 140;
                EventInnerPanels[index].Height = 140;
                Rows[rowIndex].Height = new GridLength(170);
                EventGrid.Height = EventGrid.Height + 200;

                EventInnerPanels[index].Padding = new Thickness(10, 30, 10, 0);

            }

            //Setting properties for 2x1 ad size
            else if (eventObjects[index].adType == 1)
            {
                EventInnerPanels[index].Width = 300;
                EventInnerPanels[index].Height = 170;
                
                Grid.SetColumn(EventOuterPanels[index], 0);
                Grid.SetColumnSpan(EventOuterPanels[index], 2);
                Rows[rowIndex].Height = new GridLength(170);
               
                Grid.SetColumnSpan(bg, 2);

                colIndex = 1;
                EventGrid.Height = EventGrid.Height + 170;

                EventInnerPanels[index].Padding = new Thickness(10, 40, 10, 0);
                
            }
            
            //Setting properties for 2x2 ad size
            else if (eventObjects[index].adType == 2)
            {

                EventInnerPanels[index].Width = 280;
                EventInnerPanels[index].Height = 300;

                EventInnerPanels[index].Padding = new Thickness(0, 65, 0, 0);

                Grid.SetColumn(EventOuterPanels[index], 0);
                Grid.SetColumnSpan(EventOuterPanels[index], 2);
                Rows[rowIndex].Height = new GridLength(300);
                colIndex = 1;
                
                EventGrid.Height = EventGrid.Height + 350;

            }

            EventOuterPanels[index].Padding = new Thickness(10);
            EventInnerPanels[index].HorizontalAlignment = HorizontalAlignment.Center;
            
            EventInnerPanels[index].VerticalAlignment = VerticalAlignment.Center;

            //Increase rows after second column
            if (colIndex == 1) rowIndex++;


            //Variate between columns 0/1
            if (colIndex == 0) colIndex = 1;
            else if (colIndex == 1) colIndex = 0;


            MainPage.content.Height = 900;
            eventScroller.Height = EventGrid.Height + 200;

            
            if(eventObjects[index].category == "empty")
            {
                EventOuterPanels[index].Opacity = 0;
            }
            EventOuterPanels[index].Tapped += (sender, e) => eventlinkTapped(sender, e, index);
           
        }

        /*private void eventTapped(object sender, RoutedEventArgs e, int index)
        {
            if (hidden[index] != true)
            {
                linkPanel[index].Visibility = Visibility.Visible;

                hidden[index] = true;
            }
            else
            {

                linkPanel[index].Visibility = Visibility.Collapsed;
                hidden[index] = false;
            }
        }*/

        private async void eventlinkTapped(object sender, RoutedEventArgs e, int index)
        {
            if (eventObjects[index].websiteURL != "http://www.google.com")
            {
                bool success = await Launcher.LaunchUriAsync(new Uri(eventObjects[index].websiteURL));
            }
        }

        //Designates database table category with receiving parameter
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            category = e.Parameter as string;
            DatabaseConnect(category);
        }
    }

   
}
