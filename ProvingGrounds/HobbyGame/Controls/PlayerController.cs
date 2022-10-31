using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HobbyGame.Ambulation;
using HobbyGame.Character;

namespace HobbyGame.Controls {
    class PlayerController : GameController {
        private PlayerParams playerParams;

        public PlayerController(PlayerParams playerParams) {
            this.playerParams = playerParams;
        }

        public void HandleKeyboardInput(KeyEventArgs e) {
            if (playerParams.HasControl) {
                switch (e.KeyCode) {
                    case Keys.Left:
                        playerParams.ControlledCharacter.Move(MovementDirection.Left);
                        break;
                    case Keys.Up:
                        playerParams.ControlledCharacter.Move(MovementDirection.Up);
                        break;
                    case Keys.Right:
                        playerParams.ControlledCharacter.Move(MovementDirection.Right);
                        break;
                    case Keys.Down:
                        playerParams.ControlledCharacter.Move(MovementDirection.Down);
                        break;
                    default:
                        break;
                }
            }
        }

        public void HandleMouseClick(MouseEventArgs e) {
            Console.WriteLine("Receved mouse click and doing nothing with it.");
        }
    }
}
