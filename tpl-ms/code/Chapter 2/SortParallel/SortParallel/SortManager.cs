using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SortParallel
{
    /// <summary>
    /// Clase que gestiona la ordenaci�n de listas utilizando diferentes algoritmos.
    /// </summary>
    public static class SortManager
    {
        /// <summary>
        /// Realiza la ordenaci�n de una lista de enteros utilizando diferentes algoritmos.
        /// </summary>
        /// <param name="size">El tama�o de la lista a ordenar.</param>
        public static void DoSort(int size)
        {
            // Genera una lista de n�meros aleatorios
            var random = new Random();
            var list = Enumerable.Range(0, size).Select(_ => random.Next(100)).ToList();

            // Imprime la lista original
            Console.WriteLine("Lista original:");
            list.ForEach(i => Console.Write($"{i} "));
            Console.WriteLine();

            // Crea copias de la lista para cada algoritmo de ordenaci�n
            var bubbleList = new List<int>(list);
            var insertionList = new List<int>(list);

            // Ejecuta las ordenaciones en paralelo
            var bubbleSortTask = Task.Run(() => SortAlgorithms.BubbleSort(bubbleList));
            var insertionSortTask = Task.Run(() => SortAlgorithms.InsertionSort(insertionList));

            // Espera a que ambas tareas finalicen
            Task.WaitAll(bubbleSortTask, insertionSortTask);

            // Imprime los resultados de la ordenaci�n
            Console.WriteLine("Lista ordenada por BubbleSort:");
            bubbleList.ForEach(i => Console.Write($"{i} "));
            Console.WriteLine();

            Console.WriteLine("Lista ordenada por InsertionSort:");
            insertionList.ForEach(i => Console.Write($"{i} "));
            Console.WriteLine();
        }
    }
}

