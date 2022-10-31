using System;
using System.Drawing;

namespace HobbyGame.Map {    
    class MapTileFactory {

        public MapTileFactory() {            
        }

        public MapTile Create(MapTileType type) {
            switch (type) {
                case MapTileType.Grass:
                    return new MapTile(0,0);
                case MapTileType.Dirt:
                    return new MapTile(2, 1);
                case MapTileType.Stone:
                    return new MapTile(0, 1);
                default:
                    throw new Exception("Map Tile Type not suppoted");
            }
        }
    }
}
