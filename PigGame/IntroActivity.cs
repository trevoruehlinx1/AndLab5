﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Content.Res;
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


            if ((Application.ApplicationContext.Resources.Configuration.ScreenLayout & ScreenLayout.SizeMask) == ScreenLayout.SizeSmall)
            {
                RequestedOrientation = ScreenOrientation.Portrait;
            }
            if ((Application.ApplicationContext.Resources.Configuration.ScreenLayout & ScreenLayout.SizeMask) == ScreenLayout.SizeNormal)
            {
                RequestedOrientation = ScreenOrientation.Portrait;
            }
            if ((Application.ApplicationContext.Resources.Configuration.ScreenLayout & ScreenLayout.SizeMask) == ScreenLayout.SizeLarge)
            {
                RequestedOrientation = ScreenOrientation.Landscape;
            }
            SetContentView(Resource.Layout.IntroActivity);
            // Create your application here
            var startGameButton = FindViewById<Button>(Resource.Id.startGameButton);
            var nameEditText1 = FindViewById<EditText>(Resource.Id.textInputEditText1);
            var nameEditText2 = FindViewById<EditText>(Resource.Id.textInputEditText2);
            var score1Label = FindViewById<TextView>(Resource.Id.introPlayer1Label);
            var score2Label = FindViewById<TextView>(Resource.Id.introPlayer2Label);
            var messageLabel = FindViewById<TextView>(Resource.Id.introMessageLabel);

            startGameButton.Click += (sender, e) =>
            {
                Intent intent = new Intent(this, typeof(GameActivity));
                if(nameEditText1.Text == "" || nameEditText2.Text == "")
                {
                    messageLabel.Text = "Enter a name for each player";
                }
                else
                {

                    intent.PutExtra("name1", nameEditText1.Text);
                    intent.PutExtra("name2", nameEditText2.Text);
                    nameEditText1.Enabled = false;
                    nameEditText2.Enabled = false;
                    StartActivity(intent);
                }
            };
        }
    }
}