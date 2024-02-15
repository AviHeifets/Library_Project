using Models.Helpers;

namespace Models.Models
{
    public class Book : AbstractItem
    {
        public int PublishYear { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is not Book)
                return false;

            Book? otherBook = obj as Book;

            return base.Equals(obj) && PublishYear == otherBook?.PublishYear;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), 13 * Id);
        }

    }


}
