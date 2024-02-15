using Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Journal : AbstractItem
    {
        public double Volume { get; set; }
        public DateTime PublishDate { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is not Journal)
                return false;

            Journal? otherJournal = obj as Journal;

            return base.Equals(obj) && PublishDate == otherJournal?.PublishDate && Volume == otherJournal.Volume;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Volume, PublishDate);
        }
    }
}
