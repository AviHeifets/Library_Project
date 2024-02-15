using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using ViewModels.Helpers;
using ViewModels.ViewModels;

namespace Gui.Views
{
    public partial class SearchView : UserControl
    {
        SearchViewModel vm;
        public SearchView()
        {
            vm = SearchViewModel.Init;
            DataContext = vm;
            InitializeComponent();

        }

    }
}
