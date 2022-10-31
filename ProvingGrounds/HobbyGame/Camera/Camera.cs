using System;
using System.Drawing;

namespace HobbyGame.Camera {
    class Camera {
        public Rectangle CurrentView { get => GetCurrentView(); }
        public int MaxX { get; set; }
        public int MaxY { get; set; }
        public int Width { get => view.Width; }
        public int Height { get => view.Height; }
        private Rectangle view;
        private double velocityX, velocityY, friction, updateIntervalMs;

        private DateTime cameraClock;

        public Camera(Point startPoint, Point viewSize) {
            Console.WriteLine($"Setting up camera at {startPoint} {viewSize.X}x{viewSize.Y}");
            cameraClock = DateTime.Now;
            friction = 5.5;
            updateIntervalMs = 50;
            UpdateView(startPoint, viewSize);
        }

        public void UpdateView(Point viewPoint, Point viewSize) {
            view = new Rectangle(viewPoint.X, viewPoint.Y, viewSize.X, viewSize.Y);
        }

        private Rectangle GetCurrentView() {
            if (DateTime.Now.Subtract(cameraClock) > TimeSpan.FromMilliseconds(updateIntervalMs)) {
                view.X += (int)velocityX;
                view.Y += (int)velocityY;

                if (velocityX > 0)
                    velocityX -= friction;
                else if (velocityX < 0)
                    velocityX += friction;

                if (velocityY > 0)
                    velocityY -= friction;
                else if (velocityY < 0)
                    velocityY += friction;

                if (Math.Abs(velocityX) < friction)
                    velocityX = 0;
                if (Math.Abs(velocityY) < friction)
                    velocityY = 0;

                if (view.X > MaxX)
                    view.X = MaxX;
                else if (view.X < 0)
                    view.X = 0;

                if (view.Y > MaxY)
                    view.Y = MaxY;
                else if (view.Y < 0)
                    view.Y = 0;

                cameraClock = DateTime.Now;
            }
            return new Rectangle(view.Location, view.Size);
        }
        
        public void PushLeft() {
            velocityX = -30;
        }

        public void PushRight() {
            velocityX = 30;
        }

        public void PushUp() {
            velocityY = -30;
        }

        public void PushDown() {
            velocityY = 30;
        }

        public void UpdateSize(int width, int height) {
            view = new Rectangle(view.X, view.Y, width, height);
        }
    }
}
