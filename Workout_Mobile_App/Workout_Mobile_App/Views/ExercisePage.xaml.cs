using System;
using System.IO;
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

            collectionView.ItemsSource = await App.DatabaseExerciseDraft.GetDraftExercisesAsync();
        }

        async void OnNewExerciseClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(NewExercisePage));
        }

        async void DeleteExercise(object sender, EventArgs e)
        {
            SwipeItem item = sender as SwipeItem;
            ExerciseDraft exercise = item.BindingContext as ExerciseDraft;
            await App.DatabaseExerciseDraft.DeleteDraftExerciseAsync(exercise);
            collectionView.ItemsSource = await App.DatabaseExerciseDraft.GetDraftExercisesAsync();
        }
    }
}