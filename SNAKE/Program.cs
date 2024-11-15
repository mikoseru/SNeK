// See https://aka.ms/new-console-template for more information

class Program
{
    static int screenWidth = 50;
    static int screenHeight = 50;
    static List<Position> Snek = new List<Position>();
    static Position food;
    static int score = 0;
    static int speed = 100;
    static Random random = new Random();
    static bool gameOver = false;

    enum Direction { Left, Right, Up, Down }
    static Direction currentDirect = Direction.Right;
    static Direction nextDirect = Direction.Right;

    struct Position
    {
        public int X;
        public int Y;

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    static void Main()
    {
        Snek.Add(new Position(screenWidth / 2, screenHeight / 2));

        GenerateFood();

        // Game Loop
        while (!gameOver)
        {
            HandleInput();

            MoveSnek();

            if (CheckCollisions())
            {
                gameOver = true;
                break;
            }

            Position head = Snek[0];
            if (head.X == food.X && head.Y == food.Y)
            {
                score++;
                GenerateFood();
                speed = Math.Max(50, speed - 5);
            }

            Render();

            Thread.Sleep(speed);
        }


        // Game over message
        Console.Clear();
        Console.SetCursorPosition(screenWidth / 2 - 5, screenHeight / 2);
        Console.WriteLine("GAME OVER");
        Console.SetCursorPosition(screenWidth / 2 - 5, screenHeight / 2 + 1);
        Console.WriteLine("Score: " + score);
    }

    static void SnekEatFood()
    {

    }

    static void Render()
    {
        Console.Clear();

        //Draw Snek
        foreach (var ring in Snek)
        {
            Console.SetCursorPosition(ring.X, ring.Y);
            Console.Write("O");
        }

        //Draw food
        Console.SetCursorPosition(food.X, food.Y);
        Console.Write("F");

        //Draw score
        Console.SetCursorPosition(0, screenHeight);
        Console.Write(score);
    }

    static bool CheckCollisions()
    {
        Position head = Snek[0];

        if (head.X < 0 || head.X > screenWidth || head.Y < 0 || head.Y > screenHeight)
            return true;

        for (int i = 1; i < Snek.Count; i++)
        {
            // Check head collision with body
            if (head.X == Snek[i].X && head.Y == Snek[i].Y)
                return true;
        }


        //

        return false;
    }

    static void MoveSnek()
    {
        currentDirect = nextDirect;

        Position newHead = Snek[0];

        switch (currentDirect)
        {
            case Direction.Up:
                newHead.Y--;
                break; 
            case Direction.Down:
                newHead.Y++;
                break;
            case Direction.Left:
                newHead.X--;
                break;
            case Direction.Right:
                newHead.X++;
                break;
        }

        Snek.Insert(0, newHead);

        if(newHead.X != food.X || newHead.Y != food.Y)
            Snek.RemoveAt(Snek.Count - 1);
    }

    private static void HandleInput()
    {
            var key = Console.Read();

            // Prevent the Snek from reversing direction
            if (key == ((int)ConsoleKey.UpArrow) && currentDirect != Direction.Down)
                nextDirect = Direction.Up;
            else if (key == ((int)ConsoleKey.DownArrow) && currentDirect != Direction.Up)
                nextDirect = Direction.Down;
            else if (key == ((int)ConsoleKey.LeftArrow) && currentDirect != Direction.Right)
                nextDirect = Direction.Left;
            else if (key == ((int)ConsoleKey.RightArrow) && currentDirect != Direction.Left)
                nextDirect = Direction.Right;
    }

    static void GenerateFood()
    {
        food = new Position(random.Next(0, screenWidth), random.Next(0, screenHeight));
    }
}