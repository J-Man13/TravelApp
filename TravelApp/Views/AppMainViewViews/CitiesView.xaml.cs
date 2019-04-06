using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TravelApp.ViewModels.AppMainViewModelViewModels;

namespace TravelApp.Views.AppMainViewViews
{
    /// <summary>
    /// Interaction logic for CitiesView.xaml
    /// </summary>
    public partial class CitiesView : UserControl
    {

        private Timer timer;

        public CitiesView()
        {
            InitializeComponent();


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
