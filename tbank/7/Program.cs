using System;

class Program
{
    const long MOD = 1000000007;

    static void Main()
    {
        var input = Console.ReadLine().Split();
        int n = int.Parse(input[0]);
        int k = int.Parse(input[1]);

        long result = CountBishopPlacements(n, k);
        Console.WriteLine(result % MOD);
    }

    static long CountBishopPlacements(int n, int k)
    {
        long[] diagonal1 = new long[2 * n - 1];
        long[] diagonal2 = new long[2 * n - 1];

        return CountRecursive(n, k, 0, diagonal1, diagonal2);
    }

    static long CountRecursive(int n, int k, int cell, long[] diag1, long[] diag2)
    {
        if (k == 0)
            return 1;

        if (cell == n * n || n * n - cell < k)
            return 0;

        int row = cell / n;
        int col = cell % n;
        int d1 = row + col;
        int d2 = row - col + n - 1;

        long result = CountRecursive(n, k, cell + 1, diag1, diag2);

        if (diag1[d1] == 0 && diag2[d2] == 0)
        {
            diag1[d1]++;
            diag2[d2]++;
            result += CountRecursive(n, k - 1, cell + 1, diag1, diag2);
            diag1[d1]--;
            diag2[d2]--;
        }

        return result;
    }
}

