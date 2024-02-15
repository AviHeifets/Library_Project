using BookLib.DatServices;
using Models.Helpers;
using Models.Models;
using ViewModels.Helpers;
using ViewModels.Helpers.Validations;

namespace ViewModels.ViewModels
{
    public class AddViewModel : ViewModelBase
    {
        //data base singelton
        private readonly ItemCollection db = ItemCollection.Init;

        public AddViewModel()
        {
            addBtn = new RelayCommand(execute => Add(IsBook), canExecute => CanAdd());
            PropertyChanged += (sender, e) =>  addBtn!.RaiseCanExecuteChanged();
            validations = InsertValidations();
            journalDatePicker = DateTime.Now;
            BookDatePicker = DateTime.Now.Year.ToString();
        }

        #region Commands
        //Button commands
        private RelayCommand ?addBtn;
        public RelayCommand AddBtn
        {
            get 
            {
                if (addBtn == null)
                    addBtn = new RelayCommand(execute => Add(IsBook), canExecute => CanAdd());
                return addBtn;
            }
        }

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

        #region props

        public Catagory[] Options { get; set; } = (Catagory[])Enum.GetValues(typeof(Catagory));
        private string? name;
        private string? author;
        private string? isbn;
        private string? price;
        private string? discount;
        private string? volume;
        private string? bookDatePicker;
        private string? amount;
        private List<Catagory> catagories = new List<Catagory>();
        private DateTime journalDatePicker;
        private bool isBook = true;
        private List<Func<bool>> validations;
        private string? errorMsg;

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
        public string Isbn
        {
            get { return isbn!; }
            set
            {
                if (isbn == value) return;
                isbn = value;
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
        public string BookDatePicker
        {
            get { return bookDatePicker!; }
            set
            {
                if (bookDatePicker == value) return;
                bookDatePicker = value;
                OnPropertyChanged();
            }
        }
        public string Amount
        {
            get { return amount!; }
            set
            {
                if (amount == value) return;
                amount = value;
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
        public DateTime JournalDatePicker
        {
            get { return journalDatePicker; }
            set
            {
                if (journalDatePicker == value) return;
                journalDatePicker = value;
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
                validations = InsertValidations();
            }
        }
        public string ErrorMsg
        {
            get { return errorMsg!; }
            set 
            {
                if (errorMsg == value) return;
                errorMsg = value; 
                OnPropertyChanged();
            }
        }
        #endregion

        #region functions
        //adds an item
        private void Add(bool Isbook)
        {
            AbstractItem item;
            DateTime AddedToLibrary = DateTime.Now;
            if (discount == null || discount == "")
                discount = "0";
            int num = int.Parse(Amount);

            try
            {
                if (Isbook)
                {
                    for (int i = 0; i < num; i++)
                    {
                        Book book = new Book() 
                        {
                            Isbn = double.Parse(Isbn), 
                            Name = this.Name, 
                            Author = this.Author, 
                            Price = double.Parse(this.Price), 
                            Catagories = this.Catagories,
                            AddedToLibrary = AddedToLibrary, 
                            PublishYear = int.Parse(this.bookDatePicker!), 
                            Discount = int.Parse(Discount) 
                        };
                        item = book;
                        db.AddItem(item);
                    }
                }
                else
                {
                    for (int i = 0; i < num; i++)
                    {
                        Journal journal = new Journal()
                        {
                            Isbn = double.Parse(Isbn),
                            Name = this.Name,
                            Author = this.Author,
                            Price = double.Parse(this.Price),
                            Catagories = this.Catagories,
                            AddedToLibrary = AddedToLibrary,
                            Volume = double.Parse(this.Volume),
                            PublishDate = JournalDatePicker,
                            Discount = int.Parse(Discount)
                        };
                        item = journal;
                        db.AddItem(item);
                    }
                }
            }
            catch (Exception ex ) 
            {
                ErrorMsg = (ex.Message);
            }
            
            ClearFields();
        }

        //Executes list of validations
        private bool CanAdd()
        {
            List<Func<bool>> validate = validations;

            foreach (Func<bool> function in validate)
            {
                if (!function() || function == null)
                {
                    ErrorMsg = "One or more fields are incorrect!";
                    return false;
                }
      
            }

            ErrorMsg = string.Empty;
            return true ;
        }

        //All abstract item validations
        private List<Func<bool>> InsertValidations()
        {
            ValidationHelper validations = ValidationHelper.Init;
            List<Func<bool>> validate = new List<Func<bool>>();

            validate.Add(() => validations.ValidateString(Name!, false));
            validate.Add(() => validations.ValidateString(Author!, false));
            validate.Add(() => validations.ValidateIsbn(Isbn!, false));
            validate.Add(() => validations.ValidateDouble(Price, "1000", false));
            validate.Add(() => validations.ValidateInt(Discount!, "100", true));
            validate.Add(() => validations.ValidateInt(Amount, "100", false));

            if (IsBook)
                validate.Add(() => validations.ValidateInt(BookDatePicker, DateTime.Now.Year.ToString(), false));
            else
            {
                validate.Add(() => validations.ValidateDate(JournalDatePicker));
                validate.Add(() => validations.ValidateDouble(Volume!, "1000", false));
            }
            return validate;
        }
        //return to previous screen
        private void Return()
        {
            MainViewModel.Init.CurrentView = MainNavViewModel.Init;
        }

        //clears fields after adding item
        private void ClearFields()
        {
            Name = null!;
            Author = null!;
            Isbn = null!;
            Price = null!;
            Discount = null!;
            Volume = null!;
            BookDatePicker = null!;
            Amount = null!;
            Catagories = new List<Catagory>();
        }
        #endregion
    }
}
