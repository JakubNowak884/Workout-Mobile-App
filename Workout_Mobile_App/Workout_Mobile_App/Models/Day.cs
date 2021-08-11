using System;
using SQLite;

namespace Workout_Mobile_App.Models
{
    public class Day
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
    }
}
