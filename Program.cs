namespace XOGame;

class Program
{
    static string x_symbol = "X";
    static string o_symbol = "O";
    static string space_symbol = "-";
    static string player = "player_1";
    static int size = 15;
    static bool endGame = false;
    static int number_symbol_win = 3;
    static string[,] caro_table = new string[size, size];
    static void Main(string[] args)
    {
        string confirm = "Y";
        do
        {
            Console.Clear();
            if (confirm?.ToUpper() != "Y" && confirm?.ToUpper() != "Q")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid value");
                Console.ResetColor();
            }
            Console.WriteLine("Press Y to play game");
            Console.WriteLine("Press Q to quit");
            Console.Write("Enter your choice: ");
            confirm = Console.ReadLine() ?? "";
        }
        while (confirm?.ToUpper() != "Y" && confirm?.ToUpper() != "Q");
        if (confirm.ToUpper().Equals("Q"))
            Environment.Exit(0);
        InitCaroTable();
        StartGame();
    }
    static void StartGame()
    {
        do
        {
            DrawCaroTable();
            do
            {
                PlayGame();
                DrawCaroTable();
            }
            while (!endGame);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{(player == "player_1" ? "Player 1" : "Player 2")} is win");
            Console.ResetColor();
            Console.WriteLine("===============================");
            Console.WriteLine("Press Q to quit");
            Console.WriteLine("Press any key to play again");
            Console.Write("Enter your choice: ");
            string quit_key = Console.ReadLine() ?? "";
            if (quit_key.ToUpper().Equals("Q"))
                Environment.Exit(0);
            else
            {
                InitCaroTable();
            }
        }
        while (true);

    }
    static void InitCaroTable()
    {
        endGame = false;
        player = "player_1";
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                if (row == 0)
                {
                    caro_table[row, col] = $"{(col < 10 ? (" " + col) : col)}";
                }
                else if (col == 0)
                {
                    caro_table[row, col] = $"{(row < 10 ? (" " + row) : row)}";
                }
                else
                {
                    caro_table[row, col] = space_symbol;
                }
            }
        }
    }
    static void DrawCaroTable()
    {
        Console.Clear();
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                if (caro_table[row, col] == x_symbol || caro_table[row, col] == o_symbol || caro_table[row, col] == space_symbol)
                {
                    if (caro_table[row, col] == x_symbol)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"  {caro_table[row, col]} ");
                        Console.ResetColor();
                    }
                    else if (caro_table[row, col] == o_symbol)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"  {caro_table[row, col]} ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write($"  {caro_table[row, col]} ");
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($" {caro_table[row, col]} ");
                    Console.ResetColor();
                }
            }
            Console.WriteLine();
        }
    }
    static void PlayGame()
    {
        int pos_x;
        int pos_y;
        bool isValidPosition = true;
        do
        {
            if (!isValidPosition)
            {
                Console.WriteLine("Invalid Position");
            }
            Console.Write($"{(player == "player_1" ? "Player 1" : "Player 2")} (x,y): ");
            string value = Console.ReadLine() ?? "";
            string[] position = value.Contains(",") ? value.Split(",") : new string[] { "0", "0" };
            pos_x = Convert.ToInt32(position[0]);
            pos_y = Convert.ToInt32(position[1]);
            isValidPosition = CheckValidPosition(pos_x, pos_y);
        }
        while (!isValidPosition);
        caro_table[pos_x, pos_y] = player == "player_1" ? x_symbol : o_symbol;
        endGame = CheckGameIsOver(pos_x, pos_y);
        if (!endGame)
            player = player == "player_1" ? "player_2" : "player_1";
    }
    static bool CheckValidPosition(int x, int y)
    {
        return x > 0 && y > 0 && x < size && y < size
            && caro_table[x, y] != x_symbol
            && caro_table[x, y] != o_symbol;
    }
    static bool CheckGameIsOver(int x, int y)
    {
        return CheckHorizontal(x, y)
            || CheckVertical(x, y)
            || CheckMainDiagonal(x, y)
            || CheckSecondaryDiagonal(x, y);
    }
    static bool CheckHorizontal(int current_x, int current_y)
    {
        int start_x = current_x;
        int start_y = 1;
        int end_y = size - 1;
        int count = 0;
        string count_symbol = player == "player_1" ? x_symbol : o_symbol;
        for (int y = start_y; y <= end_y; y++)
        {
            if (caro_table[start_x, y] == count_symbol)
            {
                count++;
                if (count == number_symbol_win)
                {
                    return true;
                }
            }
            else
            {
                count = 0;
            }
        }
        return false;
    }
    static bool CheckVertical(int current_x, int current_y)
    {
        int start_x = 1;
        int start_y = current_y;
        int end_x = size - 1;
        int count = 0;
        string count_symbol = player == "player_1" ? x_symbol : o_symbol;
        for (int x = start_x; x <= end_x; x++)
        {
            if (caro_table[x, start_y] == count_symbol)
            {
                count++;
                if (count == number_symbol_win)
                {
                    return true;
                }
            }
            else
            {
                count = 0;
            }
        }
        return false;
    }
    static bool CheckMainDiagonal(int current_x, int current_y)
    {
        int min = current_x > current_y ? current_y : current_x;
        int max = current_x > current_y ? current_x : current_y;
        int distance_min = min - 1;
        int distance_max = size - max;
        int start_x = current_x - distance_min;
        int start_y = current_y - distance_min;
        int end_x = current_x + distance_max;
        int end_y = current_y + distance_max;
        int count = 0;
        string count_symbol = player == "player_1" ? x_symbol : o_symbol;
        for (int x = start_x, y = start_y; x < end_x && y < end_y; x++, y++)
        {
            if (caro_table[x, y] == count_symbol)
            {
                count++;
                if (count == number_symbol_win)
                {
                    return true;
                }
            }
            else
            {
                count = 0;
            }
        }
        return false;
    }
    static bool CheckSecondaryDiagonal(int current_x, int current_y)
    {
        int tmp_current_x = current_x;
        int tmp_current_y = current_y;
        while (tmp_current_x < size - 1 && tmp_current_y > 1)
        {
            tmp_current_x++;
            tmp_current_y--;
        }
        int start_x = tmp_current_x;
        int start_y = tmp_current_y;
        int end_x = tmp_current_y;
        int end_y = tmp_current_x;
        int count = 0;
        string count_symbol = player == "player_1" ? x_symbol : o_symbol;
        for (int x = start_x, y = start_y; x >= end_x && y <= end_y; x--, y++)
        {
            if (caro_table[x, y] == count_symbol)
            {
                count++;
                if (count == number_symbol_win)
                {
                    return true;
                }
            }
            else
            {
                count = 0;
            }
        }
        return false;
    }
}
