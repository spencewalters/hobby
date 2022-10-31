using System.Drawing;

namespace HobbyGame.Effects {
    interface EffectSprite {
        int ImageDurationMs { get; set; }
        Point MapLocation { get; set; }
        Image CurrentImage();
        bool Active { get; }
    }
}