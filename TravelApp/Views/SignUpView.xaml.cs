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
using System.Windows.Shapes;
using TravelApp.ViewModels;

namespace TravelApp.Views
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class SignUpView : UserControl
    {
        public SignUpViewModel signUpViewModel;
        public SignUpView()
        {
            signUpViewModel = new SignUpViewModel();
            DataContext = signUpViewModel;
            InitializeComponent();
        }

        public bool PasswordBoxesAreEqual()
        {
            return Password.Password.Equals(ConfirmPassword.Password);
        }
    }
}
