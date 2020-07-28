using System;
using System.Collections.Generic;

namespace Metodos.Extensores
{
    internal delegate bool Predicado<T>(T x);

    internal static class MisMetodos
    {
        public static IEnumerable<T> Where<T>(this IEnumerable<T> items, Predicado<T> filtro)
        {
            foreach (T x in items)
                if (filtro(x)) yield return x;
        }

        public static T Mayor<T>(this IEnumerable<T> items) where T : IComparable
        {
            T max = default(T);
            bool empty = true;
            foreach (T x in items)
            {
                max = x; empty = false; break;
            }
            if (empty) throw new Exception("The source cannot be empty");
            foreach (T x in items)
            {
                if (x.CompareTo(max) > 0)
                    max = x;
            }
            return max;
        }

        private class Program
        {
            private static void Main(string[] args)
            {
                List<string> colores = new List<string> { "rojo", "verde", "azul", "blanco", "negro", "naranja", "amarillo" };
                int[] numeros = { 10, -60, 20, -50, 30, -40 };
                Console.WriteLine("\nColores de nombre corto");
                foreach (string s in MisMetodos.Where(colores, x => x.Length <= 5))
                    Console.WriteLine(s);
                Console.WriteLine("\nPositivos");
                foreach (int i in MisMetodos.Where(numeros, x => x > 0))
                    Console.WriteLine(i);

                Console.WriteLine("\nDe los colores de nombre corto el mayor alfabéticamente");
                Console.WriteLine(colores.Mayor());

                #region Con notación punto usando extensores

                //Convertir en extensores los métodos poniendo el this
                //Mostrar el uso del Intellisense

                Console.WriteLine("\nColores de nombre corto");
                foreach (string s in colores.Where(x => x.Length <= 5))
                    Console.WriteLine(s);
                Console.WriteLine("\nNegativos");
                foreach (int i in numeros.Where(x => x < 0))
                    Console.WriteLine(i);

                Console.WriteLine("\nDe los colores de nombre largo el mayor alfabéticamente");
                Console.WriteLine(colores.Where(x => x.Length > 5).Mayor());

                Console.WriteLine("\nDe los negativos el mayor");
                Console.WriteLine(numeros.Where(x => x < 0).Mayor());

                #endregion Con notación punto usando extensores

                Console.ReadLine();
            }
        }
    }
}