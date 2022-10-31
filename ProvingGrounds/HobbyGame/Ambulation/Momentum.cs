using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyGame.Ambulation {
    class Momentum {
        public double Velocity { get; set; }
        public MovementDirection Direction { get; set; }

        public Momentum() {
            Velocity = 0;
            Direction = MovementDirection.Down;
        }
    }
}
