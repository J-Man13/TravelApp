using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using TravelApp.Helpers;
using TravelApp.Models.TeleportWebApiModels;
using TravelApp.Services;
using TravelApp.Views;
using TravelApp.Views.AppMainViewViews;


namespace TravelApp.ViewModels.AppMainViewModelViewModels
{
    public class CitiesViewModel : ViewModelBase
    {
        private ITeleportDistrictCountryServices teleportApiServices;

        private string startPoint;
        public string StartPoint { get {return startPoint; } set{Set(ref startPoint,value); } }

        private string destination;
        public string Destination { get { return destination; } set { Set(ref destination, value); } }

        private ObservableCollection<TeleportSearchedCityDistrictModel> observableStartPoints;
        public ObservableCollection<TeleportSearchedCityDistrictModel> ObservableStartPoints
        {
            get { return observableStartPoints; }
            set { Set(ref observableStartPoints, value); }
        }

        private ObservableCollection<TeleportSearchedCityDistrictModel> observableDestination;
        public ObservableCollection<TeleportSearchedCityDistrictModel> ObservableDestination
        {
            get { return observableDestination; }
            set { Set(ref observableDestination, value); }
        }

        private Visibility startPushpinVisibility;
        public Visibility StartPushpinVisibility
        {
            get { return startPushpinVisibility; }
            set { Set(ref startPushpinVisibility, value); }
        }

        private Visibility destinationPushpinVisibility;
        public Visibility DestinationPushpinVisibility
        {
            get { return destinationPushpinVisibility; }
            set { Set(ref destinationPushpinVisibility, value); }
        }


        private Visibility startLocationsListBoxVisibility;
        public Visibility StartLocationsListBoxVisibility
        {
            get { return startLocationsListBoxVisibility; }
            set { Set(ref startLocationsListBoxVisibility, value); }
        }

        private Visibility startLocationsLoadingImageVisbility;
        public Visibility StartLocationsLoadingImageVisbility
        {
            get { return startLocationsLoadingImageVisbility; }
            set { Set(ref startLocationsLoadingImageVisbility, value); }
        }       

        private Visibility destinationLocationsListBoxVisibility;
        public Visibility DestinationLocationsListBoxVisibility
        {
            get { return destinationLocationsListBoxVisibility; }
            set { Set(ref destinationLocationsListBoxVisibility, value); }
        }


        private Visibility destinationLocationsLoadingImageVisibility;
        public Visibility DestinationLocationsLoadingImageVisibility
        {
            get { return destinationLocationsLoadingImageVisibility; }
            set { Set(ref destinationLocationsLoadingImageVisibility, value); }
        }


        private TeleportSearchedCityDistrictModel startSearchedCityDistrictModel;
        private TeleportSearchedCityDistrictModel destinationtSearchedCityDistrictModel;

        public CitiesViewModel()
        {
            teleportApiServices = new TeleportDistrictCountryWebApi();
            StartPoint = "Start Point";
            Destination = "Destination";

            StartPushpinVisibility = Visibility.Hidden;
            DestinationPushpinVisibility = Visibility.Hidden;

            StartLocationsLoadingImageVisbility = Visibility.Visible;
            DestinationLocationsLoadingImageVisibility = Visibility.Visible;

            StartLocationsListBoxVisibility = Visibility.Hidden;
            DestinationLocationsListBoxVisibility = Visibility.Hidden;

            Task.Run(() =>
            {
                IEnumerable<TeleportSearchedCityDistrictModel> list = teleportApiServices.GetSearchedCityDistrictModel("");

                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (list != null)
                    {
                        ObservableStartPoints = new ObservableCollection<TeleportSearchedCityDistrictModel>(list);
                        ObservableDestination = new ObservableCollection<TeleportSearchedCityDistrictModel>(list);
                    }

                    StartLocationsLoadingImageVisbility = Visibility.Hidden;
                    DestinationLocationsLoadingImageVisibility = Visibility.Hidden;

                    StartLocationsListBoxVisibility = Visibility.Visible;
                    DestinationLocationsListBoxVisibility = Visibility.Visible;

                });
            });

        }

        private RelayCommand<object> searchStartPoint;
        public RelayCommand<object> SearchStartPoint
        {
            get => searchStartPoint ?? (searchStartPoint = new RelayCommand<object>( async (obj) => {
                StartLocationsLoadingImageVisbility = Visibility.Visible;
                StartLocationsListBoxVisibility = Visibility.Hidden;

                DrawOrWipeOffLine((obj as CitiesView).TripsMap);

                startSearchedCityDistrictModel = null;
                StartPushpinVisibility = Visibility.Hidden;

                if (String.IsNullOrEmpty(StartPoint) || String.IsNullOrWhiteSpace(StartPoint))
                    StartPoint = "Start Point";
                LinkedList<TeleportSearchedCityDistrictModel> searchedCityDistrictModels = null;
                await Task.Run( () => searchedCityDistrictModels = new LinkedList<TeleportSearchedCityDistrictModel>(teleportApiServices.GetSearchedCityDistrictModel(StartPoint)));
                if (searchedCityDistrictModels != null)
                    ObservableStartPoints = new ObservableCollection<TeleportSearchedCityDistrictModel>(searchedCityDistrictModels);

                StartLocationsLoadingImageVisbility = Visibility.Hidden;
                StartLocationsListBoxVisibility = Visibility.Visible;
            }));
        } 


        private RelayCommand<object> searchDestination;
        public RelayCommand<object> SearchDestination
        {
            get => searchDestination ?? (searchDestination = new RelayCommand<object>(async (obj) => {
                DestinationLocationsLoadingImageVisibility = Visibility.Visible;
                DestinationLocationsListBoxVisibility = Visibility.Hidden;

                DrawOrWipeOffLine((obj as CitiesView).TripsMap);

                destinationtSearchedCityDistrictModel = null;
                DestinationPushpinVisibility = Visibility.Hidden;

                if (String.IsNullOrEmpty(Destination) || String.IsNullOrWhiteSpace(Destination))
                    Destination = "Destination";
                LinkedList<TeleportSearchedCityDistrictModel> searchedCityDistrictModels = null;
                await Task.Run(() => searchedCityDistrictModels = new LinkedList<TeleportSearchedCityDistrictModel>(teleportApiServices.GetSearchedCityDistrictModel(Destination)));
                if (searchedCityDistrictModels != null)
                    ObservableDestination = new ObservableCollection<TeleportSearchedCityDistrictModel>(searchedCityDistrictModels);

                DestinationLocationsLoadingImageVisibility = Visibility.Hidden;
                DestinationLocationsListBoxVisibility = Visibility.Visible;
            }));
        }

        private RelayCommand<object> startLocationSelected;
        public RelayCommand<object> StartLocationSelected
        {
            get => startLocationSelected ?? (startLocationSelected = new RelayCommand<object>(async obj => {
                
                if ((obj as CitiesView).StartLocationsListBox.SelectedItem == null)
                    return;
                startSearchedCityDistrictModel = (obj as CitiesView).StartLocationsListBox.SelectedItem as TeleportSearchedCityDistrictModel;

                StartPushpinVisibility = Visibility.Visible;
                (obj as CitiesView).StartPushpin.Location = new Location(startSearchedCityDistrictModel.Lattitude, startSearchedCityDistrictModel.Longtitude);

                Map map = (obj as CitiesView).TripsMap;
                Pushpin startPushPin = (obj as CitiesView).StartPushpin;
                Pushpin destinationPushpin = (obj as CitiesView).DestinationPushpin;
                DrawOrWipeOffLine(map, startPushPin, destinationPushpin);
            }));
        }

        private RelayCommand<object> destinationLocationSelected;
        public RelayCommand<object> DestinationLocationSelected
        {
            get => destinationLocationSelected ?? (destinationLocationSelected = new RelayCommand<object>(async obj => {

                if ((obj as CitiesView).DestinationLocationsListBox.SelectedItem == null)
                    return;
                destinationtSearchedCityDistrictModel = (obj as CitiesView).DestinationLocationsListBox.SelectedItem as TeleportSearchedCityDistrictModel;

                DestinationPushpinVisibility = Visibility.Visible;
                (obj as CitiesView).DestinationPushpin.Location = new Location(destinationtSearchedCityDistrictModel.Lattitude, destinationtSearchedCityDistrictModel.Longtitude);

                Map map = (obj as CitiesView).TripsMap;
                Pushpin startPushPin = (obj as CitiesView).StartPushpin;
                Pushpin destinationPushpin = (obj as CitiesView).DestinationPushpin;
                DrawOrWipeOffLine(map, startPushPin, destinationPushpin);

            }));
        }

        
        private RelayCommand<object> goNextButtonComand;
        public RelayCommand<object> GoNextButtonComand
        {
            get => goNextButtonComand ?? (goNextButtonComand = new RelayCommand<object>((obj) => {
                if(startSearchedCityDistrictModel == null || destinationtSearchedCityDistrictModel == null)
                {
                    MessageBox.Show("Start or destination point not selected");
                    return;
                }
                CurrentTeleportSearchedCityDistrictModels.StartPointTeleportSearchedCityDistrictModel = startSearchedCityDistrictModel;
                CurrentTeleportSearchedCityDistrictModels.DestinationPointTeleportSearchedCityDistrictModel = destinationtSearchedCityDistrictModel;
                (obj as CitiesView).Content = new Cities0ViewModel();
            }));
        } 


        private void DrawOrWipeOffLine(Map map, Pushpin startPushPin = null, Pushpin destinationPushpin = null)
        {
            MapPolyline m = null;
            foreach(Object obj in map.Children)
            {
                if (obj is MapPolyline)
                    m = obj as MapPolyline;
            }
            map.Children.Remove(m);

            if (startPushPin != null && destinationPushpin != null &&
                startPushPin.Visibility == Visibility.Visible &&
                destinationPushpin.Visibility == Visibility.Visible)
            {               
                MapPolyline mapPolyline = new MapPolyline();
                mapPolyline.Stroke = new SolidColorBrush(Colors.Red);
                mapPolyline.StrokeThickness = 2;
                mapPolyline.Locations = new LocationCollection();

                mapPolyline.Locations.Add(startPushPin.Location);
                mapPolyline.Locations.Add(destinationPushpin.Location);

                map.Children.Add(mapPolyline);
            }

        }        
    }
}
