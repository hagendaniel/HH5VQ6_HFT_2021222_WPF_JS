using HH5VQ6_HFT_2021221.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HH5VQ6_SGUI_2021222.Wpf.ViewModels
{
    public class NC4WVM : ObservableRecipient
    {
        private string errorMessage;
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        private RestCollection<Place> places;
        public RestCollection<Place> Places
        {
            get { return places; }
            set { SetProperty(ref places, value); }
        }

        private RestCollection<Season> firstSeasonPlaceUsed;
        public RestCollection<Season> FirstSeasonPlaceUsed
        {
            get { return firstSeasonPlaceUsed; }
            set { SetProperty(ref firstSeasonPlaceUsed, value); }
        }

        private Place selectedPlace;
        private Season selectedFirstSeasonPlaceUsed;

        public Place SelectedPlace
        {
            get { return selectedPlace; }
            set
            {
                if (value != null)
                {
                    selectedPlace = new Place()
                    {
                        PlaceName = value.PlaceName,
                        Country = value.Country,
                        PlaceId = value.PlaceId
                    };
                    OnPropertyChanged();
                    (RunQuery as RelayCommand).NotifyCanExecuteChanged();
                }
                else
                {
                    OnPropertyChanged();
                    (RunQuery as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public Season SelectedFirstSeasonPlaceUsed
        {
            get { return selectedFirstSeasonPlaceUsed; }
            set
            {
                if (value != null)
                {
                    selectedFirstSeasonPlaceUsed = new Season()
                    {
                        SeasonNickname = value.SeasonNickname,
                        PlaceId = value.PlaceId,
                        SeasonId = value.SeasonId
                    };
                    OnPropertyChanged();
                    (RunQuery as RelayCommand).NotifyCanExecuteChanged();
                }
                else
                {
                    OnPropertyChanged();
                    (RunQuery as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public ICommand RunQuery { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public void InWhichSeasonFirstGameHeldInGivenPlace(string placeName)
        {
            string url = "Seasons/inwhichseasonfirstgameingivenplace/" + placeName;
            FirstSeasonPlaceUsed = new RestCollection<Season>("http://localhost:27989/", url, "hub");
        }

        public NC4WVM()
        {
            if (!IsInDesignMode)
            {
                Places = new RestCollection<Place>("http://localhost:27989/", "places", "hub");
                RunQuery = new RelayCommand(() =>
                {
                    InWhichSeasonFirstGameHeldInGivenPlace(selectedPlace.PlaceName);
                });
                selectedPlace = new Place();
                selectedFirstSeasonPlaceUsed = new Season();
            }

        }
    }
}
