using System;
using System.Collections.Generic;
using System.Text;

class Program
{
    static void Main()
    {
        var first = Console.ReadLine().Split();
        int n = int.Parse(first[0]);
        int q = int.Parse(first[1]);

        string s = Console.ReadLine();

        List<(long l, long r)> operations = new List<(long, long)>();
        var output = new StringBuilder();

        for (int i = 0; i < q; i++)
        {
            string[] query = Console.ReadLine().Split();
            int type = int.Parse(query[0]);

            if (type == 1)
            {
                long l = long.Parse(query[1]) - 1;
                long r = long.Parse(query[2]) - 1;
                operations.Add((l, r));
            }
            else
            {
                long pos = long.Parse(query[1]) - 1;
                char c = GetCharAtPosition(s, operations, pos);
                output.AppendLine(c.ToString());
            }
        }

        Console.Write(output.ToString());
    }

    static char GetCharAtPosition(string s, List<(long l, long r)> ops, long pos)
    {
        // Проходим по операциям в обратном порядке
        for (int i = ops.Count - 1; i >= 0; i--)
        {
            var (l, r) = ops[i];
            long len = r - l + 1;

            // После операции [l, r], каждый символ на позициях [l, r] 
            // занимает теперь 2 позиции в результирующей строке

            // Пересчитываем позицию relative к началу удвоения
            if (pos < l)
            {
                // Позиция не затронута
                continue;
            }

            if (pos <= r + len)
            {
                // Позиция в пределах [l, r + len] после удвоения
                // Нужно отобразить обратно в оригинальную позицию
                long offset = pos - l;
                // Каждый символ из оригинала теперь занимает 2 позиции
                pos = l + offset / 2;
            }
            else if (pos > r + len)
            {
                // Позиция после удвоенной части
                pos -= len;
            }
        }

        return s[(int)pos];
    }
}


