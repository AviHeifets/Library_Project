using System.Windows.Controls;
using System.Windows.Data;
using ViewModels.Helpers;
using ViewModels.ViewModels;

namespace Gui.Views
{
    public partial class UpdateView : UserControl
    {
        UpdateViewModel vm;
        public UpdateView()
        {
            vm = new UpdateViewModel();
            DataContext = vm;
            InitializeComponent();
        }
    }
}

