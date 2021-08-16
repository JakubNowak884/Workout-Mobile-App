using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Workout_Mobile_App.Models;
using Xamarin.Forms;

namespace Workout_Mobile_App.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class ExercisePage : ContentPage
    {
        public string ItemId
        {
            set
            {
                LoadDay(value);
            }
        }
        public int CurrentDay;
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
        async void OnExerciseSelected(object sender, SelectionChangedEventArgs e)
        {
            string result = await DisplayPromptAsync("Weight", "Type starting weight:", initialValue: "0");

            if (e.CurrentSelection != null && result != null)
            {
                ExerciseDraft exerciseDraft = (ExerciseDraft)e.CurrentSelection.FirstOrDefault();
                Exercise exercise = new Exercise()
                {
                    Name = exerciseDraft.Name,
                    Type = exerciseDraft.Type,
                    Progress = exerciseDraft.Progress,
                    Deload = exerciseDraft.Deload,
                    DayKey = CurrentDay
                };

                try
                {
                    exercise.Weight = float.Parse(result);
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

                await App.DatabaseExercise.SaveExerciseAsync(exercise);

                await Shell.Current.GoToAsync("..");
            }

        }

        void LoadDay(string itemId)
        {
            try
            {
                CurrentDay = Convert.ToInt32(itemId);
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load day.");
            }
        }
    }
}