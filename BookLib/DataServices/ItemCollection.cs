using BookLib.Helpers;
using Models.Models;
using System.IO;
using Newtonsoft.Json;


namespace BookLib.DatServices
{
    public class ItemCollection
    {
        //used dictionary for getting faster values using ibsn , also keeps track of how many copies of each book there are
        private readonly Dictionary<double, List<AbstractItem>>? items;
        private readonly DbValidations validator;

        //singelton 
        public static ItemCollection Init { get; } = new ItemCollection();
        private ItemCollection()
        {
            items = LoadData();
            validator = DbValidations.Init;
        }

        #region Functions
        public IEnumerable<AbstractItem> this[string isbn] => GetItemsByIbsn(isbn)!;

        //return IEnumerables for readonly
        public IEnumerable<AbstractItem>? GetAllItems()
        {
            foreach (var value in items!.Values)
            {
                foreach (var item in value)
                {
                    yield return item;
                }
            }
        }

        public IEnumerable<AbstractItem>? GetItemsByIbsn(string Sisbn)
        {
            double isbn = double.Parse(Sisbn);

            if (items!.TryGetValue(isbn, out List<AbstractItem>? itemList))
            {
                foreach (var item in itemList)
                {
                    yield return item;
                }
            }
        }

        public IEnumerable<AbstractItem> GetItemsById(string Sid)
        {
            foreach (AbstractItem item in GetAllItems()!)
            {
                int id = int.Parse(Sid);
                if (item.Id == id)
                    yield return item;
            }
        }

        public IEnumerable<AbstractItem>? GetItemsByName(string name)
        {
            foreach (AbstractItem item in GetAllItems()!)
            {
                if (item.Name == name)
                    yield return item;
            }
        }

        public IEnumerable<AbstractItem>? GetItemsByAuthor(string author)
        {
            foreach (AbstractItem item in GetAllItems()!)
            {
                if (item.Author == author)
                    yield return item;
            }
        }

        public void AddItem(AbstractItem item)
        {
            validator.AddItemValidation(item);

            if (!items!.ContainsKey(item.Isbn))
                items![item.Isbn] = new List<AbstractItem>();
            items[item.Isbn].Add(item);
            SaveDataBase();
        }

        public void RemoveItem(AbstractItem item)
        {
            if (item == null)
                return;

            var list = items![item.Isbn];
            list.Remove(item);
        }

        //NOT IMPLAMENTED
        public void Borrow(AbstractItem item)
        {

            if (item.IsAvalible) // TODO: add item to person who took it
            {
                item.IsAvalible = false;
                return;
            }

            //throw new ItemNotAvalibleException();
        }

        public void SaveDataBase()
        {
            string baseDirectory = Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.Parent!.FullName;
            string relativePath = @"BookLib\DataServices\Db.json";

            // Combine with the relative path to get the full path
            string fullPath = Path.Combine(baseDirectory, relativePath);

            string json = JsonConvert.SerializeObject(items, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });
            File.WriteAllText(fullPath, json);
        }

        private Dictionary<double, List<AbstractItem>> LoadData()
        {
            string baseDirectory = Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.Parent!.FullName;
            string relativePath = @"BookLib\DataServices\Db.json";
            string fullPath = Path.Combine(baseDirectory, relativePath);

            try
            {
                string json = File.ReadAllText(fullPath);
                var items = JsonConvert.DeserializeObject<Dictionary<double, List<AbstractItem>>>(json, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                });
                return items!;
            }
            catch
            {
                return new Dictionary<double, List<AbstractItem>>();
            }
           
        }
        #endregion
    }
}
