using System;
using System.IO;
using Workout_Mobile_App.Models;
using Xamarin.Forms;

namespace Workout_Mobile_App.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class DayPage : ContentPage
    {
        public string ItemId
        {
            set
            {
                LoadDay(value);
            }
        }
        public int CurrentDay;
        public DayPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            Day day = await App.DatabaseDay.GetDayAsync(CurrentDay);
            contentPage.Title = day.Name;

            collectionView.ItemsSource = await App.DatabaseExercise.GetExercisesAsync(CurrentDay);
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
        async void OnAddExerciseClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(ExercisePage)}?{nameof(ExercisePage.ItemId)}={CurrentDay.ToString()}");
        }
    }
}