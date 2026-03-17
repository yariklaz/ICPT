using System;
using System.Collections.Generic;

namespace QuestBooking.Domain.Model
{
    public partial class Booking : Entity
    {

        public int ClientId { get; set; }

        public int SlotId { get; set; }

        public int? PromocodeId { get; set; }

        public decimal TotalPrice { get; set; }

        public string Status { get; set; }

        public DateTime? CreatedAt { get; set; }

        public virtual Client Client { get; set; }

        public virtual Promocode Promocode { get; set; }

        public virtual Timeslot Slot { get; set; }

        public virtual ICollection<Extraservice> Services { get; set; } = new List<Extraservice>();
    }
}
