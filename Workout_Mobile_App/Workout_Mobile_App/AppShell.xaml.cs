﻿using Workout_Mobile_App.Views;
using Xamarin.Forms;

namespace Workout_Mobile_App
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(WorkoutPage), typeof(WorkoutPage));
        }
    }
}