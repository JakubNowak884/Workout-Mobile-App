using System;
using SQLite;

namespace Workout_Mobile_App.Models
{
    public class Workout
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public int Exercise { get; set; } = 0;
        public int Day { get; set; } = 0;
        public int Week { get; set; } = 0;
        public string Title { get; set; } = string.Empty;
    }
}