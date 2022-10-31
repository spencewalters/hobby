using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProvingGrounds;
using HobbyGame.Ambulation;

namespace HobbyGame.Camera {
    class CameraOperator {
        public CameraOperator() {
        }

        public Camera Camera { get; internal set; }
        public Rectangle CurrentView { get => Camera.CurrentView; }

        public void SetupCamera(Point setupPoint, Point ViewSize, Point bottomRight) {
            Point topLeftPoint = CenterFrom(setupPoint, ref ViewSize);
            Camera = new Camera(topLeftPoint, ViewSize);
            Camera.MaxX = bottomRight.X - ViewSize.X;
            Camera.MaxY = bottomRight.Y - ViewSize.Y;
        }

        private static Point CenterFrom(Point setupPoint, ref Point ViewSize) {
            int X = setupPoint.X - (ViewSize.X / 2);
            X = (X > 0) ? X : 0;

            int Y = setupPoint.Y - (ViewSize.Y / 2);
            Y = (Y > 0) ? Y : 0;

            Point topLeftPoint = new Point(X, Y);
            return topLeftPoint;
        }

        internal void UpdateSize(int width, int height) {
            Camera.UpdateSize(width, height);
        }

        internal void Move(MovementDirection direction) {
            switch (direction) {
                case MovementDirection.Down:
                    Camera.PushDown();
                    break;
                case MovementDirection.Left:
                    Camera.PushLeft();
                    break;
                case MovementDirection.Right:
                    Camera.PushRight(); ;
                    break;
                case MovementDirection.Up:
                    Camera.PushUp();
                    break;
                default:
                    break;
            }
        }

        internal void CenterOn(Point mapLocation) {
            Point size = new Point(Camera.Width, Camera.Height);
            Point centered = CenterFrom(mapLocation, ref size);
            Camera.UpdateView(centered, size);
        }

        internal Point ScreenToMap(Point point) {
            return new Point(CurrentView.X + point.X, CurrentView.Y + point.Y);
        }
    }
}
