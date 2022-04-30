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
    public class NC5WVM : ObservableRecipient
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

        private RestCollection<Season> seasonWonByPlayer;
        public RestCollection<Season> SeasonWonByPlayer
        {
            get { return seasonWonByPlayer; }
            set { SetProperty(ref seasonWonByPlayer, value); }
        }

        private Player selectedPlayer;
        private Season selectedseasonWonByPlayer;

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

        public Season SelectedseasonWonByPlayer
        {
            get { return selectedseasonWonByPlayer; }
            set
            {
                if (value != null)
                {
                    selectedseasonWonByPlayer = new Season()
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

        public void WhichSeasonWasWonByGivenPlayer(int playerId)
        {
            string url = "Seasons/whichseasonwonbygivenplayer/" + playerId;
            SeasonWonByPlayer = new RestCollection<Season>("http://localhost:27989/", url, "hub");
        }

        public NC5WVM()
        {
            if (!IsInDesignMode)
            {
                Players = new RestCollection<Player>("http://localhost:27989/", "players", "hub");
                RunQuery = new RelayCommand(() =>
                {
                    WhichSeasonWasWonByGivenPlayer(SelectedPlayer.PlayerId);
                });
                SelectedPlayer = new Player();
                selectedseasonWonByPlayer = new Season();
            }

        }
    }
}
