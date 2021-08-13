using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Workout_Mobile_App.Models;
using Xamarin.Forms;

namespace Workout_Mobile_App.Views
{
    public partial class WorkoutPage : ContentPage
    {
        public int CurrentWorkout;
        public WorkoutPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var workout = new Workout();
            workout.Name = "New Workout";
            contentPage.Title = workout.Name;
            await App.DatabaseWorkout.SaveWorkoutAsync(workout);
            int lastInsertID = await App.DatabaseWorkout.GetLastInsertedID();
            CurrentWorkout = lastInsertID;
            var note = new Day();
            note.DayText = "Day 1";
            note.WorkoutKey = CurrentWorkout;
            await App.DatabaseDay.SaveDayAsync(note);
            collectionView.ItemsSource = await App.DatabaseDay.GetDaysAsync(CurrentWorkout);
        }

        async void DeleteWorkout(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                Day day = (Day)e.CurrentSelection.FirstOrDefault();

                List<Day> listOfDays = await App.DatabaseDay.GetDaysWithHigherIDAsync(day);
                foreach (Day element in listOfDays)
                {
                    int dayIndex = (int)char.GetNumericValue(element.DayText.Last());
                    element.DayText = "Day " + (dayIndex - 1).ToString();
                    await App.DatabaseDay.UpdateDayAsync(element);
                }
                await App.DatabaseDay.DeleteDayAsync(day);
            }
            collectionView.ItemsSource = await App.DatabaseDay.GetDaysAsync(CurrentWorkout);
        }

        async void AddDay(object sender, EventArgs e)
        {
            var note = new Day();
            int lastInsertID = await App.DatabaseDay.GetLastInsertedID(CurrentWorkout);
            note.DayText = "Day " + (lastInsertID + 1).ToString();
            note.WorkoutKey = CurrentWorkout;
            await App.DatabaseDay.SaveDayAsync(note);
            collectionView.ItemsSource = await App.DatabaseDay.GetDaysAsync(CurrentWorkout);
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