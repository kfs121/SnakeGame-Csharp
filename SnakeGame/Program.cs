using System;

namespace SnakeGame
{
    public enum EDirection
    {
        RIGHT, LEFT, DOWN, UP, MAX
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WindowWidth = Console.WindowWidth / 2;
            Console.BufferWidth = Console.WindowWidth;
            GameHandler handler = new GameHandler();

            while (!handler.IsGameOver)
            {
                handler.GameTick();
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo userInput = Console.ReadKey(true);
                    if(userInput.Key == ConsoleKey.LeftArrow && handler.Direction != EDirection.RIGHT)
                    {
                        handler.Direction = EDirection.LEFT;
                    }
                    else if (userInput.Key == ConsoleKey.RightArrow && handler.Direction != EDirection.LEFT)
                    {
                        handler.Direction = EDirection.RIGHT;
                    }
                    else if (userInput.Key == ConsoleKey.UpArrow && handler.Direction != EDirection.DOWN)
                    {
                        handler.Direction = EDirection.UP;
                    }
                    else if (userInput.Key == ConsoleKey.DownArrow && handler.Direction != EDirection.UP)
                    {
                        handler.Direction = EDirection.DOWN;
                    }
                }
                handler.UpdateGame();
                handler.Render();
                
            }
            Console.SetCursorPosition(1, 1);
            Console.WriteLine("Game over!");
        }
    }
}
