using System;
using System.Collections.Generic;
using System.Linq;

namespace OutRich.SumOfMultiple
{
    class Program
    {
        static void Main(string[] args)
        {
            int num = 1000;
            List<int> nums = new List<int>();
            for(var a = num - 1; a > 0; a--)
            {
                if (a % 3 == 0 || a % 5 == 0) 
                {
                    nums.Add(a);                 
                }
            }
            Console.WriteLine("The sum of all the multiples of 3 or 5 below " + num  +" is : " + nums.Sum());
        }       
    }
}
