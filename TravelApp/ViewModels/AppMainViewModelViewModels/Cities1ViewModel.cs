using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TravelApp.Helpers;
using TravelApp.Models.TeleportWebApiModels;
using TravelApp.Services;
using TravelApp.Views.AppMainViewViews;

namespace TravelApp.ViewModels.AppMainViewModelViewModels
{
    public class Cities1ViewModel : ViewModelBase
    {
        public string DestinationtPointFullName { get {return destinationtPointFullName; } set {Set(ref destinationtPointFullName, value);} }
        private string destinationtPointFullName;

        public string DestinationtPointDescription { get { return destinationtPointDescription; } set { Set(ref destinationtPointDescription, value); } }
        public string destinationtPointDescription;

        public ObservableCollection<TeleportSearchedCityDistrictCategoriesModel> ObservableCategoryTypes { get { return observableCategoryTypes; } set { Set(ref observableCategoryTypes,value); } }
        private ObservableCollection<TeleportSearchedCityDistrictCategoriesModel> observableCategoryTypes;

        public TeleportSearchedCityDistrictCategoriesModel ObservableCategoryType { get {return observableCategoryType; } set {Set(ref observableCategoryType,value);}}
        private TeleportSearchedCityDistrictCategoriesModel observableCategoryType;

        public ObservableCollection<TeleportSearchedCityDistrictCategoryModel> ObservableCategories { get { return observableCategories; } set { Set(ref observableCategories, value); } }
        public ObservableCollection<TeleportSearchedCityDistrictCategoryModel> observableCategories;

        public Visibility ObservableCategoriesVisibility { get { return observableCategoriesVisibility; } set { Set(ref observableCategoriesVisibility, value); } }
        public Visibility observableCategoriesVisibility;

        public ObservableCollection<TeleportSearchedCityDistrictScore> ObservableScores { get { return observableScores; } set { Set(ref observableScores, value); } }
        private ObservableCollection<TeleportSearchedCityDistrictScore> observableScores;
        private ObservableCollection<TeleportSearchedCityDistrictScore> observableScoresToStore;

        public string SearchedScore { get { return searchedScore; } set { Set(ref searchedScore, value); } }
        public string searchedScore;

        private TeleportSearchedCityDistrictModel destinationtSearchedCityDistrictModel;
        private ITeleportDestination_sCategoriesScoresImagesService teleportDestination_sCategoriesScoresService;

        public Cities1ViewModel()
        {
            teleportDestination_sCategoriesScoresService = new TeleportDestination_sCategoriesDetailsImagesServiceWebApi();
            destinationtSearchedCityDistrictModel = CurrentTeleportSearchedCityDistrictModels.DestinationPointTeleportSearchedCityDistrictModel;
            DestinationtPointFullName = destinationtSearchedCityDistrictModel.FullName;

            ObservableCategoryTypes = new ObservableCollection<TeleportSearchedCityDistrictCategoriesModel>(
            teleportDestination_sCategoriesScoresService.GetTeleportSearchedCityDistrictCategoryModels(
            destinationtSearchedCityDistrictModel.UrbanAreaDetailsLink));


            TeleportSearchedCityDistrictScoresInfo teleportSearchedCityDistrictScoresInfo = teleportDestination_sCategoriesScoresService.GetSearchedCityDistrictScoresInfo(destinationtSearchedCityDistrictModel.UrbanAreaScoresLink);

            DestinationtPointDescription = teleportSearchedCityDistrictScoresInfo.Summary;
            observableScoresToStore = new ObservableCollection<TeleportSearchedCityDistrictScore>(teleportSearchedCityDistrictScoresInfo.TeleportSearchedCityDistrictScores);
            ObservableScores = new ObservableCollection<TeleportSearchedCityDistrictScore>(observableScoresToStore);
        }


        private RelayCommand selectCategoryType;
        public RelayCommand SelectCategoryType
        {
            get => selectCategoryType ?? (selectCategoryType = new RelayCommand(async () => {
                ObservableCategories = new ObservableCollection<TeleportSearchedCityDistrictCategoryModel>(ObservableCategoryType.TeleportSearchedCityDistrictCategoryModels);
                ObservableCategoriesVisibility = Visibility.Visible;
            }));
        }

        private RelayCommand searchScores;
        public RelayCommand SearchScores
        {
            get => searchScores ?? (searchScores = new RelayCommand(async () => {
                ObservableScores = new ObservableCollection<TeleportSearchedCityDistrictScore>(
                    observableScoresToStore.Where(x => x.Name.ToLower().Contains(SearchedScore.ToLower())).ToList());
            }));
        }

        private RelayCommand<object> goNextButtonComand;
        public RelayCommand<object> GoNextButtonComand
        {
            get => goNextButtonComand ?? (goNextButtonComand = new RelayCommand<object>((obj) => {
                (obj as Cities1View).Content = new Cities2ViewModel();
            }));
        }

        private RelayCommand<object> goBackButtonComand;
        public RelayCommand<object> GoBackButtonComand
        {
            get => goBackButtonComand ?? (goBackButtonComand = new RelayCommand<object>((obj) =>
            {
                (obj as Cities1View).Content = new Cities0ViewModel();
            }));
        }

    }
}
