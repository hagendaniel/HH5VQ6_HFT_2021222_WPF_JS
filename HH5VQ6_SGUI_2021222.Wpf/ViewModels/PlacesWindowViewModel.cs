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
    public class PlacesWindowViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }
        public RestCollection<Place> Places { get; set; }

        public ICommand CreatePlaceButton { get; set; }
        public ICommand EditPlaceButton { get; set; }
        public ICommand DeletePlaceButton { get; set; }

        private Place currentlySelectedPlace;
        public Place CurrentlySelectedPlace
        {
            get { return currentlySelectedPlace; }
            set
            {
                /*SetProperty(ref currentlySelectedMap, value);
                (CreateMapButton as RelayCommand).NotifyCanExecuteChanged();
                (EditMapButton as RelayCommand).NotifyCanExecuteChanged();
                (DeleteMapButton as RelayCommand).NotifyCanExecuteChanged();*/

                if (value != null)
                {
                    currentlySelectedPlace = new Place()
                    {
                        PlaceName = value.PlaceName,
                        Country = value.Country,
                        PlaceId = value.PlaceId
                    };
                    OnPropertyChanged();
                    (DeletePlaceButton as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public static bool IsInDesignerMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public PlacesWindowViewModel()
        {
            if (!IsInDesignerMode)
            {
                Places = new RestCollection<Place>("http://localhost:27989/", "places", "hub");
                CreatePlaceButton = new RelayCommand(() =>
                {
                    Places.Add(new Place()
                    {
                        PlaceName = CurrentlySelectedPlace.PlaceName,
                        Country = CurrentlySelectedPlace.Country
                    });
                });

                EditPlaceButton = new RelayCommand(() =>
                {
                    try
                    {
                        Places.Update(CurrentlySelectedPlace);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }
                });

                DeletePlaceButton = new RelayCommand(() =>
                {
                    Places.Delete(CurrentlySelectedPlace.PlaceId);
                },
                () =>
                {
                    return CurrentlySelectedPlace != null;
                });
                CurrentlySelectedPlace = new Place();
            }
        }
    }
}
