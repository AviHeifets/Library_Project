using BookLib.DatServices;
using Models.Models;
using System.Collections.ObjectModel;

namespace ViewModels.Helpers.Filters
{
    internal class FilterBySearchOption
    {
        public ObservableCollection<AbstractItem> Filter(ItemCollection db, string condition, string input)
        {

            ObservableCollection<AbstractItem> filteredItems = new ObservableCollection<AbstractItem>();

            switch (condition)
            {
                case "Id":
                    foreach (var item in db.GetItemsById(input))
                    {
                        filteredItems.Add(item);
                    }
                    break;

                case "Isbn":
                    foreach (var item in db.GetItemsByIbsn(input)!)
                    {
                        filteredItems.Add(item);
                    }
                    break;

                case "Name":
                    foreach (var item in db.GetItemsByName(input)!)
                    {
                        filteredItems.Add(item);
                    }
                    break;

                case "Author":
                    foreach (var item in db.GetItemsByAuthor(input)!)
                    {
                        filteredItems.Add(item);
                    }
                    break;

                default:
                    foreach (var item in db.GetAllItems()!)
                    {
                        filteredItems.Add(item);
                    }
                    break;
            }
            return filteredItems;
        }
    }
}