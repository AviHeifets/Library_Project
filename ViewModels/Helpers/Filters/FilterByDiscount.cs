using Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Helpers.Filters
{
    internal class FilterByDiscount : IFilter
    {
        public ObservableCollection<AbstractItem> Filter(ObservableCollection<AbstractItem> db, string condition)
        {
            ObservableCollection<AbstractItem> filteredItems = new ObservableCollection<AbstractItem>();

            foreach (AbstractItem item in db)
            {
                switch (condition)
                {
                    case "10<":
                        if (item.Discount < 10)
                        {
                            filteredItems.Add(item);
                        }
                        break;
                    case "10-20":
                        if (item.Discount >= 10 && item.Discount <= 20)
                        {
                            filteredItems.Add(item);
                        }
                        break;
                    case "20-30":
                        if (item.Discount > 20 && item.Price <= 30)
                        {
                            filteredItems.Add(item);
                        }
                        break;
                    case "30+":
                        if (item.Discount > 30)
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
