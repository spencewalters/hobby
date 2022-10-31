using System;
using System.Collections.Generic;
using System.Drawing;
using HobbyGame.Character;

namespace HobbyGame.Ambulation {
    class Ambulator {
        private readonly double Friction=.35;

        public Ambulator(List<Character.Character> characters) {
            Characters = characters;
        }

        public List<Character.Character> Characters { get; }

        public void Ambulate() {
            foreach (Character.Character character in Characters) {
                if (character.Momentum.Velocity > 0) {
                    character.Action = CharacterAction.Walking;
                    MoveCharacter(character);
                }
                else if (character.MovingToTarget)
                    MoveToTarget((int)character.MaxVelocity, character);
                else
                    character.Action = CharacterAction.Standing;
            }
        }

        private void MoveCharacter(Character.Character character) {

            double velocity = character.Momentum.Velocity;
            int X=character.MapLocation.X, 
                Y=character.MapLocation.Y;

            //Console.WriteLine($"Moving {character.Name} at speed of {velocity}");

            switch (character.Momentum.Direction) {
                case MovementDirection.Down:
                    Y += (int)velocity;
                    break;
                case MovementDirection.Left:
                    X -= (int)velocity;
                    break;
                case MovementDirection.Right:
                    X += (int)velocity;
                    break;
                case MovementDirection.Up:
                    Y -= (int)velocity;
                    break;
                default:
                    break;
            }
            velocity -= Friction;
            character.Momentum.Velocity = velocity;
            character.Direction = character.Momentum.Direction;
            character.MapLocation = new Point(X, Y);
        }

        private static void MoveToTarget(int characterSpeed, Character.Character character) {
            if (character.MapLocation != character.MapTarget) {
                int currentX = (int)character.MapLocation.X;
                int currentY = (int)character.MapLocation.Y;

                if (character.MapTarget.X > currentX) {
                    int diff = character.MapTarget.X - currentX;
                    if (diff < characterSpeed) {
                        currentX += diff;
                        character.Action = CharacterAction.Standing;
                    }
                    else {
                        currentX += characterSpeed;
                        character.Action = CharacterAction.Walking;
                    }

                    character.Direction = MovementDirection.Right;
                }
                else if (character.MapTarget.X < currentX) {
                    int diff = currentX - character.MapTarget.X;
                    if (diff < characterSpeed) {
                        currentX -= diff;
                        character.Action = CharacterAction.Standing;
                    }
                    else {
                        currentX -= characterSpeed;
                        character.Action = CharacterAction.Walking;
                    }

                    character.Direction = MovementDirection.Left;
                }

                if (character.MapTarget.Y > currentY) {
                    int diff = character.MapTarget.Y - currentY;
                    if (diff < characterSpeed) {
                        currentY += diff;
                        character.Action = CharacterAction.Standing;
                    }
                    else {
                        currentY += characterSpeed;
                        character.Action = CharacterAction.Walking;
                    }

                    character.Direction = MovementDirection.Down;
                }
                else if (character.MapTarget.Y < currentY) {
                    int diff = currentY - character.MapTarget.Y;
                    if (diff < characterSpeed) {
                        currentY -= diff;
                        character.Action = CharacterAction.Standing;
                    }
                    else {
                        currentY -= characterSpeed;
                        character.Action = CharacterAction.Walking;
                    }

                    character.Direction = MovementDirection.Up;
                }

                character.MapLocation = new Point(currentX, currentY);
            }
            else
                character.MovingToTarget = false;
        }
    }
}
