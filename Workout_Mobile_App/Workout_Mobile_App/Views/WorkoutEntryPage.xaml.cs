using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Workout_Mobile_App.Models;
using Xamarin.Forms;
using System.Diagnostics;

namespace Workout_Mobile_App.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class WorkoutEntryPage : ContentPage
    {
        private string text = string.Empty;
        private string textTitle = string.Empty;
        public string ItemId
        {
            set
            {
                LoadWorkout(value);
            }
        }

        public int CurrentWorkoutID;

        public Exercise CurrentExercise;

        public WorkoutEntryPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            Workout workout = await App.DatabaseWorkout.GetWorkoutAsync(CurrentWorkoutID);
            List<Day> listOfDays = await App.DatabaseDay.GetDaysAsync(workout.ID);
            List<Exercise> listOfExercises = await App.DatabaseExercise.GetExercisesAsync(listOfDays[workout.Day].ID);
            CurrentExercise = listOfExercises[workout.Exercise];
            contentPage.Title = CurrentExercise.Name.ToString() + " - Week " + workout.Week.ToString();

            MainViewModel test = new MainViewModel()
            {
                Text = text
            };
            BindingContext = test;
        }

        void LoadWorkout(string itemId)
        {
            try
            {
                CurrentWorkoutID = Convert.ToInt32(itemId);
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load day.");
            }
        }

        async void SetBindingContext()
        {
            Workout workout = await App.DatabaseWorkout.GetWorkoutAsync(CurrentWorkoutID);
            List<Day> listOfDays = await App.DatabaseDay.GetDaysAsync(workout.ID);
            List<Exercise> listOfExercises = await App.DatabaseExercise.GetExercisesAsync(listOfDays[workout.Day].ID);

            if (workout.Exercise == (listOfExercises.Count() - 1))
            {
                workout.Exercise = 0;
                if (workout.Day == (listOfDays.Count() - 1))
                {
                    workout.Day = 0;
                    workout.Week++;
                }
                else
                {
                    workout.Day++;
                }
            }
            else
            {
                workout.Exercise++;
            }

            await App.DatabaseWorkout.UpdateWorkoutAsync(workout);

            listOfDays = await App.DatabaseDay.GetDaysAsync(workout.ID);
            listOfExercises = await App.DatabaseExercise.GetExercisesAsync(listOfDays[workout.Day].ID);
            CurrentExercise = listOfExercises[workout.Exercise];

            textTitle = CurrentExercise.Name.ToString() + " - Week " + (workout.Week + 1).ToString();
            float percentage = 0;
            float addToPercentage = 0;
            switch (CurrentExercise.Type)
            {
                case TypeOfProgress.Standard:
                    text = "Today's weight: " + CurrentExercise.Weight.ToString();
                    break;

                case TypeOfProgress.Jim_Wendler:
                    switch (workout.Week % 4)
                    {
                        case 0:
                            percentage = 0.75f;
                            addToPercentage = 0.05f;
                            break;
                        case 1:
                            percentage = 0.80f;
                            addToPercentage = 0.05f;
                            break;
                        case 2:
                            percentage = 0.75f;
                            addToPercentage = 0.10f;
                            break;
                        case 3:
                            percentage = 0.40f;
                            addToPercentage = 0.10f;
                            break;
                        default:
                            break;
                    }
                    text = "Today's weight for first set: " + (CurrentExercise.Weight * percentage).ToString() + '\n';
                    percentage += addToPercentage;
                    text += "Today's weight for second set: "+ (CurrentExercise.Weight * percentage).ToString() + '\n';
                    percentage += addToPercentage;
                    text += "Today's weight for third set: " + (CurrentExercise.Weight * percentage).ToString() + '\n';
                    break;

                case TypeOfProgress.Jim_Wendler_Plus_Sidework:
                    switch (workout.Week % 4)
                    {
                        case 0:
                            percentage = 0.75f;
                            addToPercentage = 0.05f;
                            break;
                        case 1:
                            percentage = 0.80f;
                            addToPercentage = 0.05f;
                            break;
                        case 2:
                            percentage = 0.75f;
                            addToPercentage = 0.10f;
                            break;
                        case 3:
                            percentage = 0.40f;
                            addToPercentage = 0.10f;
                            break;
                    }
                    text = "Today's weight for first set: " + (CurrentExercise.Weight * percentage).ToString() + '\n';
                    percentage += addToPercentage;
                    text += "Today's weight for second set: " + (CurrentExercise.Weight * percentage).ToString() + '\n';
                    percentage += addToPercentage;
                    text += "Today's weight for third set: " + (CurrentExercise.Weight * percentage).ToString() + '\n';
                    text += "Today's weight for 5x10: " + (CurrentExercise.Weight * 0.6).ToString() + '\n';
                    break;

                default:
                    break;
            }
            MainViewModel test = (MainViewModel)BindingContext;
            test.Text = text;
            BindingContext = test;
            workout.Title = textTitle;
            await App.DatabaseWorkout.UpdateWorkoutAsync(workout);
            contentPage.Title = workout.Title;
        }
        async void OnContinueWithDefaultProgressClicked(object sender, EventArgs e)
        {
            CurrentExercise.Weight += CurrentExercise.Progress;
            await App.DatabaseExercise.UpdateExerciseAsync(CurrentExercise);

            SetBindingContext();
        }
        async void OnContinueWithAlternativeProgressClicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Progress (in kg)", "Type alternative progress:");
            try
            {
                CurrentExercise.Progress = float.Parse(result);
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
            await App.DatabaseExercise.UpdateExerciseAsync(CurrentExercise);

            SetBindingContext();
        }
        async void OnContinueWithoutProgressClicked(object sender, EventArgs e)
        {
            await App.DatabaseExercise.UpdateExerciseAsync(CurrentExercise);

            SetBindingContext();
        }
        async void OnDeloadClicked(object sender, EventArgs e)
        {
            CurrentExercise.Weight *= CurrentExercise.Deload * 0.01f;
            await App.DatabaseExercise.UpdateExerciseAsync(CurrentExercise);

            SetBindingContext();
        }
    }
}