using CoffeeFinder.Model;
using CoffeeFinder.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CoffeeFinder.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private FoursquareService _fss;
        private LocationService _locationService;

        public string ViewTitle { get; set; }

        private ObservableCollection<Venue> _venues;
        public ObservableCollection<Venue> Venues
        {
            get
            {
                return _venues;
            }
            set
            {
                _venues = value;
                base.RaisePropertyChanged(() => Venues);
            }
        }

        private bool _isBusy = false;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                base.RaisePropertyChanged(() => IsBusy);
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            this.ViewTitle = "COFFEE FINDER";

            this.Venues = new ObservableCollection<Venue>();

            this._locationService = new LocationService();
            this._fss = new FoursquareService();


            GetCoffeeVenues();
        }

        private RelayCommand _refreshVenuesCommand;
        public RelayCommand RefreshVenuesCommand
        {
            get
            {
                return this._refreshVenuesCommand ?? (this._refreshVenuesCommand = new RelayCommand(ExecuteRefreshVenuesCommand));
            }
        }

        private void ExecuteRefreshVenuesCommand()
        {
            if (IsBusy)
                return;

            // Reset the collection
            this.Venues.Clear();

            GetCoffeeVenues();
        }

        private async void GetCoffeeVenues()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var coords = await this._locationService.GetLocationAsync();
                var lat = coords.Latitude.ToString();
                var lon = coords.Longitude.ToString();

                var response = await this._fss.GetVenues("coffee", lat, lon);

                foreach (var v in response.Venues)
                {
                    this.Venues.Add(v);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}