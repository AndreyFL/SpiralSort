using System;
using System.Threading;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace SpiralSort
{
    class Program
    {
        static int[,] InitArray2d(int countLines, int countColumns)
        {
            int[,] array = new int[countLines, countColumns];
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Thread.Sleep(15);
                    array[i, j] = new Random().Next(100);
                    //array[i, j] = i * array.GetLength(1) + j;

                }

            }
            return array;
        }

        static void ShowArray2d(int[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write("{0,2} ", array[i, j]);
                }
                Console.WriteLine();
            }
        }

        static int[,] SpiralTypeSort(int[,] inArray)
        {
            int[] flatArray = new int[inArray.GetLength(0) * inArray.GetLength(1)];
            for (int i = 0; i < inArray.GetLength(0); i++)
                for (int j = 0; j < inArray.GetLength(1); j++)
                    flatArray[i * inArray.GetLength(1) + j] = inArray[i, j];
            Array.Sort(flatArray);

            int tmpInt = 0;
            int[,] outArray = new int[inArray.GetLength(0), inArray.GetLength(1)];

            int countCycles = (inArray.GetLength(0) >= inArray.GetLength(1)) ? inArray.GetLength(1) : inArray.GetLength(0);

            for (int i = 0; i <= countCycles / 2; i++)
            {
                // Заполняю верхний ряд
                if (i >= inArray.GetLength(1) - i)
                    return outArray;
                for (int k = i; k < inArray.GetLength(1) - i; k++)
                    outArray[i, k] = flatArray[tmpInt++];

                // Заполняю правый ряд
                if ((i + 1) >= (inArray.GetLength(0) - i))
                    return outArray;
                for (int k = i + 1; k < inArray.GetLength(0) - i; k++)
                    outArray[k, inArray.GetLength(1) - 1 - i] = flatArray[tmpInt++];

                // Заполняю нижний ряд
                if ((inArray.GetLength(1) - 2 - i) < i)
                    return outArray;
                for (int k = inArray.GetLength(1) - 2 - i; k >= i; k--)
                    outArray[inArray.GetLength(0) - 1 - i, k] = flatArray[tmpInt++];

                // Заполняю левый ряд
                if (inArray.GetLength(0) - 2 - i <= i)
                    return outArray;
                for (int k = inArray.GetLength(0) - 2 - i; k > i; k--)
                    outArray[k, i] = flatArray[tmpInt++];
            }
            return outArray;
        }


        static void Main(string[] args)
        {
            Random r = new Random();
            int[,] array = new int[r.Next(1, 10), r.Next(1, 10)];
            array = InitArray2d(array.GetLength(0), array.GetLength(1));
            Console.WriteLine("Исходный массив:");
            ShowArray2d(array);

            array = SpiralTypeSort(array);

            Console.WriteLine("\nМассив отсортированный по спирали:");
            ShowArray2d(array);

            Console.WriteLine("Для продолжения нажмите любую клавишу . . .");
            Console.ReadKey();
        }
    }
}