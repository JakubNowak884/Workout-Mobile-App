using System;
using System.IO;
using Workout_Mobile_App.Data;
using Xamarin.Forms;
using System.Collections.Generic;

namespace Workout_Mobile_App
{
    public partial class App : Application
    {
        static WorkoutDatabase databaseWorkout;
        static DayDatabase databaseDay;
        static ExerciseDatabase databaseExercise;

        // Create the database connection as a singleton.
        public static WorkoutDatabase DatabaseWorkout
        {
            get
            {
                if (databaseWorkout == null)
                {
                    databaseWorkout = new WorkoutDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DatabaseWorkout.db3"));
                }
                return databaseWorkout;
            }
        }

        public static DayDatabase DatabaseDay
        {
            get
            {
                if (databaseDay == null)
                {
                    databaseDay = new DayDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DatabaseDay.db3"));
                }
                return databaseDay;
            }
        }

        public static ExerciseDatabase DatabaseExercise
        {
            get
            {
                if (databaseExercise == null)
                {
                    databaseExercise = new ExerciseDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DatabaseExercise.db3"));
                }
                return databaseExercise;
            }
        }

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}