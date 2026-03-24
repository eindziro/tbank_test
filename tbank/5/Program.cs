using System;
using System.Collections.Generic;
using System.Linq;

class Solution
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        int[] a = Console.ReadLine().Split().Select(int.Parse).ToArray();

        int[] result = new int[n];

        // Для каждого возможного финального значения найти минимальное количество ходов
        var possibleValues = new HashSet<int>(a);

        var stepsToUniform = new Dictionary<int, int>();

        // BFS для поиска всех состояний и ходов до них
        var allStates = BFSAll(a, n);

        // Находим для каждого возможного значения минимальное количество ходов чтобы все стали равны
        foreach (var kvp in allStates)
        {
            int[] arr = StringToState(kvp.Key, n);
            int moves = kvp.Value;

            if (arr.All(x => x == arr[0]))
            {
                int value = arr[0];
                if (!stepsToUniform.ContainsKey(value))
                {
                    stepsToUniform[value] = moves;
                }
            }
        }

        // Для каждого человека вывести минимальное количество ходов до его финального значения
        for (int person = 0; person < n; person++)
        {
            int value = a[person];
            result[person] = stepsToUniform.ContainsKey(value) ? stepsToUniform[value] : int.MaxValue;
        }

        Console.WriteLine(string.Join(" ", result));
    }

    static Dictionary<string, int> BFSAll(int[] a, int n)
    {
        string initial = StateToString(a);
        var queue = new Queue<(string state, int moves)>();
        var visited = new Dictionary<string, int> { { initial, 0 } };

        queue.Enqueue((initial, 0));

        while (queue.Count > 0)
        {
            var (state, moves) = queue.Dequeue();
            int[] arr = StringToState(state, n);

            // Пробуем все возможные тройки подряд идущих людей
            for (int i = 0; i < n; i++)
            {
                int i1 = i;
                int i2 = (i + 1) % n;
                int i3 = (i + 2) % n;

                int min = Math.Min(arr[i1], Math.Min(arr[i2], arr[i3]));
                int max = Math.Max(arr[i1], Math.Max(arr[i2], arr[i3]));

                // Пробуем изменить каждого из трёх на min или max
                for (int j = 0; j < 3; j++)
                {
                    int idx = (i + j) % n;

                    foreach (int val in new[] { min, max })
                    {
                        if (arr[idx] == val) continue;

                        int[] newArr = (int[])arr.Clone();
                        newArr[idx] = val;
                        string newState = StateToString(newArr);

                        if (!visited.ContainsKey(newState))
                        {
                            visited[newState] = moves + 1;
                            queue.Enqueue((newState, moves + 1));
                        }
                    }
                }
            }
        }

        return visited;
    }

    static string StateToString(int[] arr)
    {
        return string.Join(",", arr);
    }

    static int[] StringToState(string state, int n)
    {
        return state.Split(',').Select(int.Parse).ToArray();
    }
}