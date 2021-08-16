using System;
using SQLite;

namespace Workout_Mobile_App.Models
{
    public enum TypeOfProgress
    {
        Standard,
        JimWendler
    }
    public class ExerciseDraft
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public TypeOfProgress Type { get; set; }
        public float Progress { get; set; }
    }
}
