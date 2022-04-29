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
    public class PlayersWindowViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }
        public RestCollection<Player> Players { get; set; }

        public ICommand CreatePlayerButton { get; set; }
        public ICommand EditPlayerButton { get; set; }
        public ICommand DeletePlayerButton { get; set; }

        private Player currentlySelectedPlayer;
        public Player CurrentlySelectedPlayer
        {
            get { return currentlySelectedPlayer; }
            set
            {
                /*SetProperty(ref currentlySelectedMap, value);
                (CreateMapButton as RelayCommand).NotifyCanExecuteChanged();
                (EditMapButton as RelayCommand).NotifyCanExecuteChanged();
                (DeleteMapButton as RelayCommand).NotifyCanExecuteChanged();*/

                if (value != null)
                {
                    currentlySelectedPlayer = new Player()
                    {
                        PlayerName = value.PlayerName,
                        Born = Convert.ToDateTime(value.Born),
                        SeasonId = value.SeasonId,
                        PlayerId = value.PlayerId
                    };
                    OnPropertyChanged();
                    (DeletePlayerButton as RelayCommand).NotifyCanExecuteChanged();
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

        public PlayersWindowViewModel()
        {
            if (!IsInDesignerMode)
            {
                Players = new RestCollection<Player>("http://localhost:27989/", "players", "hub");
                CreatePlayerButton = new RelayCommand(() =>
                {
                    /*try
                    {*/
                        Players.Add(new Player()
                        {
                            PlayerName = CurrentlySelectedPlayer.PlayerName,
                            Born = CurrentlySelectedPlayer.Born,
                            SeasonId = CurrentlySelectedPlayer.SeasonId,
                            EliminatedOnMap_MapId = currentlySelectedPlayer.EliminatedOnMap_MapId
                        });
                    /*}
                    catch (Exception ex)
                    {
                        ErrorMessage = "Invalid season";
                    }*/
                });

                EditPlayerButton = new RelayCommand(() =>
                {
                    try
                    {
                        Players.Update(CurrentlySelectedPlayer);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }
                });

                DeletePlayerButton = new RelayCommand(() =>
                {
                    Players.Delete(CurrentlySelectedPlayer.PlayerId);
                },
                () =>
                {
                    return CurrentlySelectedPlayer != null;
                });
                CurrentlySelectedPlayer = new Player();
            }
        }
    }
}
