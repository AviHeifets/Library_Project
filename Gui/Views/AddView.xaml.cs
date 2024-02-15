using System.Windows.Controls;
using System.Windows.Data;
using ViewModels.Helpers;
using ViewModels.ViewModels;

namespace Gui.Views
{

    public partial class AddView : UserControl
    {
        AddViewModel vm;
        public AddView()
        {
            vm = new AddViewModel();
            DataContext = vm;
            InitializeComponent();

        }
      
    }
}
