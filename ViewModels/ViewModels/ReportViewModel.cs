using Models.Models;
using System.Collections.ObjectModel;
using System.IO.Pipes;
using ViewModels.Helpers;

namespace ViewModels.ViewModels
{
    public class ReportViewModel : ViewModelBase
    {
        public ReportViewModel()
        {
            SelectedItem = SearchViewModel.Init.SelectedItem;
            try
            {
                if (!(SelectedItem is Journal))
                    throw new Exception();
                Journal? i = SelectedItem as Journal;
                PublishDate = i.PublishDate.Year;
                Type = "Journal";
            }
            catch
            {
                Book? i = SelectedItem as Book;
                PublishDate = i.PublishYear;
                Type = "book";
            }

        }

        #region Commands
        private RelayCommand? returnBtn;
        public RelayCommand ReturnBtn
        {
            get
            {
                if (returnBtn == null)
                    returnBtn = new RelayCommand(execute => Return());
                return returnBtn!;
            }

        }
        #endregion

        #region Props
        private AbstractItem? selectedItem;
        private string? formattedCatagories;
        private int publishDate;
        private string? type;

        public string Type
        {
            get { return type!; }
            set 
            {
                type = value;
                OnPropertyChanged();
            }
        }

        public int PublishDate
        {
            get { return publishDate; }
            set 
            {
                publishDate = value;
                OnPropertyChanged();
            }
        }
        public string FormattedCategories
       {
            get
            {
                return string.Join(", ", SelectedItem.Catagories.Select(category => category));
            }
            set
            {
                formattedCatagories = value;
                OnPropertyChanged();
            }
        }
        public AbstractItem SelectedItem
        {
            get { return selectedItem!; }
            set 
            {
                if (selectedItem == value) return;
                selectedItem = value; 
                OnPropertyChanged();
            }
            
        }
        #endregion

        #region Functions
        private void Return()
        {
            SearchViewModel.Init.SelectedItem = null!;
            MainViewModel.Init.CurrentView = SearchViewModel.Init;
        }
        #endregion
    }
}