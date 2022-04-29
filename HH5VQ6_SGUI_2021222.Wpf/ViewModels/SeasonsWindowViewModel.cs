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
    public class SeasonsWindowViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }
        public RestCollection<Season> Seasons { get; set; }

        public ICommand CreateSeasonButton { get; set; }
        public ICommand EditSeasonButton { get; set; }
        public ICommand DeleteSeasonButton { get; set; }

        private Season currentlySelectedSeason;
        public Season CurrentlySelectedSeason
        {
            get { return currentlySelectedSeason; }
            set
            {
                /*SetProperty(ref currentlySelectedMap, value);
                (CreateMapButton as RelayCommand).NotifyCanExecuteChanged();
                (EditMapButton as RelayCommand).NotifyCanExecuteChanged();
                (DeleteMapButton as RelayCommand).NotifyCanExecuteChanged();*/

                if (value != null)
                {
                    currentlySelectedSeason = new Season()
                    {
                        SeasonNickname = value.SeasonNickname,
                        PlaceId = value.PlaceId,
                        SeasonId = value.SeasonId
                    };
                    OnPropertyChanged();
                    (DeleteSeasonButton as RelayCommand).NotifyCanExecuteChanged();
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

        public SeasonsWindowViewModel()
        {
            if (!IsInDesignerMode)
            {
                Seasons = new RestCollection<Season>("http://localhost:27989/", "seasons", "hub");
                CreateSeasonButton = new RelayCommand(() =>
                {
                    Seasons.Add(new Season()
                    {
                        SeasonNickname = CurrentlySelectedSeason.SeasonNickname,
                        PlaceId = CurrentlySelectedSeason.PlaceId
                    });
                });

                EditSeasonButton = new RelayCommand(() =>
                {
                    try
                    {
                        Seasons.Update(CurrentlySelectedSeason);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }
                });

                DeleteSeasonButton = new RelayCommand(() =>
                {
                    Seasons.Delete(CurrentlySelectedSeason.SeasonId);
                },
                () =>
                {
                    return CurrentlySelectedSeason != null;
                });
                CurrentlySelectedSeason = new Season();
            }
        }
    }
}
