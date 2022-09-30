using System;
using System.Collections.Generic;

namespace Sept30
{
    public class Sept30
    {
        public static void Main(String[] args)
        {
            Console.WriteLine("Get Largest Gap:");
            string str = Console.ReadLine();
            string[] split = str.Split(", ");
            int[] arr = new int[split.Length];
            for(int i = 0; i < split.Length; i++){
                arr[i] = Int32.Parse(split[i]);
            }
            string result = "";
            result = getGap(arr).ToString();
            Console.WriteLine(result);
        }

        public static int getGap(int[] arr)
        {
            Array.Sort(arr);
            int previous = 0, largest = 0;
            for(int i = 0; i < arr.Length; i++){
                int curr = arr[i];
                if(i != 0){
                    int diff = curr - previous;
                    if(diff > largest){
                        largest = diff;
                    }                
                }
                previous = curr;    
            }
            return largest;
        }
    }

}