using System;
using System.Collections.Generic;
using System.Drawing;

namespace HobbyGame.Effects {
    class ClickEffect : EffectSprite {
        public int ImageDurationMs { get; set; }
        public Point MapLocation { get; set; }
        public bool Active { get; internal set; }
        public List<Image> Images;
        private DateTime imageClock;
        private int currentFrame = 0;

        public ClickEffect(int durationMs, Point maplocation) {
            ImageDurationMs = durationMs;
            MapLocation = maplocation;
            Active = true;
        }

        public Image CurrentImage() {
            if (DateTime.Now.Subtract(imageClock) > TimeSpan.FromMilliseconds(ImageDurationMs)) {
                currentFrame++;
                if (currentFrame >= Images.Count) {
                    currentFrame = 0;
                    Active = false;
                }
                imageClock = DateTime.Now;
            }
            return Images[currentFrame];
        }
    }
}
