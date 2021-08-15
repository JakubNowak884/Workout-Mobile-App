using System;
using SQLite;

namespace Workout_Mobile_App.Models
{
    public class Exercise
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
