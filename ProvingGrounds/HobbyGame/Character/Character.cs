using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using HobbyGame.Ambulation;

namespace HobbyGame.Character {
    class Character {
        public string Name { get; set; }
        public Point MapLocation { get; internal set; }
        public Point MapTarget { get; set; }
        public int ImageDurationMs { get; set; }
        public MovementDirection Direction { get; set; }
        public CharacterAction Action { get; set; }
        public Momentum Momentum { get; set; }
        public string DefaultNoise = "noise.wav";
        public bool MovingToTarget { get; set; }

        public readonly int Width = 64;
        public readonly int Height = 128;
        public readonly double MaxVelocity = 9;
        private Image ImageFrames;

        internal void Move(MovementDirection direction) {
            Momentum.Direction = direction;
            Momentum.Velocity = MaxVelocity;
        }

        private DateTime imageClock;
        private int currentFrame;

        public Character(string name, Point mapLocation, Image imageFrames) {
            Name = name;
            MapLocation = mapLocation;
            ImageFrames = imageFrames;
            PixelFormat format = ImageFrames.PixelFormat;
            Console.WriteLine("Image buffer format: " + format.ToString());

            MovingToTarget = false;
            Momentum = new Momentum();
            ResetAnimation();
        }

        public void ResetAnimation() {
            Direction = MovementDirection.Down;
            Action = CharacterAction.Standing;
            imageClock = DateTime.Now;
            currentFrame = 0;
        }

        // determine current frame by 
        //   direction facing
        //   state:  walking/still
        private Rectangle CurrentFrame() {
            Rectangle sourceRectangle = new Rectangle(2, 2, Width, Height);

            if (DateTime.Now.Subtract(imageClock) > TimeSpan.FromMilliseconds(ImageDurationMs)) {

                switch (Action) {
                    case CharacterAction.Standing:
                        currentFrame = 0;
                        break;
                    case CharacterAction.Walking:
                        currentFrame = (currentFrame == 1) ? 3 : 1;
                        break;
                    default:
                        break;
                }

                imageClock = DateTime.Now;
            }

            int directionInt = (int)Direction;
            sourceRectangle.X = 1 + (1 * currentFrame) + (Width * currentFrame);
            sourceRectangle.Y = 1 + (1 * directionInt) + (Height * directionInt);

            return sourceRectangle;
        }

        internal void DrawTo(Graphics graphicsObj, Rectangle visibleArea) {
            Rectangle destinationRectangle = new Rectangle(
                MapLocation.X - visibleArea.X,
                MapLocation.Y - visibleArea.Y,
                Width, Height);

            graphicsObj.DrawImage(ImageFrames, destinationRectangle, CurrentFrame(), GraphicsUnit.Pixel);
        }

        internal bool IsIn(Rectangle currentView) {
            return (MapLocation.X > currentView.X &&
                MapLocation.X < (currentView.X + currentView.Width) &&
                MapLocation.Y > currentView.Y &&
                MapLocation.Y < (currentView.Y + currentView.Height));
        }

        internal void SetTargetPoint(Point mapPoint) {
            MapTarget = new Point(mapPoint.X - (Width / 2), mapPoint.Y - Height);
        }
    }
}
