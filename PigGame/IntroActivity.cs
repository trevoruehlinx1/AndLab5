using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PigGame
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class IntroActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.IntroActivity);
            // Create your application here
            var newGameButton = FindViewById<Button>(Resource.Id.newGameButton);
            var nameEditText1 = FindViewById<EditText>(Resource.Id.textInputEditText1);
            var nameEditText2 = FindViewById<EditText>(Resource.Id.textInputEditText2);
            var score1Label = FindViewById<TextView>(Resource.Id.scoreLabel1);
            var score2Label = FindViewById<TextView>(Resource.Id.scoreLabel2);
            var whosTurnLabel = FindViewById<TextView>(Resource.Id.whosTurnLabel);

            newGameButton.Click += (sender, e) =>
            {
                Intent intent = new Intent(this, typeof(GameActivity));
                if(nameEditText1.Text == "" || nameEditText2.Text == "")
                {
                    whosTurnLabel.Text = "Enter a name for each player";
                }
                else
                {
                    intent.PutExtra("name1", nameEditText1.Text);
                    intent.PutExtra("name2", nameEditText2.Text);
                    StartActivity(intent);
                }
            };
        }
    }
}