using HH5VQ6_HFT_2021221.Client;
using HH5VQ6_HFT_2021221.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HH5VQ6_SGUI_2021222.Wpf.ViewModels
{
    public class MapsWindowViewModel : ObservableRecipient
    {

        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }
        public RestCollection<Map> Maps { get; set; }

        public ICommand CreateMapButton { get; set; }
        public ICommand EditMapButton { get; set; }
        public ICommand DeleteMapButton { get; set; }

        private Map currentlySelectedMap;
        public Map CurrentlySelectedMap
        {
            get { return currentlySelectedMap; }
            set
            {
                /*SetProperty(ref currentlySelectedMap, value);
                (CreateMapButton as RelayCommand).NotifyCanExecuteChanged();
                (EditMapButton as RelayCommand).NotifyCanExecuteChanged();
                (DeleteMapButton as RelayCommand).NotifyCanExecuteChanged();*/

                if (value != null)
                {
                    currentlySelectedMap = new Map()
                    {
                        MapName = value.MapName,
                        Difficulty = value.Difficulty,
                        MapId=value.MapId
                    };
                    OnPropertyChanged();
                    (DeleteMapButton as RelayCommand).NotifyCanExecuteChanged();
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

        public MapsWindowViewModel()
        {
            if (!IsInDesignerMode)
            {
                Maps = new RestCollection<Map>("http://localhost:27989/", "maps", "hub");
                CreateMapButton = new RelayCommand(() =>
                {
                    Maps.Add(new Map()
                    {
                        MapName = CurrentlySelectedMap.MapName,
                        Difficulty = CurrentlySelectedMap.Difficulty
                    });
                });

                EditMapButton = new RelayCommand(() =>
                {
                    try
                    {
                        Maps.Update(CurrentlySelectedMap);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }
                });

                DeleteMapButton = new RelayCommand(() =>
                {
                    Maps.Delete(CurrentlySelectedMap.MapId);
                },
                () =>
                {
                    return CurrentlySelectedMap != null;
                });
                CurrentlySelectedMap = new Map();
            }
        }
    }
}
