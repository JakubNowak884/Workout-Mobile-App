using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Workout_Mobile_App.Models;
using Xamarin.Forms;

namespace Workout_Mobile_App.Views
{
    public partial class NewExercisePage : ContentPage
    {
        public NewExercisePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var exercise = new ExerciseDraft()
            {
                Name = "New Exercise",
                Type = TypeOfProgress.Standard,
                Progress = 1.0f
            };

            contentPage.Title = exercise.Name;
            await App.DatabaseExerciseDraft.SaveDraftExerciseAsync(exercise);
            BindingContext = exercise;
        }

        async void ChangeName(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Change Name", "Type new name:");
            ExerciseDraft exercise = (ExerciseDraft)BindingContext;
            exercise.Name = result;
            await App.DatabaseExerciseDraft.UpdateDraftExerciseAsync(exercise);
            contentPage.Title = exercise.Name;
        }
        async void OnChangeTypeClicked(object sender, EventArgs e)
        {
            ExerciseDraft exercise = (ExerciseDraft)BindingContext;

            if (exercise.Type == Enum.GetValues(typeof(TypeOfProgress)).Cast<TypeOfProgress>().Last())
            {
                exercise.Type = 0;
            }
            else
            {
                exercise.Type++;
            }

            await App.DatabaseExerciseDraft.UpdateDraftExerciseAsync(exercise);
            BindingContext = await App.DatabaseExerciseDraft.GetDraftExerciseAsync(exercise.ID);
        }
        async void OnChangeProgressClicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Progress (in kg)", "Type new progress:");
            ExerciseDraft exercise = (ExerciseDraft)BindingContext;
            try
            {
                exercise.Progress = float.Parse(result);
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Typed value is empty.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Typed value is not a number.");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Typed value is out of range.");
            }

            await App.DatabaseExerciseDraft.UpdateDraftExerciseAsync(exercise);
            BindingContext = await App.DatabaseExerciseDraft.GetDraftExerciseAsync(exercise.ID);
        }
    }
}