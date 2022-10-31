using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HobbyGame.Character;

namespace HobbyGame.Controls {
    interface GameController {
        void HandleMouseClick(MouseEventArgs e);
        void HandleKeyboardInput(KeyEventArgs e);
    }
}
