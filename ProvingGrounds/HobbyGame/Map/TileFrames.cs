using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyGame.Map {
    class TileFrames {
        public Image Image { get; internal set; }
        private readonly int tileSide = 64;

        public TileFrames(string file) {
            string path = System.IO.Directory.GetCurrentDirectory() + "\\tiles\\";
            Image = Image.FromFile(path + file);
        }

        internal Rectangle Tile(MapTile tile) {
            int sourceX = 1 + (tile.FrameIndex.X * 1) + (tile.FrameIndex.X * tileSide);
            int sourceY = 1 + (tile.FrameIndex.Y * 1) + (tile.FrameIndex.Y * tileSide);
            return new Rectangle(sourceX, sourceY, tileSide, tileSide);
        }
    }
}
