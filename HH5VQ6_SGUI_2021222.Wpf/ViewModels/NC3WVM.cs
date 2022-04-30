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
    public class NC3WVM : ObservableRecipient
    {
        private string errorMessage;
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        private RestCollection<Season> seasons;
        public RestCollection<Season> Seasons
        {
            get { return seasons; }
            set { SetProperty(ref seasons, value); }
        }

        private RestCollection<Player> winningPlayer;
        public RestCollection<Player> WinningPlayer
        {
            get { return winningPlayer; }
            set { SetProperty(ref winningPlayer, value); }
        }

        private Season selectedSeason;
        private Player selectedWinningPlayer;

        public Season SelectedSeason
        {
            get { return selectedSeason; }
            set
            {
                if (value != null)
                {
                    selectedSeason = new Season()
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

        public Player SelectedWinningPlayer
        {
            get { return selectedWinningPlayer; }
            set
            {
                if (value != null)
                {
                    selectedWinningPlayer = new Player()
                    {
                        PlayerName = value.PlayerName,
                        Born = value.Born,
                        SeasonId = value.SeasonId,
                        EliminatedOnMap_MapId = value.EliminatedOnMap_MapId
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

        public void WhoWonGivenSeason(int seasonId)
        {
            string url = "Players/whowongivenseason/" + seasonId/*seasonName.Split(' ')[0]+"%20"+ seasonName.Split(' ')[1]*/;
            WinningPlayer = new RestCollection<Player>("http://localhost:27989/", url, "hub");
        }

        public NC3WVM()
        {
            if (!IsInDesignMode)
            {
                Seasons = new RestCollection<Season>("http://localhost:27989/", "seasons", "hub");
                RunQuery = new RelayCommand(() =>
                {
                    WhoWonGivenSeason(selectedSeason.SeasonId);
                });
                selectedSeason = new Season();
                selectedWinningPlayer = new Player();
            }

        }
    }
}
