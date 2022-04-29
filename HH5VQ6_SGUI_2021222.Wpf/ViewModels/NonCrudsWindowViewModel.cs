using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH5VQ6_SGUI_2021222.Wpf.ViewModels
{
    public class NonCrudsWindowViewModel : ObservableRecipient
    {
        //RestService rest = new RestService("http://localhost:27989");

        public RelayCommand ManageMapsCommand { get; set; }
        public RelayCommand ManagePlacesCommand { get; set; }
        public RelayCommand ManagePlayersCommand { get; set; }
        public RelayCommand ManageSeasonsCommand { get; set; }
        public RelayCommand ManageNonCrudsCommand { get; set; }

        public NonCrudsWindowViewModel()
        {
            ManageMapsCommand = new RelayCommand(InitializeMapsWindow);
            ManagePlacesCommand = new RelayCommand(InitializePlacesWindow);
            ManagePlayersCommand = new RelayCommand(InitializePlayersWindow);
            ManageSeasonsCommand = new RelayCommand(InitializeSeasonsWindow);
            ManageNonCrudsCommand = new RelayCommand(InitializeNonCrudsWindow);
        }

        private void InitializeMapsWindow()
        {
            new MapsWindow().Show();
        }
        private void InitializePlacesWindow()
        {
            new PlacesWindow().Show();
        }
        private void InitializePlayersWindow()
        {
            new PlayersWindow().Show();
        }
        private void InitializeSeasonsWindow()
        {
            new SeasonsWindow().Show();
        }
        private void InitializeNonCrudsWindow()
        {
            new NonCrudsWindow().Show();
        }
    }
}
