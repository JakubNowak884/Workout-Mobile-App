using System;
using SQLite;

namespace Workout_Mobile_App.Models
{
    public class Workout
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}