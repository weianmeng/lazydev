using System;

namespace LazyDev.Log.Postgres.Models
{
    public class LogMessageModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime CreatedTime { get; set; }

    }
}
