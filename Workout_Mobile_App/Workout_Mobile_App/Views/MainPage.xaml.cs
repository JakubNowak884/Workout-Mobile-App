﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Workout_Mobile_App.Models;
using Xamarin.Forms;

namespace Workout_Mobile_App.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            collectionView.ItemsSource = await App.DatabaseWorkout.GetWorkoutsAsync();
        }

        async void DeleteWorkout(object sender, EventArgs e)
        {
            SwipeItem item = sender as SwipeItem;
            Workout workout = item.BindingContext as Workout;
            List<Day> listOfDays = await App.DatabaseDay.GetDaysAsync(workout.ID);
            foreach (Day day in listOfDays)
            {
                await App.DatabaseDay.DeleteDayAsync(day);
            }
            await App.DatabaseWorkout.DeleteWorkoutAsync(workout);
            collectionView.ItemsSource = await App.DatabaseWorkout.GetWorkoutsAsync();
        }

        async void OnAddClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(WorkoutPage));
        }

        async void WorkoutSelected(object sender, SelectionChangedEventArgs e)
        {
            bool answer = await DisplayAlert("Question?", "Would you like to play a game", "Yes", "No");
        }
    }
}
//TODO
//usuwanie ćwiczenia, ćwiczeń razem z dniem, ćwiczeń razem z workoutem
//swoje cwiczenie: rodzaj progresu(standard, 5/3/1), domyślny progres w kg, nazwa, ciężar startowy