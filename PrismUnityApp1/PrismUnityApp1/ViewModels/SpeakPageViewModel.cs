using Prism.Commands;
using Prism.Mvvm;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrismUnityApp1.ViewModels
{
    
    public class SpeakPageViewModel : BindableBase
    {
            private readonly ITextToSpeech _textToSpeech;

            private string _textToSay = "Hello from Xamarin Forms and Prism.";
            public string TextToSay
            {
                get { return _textToSay; }
                set { SetProperty(ref _textToSay, value); }
            }

            public DelegateCommand SpeakCommand { get; set; }

            public SpeakPageViewModel(ITextToSpeech textToSpeech)
            {
                _textToSpeech = textToSpeech;
                SpeakCommand = new DelegateCommand(Speak);
            }

            private void Speak()
            {
                _textToSpeech.Speak(TextToSay);
            }
        }
}
