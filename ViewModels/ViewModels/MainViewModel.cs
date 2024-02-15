
using ViewModels.Helpers;

namespace ViewModels.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        //singelton
        public static MainViewModel Init { get; } = new MainViewModel();
        private MainViewModel()
        {
            currentView = MainNavViewModel.Init;
        }

        //holding current view
        private ViewModelBase? currentView;
        public ViewModelBase CurrentView
        {
            get { return currentView!; }
            set
            {
                if (currentView == value) { return; }
                currentView = value;
                OnPropertyChanged();
            }
        }
    }
}
