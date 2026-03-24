using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static List<int>[] graph;
    static int n, m;

    static void Main()
    {
        string[] input = Console.ReadLine().Split();
        n = int.Parse(input[0]);
        m = int.Parse(input[1]);

        graph = new List<int>[n + 1];
        for (int i = 1; i <= n; i++)
            graph[i] = new List<int>();

        List<(int, int)> edges = new List<(int, int)>();

        for (int i = 0; i < m; i++)
        {
            input = Console.ReadLine().Split();
            int a = int.Parse(input[0]);
            int b = int.Parse(input[1]);

            graph[a].Add(b);
            graph[b].Add(a);
            edges.Add((a, b));
        }

        int minCycle = int.MaxValue;

        foreach (var edge in edges)
        {
            int u = edge.Item1;
            int v = edge.Item2;

            int distance = BFS(u, v, u, v);

            if (distance != -1)
            {
                minCycle = Math.Min(minCycle, distance + 1);
            }
        }

        if (minCycle == int.MaxValue)
            Console.WriteLine(-1);
        else
            Console.WriteLine(minCycle);
    }

    static int BFS(int start, int target, int forbiddenU, int forbiddenV)
    {
        int[] dist = new int[n + 1];
        for (int i = 1; i <= n; i++)
            dist[i] = -1;

        Queue<int> queue = new Queue<int>();
        dist[start] = 0;
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            int current = queue.Dequeue();

            foreach (int next in graph[current])
            {
                if ((current == forbiddenU && next == forbiddenV) ||
                    (current == forbiddenV && next == forbiddenU))
                    continue;

                if (dist[next] == -1)
                {
                    dist[next] = dist[current] + 1;
                    if (next == target)
                        return dist[next];
                    queue.Enqueue(next);
                }
            }
        }

        return -1;
    }
}