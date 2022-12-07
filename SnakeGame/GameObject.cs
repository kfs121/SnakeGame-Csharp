using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeGame
{
    class GameObject
    {
        private static bool mIsGameOver = false;
        public static bool IsGameOver
        {
            get { return mIsGameOver; }
            set
            {
                mIsGameOver = value;
            }
        }
        public int X { get; private set; }
        public int Y { get; private set; }
        public char Image { get; private set; }
        public GameObject(int x, int y, char c)
        {
            X = x;
            Y = y;
            Image = c;
        }

        public void setImage(char c)
        {
            Image = c;
        }

        public void MoveDirection(EDirection direction)
        {
            switch (direction)
            {
                case EDirection.LEFT:
                    MoveLeft();
                    break;
                case EDirection.RIGHT:
                    MoveRight();
                    break;
                case EDirection.UP:
                    MoveUp();
                    break;
                case EDirection.DOWN:
                    MoveDown();
                    break;
            }
        }

        private void MoveLeft()
        {
            if (X - 1 < 0)
            {
                IsGameOver = true;
                return;
            }
            X--;
            
        }

        private void MoveRight()
        {
            if (X + 1 >= Console.WindowWidth)
            {
                IsGameOver = true;
                return;
            }
            X++;
            
        }

        private void MoveUp()
        {
            if (Y + 1 >= Console.WindowHeight)
            {
                IsGameOver = true;
                return;
            }
            Y++;
            
        }

        private void MoveDown()
        {
            if (Y - 1< 0)
            {
                IsGameOver = true;
                return;
            }
            Y--;
            
        }

        public bool CheckCollision(GameObject pos)
        {
            if (pos == null)
            {
                return false;
            }
            return (X == pos.X && Y == pos.Y);
        }
    }
}
