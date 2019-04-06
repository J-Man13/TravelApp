using System;
using System.Windows;
using System.Windows.Controls;
using TravelApp.ViewModels.AppMainViewModelViewModels;
using System.Timers;
using System.Device.Location;
using Microsoft.Maps.MapControl.WPF;

namespace TravelApp.Views.AppMainViewViews
{
    public partial class TripsView : UserControl
    {
        private TripsViewModel tripsViewModel;
        private Timer timer;

        public TripsView()  
        {
            InitializeComponent();
            tripsViewModel = new TripsViewModel();
            DataContext = tripsViewModel;

            timer = new System.Timers.Timer();
            timer.Elapsed += new ElapsedEventHandler((object source, ElapsedEventArgs e) =>
            {
                try
                {
                    GeoCoordinateWatcher watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High);
                    watcher.Start();

                    watcher.PositionChanged += (s, ev) =>
                    {
                        var coordinate = ev.Position.Location;
                        Dispatcher.Invoke(() => PushPinMe.Location = new Location(coordinate.Latitude, coordinate.Longitude));
                        watcher.Stop();
                    };
                }
                catch (Exception)
                {
                    MessageBox.Show("Check your internet or GPS connection");
                }
            });
            timer.Interval = 5000;
            timer.Start();
            FindMe();
        }

        private void FindMe()
        {

            GeoCoordinateWatcher watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High);
            watcher.Start();

            watcher.PositionChanged += (s, ev) =>
            {
                var coordinate = ev.Position.Location;
                TripsMap.Center = new Location(coordinate.Latitude, coordinate.Longitude);
                TripsMap.ZoomLevel = 18;
                PushPinMe.Location = new Location(coordinate.Latitude, coordinate.Longitude);
                watcher.Stop();
            };
        }
    }
}
