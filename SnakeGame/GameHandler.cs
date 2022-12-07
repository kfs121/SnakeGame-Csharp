using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SnakeGame
{
    class GameHandler
    {
        private bool mIsGameOver;
        public bool IsGameOver { 
            get 
            {
                return (GameObject.IsGameOver || mIsGameOver);
            }
            private set
            {
                mIsGameOver = value;
            } 
        }
        const int INIT_LENTH = 6;
        const double INIT_SLEEP_TIME = 100;
        const int FOOD_DISAPPEAR_TIME = 15000;
        const double MIN_SLEEP_TIME = 1;
        public int UserPoints { get; private set; }

        private int lastFoodTime;
        private int negativePoints;
        private double sleepTime;
 

        public EDirection Direction { get; set; }
        private List<GameObject> obstacles;
        private List<GameObject> snakeList;
        private GameObject food;
        private ScreenBuffer screenBuffer;
        private Random random;

        public GameHandler()
        {
            lastFoodTime = Environment.TickCount;
            negativePoints = 0;
            sleepTime = INIT_SLEEP_TIME;
            IsGameOver = false;
            obstacles = new List<GameObject>();
            random = new Random();
            addFood();
            for(int i = 0; i < 5; i++)
            {
                addObstacle();
            }
            Direction = EDirection.RIGHT;
            snakeList = new List<GameObject>();
            for(int i = 0; i < INIT_LENTH; i++)
            {
                snakeList.Add(new GameObject(i, 1, '*'));
            }
            screenBuffer = new ScreenBuffer();
        }
        public void GameTick()
        {
            Thread.Sleep((int)sleepTime);
            reduceSleepTime(0.01);
        }

        public void UpdateGame()
        {
            addNegativePoint(1);
            GameObject snakeHead = snakeList.Last();
            GameObject snakeNewHead = new GameObject(snakeHead.X, snakeHead.Y, getHeadImage(Direction));
            snakeNewHead.MoveDirection(Direction);

            if (snakeList.CheckCollision(snakeNewHead) || obstacles.CheckCollision(snakeNewHead))
            {
                IsGameOver = true;
                return;
            }
            snakeList.Add(snakeNewHead);
            snakeList[snakeList.Count - 2].setImage('*');

            if (snakeNewHead.CheckCollision(food))
            {
                addFood();
                addObstacle();
                lastFoodTime = Environment.TickCount;
                reduceSleepTime(1);
            }
            else
            {
                screenBuffer.DrawToBackBuffer(snakeList[0], ' ');
                snakeList.RemoveAt(0);
            }

            if(Environment.TickCount - lastFoodTime >= FOOD_DISAPPEAR_TIME)
            {
                addNegativePoint(100);
                addFood();
                lastFoodTime = Environment.TickCount;
            }
        }

        public void Render()
        {
            screenBuffer.DrawToBackBuffer(snakeList);
            screenBuffer.DrawToBackBuffer(food);
            screenBuffer.DrawToBackBuffer(obstacles);
            drawPoints();
            screenBuffer.Render();
        }

        private char getHeadImage(EDirection direction)
        {
            switch (direction)
            {
                case EDirection.RIGHT:
                    return '>';
                case EDirection.LEFT:
                    return '<';
                case EDirection.UP:
                    return '^';
                case EDirection.DOWN:
                    return 'v';
                default:
                    return '\0';
            }
        }

        private void addObstacle()
        {
            GameObject obstacle;
            do
            {
                obstacle = new GameObject(random.Next(Console.WindowWidth), random.Next(Console.WindowHeight), '=');
            }
            while (snakeList.CheckCollision(obstacle) || obstacles.CheckCollision(obstacle) ||
            food.CheckCollision(obstacle));
            obstacles.Add(obstacle);
        }

        private void addFood()
        {
            do
            {
                food = new GameObject(random.Next(Console.WindowWidth), random.Next(Console.WindowHeight), '@');
            }
            while (snakeList.CheckCollision(food) || obstacles.CheckCollision(food));
        }

        private void addNegativePoint(int p)
        {
            negativePoints += p;
        }

        private void reduceSleepTime(double time)
        {
            if(sleepTime >= MIN_SLEEP_TIME)
            {
                sleepTime -= time;
            }
        }

        private void drawPoints()
        {
            UserPoints = (snakeList.Count - INIT_LENTH) * 1000 - negativePoints;
            UserPoints = Math.Max(UserPoints, 0);
            screenBuffer.DrawToBackBuffer(Console.WindowWidth - 20, Console.WindowHeight - 2, $"Points : {UserPoints}");
        }
    }
}
