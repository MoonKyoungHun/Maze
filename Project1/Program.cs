namespace Project1
{
    internal class Program
    {
        public struct GameData()
        {
            public bool running;

            public bool[,] map;
            public ConsoleKey inputKey;
            public Point player;
            public Point goal;
            public Point temp;
            public Point temp1;
            public Point fake;
            public Point fake1;
            public Point wall;
        }
        public struct Point()
        {
            public int x;
            public int y;
        }

        
        public static void start()
        {

            Console.CursorVisible = false;

            data.running = true;

            data.map = new bool[,]

            {
                { false, false, false, false, false, false, false, false, false, false, false, false},
                { false,  true, false, false, false,  true,  true, false,  true,  true,  true, false},
                { false,  true, false, false, false,  true,  true, false,  true,  true,  true, false},
                { false,  true, false, false, false, false,  true, false, false, false,  true, false},
                { false,  true, false, false, false, false,  true, false, false, false,  true, false},
                { false,  true,  true,  true, true,   true,  true,  true,  true, false,  true, false},
                { false,  true, false, false, false,  true,  true,  true,  true, false,  true, false},
                { false,  true, false,  true, false,  true,  true, false, false, false, false, false},
                { false,  true, false,  true,  true,  true,  true, false, false, false, false, false},
                { false,  true, false,  true,  true,  true,  true,  true,  true,  true, false, false},
                { false,  true, false,  true,  true,  true,  true,  true,  true,  true,  true, false},
                { false, false, false, false, false, false, false, false, false, false, false, false},
            };


            data.wall = new Point { x = 7, y = 1 };
            data.fake = new Point { x = 6, y = 4 };
            data.fake1 = new Point { x = 6, y = 3 };
            data.temp = new Point() { x = 10, y = 10 };
            data.temp1 = new Point() { x = 10, y = 6 };
            data.player = new Point() { x = 1, y = 1 };
            data.goal = new Point() { x = 8, y = 1 };


            Console.Clear();
            Console.WriteLine(" ==========================");
            Console.WriteLine("||                        ||");
            Console.WriteLine("||     랜덤 미로 찾기     ||");
            Console.WriteLine("||                        ||");
            Console.WriteLine(" ==========================");
            Console.WriteLine();
            Console.WriteLine("아무 키 하나 눌러주세요.");
            Console.ReadKey();
        }

        static GameData data;

        static void Main(string[] args)
        {
            start();

            while (data.running)
            {
                Render();
                Input();
                Update();
            }

            End();
        }

        static void End()
        {
            Console.Clear();

            Console.WriteLine(" ==========================");
            Console.WriteLine("||                        ||");
            Console.WriteLine("||     탈출     성공!     ||");
            Console.WriteLine("||                        ||");
            Console.WriteLine(" ==========================");
        }

        static void Render()
        {
            Console.Clear();

            PrintMap();
            //PrintWall();
            PrintPlayer();
            PrintGoal();
            PrintTemp();
            PrintFake();
            PrintBlink();
        }

        static void PrintMap()
        {
            for (int y = 0; y < data.map.GetLength(0); y++)
            {
                for (int x = 0; x < data.map.GetLength(1); x++)
                {
                    if (data.wall.x == x && data.wall.y == y)
                    {
                        Console.Write(" ");
                    }
                    else if (data.map[y,x])
                    {
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.Write("#");
                    }

                }
                Console.WriteLine();
            }
            
        }

        static void PrintPlayer()
        {
            Console.SetCursorPosition(data.player.x, data.player.y);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("P");
            Console.ResetColor();
        }

        static void PrintGoal()
        {
            Console.SetCursorPosition(data.goal.x, data.goal.y);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("G");
            Console.ResetColor();
        }

        static void PrintTemp()
        {
            Console.SetCursorPosition(data.temp.x, data.temp.y);
            Console.WriteLine("#");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(data.temp1.x, data.temp1.y);
            Console.WriteLine("#");
            Console.ResetColor();
        }
        static void PrintFake()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.SetCursorPosition(data.fake.x, data.fake.y);
            Console.WriteLine("#");
            Console.SetCursorPosition(data.fake1.x, data.fake1.y);
            Console.WriteLine("#");
            Console.ResetColor();
        }

        static void PrintBlink()
        {
            if (data.player.x == data.temp.x && data.player.y == data.temp.y)
            {
                data.player = data.temp1;
            }
            else if (data.player.x == data.temp1.x && data.player.y == data.temp1.y)
            {
                data.player = data.temp;
            }
        }


        //static void PrintWall()
        //{
        //    Console.SetCursorPosition(data.wall.x, data.wall.y);
        //    Console.WriteLine(" ");
        //}


        static void Input()
        {
            data.inputKey = Console.ReadKey(true).Key;
        }

        static void Update()
        {
            Move();
            CheckGameClear();
        }

        static void Move()
        {
            switch (data.inputKey)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    MoveUp();
                    break;

                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    MoveDown();
                    break;

                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    MoveLeft();
                    break;

                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    MoveRight();
                    break;

            }
        }


        static void MoveUp()
        {
            Point next = new Point() { x = data.player.x, y = data.player.y - 1 };
            if (data.map[next.y, next.x])
            {
                data.player = next;
            }
        }
        static void MoveDown()
        {
            Point next = new Point() { x = data.player.x, y = data.player.y + 1 };
            if (data.map[next.y, next.x])
            {
                data.player = next;
            }
        }
        static void MoveLeft()
        {
            Point next = new Point() { x = data.player.x - 1, y = data.player.y };
            if (data.map[next.y, next.x])
            {
                data.player = next;
            }
        }
        static void MoveRight()
        {
            Point next = new Point() { x = data.player.x + 1, y = data.player.y };
            if (data.map[next.y, next.x])
            {
                data.player = next;
            }
        }

        static void CheckGameClear()
        {
            if (data.player.x == data.goal.x && data.player.y == data.goal.y)
            {
                data.running = false;
            }
        }
    }
}
