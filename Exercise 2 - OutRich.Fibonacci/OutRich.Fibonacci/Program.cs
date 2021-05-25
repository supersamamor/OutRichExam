using System;
using System.Collections.Generic;
using System.Linq;

namespace OutRich.SumOfMultiple
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> fibonacciSequence = new List<int>();           
            int num1 = 0;
            int num2 = 1;
            int sum = 1;           
            while (sum < 4000000)
            {               
                if (sum % 2 == 0)
                {
                    fibonacciSequence.Add(sum);
                }
                sum = num1 + num2;
                num1 = num2;
                num2 = sum;   
            }
            Console.WriteLine("The sum of all even numbers is : " + fibonacciSequence.Sum());
        }       
    }
}
