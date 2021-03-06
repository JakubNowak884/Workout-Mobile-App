using System;
using SQLite;

namespace Workout_Mobile_App.Models
{
    public enum TypeOfProgress
    {
        Standard,
        Jim_Wendler,
        Jim_Wendler_Plus_Sidework
    }
    public class ExerciseDraft
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public TypeOfProgress Type { get; set; }
        public float Progress { get; set; }
        public float Deload { get; set; }
    }
}
