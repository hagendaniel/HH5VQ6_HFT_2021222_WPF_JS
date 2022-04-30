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

        public RelayCommand NC1Command { get; set; }
        public RelayCommand NC2Command { get; set; }
        public RelayCommand NC3Command { get; set; }
        public RelayCommand NC4Command { get; set; }
        public RelayCommand NC5Command { get; set; }

        public NonCrudsWindowViewModel()
        {
            NC1Command = new RelayCommand(InitializeNC1Window);
            NC2Command = new RelayCommand(InitializeNC2Window);
            NC3Command = new RelayCommand(InitializeNC3Window);
            NC4Command = new RelayCommand(InitializeNC4Window);
            NC5Command = new RelayCommand(InitializeNC5Window);
        }

        private void InitializeNC1Window()
        {
            new NC1().Show();
        }
        private void InitializeNC2Window()
        {
            new NC2().Show();
        }
        private void InitializeNC3Window()
        {
            new NC3().Show();
        }
        private void InitializeNC4Window()
        {
            new NC4().Show();
        }
        private void InitializeNC5Window()
        {
            new NC5().Show();
        }
    }
}
