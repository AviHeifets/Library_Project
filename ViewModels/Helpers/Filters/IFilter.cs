using Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Helpers.Filters
{
    internal interface IFilter
    {
        public ObservableCollection<AbstractItem> Filter(ObservableCollection<AbstractItem> db, string condition);
    }
}
