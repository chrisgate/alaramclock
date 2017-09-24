﻿using System.Collections.Generic;
using System.Diagnostics;
using Android.Speech.Tts;
using Java.Lang;
using Xamarin.Forms;

[assembly: Dependency(typeof(PrismUnityApp1.Droid.TextToSpeech_Android))]

namespace PrismUnityApp1.Droid
{
    public class TextToSpeech_Android : Object, ITextToSpeech, TextToSpeech.IOnInitListener
    {
        private TextToSpeech speaker;
        private string toSpeak;

        

        public void Speak(string text)
        {
            var c = Forms.Context;
            toSpeak = text;
            if (speaker == null)
            {
                speaker = new TextToSpeech(c, this);
            }
            else
            {
                var p = new Dictionary<string, string>();
                speaker.Speak(toSpeak, QueueMode.Flush, p);
                Debug.WriteLine("spoke " + toSpeak);
            }
        }

        #region IOnInitListener implementation

        public void OnInit(OperationResult status)
        {
            if (status.Equals(OperationResult.Success))
            {
                Debug.WriteLine("speaker init");
                var p = new Dictionary<string, string>();
                speaker.Speak(toSpeak, QueueMode.Flush, p);
            }
            else
            {
                Debug.WriteLine("was quiet");
            }
        }

        #endregion
    }
}