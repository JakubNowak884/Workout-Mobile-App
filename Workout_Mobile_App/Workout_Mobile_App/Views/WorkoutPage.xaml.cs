using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Workout_Mobile_App.Models;
using Xamarin.Forms;

namespace Workout_Mobile_App.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class WorkoutPage : ContentPage
    {
        public string ItemId
        {
            set
            {
                LoadWorkout(value);
            }
        }
        public int CurrentWorkout;
        public WorkoutPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            Workout workout = await App.DatabaseWorkout.GetWorkoutAsync(CurrentWorkout);
            contentPage.Title = workout.Name;

            collectionView.ItemsSource = await App.DatabaseDay.GetDaysAsync(CurrentWorkout);
        }

        async void DeleteDay(object sender, EventArgs e)
        {
            SwipeItem item = sender as SwipeItem;
            Day day = item.BindingContext as Day;

            List<Day> listOfDays = await App.DatabaseDay.GetDaysWithHigherIDAsync(day);
            foreach (Day element in listOfDays)
            {
                int dayIndex = (int)char.GetNumericValue(element.Name.Last());
                element.Name = "Day " + (dayIndex - 1).ToString();
                await App.DatabaseDay.UpdateDayAsync(element);
            }

            List<Exercise> listOfExercises = await App.DatabaseExercise.GetExercisesAsync(day.ID);
            foreach (Exercise exercise in listOfExercises)
            {
                await App.DatabaseExercise.DeleteExerciseAsync(exercise);
            }
            await App.DatabaseDay.DeleteDayAsync(day);

            collectionView.ItemsSource = await App.DatabaseDay.GetDaysAsync(CurrentWorkout);
        }

        async void DaySelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                Day day = (Day)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync($"{nameof(DayPage)}?{nameof(DayPage.ItemId)}={day.ID.ToString()}");
            }
        }

        async void AddDay(object sender, EventArgs e)
        {
            var note = new Day();
            int lastInsertID = await App.DatabaseDay.CountDays(CurrentWorkout);
            note.Name = "Day " + (lastInsertID + 1).ToString();
            note.WorkoutKey = CurrentWorkout;
            await App.DatabaseDay.SaveDayAsync(note);
            collectionView.ItemsSource = await App.DatabaseDay.GetDaysAsync(CurrentWorkout);
        }
        void LoadWorkout(string itemId)
        {
            try
            {
                CurrentWorkout = Convert.ToInt32(itemId);
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load workout.");
            }
        }
        async void ChangeName(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Change Name", "Type new name:");
            Workout workout = await App.DatabaseWorkout.GetWorkoutAsync(CurrentWorkout);
            workout.Name = result;
            await App.DatabaseWorkout.UpdateWorkoutAsync(workout);
            contentPage.Title = workout.Name;
        }
    }
}