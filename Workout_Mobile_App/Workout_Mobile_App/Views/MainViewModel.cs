using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Workout_Mobile_App.Models
{
    public class MainViewModel : INotifyPropertyChanged
    {
        string text = string.Empty;
        public string Text
        {
            get => text;
            set
            {
                if (text == value)
                    return;

                text = value;
                OnPropertyChanged(nameof(Text));
                OnPropertyChanged(nameof(DisplayText));
            }
        }

        public string DisplayText => $"Text Entered: {Text}";

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
