using GCUF.Common;
using GCUF.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
   
    public sealed partial class ContactsPage : Page
    {
     ///   public static Contact DataModel;
        
      //  ObservableCollection<ContactsPage> myData = new ObservableCollection<ContactsPage>();
        ObservableCollection<Contact> myData = new ObservableCollection<Contact>();
        
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public ContactsPage()
        {
            this.InitializeComponent();
          //  DataModel = new Contact("Bilal", "Developer", "03138745211");
            LoadData();
            lstData.DataContext = myData;
          

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
            
        }
        public void LoadData()
        {
            //Contacts Data
            myData.Add(new Contact("Dr. Zakir Hussain", "Vice Chancellor", "03138745211"));
            myData.Add(new Contact("Muhammad Ayub", "Registrar", "(041)920-1266"));
            myData.Add(new Contact("Mr.Zafar Hussain", "Info Center", "0419200886"));
            myData.Add(new Contact("Bilal Mustafa", "App Developer", "03138745211"));

          
            

        }

       
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

       
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        
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

        private void lstData_ItemClick(object sender, ItemClickEventArgs e)
        {
           

            var PhoneName = ((Contact)e.ClickedItem).Name;
            var Phone = ((Contact)e.ClickedItem).PhoneNumber;

            Windows.ApplicationModel.Calls.PhoneCallManager.ShowPhoneCallUI(Phone, PhoneName);
            

        }

        
    }
}
