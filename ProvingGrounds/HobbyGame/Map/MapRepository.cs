using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyGame.Map {
    class MapRepository {
        public void Save(Map map) {
            Console.WriteLine(map);
        }

        public Map Load(string loadData, string imageFile) {
            int row = 0, col = 0;
            MapTileFactory tileFactory = new MapTileFactory();
            List<string> mapLines = loadData.Trim().Split('\n').ToList<string>();
            string mapName = mapLines[0].Trim();
            mapLines.RemoveAt(0);

            int columnCount = mapLines[1].Length;
            int rowCount = mapLines.Count;
            Map map = new Map(mapName, columnCount, rowCount, imageFile);

            MapTile tile = tileFactory.Create(MapTileType.Dirt);
            foreach (string line in mapLines) {
                col = 0;
                foreach (char symbol in line) {

                    if (symbol == 'D') {
                        tile = tileFactory.Create(MapTileType.Dirt);
                    }
                    else if (symbol == 'G') {
                        tile = tileFactory.Create(MapTileType.Grass);
                    }
                    else if (symbol == 'S') {
                        tile = tileFactory.Create(MapTileType.Stone);
                    }
                    map.SetGridTile(row, col, tile);
                    col++;
                }
                row++;
            }
            return map;
        }
    }
}
