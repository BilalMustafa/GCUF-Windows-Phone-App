using GCUF.Common;
using GCUF.NewsFeed;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace GCUF
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewsFeedPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public NewsFeedPage()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
            Loaded += NewsFeedPage_Loaded;
        }

        public async void NewsFeedPage_Loaded(object sender, RoutedEventArgs e)
        {
            loadingprogress.IsActive = true;
            bool internetworking;
            internetworking = true;
            try
            {
                HttpClient httpclient = new HttpClient();
                //Getting the data from the website
                var rsscontent = await httpclient.GetStringAsync(new Uri("http://gcuf.edu.pk/feed/"));
                //Parsing the content into XML Elements
                XElement xmlitems = XElement.Parse(rsscontent);
                //Getting the items from the content and decending them
                List<XElement> elements = xmlitems.Descendants("item").ToList();
                //Creating the Observable collection of RSS Items
                List<RSSItem> aux = new List<RSSItem>();
                foreach (XElement rssItem in elements)
                {
                    RSSItem rss = new RSSItem();
                    rss.Title1 = rssItem.Element("title").Value;
                    rss.Link1 = rssItem.Element("link").Value;
                    aux.Add(rss);



                }
                loadingprogress.IsActive = false;

                ListBoxRss.DataContext = aux;
            }
            catch(HttpRequestException)
            {
                internetworking = false;
            }
            if(internetworking == false)
            {
                var messageDialoghttp = new Windows.UI.Popups.MessageDialog("Unable to connect to Internet:(");
                messageDialoghttp.Title = "No Internet Connection!";
                try
                {
                    messageDialoghttp.ShowAsync();
                    loadingprogress.IsActive = false;
                    Frame.Navigate(typeof(HubPage));




                }
                catch (UnauthorizedAccessException)
                {

                }

            }
            

            //throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private async void ListBoxRss_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                var launchlink = ((RSSItem)e.ClickedItem).Link1;
                var uri = new Uri(launchlink);


                var linklaunched = await Windows.System.Launcher.LaunchUriAsync(uri);
            }
            catch (InvalidCastException)
            {
                var messageDialog = new Windows.UI.Popups.MessageDialog("Sorry!, This feature will be available soon:)");
                messageDialog.Title = "Launch the link in the WebBrowser!";


                try
                {
                    messageDialog.ShowAsync();
                }
                catch (UnauthorizedAccessException)
                {

                }




            }


        }
    }
}
