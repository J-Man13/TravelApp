using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows;
using System.Windows.Media.Imaging;
using TravelApp.Helpers;
using TravelApp.Models.EntityModels;
using TravelApp.Models.TeleportWebApiModels;
using TravelApp.Services;
using TravelApp.Views.AppMainViewViews;

namespace TravelApp.ViewModels.AppMainViewModelViewModels
{
    public class Cities2ViewModel : ViewModelBase
    {

        private DateTime selectedDepartmentDate;
        public DateTime SelectedDepartmentDate { get { return selectedDepartmentDate; } set { Set(ref selectedDepartmentDate, value); } }

        private DateTime selectedArrivalDate;
        public DateTime SelectedArrivalDate { get { return selectedArrivalDate; } set { Set(ref selectedArrivalDate, value); } }

        private String tripAimDescription;
        public String TripAimDescription { get { return tripAimDescription; } set { Set(ref tripAimDescription, value); } }

        private BitmapSource destinationImageSource;
        public BitmapSource DestinationImageSource { get { return destinationImageSource; } set { Set(ref destinationImageSource, value); } }

        private String destinationPointName;
        public String DestinationPointName { get { return destinationPointName; } set { Set(ref destinationPointName, value); } }

        private ITeleportDestination_sCategoriesScoresImagesService iTeleportDestination_SCategoriesScoresImagesService;
        private TeleportSearchedCityDistrictModel destinationtSearchedCityDistrictModel;

        private IUserService iUserService;
        private ITripEntities iTripEntities;

        public Cities2ViewModel()
        {
            SelectedDepartmentDate = DateTime.Now;
            SelectedArrivalDate = DateTime.Now;
            TripAimDescription = "";

            iTeleportDestination_SCategoriesScoresImagesService = new TeleportDestination_sCategoriesDetailsImagesServiceWebApi();
            destinationtSearchedCityDistrictModel = CurrentTeleportSearchedCityDistrictModels.DestinationPointTeleportSearchedCityDistrictModel;

            DestinationPointName = destinationtSearchedCityDistrictModel.FullName;

            BitmapImage bitmapImage = iTeleportDestination_SCategoriesScoresImagesService.GetSearchedCityImage(destinationtSearchedCityDistrictModel.UrbanAreaImagesLink);
            if (bitmapImage != null)
                DestinationImageSource = bitmapImage;

            iUserService = new UserServiceLocalMSSQLDB();
            iTripEntities = new TripEntitiesLocalMSSQLDB();
        }

        private RelayCommand<object> goBackButtonComand;
        public RelayCommand<object> GoBackButtonComand
        {
            get => goBackButtonComand ?? (goBackButtonComand = new RelayCommand<object>((obj) =>
            {
                if (string.IsNullOrEmpty(destinationtSearchedCityDistrictModel.UrbanAreaLink))
                    (obj as Cities2View).Content = new Cities0ViewModel();
                else
                    (obj as Cities2View).Content = new Cities1ViewModel();
            }));
        }

        
        private RelayCommand<object> addTripButtonComand;
        public RelayCommand<object> AddTripButtonComand
        {
            get => addTripButtonComand ?? (addTripButtonComand = new RelayCommand<object>((obj) =>
            {
                if (SelectedDepartmentDate.CompareTo(DateTime.Today) < 1)
                {
                    MessageBox.Show("Department date must be greater than current date");
                    return;
                }

                if (SelectedArrivalDate.CompareTo(DateTime.Today) < 1)
                {
                    MessageBox.Show("Arrival date must be greater than current date");
                    return;
                }

                TripEntity tripEntity = iTripEntities.AddTripEntity(new TripEntity()
                {                    
                    FromSearchedCityDistrictModel = CurrentTeleportSearchedCityDistrictModels.StartPointTeleportSearchedCityDistrictModel,
                    ToSearchedCityDistrictModel = CurrentTeleportSearchedCityDistrictModels.DestinationPointTeleportSearchedCityDistrictModel,
                    DepartmentDateTime = selectedDepartmentDate,
                    ArrivalDateTime = selectedArrivalDate,
                    TripAim = TripAimDescription,
                    UserEntity = CurrentUserEntity.UserEntity,
                    UserEntityId = CurrentUserEntity.UserEntity.Id
                });
                CurrentUserEntity.UserEntity.TripEntities.Add(tripEntity);
            }));
        }

    }
}
