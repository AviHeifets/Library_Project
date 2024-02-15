
using BookLib.DatServices;
using Models.Helpers;
using Models.Models;
using System.Collections.ObjectModel;
using System.Reflection;
using ViewModels.Helpers;
using ViewModels.Helpers.Filters;

namespace ViewModels.ViewModels
{
    public class SearchViewModel : ViewModelBase
    {
        //singelton
        public static SearchViewModel Init { get; } = new SearchViewModel();
        private SearchViewModel()
        {
            SearchByOptions = new ObservableCollection<string> { "All", "Id", "Isbn", "Name", "Author" };
            PriceOptions = new ObservableCollection<string> { "All", "50<", "50-99", "100-149", "150+" };
            DiscountOptions = new ObservableCollection<string> { "All", "10<", "10-20", "20-30", "30+" };
            YearOptions = new ObservableCollection<string> { "All", "1800<", "1800 - 1899", "1900 - 1999", "2000+" };
            TypeOptions = new ObservableCollection<string> { "All", "Book", "Journal" };
            Results = new ObservableCollection<ObservableDictionary>();
            PropertyChanged += (sender, e) => UpdateCommands();
        }

        //database
        private readonly ItemCollection db = ItemCollection.Init;

        #region Commands
        //commands
        private RelayCommand? searchBtn;
        public RelayCommand SearchBtn
        {
            get
            {
                if (searchBtn == null)
                {
                    searchBtn = new RelayCommand(execute => Search());
                }
                return searchBtn;
            }
        }


        private RelayCommand? updateBtn;
        public RelayCommand UpdateBtn
        {
            get
            {
                if (updateBtn == null)
                {
                    updateBtn = new RelayCommand(execute => Update(), canExecute => IsSelected());
                }
                return updateBtn;
            }
        }


        private RelayCommand? removeBtn;
        public RelayCommand RemoveBtn
        {
            get
            {
                if (removeBtn == null)
                {
                    removeBtn = new RelayCommand(execute => Remove(), canExecute => IsSelected());
                }
                return removeBtn;
            }
        }


        private RelayCommand? returnBtn;
        public RelayCommand ReturnBtn
        {
            get
            {
                if (returnBtn == null)
                {
                    returnBtn = new RelayCommand(execute => Return());
                }
                return returnBtn;
            }
        }


        private RelayCommand? detailsBtn;
        public RelayCommand DetailsBtn
        {
            get 
            {
                if (detailsBtn == null)
                {
                    detailsBtn = new RelayCommand(execute => Details(), canExecute => IsSelected());
                }
                return detailsBtn; 
            }
        }

        #endregion

        #region Search option collections
        //Options for search
        public ObservableCollection<string> SearchByOptions { get; set; }
        public ObservableCollection<string> PriceOptions { get; set; }
        public ObservableCollection<string> DiscountOptions { get; set; }
        public ObservableCollection<string> YearOptions { get; set; }
        public ObservableCollection<string> TypeOptions { get; set; }
        #endregion

        #region Props
        private ObservableCollection<ObservableDictionary>? results;
        public ObservableCollection<ObservableDictionary> Results
        {
            get { return results!; }
            set
            {
                if (results == value) return;
                results = value!;
                OnPropertyChanged();
            }
        }


        private string? price = "All";
        private string? discount = "All";
        private string? type = "All";
        private string? searchBy = "All";
        private string? searchInput;
        private AbstractItem? selectedItem;

        public string Type
        {
            get { return type!; }
            set
            {
                if (type == value) return;
                type = value;
                OnPropertyChanged();
            }
        }
        public string SearchBy
        {
            get { return searchBy!; }
            set
            {
                if (searchBy == value) return;
                searchBy = value;
                OnPropertyChanged();
            }
        }
        public string Discount
        {
            get { return discount!; }
            set
            {
                if (discount == value) return;
                discount = value;
                OnPropertyChanged();
            }
        }
        public string Price
        {
            get { return price!; }
            set
            {
                if (price == value) return;
                price = value;
                OnPropertyChanged();
            }
        }
        public string SearchInput
        {
            get { return searchInput!; }
            set
            {
                if (searchInput == value) return;
                searchInput = value;
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
                UpdateCommands();
            }
        }
        #endregion

        #region Functions
        private void Search()
        {
            ObservableCollection<AbstractItem> items = new FilterBySearchOption().Filter(db, SearchBy, SearchInput);
            Dictionary<IFilter, string> filterConditionMap = new Dictionary<IFilter, string>
            {
                { new FilterByType(), Type },
                { new FilterByDiscount(), Discount },
                { new FilterByPrice(), Price }
            };

            if (items.Count != 0)
            {
                foreach (KeyValuePair<IFilter, string> kvp in filterConditionMap)
                {
                    items = kvp.Key.Filter(items, kvp.Value);
                }
            }

            Results!.Clear();

            // Group items by ISBN and populate Results
            foreach (AbstractItem item in items)
            {
                ObservableDictionary res = null!;
                foreach (var r in results!)
                {
                    if (item.Isbn == r.Isbn)
                    {
                        res = r;
                        break;
                    }
                }

                if (res == null)
                {
                    res = new ObservableDictionary() { Isbn = item.Isbn };
                    Results.Add(res);
                }
                res.Items!.Add(item);

            }
        }
        private void Update()
        {
            MainViewModel.Init.CurrentView = new UpdateViewModel();
        }
        private void Remove()
        {
            if (selectedItem == null || results == null) return;

            db.RemoveItem(SelectedItem!);
            foreach (var r in results!)
            {
                if (selectedItem!.Isbn == r.Isbn && r.Items != null)
                {
                    r.Items.Remove(SelectedItem!);
                    break;
                }
            }
            db.SaveDataBase();
            selectedItem = null;
            Search();
        }
        private bool IsSelected()
        {
            if (SelectedItem != null)
                return true;
            return false;
        }
        private void Return()
        {
            MainViewModel.Init.CurrentView = MainNavViewModel.Init;
        }
        private void Details()
        {
            if(SelectedItem == null) return;
            MainViewModel.Init.CurrentView = new ReportViewModel();
        }
        private void UpdateCommands()
        {
            UpdateBtn.RaiseCanExecuteChanged();
            RemoveBtn.RaiseCanExecuteChanged();
            DetailsBtn.RaiseCanExecuteChanged();
        }
        #endregion

    }
}
