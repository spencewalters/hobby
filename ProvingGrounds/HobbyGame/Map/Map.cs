using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;

namespace HobbyGame.Map {
    class Map {
        public string Name { get; set; }
        public int Width { get; }
        public int Height { get; }
        private readonly int TileSideSize = 64;
        public Point CenterPoint { get => new Point(Width * TileSideSize / 2, Height * TileSideSize / 2); }
        public Point BottomRightPoint { get => new Point(Width * TileSideSize, Height * TileSideSize); }
        public TileFrames frames;
        public Image ImageFrames { get; set; }
        private Dictionary<int, Dictionary<int, MapTile>> grid;
        private Bitmap mapGraphicsBuffer;
        private Graphics mapGraphics;

        public Map(string name, int width, int height, string tileFramesFile) {
            this.Name = name;
            this.frames = new TileFrames(tileFramesFile);

            grid = new Dictionary<int, Dictionary<int, MapTile>>();
            Width = width;
            Height = height;
        }

        public void SetupGraphics() {
            Stopwatch stopwatch = Stopwatch.StartNew();

            mapGraphicsBuffer = new Bitmap(Width * TileSideSize, Height * TileSideSize);
            mapGraphics = Graphics.FromImage(mapGraphicsBuffer);

            for (int rowIndex = 0; rowIndex < Height; rowIndex++) {
                for (int columnIndex = 0; columnIndex < Width; columnIndex++) {
                    int placementX = (columnIndex * TileSideSize);
                    int placementY = (rowIndex * TileSideSize);

                    MapTile tile = Tile(rowIndex, columnIndex);
                    Rectangle destination = new Rectangle(placementX, placementY, TileSideSize, TileSideSize);

                    mapGraphics.DrawImage(frames.Image, destination, frames.Tile(tile), GraphicsUnit.Pixel);
                }
            }

            stopwatch.Stop();
            Console.WriteLine($"Map graphics rendered in {stopwatch.ElapsedMilliseconds.ToString("0.0")}ms");
        }

        public static Map CreatePlainMap(int width, int height, string tileFramesFile) {
            string path = System.IO.Directory.GetCurrentDirectory() + "\\tiles\\";
            Map map = new Map("New World", width, height, tileFramesFile);
            map.ResetAllTilesTo(MapTileType.Grass);
            map.SetupGraphics();
            return map;
        }

        public static Map CreateRandomMap(int width, int height, string tileFramesFile) {
            string path = System.IO.Directory.GetCurrentDirectory() + "\\tiles\\";
            Map map = new Map("New World", width, height, tileFramesFile);
            map.ResetAllTilesRandomly();
            map.SetupGraphics();
            return map;
        }

        private void ResetAllTilesRandomly() {
            Random randomizer = new Random();

            MapTile tile = new MapTile(randomizer.Next(0, 3), randomizer.Next(0, 5));
            for (int row = 0; row < Width; row++) {
                for (int column = 0; column < Height; column++) {
                    SetGridTile(row, column, tile);
                    tile = new MapTile(randomizer.Next(0, 3), randomizer.Next(0, 5));
                }
            }
        }

        public void DrawTo(Graphics gameGraphics, Rectangle viewRectangle) {
            // draw from mapgraphics to game graphics

            gameGraphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;

            //Stopwatch stopwatch = Stopwatch.StartNew();

            //3-6ms
            gameGraphics.DrawImage(
                mapGraphicsBuffer,
                new Rectangle(0, 0, viewRectangle.Width, viewRectangle.Height),
                viewRectangle,
                GraphicsUnit.Pixel
                );

            //stopwatch.Stop();
            //Console.WriteLine($"Map drawn in {stopwatch.ElapsedMilliseconds.ToString("0.0")}ms");

            gameGraphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
        }

        public void OldDrawTo(Graphics gameGraphics, Rectangle viewRectangle) {
            int tileSide = 64;
            int startColumn = (viewRectangle.X / tileSide);
            int lastColumn = ((viewRectangle.X + viewRectangle.Width) / tileSide);

            int startRow = (viewRectangle.Y / tileSide);
            int lastRow = ((viewRectangle.Y + viewRectangle.Height) / tileSide);

            gameGraphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
            gameGraphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
            int countDraws = 0;
            for (int rowIndex = startRow; rowIndex <= lastRow; rowIndex++) {
                for (int columnIndex = startColumn; columnIndex <= lastColumn; columnIndex++) {
                    if ((rowIndex < Height) && (columnIndex < Width)) {
                        int placementX = (columnIndex * tileSide) - viewRectangle.X;
                        int placementY = (rowIndex * tileSide) - viewRectangle.Y;

                        MapTile tile = Tile(rowIndex, columnIndex);
                        Rectangle destination = new Rectangle(placementX, placementY, tileSide, tileSide);

                        gameGraphics.DrawImage(frames.Image, destination, frames.Tile(tile), GraphicsUnit.Pixel);
                        countDraws++;
                    }
                }
            }
            gameGraphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
        }

        public MapTile Tile(int rowIndex, int columnIndex) {
            if (grid.ContainsKey(rowIndex)) {
                Dictionary<int, MapTile> gridRow = grid[rowIndex];
                if (gridRow.ContainsKey(columnIndex)) {
                    return gridRow[columnIndex];
                }
            }

            throw new Exception("Tile not found");
        }

        private void ResetAllTilesTo(MapTileType setType) {
            MapTileFactory factory = new MapTileFactory();
            MapTile tile = factory.Create(setType);
            for (int row = 0; row < Width; row++) {
                for (int column = 0; column < Height; column++) {
                    SetGridTile(row, column, tile);
                }
            }
        }

        public void SetGridTile(int row, int column, MapTile tile) {
            if (grid.ContainsKey(row)) {
                Dictionary<int, MapTile> gridRow = grid[row];
                if (gridRow.ContainsKey(column)) {
                    gridRow[column] = tile;
                }
                else {
                    gridRow.Add(column, tile);
                }
            }
            else {
                Dictionary<int, MapTile> newRow = new Dictionary<int, MapTile>();
                newRow.Add(column, tile);
                grid.Add(row, newRow);
            }
        }


        public override string ToString() {
            StringBuilder textBuilder = new StringBuilder();
            textBuilder.Append(Name);
            for (int rowIndex = 0; rowIndex < Height; rowIndex++) {
                textBuilder.AppendLine("");
                for (int columnIndex = 0; columnIndex < Width; columnIndex++) {
                    MapTile tile = Tile(rowIndex, columnIndex);
                    string symbol = "G";
                    textBuilder.Append(symbol);
                }
            }
            return textBuilder.ToString();
        }
    }
}
