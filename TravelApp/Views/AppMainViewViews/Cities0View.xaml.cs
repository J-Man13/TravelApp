using System;
using System.Collections.Generic;
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
using TravelApp.ViewModels.AppMainViewModelViewModels;

namespace TravelApp.Views.AppMainViewViews
{
    /// <summary>
    /// Interaction logic for Cities0View.xaml
    /// </summary>
    public partial class Cities0View : UserControl
    {
        private Cities0ViewModel cities0ViewModel;
        public Cities0View()
        {
            InitializeComponent();
            cities0ViewModel = new Cities0ViewModel();
            DataContext = cities0ViewModel;
        }
    }
}
