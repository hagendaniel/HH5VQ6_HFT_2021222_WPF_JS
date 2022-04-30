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
    public class NC1WVM : ObservableRecipient
    {
        private string errorMessage;
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        private RestCollection<Player> players;
        public RestCollection<Player> Players
        {
            get { return players; }
            set { SetProperty(ref players, value); }
        }

        private RestCollection<Place> placeWherePlayerGotEliminated;
        public RestCollection<Place> PlaceWherePlayerGotEliminated
        {
            get { return placeWherePlayerGotEliminated; }
            set { SetProperty(ref placeWherePlayerGotEliminated, value); }
        }

        private Player selectedPlayer;
        private Place placeWhereSelectedPlayerGotEliminated;

        public Player SelectedPlayer
        {
            get { return selectedPlayer; }
            set
            {
                if (value != null)
                {
                    selectedPlayer = new Player()
                    {
                        PlayerName = value.PlayerName,
                        Born = Convert.ToDateTime(value.Born),
                        SeasonId = value.SeasonId,
                        PlayerId = value.PlayerId
                    };
                    OnPropertyChanged();
                    (RunQuery as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public Place PlaceWhereSelectedPlayerGotEliminated
        {
            get { return placeWhereSelectedPlayerGotEliminated; }
            set
            {
                /*SetProperty(ref currentlySelectedMap, value);
                (CreateMapButton as RelayCommand).NotifyCanExecuteChanged();
                (EditMapButton as RelayCommand).NotifyCanExecuteChanged();
                (DeleteMapButton as RelayCommand).NotifyCanExecuteChanged();*/

                if (value != null)
                {
                    placeWhereSelectedPlayerGotEliminated = new Place()
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

        public ICommand RunQuery { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public void InWhichCityGivenPlayerDied(int playerId)
        {
            string url = "Places/inwhichcityplayerdied/" + playerId;
            PlaceWherePlayerGotEliminated = new RestCollection<Place>("http://localhost:27989/", url, "hub");
        }

        public NC1WVM()
        {
            if (!IsInDesignMode)
            {
                Players = new RestCollection<Player>("http://localhost:27989/", "players", "hub");
                RunQuery = new RelayCommand(() =>
                {
                    InWhichCityGivenPlayerDied(SelectedPlayer.PlayerId);
                });
                SelectedPlayer = new Player();
                PlaceWhereSelectedPlayerGotEliminated = new Place();
            }

        }
    }
}
