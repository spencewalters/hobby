using HobbyGame.Map;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Media;
using System.Threading;
using System.Windows.Forms;
using HobbyGame;

namespace HobbyGame.Programs {
    public partial class CrappyStardew : Form {

        private int targetFPS = 90;
        private SoundPlayer soundPlayer;
        private SoundPlayer musicPlayer;

        private GameModel game;
        private KeyEventArgs RepeatInput;

        public CrappyStardew() {
            InitializeComponent();

            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            SetupMediaPlayer();
            PlayMusic();
        }

        private void CrappyStardew_Load(object sender, EventArgs e) {
            FocusForControl();
            SetupGameWithin(this);
        }

        private void SetupGameWithin(Control control) {
            //game = new GameModel(targetFPS, new Point(gamePanel.Width, gamePanel.Height), gamePanel);
            game = new GameModel(targetFPS, new Point(control.Width, control.Height), control);
            var animationThread = new Thread(new ParameterizedThreadStart(StartGameLoop));
            animationThread.Start(game);

            statTimer.Start();            
        }

        private static void StartGameLoop(Object passedObject) {
            GameModel game = (GameModel)passedObject;
            Console.WriteLine("Starting game loop");
            Thread.Sleep(100);
            while (game.IsActive) {
                game.GameLoop();
                Thread.Sleep(1);
            }

            Console.WriteLine("Exited game");
        }

        private void SetupMediaPlayer() {
            soundPlayer = new SoundPlayer();
        }

        private void CrappyStardew_FormClosing(object sender, FormClosingEventArgs e) {
            game.Stop();
            musicPlayer.Dispose();
            soundPlayer.Dispose();
        }

        //private void ControlPeople(MouseEventArgs e) {
        //    if (e.Button == MouseButtons.Left) {
        //        if (characters.Count > 0) {
        //            Point mapPoint = ScreenToMap(new Point(e.X, e.Y));
        //            MoveCharacterTo((int)controlledCharacter.Value, mapPoint);

        //            int halfWidth = 32;
        //            int height = 32;
        //            effects.Add(effectsFactory.Create(200, new Point(e.X - halfWidth, e.Y - height)));
        //        }
        //    }
        //    else if (e.Button == MouseButtons.Right) {
        //        Point mapPoint = ScreenToMap(new Point(e.X, e.Y));
        //        for (int characterIndex = 0; characterIndex < characters.Count; characterIndex++) {
        //            MoveCharacterTo(characterIndex, mapPoint);
        //        }
        //    }
        //    else if (e.Button == MouseButtons.Middle) {
        //        Point mapPoint = ScreenToMap(new Point(e.X, e.Y));
        //        for (int characterIndex = 0; characterIndex < characters.Count; characterIndex++) {
        //            MoveCharacterTo(characterIndex, ScreenToMap(RandomScreenPoint()));
        //        }
        //    }
        //}

        private void PlayMusic() {
            musicPlayer = new SoundPlayer();
            musicPlayer.SoundLocation = "music\\music.wav";
            musicPlayer.Play();
        }

        private void gamePanel_Resize(object sender, EventArgs e) {
            game.UpdateImageBuffer(this.Width, this.Height);
        }

        private void createCharacterButton_Click(object sender, EventArgs e) {
            game.AddNewPerson();
            FocusForControl();
        }

        private void FocusForControl() {
            textBox1.Focus();
        }

        private void saveMapButton_Click(object sender, EventArgs e) {
            game.Save();
        }

        private void loadMapButton_Click(object sender, EventArgs e) {
            LoadMapForm form = new LoadMapForm();
            if (form.ShowDialog() == DialogResult.Yes) {
                string loaded = form.LoadMapText;
                game.LoadMap(loaded);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {
            textBox1.Text = "";
        }

        private void statTimer_Tick(object sender, EventArgs e) {
            double fps = game.FramesPerSecond;
            framesPerSecTextbox.Text = fps.ToString("0.0");
            loopsPerSecTextbox.Text = game.LoopsPerSecond.ToString("0.0");

            int diff = targetFPS - (int)fps;
            if (diff < 12)
                framesPerSecTextbox.BackColor = Color.LightGreen;
            else if (diff < 30)
                framesPerSecTextbox.BackColor = Color.Yellow;
            else
                framesPerSecTextbox.BackColor = Color.Red;

            framesPerSecTextbox.ForeColor = Color.Black;
        }

        private void CrappyStardew_KeyDown(object sender, KeyEventArgs e) {
            game.HandleKeyboard(e);
            RepeatInput = e;
            keypressTimer.Start();
        }

        private void gamePanel_MouseClick(object sender, MouseEventArgs e) {
            FocusForControl();
            game.HandleMouse(e);
        }

        private void CrappyStardew_KeyUp(object sender, KeyEventArgs e) {
            keypressTimer.Stop();
        }

        private void keypressTimer_Tick(object sender, EventArgs e) {
            game.HandleKeyboard(RepeatInput);
        }
    }
}
