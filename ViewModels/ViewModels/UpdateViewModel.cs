using BookLib.DatServices;
using Models.Helpers;
using Models.Models;
using System.Collections.ObjectModel;
using ViewModels.Helpers;
using ViewModels.Helpers.Validations;

namespace ViewModels.ViewModels
{
    public class UpdateViewModel : ViewModelBase
    {
        public UpdateViewModel()
        {
            //get item from db
            
            FillFields();
            PropertyChanged += (sender, e) => UpdateBtn.RaiseCanExecuteChanged();
            validations = InsertValidations()!;
        }

        //db instance
        private readonly ItemCollection db = ItemCollection.Init;

        #region Commands
        private RelayCommand ?updateBtn;
        public RelayCommand UpdateBtn
        {
            get 
            {
                if (updateBtn == null)
                    updateBtn = new RelayCommand(execute => Update(), canExecute => CanUpdate());
                return updateBtn!; 
            }
     
        }


        private RelayCommand ?returnBtn;
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

        #region props
        private string? name;
        private string? author;
        private string? price;
        private string? discount;
        private string? volume;
        private List<Catagory> catagories = new List<Catagory>();
        private bool isBook = false;
        private ObservableCollection<AbstractItem> ?selectedItem;
        private bool isValidated = false;
        private List<Func<bool>> validations;

        public string Volume
        {
            get { return volume!; }
            set
            {
                if (volume == value) return;
                volume = value;
                OnPropertyChanged();
            }
        }
        public List<Catagory> Catagories
        {
            get { return catagories; }
            set
            {
                catagories = value;
                OnPropertyChanged();
            }
        }
        public string Name
        {
            get { return name!; }
            set
            {
                if (name == value) return;
                name = value;
                OnPropertyChanged();
            }
        }
        public string Author
        {
            get { return author!; }
            set
            {
                if (author == value) return;
                author = value;
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
        public bool IsBook
        {
            get { return isBook; }
            set
            {
                if (isBook == value) return;
                isBook = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<AbstractItem> SelectedItem
        {
            get { return selectedItem!; }
            set
            {
                selectedItem = value;
                OnPropertyChanged();
            }
        }
        public Catagory[] Options { get; set; } = (Catagory[])Enum.GetValues(typeof(Catagory));
        public bool IsValidated
        {
            get { return isValidated; }
            set 
            {
                if (isValidated == value) { return; }
                isValidated = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region functions
        //return to previous screen
        private void Return()
        {
            SearchViewModel.Init.SelectedItem = null!;
            MainViewModel.Init.CurrentView = SearchViewModel.Init;
        }

        //update item
        private void Update()
        {
            //update item
            foreach (var item in selectedItem!)
            {
                item.Name = Name;
                item.Author = Author;
                item.Price = double.Parse(Price);
                item.Discount = int.Parse(Discount);
                item.Catagories = Catagories;
                if (!IsBook)
                {
                    Journal? i = item as Journal;
                    i!.Volume = double.Parse(volume!);
                }
            }
            db.SaveDataBase();
            ClearFields();
        }

        //Validate before updating
        private bool CanUpdate() 
        {
            List<Func<bool>> validate = validations;

            foreach (Func<bool> function in validate)
            {
                if (!function() || function == null)
                    return false;
            }

            return true;
        }

        //inserts the validation list
        private List<Func<bool>>? InsertValidations()
        {
            ValidationHelper validations = ValidationHelper.Init;
            List<Func<bool>> validate = new List<Func<bool>>();

            validate.Add(() => validations.ValidateString(Name!, false));
            validate.Add(() => validations.ValidateString(Author!, false));
            validate.Add(() => validations.ValidateDouble(Price, "1000", false));
            validate.Add(() => validations.ValidateInt(Discount!, "100", true));

            if (!IsBook)
                validate.Add(() => validations.ValidateDouble(Volume!, "1000", false));

            return validate;
        }

        //fills fields with chosen items details
        private void FillFields()
        {
            selectedItem = new ObservableCollection<AbstractItem>();

            foreach (var item in db.GetItemsByIbsn(SearchViewModel.Init.SelectedItem.Isbn.ToString())!)
                selectedItem.Add(item);

            //check if journal or book
            try
            {
                foreach (var i in selectedItem)
                {
                    Journal ?item = i as Journal;
                    volume = item!.Volume.ToString();
                }
            }
            catch (Exception) { isBook = true; }

            //copy props to not change db directly 
            Catagories = selectedItem[0].Catagories!;
            name = selectedItem[0].Name;
            author = selectedItem[0].Author;
            price = selectedItem[0].Price.ToString();
            discount = selectedItem[0].Discount.ToString();
        }

        //clear fields after updating
        private void ClearFields()
        {
            Name = null!;
            Author = null!;
            Price = null!;
            Discount = null!;
            Volume = null!;
            Catagories = new List<Catagory>();
        }
        #endregion
    }
}
