using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyGame.Controls {
    class PlayerParams {
        public Character.Character ControlledCharacter { get; set; }
        public bool HasControl { get; set; }

        public PlayerParams() {
            HasControl = false;
        }
    }
}
