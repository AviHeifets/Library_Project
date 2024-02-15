using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Helpers;

namespace ViewModels.ViewModels
{
    public class MainNavViewModel : ViewModelBase
    {
        public static MainNavViewModel Init { get; } = new MainNavViewModel();

        private MainNavViewModel() { }

        //commands
        public RelayCommand SearchBtn => new RelayCommand(execute => Search());
        public RelayCommand AddBtn => new RelayCommand(execute => Add());

        private void Search()
        {
            MainViewModel.Init.CurrentView = SearchViewModel.Init;
        }
        private void Add()
        {
            MainViewModel.Init.CurrentView = new AddViewModel();
        }
    }
}
