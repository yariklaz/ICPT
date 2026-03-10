using System;
using System.Collections.Generic;

namespace QuestBooking.Domain.Model
{
    public partial class Questroom : Entity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int MaxPlayers { get; set; }

        public decimal BasePrice { get; set; }

        public int? DurationMinutes { get; set; }

        public virtual ICollection<Timeslot> Timeslots { get; set; } = new List<Timeslot>();
    }
}