/// <summary>
/// Este programa demuestra el uso de ContinueWhenAll para ejecutar una tarea continua despu�s de que varias tareas hayan finalizado en C#.
/// </summary>
/// <remarks>
/// Versi�n de C#: 7.3
/// Objetivo .NET: .NET Framework 4.8
/// </remarks>

using System;
using System.Threading;
using System.Threading.Tasks;

namespace ContinueWhenAll
{
    class ProgramWithLock
    {
        private static readonly Random random = new Random();

        /// <summary>
        /// Realiza un c�lculo y devuelve el resultado.
        /// </summary>
        /// <returns>El resultado del c�lculo.</returns>
        static int PerformCalculation()
        {
            lock (random)
            {
                var rnd = random.Next(1, 101); // Genera un n�mero aleatorio entre 1 y 100
                Console.WriteLine($"Random: {rnd}");
                return rnd;
            }
        }

        /// <summary>
        /// Punto de entrada principal para la aplicaci�n.
        /// </summary>
        /// <param name="args">Argumentos de l�nea de comandos.</param>
        static void __Main__(string[] args)
        {
            // Crea la tarea A que realiza un c�lculo
            var TaskA = new Task<int>(() =>
            {
                Console.WriteLine("TaskA started.");
                Thread.SpinWait(int.MaxValue);
                return PerformCalculation();
            });

            // Crea la tarea B que realiza un c�lculo
            var TaskB = new Task<int>(() =>
            {
                Console.WriteLine("TaskB started.");
                Thread.SpinWait(int.MaxValue);
                return PerformCalculation();
            });

            // Crea una tarea continua que se ejecuta cuando TaskA y TaskB han finalizado
            var total = Task.Factory.ContinueWhenAll(
                new Task<int>[] { TaskA, TaskB },
                (tasks) => Console.WriteLine("Total = {0}", tasks[0].Result + tasks[1].Result)
            );

            // Inicia las tareas A y B
            TaskA.Start();
            TaskB.Start();

            // Espera a que TaskA y TaskB finalicen
            Task.WaitAll(TaskA, TaskB);

            // Espera a que la tarea continua finalice
            total.Wait();

            // Mensaje para indicar que el programa ha finalizado
            Console.WriteLine("Presiona enter para salir");
            Console.ReadLine();
        }
    }
}

