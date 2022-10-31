using System.Collections.Generic;
using System.Drawing;

namespace HobbyGame.Effects {
    class EffectsFactory {
        private string path;

        public EffectsFactory() {
            path = System.IO.Directory.GetCurrentDirectory() + "\\effects\\";
        }

        public EffectSprite Create(int durationMs, Point mapLocation) {
            ClickEffect sprite = new ClickEffect(durationMs, mapLocation);
            sprite.Images = LoadImages();
            
            return sprite;
        }


        private List<Image> LoadImages() {
            List<Image> imageList = new List<Image>();
            imageList.Add(Image.FromFile(path + "click-effect-2.png"));
            imageList.Add(Image.FromFile(path + "click-effect-3.png"));
            imageList.Add(Image.FromFile(path + "click-effect-4.png"));
            imageList.Add(Image.FromFile(path + "click-effect-5.png"));
            imageList.Add(Image.FromFile(path + "click-effect-6.png"));
            imageList.Add(Image.FromFile(path + "click-effect-1.png"));
            return imageList;
        }

    }
}
