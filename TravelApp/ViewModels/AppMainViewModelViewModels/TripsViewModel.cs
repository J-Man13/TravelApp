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
using TravelApp.Models.EntityModels;
using TravelApp.Services;

namespace TravelApp.ViewModels.AppMainViewModelViewModels
{
    public class TripsViewModel : ViewModelBase
    {
        public ObservableCollection<TripEntity> ObservableTrips { get { return observableTrips; } set{ Set(ref observableTrips,value);} }
        private ObservableCollection<TripEntity> observableTrips;

        public Visibility ObservableTripsVisibility { get { return observableTripsVisibility; } set { Set(ref observableTripsVisibility, value); } }
        private Visibility observableTripsVisibility;

        public Visibility LoadingVisbility { get { return loadingVisbility; } set { Set(ref loadingVisbility, value); } }
        private Visibility loadingVisbility;

        private ITripEntitiesService tripEntitiesService;

        public TripsViewModel()
        {
            ObservableTripsVisibility = Visibility.Hidden;
            LoadingVisbility = Visibility.Visible;

            Task.Run(() =>
            {
                List<TripEntity> tripEntities = new List<TripEntity>(CurrentUserEntity.UserEntity.TripEntities);
                App.Current.Dispatcher.Invoke(() =>
                {
                    ObservableTrips = new ObservableCollection<TripEntity>(tripEntities);
                });

                ObservableTripsVisibility = Visibility.Visible;
                LoadingVisbility = Visibility.Hidden;
            });

            tripEntitiesService = new TripEntitiesLocalMSSQLDB();
        }

        private RelayCommand<long> deleteTripButtonComand;
        public RelayCommand<long> DeleteTripButtonComand
        {
            get => deleteTripButtonComand ?? (deleteTripButtonComand = new RelayCommand<long>((id) =>
            {
                ObservableTrips.Remove(ObservableTrips.Where(t => t.Id == id).FirstOrDefault());
                tripEntitiesService.DeleteTripEntity(id);                
            }));
        }

    }
}
