using System.Collections.Generic;
using System.Drawing;

namespace HobbyGame.Character {
    class CharacterFactory {
        private string path;

        public CharacterFactory() {
            path = System.IO.Directory.GetCurrentDirectory() + "\\characters\\";
        }

        public Character Create(string name, Point location) {
            Image imageFrames = LoadImageFrames(name);
            Character character = new Character(name, location, imageFrames);
            character.ImageDurationMs = 200;

            return character;
        }

        private Image LoadImageFrames(string name) {
            Image imageFrames;

            if (name.ToLower() == "girl") {
                imageFrames = Image.FromFile(path + "girl.png");
            }
            else if (name.ToLower() == "moviestar") {
                imageFrames = Image.FromFile(path + "star.png");
            }
            else {
                imageFrames = Image.FromFile(path + "boy.png");                
            }

            return imageFrames;
        }
    }
}
