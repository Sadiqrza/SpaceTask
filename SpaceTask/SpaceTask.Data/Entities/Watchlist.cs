using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceTask.Data.Entities
{
    public class Watchlist
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string MovieName { get; set; }
        public string MovieId { get; set; }
        public DateTime SendingMailTime { get; set; }
        public bool IsWatched { get; set; }
    }
}
