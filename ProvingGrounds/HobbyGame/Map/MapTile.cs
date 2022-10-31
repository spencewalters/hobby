using System.Drawing;

namespace HobbyGame.Map {
    class MapTile {
        
        public MapTile(int v1, int v2) {
            FrameIndex = new Point(v1, v2);
        }

        public Point FrameIndex { get; set; }
    }
}
