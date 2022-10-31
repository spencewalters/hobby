using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HobbyGame.Controls {
    class EmptyController : GameController {
        public void HandleKeyboardInput(KeyEventArgs e) {
            Console.WriteLine("Receved keyboard input and doing nothing with it.");
        }

        public void HandleMouseClick(MouseEventArgs e) {
            Console.WriteLine("Receved mouse click and doing nothing with it.");
        }
    }
}
