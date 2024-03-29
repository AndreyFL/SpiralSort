﻿using System;
using System.Threading;

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
                    Console.Write("{0,2} ", array[i, j]);
                Console.WriteLine();
            }
        }

        static int[,] SpiralTypeSort(int[,] inArray)
        {
            // Использую для занесения всех элеметов исходного массива, одномерный (промежуточный) массив flatArray.
            int[] flatArray = new int[inArray.GetLength(0) * inArray.GetLength(1)];
            for (int i = 0; i < inArray.GetLength(0); i++)
                for (int j = 0; j < inArray.GetLength(1); j++)
                    flatArray[i * inArray.GetLength(1) + j] = inArray[i, j];
            // Cортирую одномерный (промежуточный) массив стандартным методом.
            Array.Sort(flatArray);

            // Переменная с помощью которой будет выполняться итерация по промежуточному массиву flatArray.
            int tmpInt = 0;
            // Создаю новый массив, в который будут записываться по спирали элементы из промежуточного flatArray.
            int[,] outArray = new int[inArray.GetLength(0), inArray.GetLength(1)];

            // В случае не квадратных массивов (кол-во столбцов не равно кол-ву строк) определяю по какой координате меньше
            // элементов, для определения необходимого числа общих циклов (Количество спиралей, может даже и не полных).
            int countCycles = (inArray.GetLength(0) >= inArray.GetLength(1)) ? inArray.GetLength(1) : inArray.GetLength(0);

            for (int i = 0; i <= countCycles / 2; i++)
            {
                // Проверяю есть ли место в результирующем массиве для вывода очередного верхнего ряда
                // если нет, формирование результирующего массива закончено, возвращаю результат,
                // если есть еще место, вывожу ряд.
                if (i > (outArray.GetLength(1) - 1 - i))
                    return outArray;
                for (int k = i; k < inArray.GetLength(1) - i; k++)//Строка выводится полностью, длинна зависит от номера витка спирали.
                    outArray[i, k] = flatArray[tmpInt++];

                // Проверяю есть ли место в результирующем массиве для вывода очередного правого ряда
                // если нет, формирование результирующего массива закончено, возвращаю результат,
                // если есть еще место, вывожу ряд.
                if (i > (outArray.GetLength(0) - 2 - i))
                    return outArray;
                for (int k = i + 1; k < outArray.GetLength(0) - i; k++)//Правый столбец выводится не полностью, т.к. верхний элемент столбца уже заполнен при выводе верхней строки.
                    outArray[k, outArray.GetLength(1) - 1 - i] = flatArray[tmpInt++];

                // Проверяю есть ли место в результирующем массиве для вывода очередного нижнего ряда
                // если нет, формирование результирующего массива закончено, возвращаю результат,
                // если есть еще место, вывожу ряд.
                if (i > (outArray.GetLength(1) - 2 - i))
                    return outArray;
                for (int k = outArray.GetLength(1) - 2 - i; k >= i; k--)//Нижняя строка выводится не полностью, т.к. правый элемент строки уже заполнен выводом правого столбца.
                    outArray[outArray.GetLength(0) - 1 - i, k] = flatArray[tmpInt++];

                // Проверяю есть ли место в результирующем массиве для вывода очередного левого ряда
                // если нет, формирование результирующего массива закончено, возвращаю результат,
                // если есть еще место, вывожу ряд.
                if (i > (outArray.GetLength(0) - 3 - i))
                    return outArray;
                for (int k = outArray.GetLength(0) - 2 - i; k > i; k--)// Левый столбец выводится не полностью, т.к. верхний и нижний элементы столбца уже заполнены во время вывода верхней и нижних строк.
                    outArray[k, i] = flatArray[tmpInt++];
            }
            return outArray;
        }


        static void Main(string[] args)
        {
            // Программа производит сортировку по возростанию элеметов исходного двухмерного массива, и 
            // располагает результат по сприрали. Результат выводится на консоль.

            Random r = new Random();

            do
            {
                // Создаю двухмерный массив, со случайными количествами
                // строк и столбцов. Кол-во может варьироваться от 1 до 10.
                int[,] array = new int[r.Next(1, 10), r.Next(1, 10)];

                // Метод наполняющий массив значениями.
                // В данном случае это заполнение случайными значениями от 0 до 99(включительно).
                array = InitArray2d(array.GetLength(0), array.GetLength(1));

                Console.Clear();
                Console.WriteLine("Массив {0} x {1}.\nИсходный:", array.GetLength(0), array.GetLength(1));
                // Метод выводящий двухмерный массив на консоль.
                ShowArray2d(array);

                // SpiralTypeSort(array) - отсортировывает элементы массива и располагает из по спирали
                // Метод возвращает отсортированный массив (тип int[,]).
                array = SpiralTypeSort(array);

                Console.WriteLine("\nОтсортированный по спирали:");
                ShowArray2d(array);

                Console.WriteLine("Для продолжения нажмите любую клавишу . . .");
                Console.ReadKey();
            } while (true);
        }
    }
}
