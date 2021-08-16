using System;
using SQLite;

namespace Workout_Mobile_App.Models
{
    public class Exercise
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public TypeOfProgress Type { get; set; }
        public float Progress { get; set; }
        public float Deload { get; set; }
        public float Weight { get; set; }
        public int DayKey { get; set; }
    }
}
