using System;

class Program
{
    static void Main()
    {
        string s = Console.ReadLine();
        string target1 = "tbank";
        string target2 = "study";

        int n = s.Length;
        int minChanges = int.MaxValue;

        for (int i = 0; i <= n - target1.Length; i++)
        {
            for (int j = 0; j <= n - target2.Length; j++)
            {
                int changes = TryPlacement(s, i, target1, j, target2);
                minChanges = Math.Min(minChanges, changes);
            }
        }

        Console.WriteLine(minChanges);
    }

    static int TryPlacement(string s, int pos1, string target1, int pos2, string target2)
    {
        int n = s.Length;
        char[] result = s.ToCharArray();

        int start1 = pos1;
        int end1 = pos1 + target1.Length - 1;
        int start2 = pos2;
        int end2 = pos2 + target2.Length - 1;

        if (!(end1 < start2 || end2 < start1))
        {
            int overlapStart = Math.Max(start1, start2);
            int overlapEnd = Math.Min(end1, end2);

            for (int i = overlapStart; i <= overlapEnd; i++)
            {
                if (target1[i - start1] != target2[i - start2])
                {
                    return int.MaxValue;
                }
            }
        }

        for (int i = 0; i < target1.Length; i++)
        {
            result[pos1 + i] = target1[i];
        }

        for (int i = 0; i < target2.Length; i++)
        {
            result[pos2 + i] = target2[i];
        }

        int changes = 0;
        for (int i = 0; i < n; i++)
        {
            if (s[i] != result[i])
            {
                changes++;
            }
        }

        return changes;
    }
}
