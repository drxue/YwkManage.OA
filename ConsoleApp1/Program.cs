using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
   public class Program
    {
        static void Main(string[] args)
        {
            int[] tempint = { 1, 2, 3, 4, 5 };

            IQueryable<int> abc = tempint.Where(e => true) as IQueryable<int>;
            var temp = abc.Where(e => e > 2).ToList();
            Console.WriteLine(temp);
            Console.ReadKey();
        }
    }
}
