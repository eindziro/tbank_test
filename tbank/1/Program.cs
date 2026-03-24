using System;
using System.Linq;

class Program
{
    static void Main()
    {
        string str = Console.ReadLine();

        char[] digits = str.ToCharArray();

        Array.Sort(digits);

        int firstNonZeroIndex = -1;
        for (int i = 0; i < digits.Length; i++)
        {
            if (digits[i] != '0')
            {
                firstNonZeroIndex = i;
                break;
            }
        }

        if (firstNonZeroIndex != 0)
        {
            char temp = digits[0];
            digits[0] = digits[firstNonZeroIndex];
            digits[firstNonZeroIndex] = temp;
        }


        Console.WriteLine(new string(digits));
    }
}