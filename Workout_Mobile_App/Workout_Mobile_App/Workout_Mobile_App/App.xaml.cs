using System;
using System.IO;
using Workout_Mobile_App.Data;
using Xamarin.Forms;

namespace Workout_Mobile_App
{
    public partial class App : Application
    {
        static WorkoutDatabase database;

        // Create the database connection as a singleton.
        public static WorkoutDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new WorkoutDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Notes.db3"));
                }
                return database;
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