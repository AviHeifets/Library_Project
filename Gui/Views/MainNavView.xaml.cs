using System.Windows.Controls;
using ViewModels.Helpers;
using ViewModels.ViewModels;

namespace Gui.Views
{
    public partial class MainNavView : UserControl
    {
        MainNavViewModel vm;
        public MainNavView()
        {
            vm = MainNavViewModel.Init;
            DataContext = vm;
            InitializeComponent();
        }
    }
}
