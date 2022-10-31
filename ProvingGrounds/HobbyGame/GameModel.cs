using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HobbyGame.Character;
using HobbyGame.Effects;
using HobbyGame.Map;
using HobbyGame.Ambulation;
using HobbyGame.Controls;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace HobbyGame {
    class GameModel {
        private double paintIntervalMs;
        private Stopwatch gameStopwatch;
        private List<Character.Character> characters;
        private List<EffectSprite> effects;
        private CharacterFactory characterFactory;
        private EffectsFactory effectsFactory;
        private Random random = new Random();
        private Map.Map LoadedMap;
        private Camera.CameraOperator cameraOperator;
        private Ambulator ambulator;
        private GameControllerFactory controllerFactory;
        private GameController controller;
        private PlayerParams playerParams;
        private int framesPerSec;
        private Point ScreenSize;
        private Control gamePanel;
        private Bitmap imageBuffer;
        private Graphics graphics;
        private CycleTimer fpsTimer;
        private CycleTimer loopTimer;
        private Bitmap testImage;

        public bool IsActive { get; internal set; }

        public GameModel(int fps, Point screenSize, Control panel) {
            framesPerSec = fps;
            ScreenSize = screenSize;
            gamePanel = panel;

            characters = new List<Character.Character>();
            ambulator = new Ambulator(characters);
            effects = new List<EffectSprite>();
            playerParams = new PlayerParams();
            fpsTimer = new CycleTimer();
            loopTimer = new CycleTimer();

            InitFactories();
            SetupTiming();
            SetupMap(MapTileType.Grass);
            SetupCamera();
            UpdateImageBuffer(ScreenSize.X, ScreenSize.Y);
            SetupGameController();
            IsActive = true;

            testImage = (Bitmap)Image.FromFile("characters\\star.png");
        }

        private void SetupTiming() {
            paintIntervalMs = (double)(1000 / framesPerSec);
            Console.WriteLine($"Set paint interval to {paintIntervalMs}ms");
            gameStopwatch = Stopwatch.StartNew();
        }

        public void GameLoop() {
            DrawGamePanel();
            ambulator.Ambulate();
            MoveCamera();
            loopTimer.CountCycle();
        }

        internal void Stop() {
            IsActive = false;
        }

        private void DrawGamePanel() {
            gameStopwatch.Restart();
            DrawElementsToPanel();
            fpsTimer.CountCycle();
        }

        public double FramesPerSecond { get => fpsTimer.CyclesPerSecond; }
        public double LoopsPerSecond { get => loopTimer.CyclesPerSecond; }

        private void DrawElementsToPanel() {
            try {

                DrawVisibleMap(graphics);
                DrawCharacters(graphics);
                DrawEffects(graphics);

                Stopwatch panelRendTimer = Stopwatch.StartNew();

                using (Graphics panelGraphics = gamePanel.CreateGraphics()) {
                    //panelGraphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                    //panelGraphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
                    //panelGraphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;
                    //panelGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                    //panelGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;

                    // 20-30ms
                    panelGraphics.DrawImageUnscaled(imageBuffer, 0, 0);

                }
                panelRendTimer.Stop();
                // Console.WriteLine($"Panel rendered in {panelRendTimer.ElapsedMilliseconds.ToString("0.0")}ms");
            }
            catch (Exception exception) {
                Console.WriteLine("Exception:" + exception.Message + "\n" +
                    "Trace:" + exception.StackTrace);
            }
        }

        private void DrawVisibleMap(Graphics g) {
            LoadedMap.DrawTo(g, cameraOperator.CurrentView);
        }

        public void UpdateImageBuffer(int width, int height) {
            if (imageBuffer != null) {
                imageBuffer.Dispose();
                graphics.Dispose();
                Console.WriteLine("Image buffer & graphics disposed");
            }
            imageBuffer = new Bitmap(width, height);
            graphics = Graphics.FromImage(imageBuffer);
            cameraOperator.UpdateSize(width, height);
            Console.WriteLine("Image buffer, graphics, and camera reset");

            PixelFormat format = imageBuffer.PixelFormat;
            Console.WriteLine("Image buffer format: " + format.ToString());
        }

        private void DrawEffects(Graphics g) {
            List<EffectSprite> removal = new List<EffectSprite>();
            foreach (EffectSprite effect in effects) {
                if (effect.Active)
                    g.DrawImage(effect.CurrentImage(), effect.MapLocation);
                else
                    removal.Add(effect);
            }

            foreach (EffectSprite effect in removal)
                effects.Remove(effect);
        }

        private void DrawCharacters(Graphics graphicsObj) {
            foreach (Character.Character character in characters) {
                if (character.IsIn(cameraOperator.CurrentView)) {
                    character.DrawTo(graphicsObj, cameraOperator.CurrentView);
                }
            }
        }

        private void SetupGameController() {
            controller = controllerFactory.GetEmptyController();
        }

        private void SetupCamera() {

            cameraOperator = new Camera.CameraOperator();

            if (gamePanel.InvokeRequired) {
                cameraOperator.SetupCamera(
                    LoadedMap.CenterPoint,
                    ScreenSize,
                    LoadedMap.BottomRightPoint);
            }
            else {
                cameraOperator.SetupCamera(
                    LoadedMap.CenterPoint,
                    new Point(gamePanel.Width, gamePanel.Height),
                    LoadedMap.BottomRightPoint);
            }
        }

        private void InitFactories() {
            controllerFactory = new GameControllerFactory();
            characterFactory = new CharacterFactory();
            effectsFactory = new EffectsFactory();
        }

        private void SetupMap(MapTileType mapType) {
            //LoadedMap = Map.Map.CreatePlainMap(40, 40, "tiles.png");
            LoadedMap = Map.Map.CreateRandomMap(40, 40, "tiles.png");
        }

        private void MoveCamera() {
            if (playerParams.HasControl)
                cameraOperator.CenterOn(playerParams.ControlledCharacter.MapLocation);
        }

        internal void Save() {
            Console.WriteLine("Save option clicked");
        }

        private void MoveCharacterTo(int characterIndex, Point mapPoint) {
            Character.Character character = characters[characterIndex];
            character.SetTargetPoint(mapPoint);
        }

        private void ControlNewestCharacter() {
            Character.Character newest = characters[characters.Count - 1];
            playerParams.ControlledCharacter = newest;
            playerParams.HasControl = true;
            controller = controllerFactory.GetGameController(playerParams);
            cameraOperator.CenterOn(newest.MapLocation);
            Console.WriteLine($"Player now controls {newest.Name}");
        }

        private void SaveMap() {
            MapRepository repo = new MapRepository();
            repo.Save(LoadedMap);
        }

        public void LoadMap(string loadData) {
            MapRepository repo = new MapRepository();
            LoadedMap = repo.Load(loadData, "tiles.png");
            Console.WriteLine($"Loaded '{LoadedMap.Name}' ({LoadedMap.Width}x{LoadedMap.Height}) map ");
        }

        public void AddNewPerson() {
            AddRandomCharacter();
            ControlNewestCharacter();
        }

        private void AddRandomCharacter() {
            Point startingPoint = RandomScreenPoint();
            int chance = random.Next(100);
            Character.Character character;
            if (chance < 40)
                character = characterFactory.Create("girl", startingPoint);
            else if (chance < 80)
                character = characterFactory.Create("boy", startingPoint);
            else
                character = characterFactory.Create("moviestar", startingPoint);

            character.MapTarget = cameraOperator.ScreenToMap(RandomScreenPoint());
            characters.Add(character);
        }
        
        private Point RandomScreenPoint() {
            return new Point(random.Next(1, gamePanel.Width - 50), random.Next(1, gamePanel.Height - 80));
        }

        public void HandleMouse(MouseEventArgs e) {
            controller.HandleMouseClick(e);
        }

        public void HandleKeyboard(KeyEventArgs e) {
            controller.HandleKeyboardInput(e);
        }

    }
}
