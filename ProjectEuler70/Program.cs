using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler70
{
    class Program
    {

        static List<long>[] GetPrimeDivisors(int cap)
        {
            int i, j;
            List<long>[] Answer = new List<long>[cap+1];
                     
            for (i = 0; i <= cap; ++i)
            {
                Answer[i] = new List<long>();
            }

            for (i = 2; i <= cap; i++)
            {
                if (Answer[i].Count() == 0) // if i is prime...
                {
                    j = 2;
                    while (i * j <= cap)
                    {
                        Answer[i * j].Add(i);
                        j++;
                    }
                }
            }
            return Answer;
        }

        static bool ArePermutations(int x, int y)
        {
            if (x % 9 != y % 9) return false; // integers must be congruent mod 9 to be permutations
            int[] xh = new int[10], yh = new int[10];
            while (x > 0)
            {
                xh[x % 10]++;
                x /= 10;
            }
            while (y > 0)
            {
                yh[y % 10]++;
                y /= 10;
            }
            for (int i = 0; i < 10; i++)
                if (xh[i] != yh[i])
                    return false;
            return true;
        }

        static void Main(string[] args)
        {

            System.Diagnostics.Stopwatch Timer = new System.Diagnostics.Stopwatch();
            Timer.Start();

            const int max = 10000000;
            List<long>[] Divs = GetPrimeDivisors(max);

            long Totient;

            double theratio=(double)max;
            int then=1;

            for (int i = 1; i <= max; i++)
            {
                if (Divs[i].Count() == 0)
                {
                    Totient = i - 1;
                }
                else
                {
                    Totient = Divs[i].Select(x => x - 1).Aggregate((x, y) => x * y);
                    Totient *= i;
                    Totient /= Divs[i].Aggregate((x, y) => x * y);
                }
                if (ArePermutations(i, (int)Totient))
                {
                    if(i/(double)Totient < theratio)
                    {
                        theratio = i / (double)Totient;
                        then = i;
                    }
                }
            }
            Console.WriteLine(then);
            Console.WriteLine(Timer.ElapsedMilliseconds);
        }
    }
}
