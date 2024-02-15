using Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Helpers
{
    public class ObservableDictionary : INotifyPropertyChanged
    {
        public ObservableDictionary()
        {
            Items = new ObservableCollection<AbstractItem>();
            items!.CollectionChanged += OnCollectionChanged!;
        }

        #region Props
        private double isbn;
        private int length;
        private ObservableCollection<AbstractItem>? items;

        public double Isbn
        {
            get { return isbn; }
            set {
                if (value ==  isbn) { return; }
                isbn = value;
                OnPropertyChanged();
            }
        }
        public int Length
        {
            get { return length; }
            set 
            { 
                if (value == length) { return; }
                length = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<AbstractItem>? Items
        {
            get { return items; }
            set 
            {
                items = value;
                Length = items!.Count;
                OnPropertyChanged();
            }
        }
        #endregion

        //updates length on the collection changed
        public void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) => Length = Items!.Count;


        //IPropertyChanged implamentation
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null!) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
