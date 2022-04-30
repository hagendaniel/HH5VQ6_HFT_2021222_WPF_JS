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
    public class NC2WVM : ObservableRecipient
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

        private RestCollection<Map> deadliestMap;
        public RestCollection<Map> DeadliestMap
        {
            get { return deadliestMap; }
            set { SetProperty(ref deadliestMap, value); }
        }

        private Season selectedSeason;
        private Map selectedDeadliestMap;

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

        public Map SelectedDeadliestMap
        {
            get { return selectedDeadliestMap; }
            set
            {
                if (value != null)
                {
                    selectedDeadliestMap = new Map()
                    {
                        MapName = value.MapName,
                        Difficulty = value.Difficulty,
                        MapId = value.MapId
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

        public void WhichMapGaveTheMostDeadlyExperience(string seasonName)
        {
            string url = "Maps/thekillermap/" + seasonName/*seasonName.Split(' ')[0]+"%20"+ seasonName.Split(' ')[1]*/;
            DeadliestMap = new RestCollection<Map>("http://localhost:27989/", url, "hub");
        }

        public NC2WVM()
        {
            if (!IsInDesignMode)
            {
                Seasons = new RestCollection<Season>("http://localhost:27989/", "seasons", "hub");
                RunQuery = new RelayCommand(() =>
                {
                    WhichMapGaveTheMostDeadlyExperience(selectedSeason.SeasonNickname);
                });
                selectedSeason = new Season();
                selectedDeadliestMap = new Map();
            }

        }
    }
}
