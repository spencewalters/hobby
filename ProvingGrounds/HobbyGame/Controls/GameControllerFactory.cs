using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HobbyGame.Character;

namespace HobbyGame.Controls {
    class GameControllerFactory {
        public GameController GetGameController(PlayerParams playerParams) {
            return new PlayerController(playerParams);
        }

        public GameController GetEmptyController() {
            return new EmptyController();
        }
    }
}
