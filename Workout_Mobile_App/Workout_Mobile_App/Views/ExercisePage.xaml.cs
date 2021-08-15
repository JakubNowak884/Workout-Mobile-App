using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Workout_Mobile_App.Models;
using Xamarin.Forms;

namespace Workout_Mobile_App.Views
{
    public partial class ExercisePage : ContentPage
    {
        public ExercisePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            Exercise exercise = new Exercise();
            exercise.Name = "New exercise";
            await App.DatabaseExercise.SaveExerciseAsync(exercise);

            BindingContext = exercise;
            contentPage.Title = exercise.Name;
        }
    }
}