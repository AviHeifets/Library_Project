using BookLib.DatServices;
using BookLib.Exceptions;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLib.Helpers
{
    internal class DbValidations
    {
        //singleton
        public static DbValidations Init { get; } = new DbValidations();
        private DbValidations() { }

        public bool AddItemValidation(AbstractItem item)
        {
            ItemCollection db = ItemCollection.Init; ;
            if (item == null) 
                throw new ArgumentNullException("item is null");

            while (db.GetItemsById(item.Id.ToString()).Any())
            {
                item.Id++;
            }

            IEnumerable<AbstractItem> list = db.GetItemsByIbsn(item.Isbn.ToString()!)!;

            foreach (AbstractItem i in list)
            {
                if (item.Name != i.Name || item.Author != i.Author || item.Price != i.Price)
                {
                    throw new AddItemValidationException("Isbn allready exists");
                }
                break;
            }
            return true;
        }
    }
}
