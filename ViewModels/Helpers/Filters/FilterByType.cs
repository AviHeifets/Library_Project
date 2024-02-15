using Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Helpers.Filters
{
    internal class FilterByType : IFilter
    {
        public ObservableCollection<AbstractItem> Filter(ObservableCollection<AbstractItem> db, string condition)
        {
            ObservableCollection<AbstractItem> filteredItems = new ObservableCollection<AbstractItem>();

            if (condition == "All")
                return db;

            foreach (AbstractItem item in db)
            {
                if (condition == "Book" && item is Book)
                {
                    filteredItems.Add(item);
                }
                else if (condition == "Journal" && item is Journal)
                {
                    filteredItems.Add(item);
                }
            }
            return filteredItems;
        }
    }
}
