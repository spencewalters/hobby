using HobbyGame.Map;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace HobbyGame {
    class MapMakerModel {
        private Control tileGroupbox;
        private Control mapPanel;
        private TileFrames tileFrames;
        private readonly int tileRowMax = 10;
        private readonly int tileColMax = 10;
        private List<MapTile> tileList;
        private MapTile Selected;
        private Camera.CameraOperator cameraOperator;
        private Map.Map currentMap;
        public bool MapLoaded { get; internal set; }
        public bool ChangesMade { get; internal set; }

        public MapMakerModel(Control tileGroupbox, Control mapPanel) {
            this.tileGroupbox = tileGroupbox;
            this.mapPanel = mapPanel;
            tileFrames = new TileFrames("tiles.png");
            LoadTilesAsList();
            SelectTileAt(1, 1);
            
            ChangesMade = false;
            MapLoaded = false;
        }

        private void SetupCamera(Map.Map map) {
            cameraOperator = new Camera.CameraOperator();
            cameraOperator.SetupCamera(
                map.CenterPoint,
                new Point(mapPanel.Width, mapPanel.Height),
                currentMap.BottomRightPoint);
        }

        private void LoadTilesAsList() {
            tileList = new List<MapTile>();

            for (int tileRow = 0; tileRow < tileRowMax; tileRow++) {
                for (int tileCol = 0; tileCol < tileColMax; tileCol++) {
                    MapTile tile = new MapTile(tileRow, tileCol);
                    tileList.Add(tile);
                }
            }
        }

        public void DrawTiles() {
            int placementX = 1;
            int placementY = 1;

            using (Graphics target = tileGroupbox.CreateGraphics()) {
                foreach (MapTile tile in tileList) {
                    Rectangle destination = new Rectangle(placementX, placementY, 64, 64);

                    target.DrawImage(
                        tileFrames.Image,
                        destination,
                        tileFrames.Tile(tile),
                        GraphicsUnit.Pixel);
                    placementX += 65;
                    if (placementX > (tileGroupbox.Width - 66)) {
                        placementX = 1;
                        placementY += 65;
                    }
                }
            }
        }

        internal void NewMap() {
            Console.WriteLine("New map");
            //SetupCamera(currentMap);
        }

        internal void Save() {
            Console.WriteLine("Save map");
        }

        internal void Open(string fileName) {
            Console.WriteLine("Open " + fileName);
        }

        internal void MapClicked(MouseEventArgs e) {
            if (MapLoaded) {
                Console.WriteLine($"click at {e.X}, {e.Y}");
                PlaceTileOnMap(e.X, e.Y);
                //ModifyWorldAt()

                ChangesMade = true;
            }
        }

        internal void SetTileSet(string tileSetSelected) {
            Console.WriteLine("Change tile set to " + tileSetSelected);
        }

        private void PlaceTileOnMap(int x, int y) {
            int newX = (x / 64) * 64;
            int newY = (y / 64) * 64;

            using (Graphics target = mapPanel.CreateGraphics()) {
                Rectangle destination = new Rectangle(newX, newY, 64, 64);
                target.DrawImage(tileFrames.Image, destination, tileFrames.Tile(Selected), GraphicsUnit.Pixel);
            }
        }

        internal void TileClicked(MouseEventArgs e) {
            Console.WriteLine($"click at {e.X}, {e.Y}");
            SelectTileAt(e.X, e.Y);
        }

        private void SelectTileAt(int x, int y) {
            int newX = (x / 66);
            int newY = (y / 66);
            Console.WriteLine($"Tile select: {newX}, {newY}");

            int selectIndex = newX + (newY * 4);
            Console.WriteLine($"Get #" + selectIndex);
            Selected = tileList[selectIndex];

            newX *= 66;
            newY *= 66;
            Console.WriteLine($"Select at {newX}, {newY}");

            using (Graphics target = tileGroupbox.CreateGraphics()) {
                Pen pen = new Pen(Color.Red);
                target.DrawRectangle(pen, new Rectangle(newX, newY, 64, 64));
            }
        }
    }
}
