using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TravelApp.Helpers;

namespace TravelApp.Views.AppMainViewViews
{
    /// <summary>
    /// Interaction logic for Cities1View.xaml
    /// </summary>
    public partial class Cities1View : UserControl
    {
        public Cities1View()
        {
            InitializeComponent();
            FindDestination();
        }


        private void FindDestination()
        {
            GeoCoordinateWatcher watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High);
            watcher.Start();

            destMap.Center = new Location(CurrentTeleportSearchedCityDistrictModels.DestinationPointTeleportSearchedCityDistrictModel.Lattitude, CurrentTeleportSearchedCityDistrictModels.DestinationPointTeleportSearchedCityDistrictModel.Longtitude);
            destMap.ZoomLevel = 10;
            DestinationPushpin.Location = new Location(CurrentTeleportSearchedCityDistrictModels.DestinationPointTeleportSearchedCityDistrictModel.Lattitude, CurrentTeleportSearchedCityDistrictModels.DestinationPointTeleportSearchedCityDistrictModel.Longtitude);
            watcher.Stop();
        } 
    }
}
