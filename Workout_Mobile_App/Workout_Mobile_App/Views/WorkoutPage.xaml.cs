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
        public WorkoutPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Retrieve all the notes from the database, and set them as the
            // data source for the CollectionView.
            collectionView.ItemsSource = await App.Database.GetNotesAsync();
        }

        async void DeleteWorkout(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                Workout workout = (Workout)e.CurrentSelection.FirstOrDefault();
                await App.Database.DeleteNoteAsync(workout);
            }
            collectionView.ItemsSource = await App.Database.GetNotesAsync();
        }

        async void AddDay(object sender, EventArgs e)
        {
            var note = new Workout();
            note.Text = "Day " + note.ID.ToString();
            await App.Database.SaveNoteAsync(note);
            collectionView.ItemsSource = await App.Database.GetNotesAsync();
        }
    }
}