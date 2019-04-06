using GalaSoft.MvvmLight;


namespace TravelApp.ViewModels
{
    public class AppViewModel : ViewModelBase
    {
        private ViewModelBase currentPage;        
        public ViewModelBase CurrentPage { get => currentPage; set => Set(ref currentPage, value); }

        public AppViewModel()
        {
            CurrentPage = new LoginViewModel();
        }

    }
}
