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

            // Retrieve all the notes from the database, and set them as the
            // data source for the CollectionView.
            var workout = new Workout();
            workout.Name = "New Workout";
            contentPage.Title = workout.Name;
            await App.DatabaseWorkout.SaveNoteAsync(workout);
            int lastInsertID = await App.DatabaseWorkout.GetLastInsertID();
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
                if (day.ID != 0)
                {
                    List<Day> listOfDays = await App.DatabaseDay.GetDaysWithHigherIDAsync(day);
                    foreach (Day element in listOfDays)
                    {
                        int dayIndex = (int)char.GetNumericValue(element.DayText.Last());
                        element.DayText = "Day " + (dayIndex - 1).ToString();
                    }
                    await App.DatabaseDay.DeleteDayAsync(day);
                }
            }
            collectionView.ItemsSource = await App.DatabaseDay.GetDaysAsync(CurrentWorkout);
        }

        async void AddDay(object sender, EventArgs e)
        {
            var note = new Day();
            int lastInsertID = await App.DatabaseDay.GetLastInsertID(CurrentWorkout);
            note.DayText = "Day " + (lastInsertID + 1).ToString();
            note.WorkoutKey = CurrentWorkout;
            await App.DatabaseDay.SaveDayAsync(note);
            collectionView.ItemsSource = await App.DatabaseDay.GetDaysAsync(CurrentWorkout);
        }
    }
}