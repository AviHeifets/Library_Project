using Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Helpers.Filters
{
    internal class FilterByPrice : IFilter
    {
        public ObservableCollection<AbstractItem> Filter(ObservableCollection<AbstractItem> db, string condition)
        {
            ObservableCollection<AbstractItem> filteredItems = new ObservableCollection<AbstractItem>();

            foreach (AbstractItem item in db)
            {
                switch (condition)
                {
                    case "50<":
                        if (item.Price < 50)
                        {
                            filteredItems.Add(item);
                        }
                        break;
                    case "50-99":
                        if (item.Price >= 50 && item.Price <= 99)
                        {
                            filteredItems.Add(item);
                        }
                        break;
                    case "100-149":
                        if (item.Price >= 100 && item.Price <= 149)
                        {
                            filteredItems.Add(item);
                        }
                        break;
                    case "150+":
                        if (item.Price >= 150)
                        {
                            filteredItems.Add(item);
                        }
                        break;
                    default:
                        filteredItems = db;
                        break;
                }
            }
            return filteredItems;
        }
    }
}
