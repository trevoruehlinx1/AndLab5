using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using System;
using Newtonsoft.Json;
using System.IO;
using System.Xml.Serialization;
using Android.Content;
using Android.Runtime;
using Android.Views;

namespace PigGame
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class GameActivity : AppCompatActivity
    {
        PigGameLogic gameLogic;
        string player1Name;
        string player2Name;
        int player1Score;
        int player2Score;
        int pointsForTurn;
        int lastRollValue;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.GameActivity);

            // create all of the views
            var newGameButton = FindViewById<Button>(Resource.Id.newGameButton);
            var rollButton = FindViewById<Button>(Resource.Id.rollButton);
            var newPlayersButton = FindViewById<Button>(Resource.Id.newPlayersPuttonButton);
            var endTurnButton = FindViewById<Button>(Resource.Id.endTurnButton);
            var score1Label = FindViewById<TextView>(Resource.Id.scoreLabel1);
            var score2Label = FindViewById<TextView>(Resource.Id.scoreLabel2);
            var scoreTextView1 = FindViewById<TextView>(Resource.Id.scoreTextView1);
            var scoreTextView2 = FindViewById<TextView>(Resource.Id.scoreTextView2);
            var whosTurnLabel = FindViewById<TextView>(Resource.Id.whosTurnLabel);
            var pointsForTurnTextView = FindViewById<TextView>(Resource.Id.pointsForTurnTextView);
            var diceImageView = FindViewById<ImageView>(Resource.Id.diceImageView);

            //if the game has been saved get the page out of the json
            if (savedInstanceState != null)
            {
                gameLogic = new PigGameLogic();
                string jsonGame = savedInstanceState.GetString("pageInJson");
                gameLogic = JsonConvert.DeserializeObject<PigGameLogic>(jsonGame);

                score1Label.Text = gameLogic.Player1Name + "'s Score";
                score2Label.Text = gameLogic.Player2Name + "'s Score";
                scoreTextView1.Text = gameLogic.Player1Score.ToString();
                scoreTextView2.Text = gameLogic.Player2Score.ToString();
                pointsForTurnTextView.Text = gameLogic.PointsForTurn.ToString();
                diceImageView.SetImageResource(gameLogic.SetDiceImage());

                if (gameLogic.Player1Turn == true)
                    whosTurnLabel.Text = gameLogic.Player1Name + "'s Turn";
                else
                    whosTurnLabel.Text = gameLogic.Player2Name + "'s Turn";

            }
            else
            {

                //Create a new game logic
                gameLogic = new PigGameLogic();
                player1Name = Intent.GetStringExtra("name1");
                player2Name = Intent.GetStringExtra("name2");
                score1Label.Text = player1Name + "'s Score";
                score2Label.Text = player2Name + "'s Score";
                gameLogic.GetPlayerNames(player1Name, player2Name);
                whosTurnLabel.Text = player1Name + "'s Turn";
                player1Score = 0;
                player2Score = 0;
                diceImageView.SetImageResource(Resource.Drawable.pig);
            }
            newGameButton.Click += (sender, e) =>
            {
                diceImageView.SetImageResource(Resource.Drawable.pig);
                gameLogic = new PigGameLogic();
                player1Name = Intent.GetStringExtra("name1");
                player2Name = Intent.GetStringExtra("name2");
                gameLogic.GetPlayerNames(player1Name,player2Name);
                scoreTextView1.Text = gameLogic.Player1Score.ToString();
                scoreTextView2.Text = gameLogic.Player2Score.ToString();
                whosTurnLabel.Text = player1Name + "'s Turn";
                pointsForTurnTextView.Text = "0";
            };
            rollButton.Click += (sender, e) =>
            {
                string roll = gameLogic.RollDie();
                if (roll != "0")
                    pointsForTurnTextView.Text = roll;
                else
                {
                    rollButton.Enabled = false;
                    whosTurnLabel.Text = "You rolled a 1";
                    pointsForTurnTextView.Text = roll;
                }
                diceImageView.SetImageResource(gameLogic.SetDiceImage());
            };
            endTurnButton.Click += (sender, e) =>
            {
                gameLogic.EndTurn();
                rollButton.Enabled = true;
                if (gameLogic.CheckForWinner() != "")
                {
                    whosTurnLabel.Text = gameLogic.CheckForWinner();
                    rollButton.Enabled = false;
                    endTurnButton.Enabled = false;
                }
                else
                {
                    scoreTextView1.Text = gameLogic.Player1Score.ToString();
                    scoreTextView2.Text = gameLogic.Player2Score.ToString();
                    pointsForTurnTextView.Text = gameLogic.PointsForTurn.ToString();
                    if (gameLogic.Player1Turn == false)
                        whosTurnLabel.Text = gameLogic.Player2Name + "'s Turn";
                    else
                        whosTurnLabel.Text = gameLogic.Player1Name + "'s Turn";
                }
            };
            newPlayersButton.Click += (sender, e) =>
            {
                Intent intent = new Intent(this, typeof(IntroActivity));
                StartActivity(intent);
            };
        }
        protected override void OnSaveInstanceState(Bundle outState)
        {

            string pageInJson = JsonConvert.SerializeObject(this.gameLogic, Formatting.Indented);
            outState.PutString("pageInJson",pageInJson);

            base.OnSaveInstanceState(outState);
        }
    }
}
// EXPERIMENTAL STUFF(DETECTING ORIENTATION)
//var windowManager = ApplicationContext.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();
//var orientation = windowManager.DefaultDisplay.Rotation;
//if (orientation != Android.Views.SurfaceOrientation.Rotation0)
//Android.Views.SurfaceOrientation.Rotation270
