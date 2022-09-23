using System;
using System.Collections.Generic;

namespace Sept23
{
    public class Sept23
    {
        public static void Main(String[] args)
        {
            Console.WriteLine("Get Second Largest:");
            int T = Int32.Parse(Console.ReadLine());
            string result = "";
            for (int i = 0; i < T; i++)
            {
                if(i != 0) {
                    result += '\n';
                }
                string S = Console.ReadLine();
                int[] digits = getDigits(S);
                result += getSecond(digits).ToString();
            }
            Console.WriteLine(result);


            Console.WriteLine("Get Sum Digits:");
            int T1 = Int32.Parse(Console.ReadLine());
            string result1 = "";
            for (int i = 0; i < T1; i++)
            {
                if(i != 0) {
                    result1 += '\n';
                }
                string S = Console.ReadLine();
                result1 += getSum(S);
            }
            Console.WriteLine(result1);
        }

        public static int[] getDigits(string S)
        {
            char[] delimiters = {' '};
            string[] elements = S.Split(delimiters);
            List<int> digits = new List<int>();
            foreach (string e in elements)
            {
                try
                {
                    int el = Int32.Parse(e);
                    digits.Add(el);
                }
                catch
                {
                    Console.WriteLine("Removed non int: " + e);
                }
            }

            int[] result = digits.ToArray();
            return result;
        }

        public static int getSecond(int[] Digits)
        {
            int smallest = 0, second = 0;
            foreach (int d in Digits)
            {
                if(smallest == 0){
                    smallest = d;
                }
                else if(second == 0){
                    second = d;
                }
                else {
                    if(d < second){
                        if(d < smallest){
                            second = smallest;
                            smallest = d;
                        }
                        else{
                            second = d;
                        }
                    }
                }
            }
            return second;
        }

        public static int getSum(string S){
            List<int> digits = new List<int>();
            int sum = 0;
            foreach(char c in S){
                if(Char.IsDigit(c))
                {
                    sum += Int32.Parse(c.ToString());
                }
            }
            return sum;
        }
    }

}