using Models.Helpers;

namespace Models.Models
{
    public abstract class AbstractItem
    {
        public double Isbn { get; set; }
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Author { get; set; }
        public double Price { get; set; }
        public List<Catagory> Catagories
        {
            get { return catagories!; }
            set
            {
                List<Catagory> copy = new List<Catagory>();
                foreach (var catagory in value)
                {
                    copy.Add(catagory);
                }
                catagories = copy;
            }
        }
        public DateTime AddedToLibrary { get; set; }
        public bool IsAvalible { get; set; }
        public double Discount { get; set; }

        private List<Catagory> ?catagories;
        private static int IdCounter;

        public AbstractItem()
        {
            Id = IdCounter++;
            IsAvalible = true;
            Discount = 0;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id.GetHashCode(), Name?.GetHashCode(), Author?.GetHashCode());
        }

        public override bool Equals(object? obj)
        {
            if (obj is not AbstractItem otherItem)
                return false;

            return Id == otherItem.Id;
        }

    }
}
