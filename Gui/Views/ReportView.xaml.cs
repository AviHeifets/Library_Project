using System.Windows.Controls;
using ViewModels.ViewModels;

namespace Gui.Views
{
    public partial class DetailsView : UserControl
    {
     
        public DetailsView()
        {
            DataContext = new ReportViewModel();
            InitializeComponent();
        }
    }
}
