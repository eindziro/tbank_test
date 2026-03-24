using System;
using System.Collections.Generic;
using System.Text;

class Program
{
    static void Main()
    {
        int t = int.Parse(Console.ReadLine());
        StringBuilder output = new StringBuilder();

        for (int test = 0; test < t; test++)
        {
            string s = Console.ReadLine();
            int n = s.Length;

            int[,] a = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    a[i, j] = s[(i + j) % n] - '0';
                }
            }

            long maxArea = FindMaxRectangle(a, n);
            output.AppendLine(maxArea.ToString());
        }

        Console.Write(output.ToString());
    }

    static long FindMaxRectangle(int[,] matrix, int n)
    {
        int[] heights = new int[n];
        long maxArea = 0;

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                heights[j] = matrix[i, j] == 1 ? heights[j] + 1 : 0;
            }

            maxArea = Math.Max(maxArea, LargestRectangleArea(heights));
        }

        return maxArea;
    }

    static long LargestRectangleArea(int[] heights)
    {
        long maxArea = 0;
        Stack<int> stack = new Stack<int>();

        for (int i = 0; i < heights.Length; i++)
        {
            while (stack.Count > 0 && heights[stack.Peek()] > heights[i])
            {
                int h = heights[stack.Pop()];
                int w = i - (stack.Count > 0 ? stack.Peek() + 1 : 0);
                maxArea = Math.Max(maxArea, (long)h * w);
            }

            stack.Push(i);
        }

        while (stack.Count > 0)
        {
            int h = heights[stack.Pop()];
            int w = heights.Length - (stack.Count > 0 ? stack.Peek() + 1 : 0);
            maxArea = Math.Max(maxArea, (long)h * w);
        }

        return maxArea;
    }
}
