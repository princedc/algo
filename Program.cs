using System;
using System.Collections.Generic;

namespace Algo
{
    class Program
    {
        static void Main(string[] args)
        {
            List<String> S = new List<String> { "aaaccc", "aaabb", "aaaaaccccc", "aaaaabbb", "aaaacb", "ac", "aaaaacbcc", "ab" };
            List<String> T = new List<String>();
            List<String> P = new List<String>();
            List<String> A = new List<String>();
            string pattern, axiom;

            //T.ForEach(item => Console.WriteLine(item));
            string itemToAdd = string.Empty;
            string currentWord = String.Empty;
            bool foundPattern = false;
            while (S.Count > 0)
            {
                foundPattern = false;
                currentWord = S[0];
                //if(T.Count == 0)
                //    T.Insert(0, itemToAdd);
                S.Remove(currentWord);

                for (int ti = 0; ti < T.Count; ti++)
                {
                    if (currentWord.Contains(T[ti]))
                    {
                        pattern = currentWord;
                        axiom = T[ti];
                        ProcessAddToP(pattern, A, P, T);
                        ProcessAddToA(axiom, A, P, T);
                        T.Remove(T.Find(i => i.Equals(axiom)));
                        foundPattern = true;
                    }
                    else if (T[ti].Contains(currentWord))
                    {
                        axiom = currentWord;
                        pattern = T[ti];
                        ProcessAddToP(pattern, A, P, T);
                        ProcessAddToA(axiom, A, P, T);
                        T.Remove(T.Find(i => i.Equals(axiom)));
                        foundPattern = true;
                    }
                }
                if (!foundPattern)
                {
                    T.Add(currentWord);
                }


                //    for (int ti = 0; ti < T.Count - 1; ti++)
                //{
                //    if (T[ti].Contains(T[ti + 1]))
                //    {
                //        pattern = T[ti];
                //        axiom = T[ti + 1];
                //        ProcessAddToP(pattern, A, P, T);
                //        ProcessAddToA(axiom, A, P, T);
                //        T.Remove(T.Find(i => i.Equals(axiom)));
                //    }
                //    else if (T[ti + 1].Contains(T[ti]))
                //    {
                //        pattern = T[ti + 1];
                //        axiom = T[ti];
                //        ProcessAddToP(pattern, A, P, T);
                //        ProcessAddToA(axiom, A, P, T);
                //        T.Remove(T.Find(i => i.Equals(axiom)));
                //        //P.Add(T[tj]);
                //        //A.Add(T[ti]);
                //    }
                //}

                T.ForEach(item => Console.WriteLine("T :" + item));
                P.ForEach(item => Console.WriteLine("P :" + item));
                A.ForEach(item => Console.WriteLine("A :" + item));
                S.ForEach(item => Console.WriteLine("S :" + item));
                Console.WriteLine("-----------------------------------------------------------");

            }

            if (T.Count == 1)
            {
                ProcessAddToP(T[0], A, P, T);
                T.RemoveAt(0);
            }
            else
                while (T.Count > 1)
                {
                    for (int i = 0; i <= T.Count - 2; i++)
                    {
                        if (T[i].Contains(T[i + 1]))
                        {
                            pattern = T[i];
                            axiom = T[i + 1];
                            ProcessAddToP(pattern, A, P, T);
                            ProcessAddToA(axiom, A, P, T);

                        }
                        else if (T[i + 1].Contains(T[i]))
                        {
                            pattern = T[i + 1];
                            axiom = T[i];
                            ProcessAddToP(pattern, A, P, T);
                            ProcessAddToA(axiom, A, P, T);
                        }
                    }
                }

            T.ForEach(item => Console.WriteLine("T :" + item));
            P.ForEach(item => Console.WriteLine("P :" + item));
            A.ForEach(item => Console.WriteLine("A :" + item));
            S.ForEach(item => Console.WriteLine("S :" + item));
            Console.WriteLine("-----------------------------------------------------------");

            foreach ( var w in A)
            {
                for (int i = 0; i < P.Count; i++)
                    P[i] = P[i].Replace(w, "~");
            }
            


            Console.WriteLine("--------------------------Final Result---------------------------------");

            T.ForEach(item => Console.WriteLine("T :" + item));
            P.ForEach(item => Console.WriteLine("P :" + item));
            A.ForEach(item => Console.WriteLine("A :" + item));
            S.ForEach(item => Console.WriteLine("S :" + item));
            Console.WriteLine("-----------------------------------------------------------");
            Console.Read();
        }
        public static void ProcessAddToA(string currentWord, List<String> A, List<String> P, List<String> T)
        {
            bool isPatternWithIn = false;
            string pattern, axiom;
            for (int i = 0; i < A.Count; i++)
            {
                if (currentWord.Contains(A[i]) && currentWord.Length != A[i].Length)
                {
                    //A.Add(A[i]);
                    ProcessAddToP(currentWord, A, P, T);
                    isPatternWithIn = true;

                }
                else if (A[i].Contains(currentWord) && currentWord.Length != A[i].Length)
                {
                    pattern = A[i];
                    axiom = currentWord;
                    ProcessAddToP(pattern, A, P, T);
                    A.Remove(A.Find(item => item.Equals(pattern)));
                    ProcessAddToA(currentWord, A, P, T);
                    isPatternWithIn = true;
                }
            }
            if (!isPatternWithIn && !A.Exists(item => item.Equals(currentWord)))
            {
                A.Add(currentWord);
            }
        }
        public static void ProcessAddToP(string currentWord, List<String> A, List<String> P, List<String> T)
        {
            bool isPatternWithIn = false;
            string axiom;
            for (int i = 0; i < P.Count; i++)
            {
                if (currentWord.Length != P[i].Length && currentWord.Contains(P[i]))
                {
                    //A.Add(P[i]);
                    axiom = P[i];
                    ProcessAddToA(axiom, A, P, T);

                    P.Remove(P.Find(item => item.Equals(currentWord)));
                    T.Remove(T.Find(item => item.Equals(currentWord)));
                    isPatternWithIn = true;
                    //sourceToAdd.Remove(sourceToAdd[i]);
                    //P.Add(currentWork);
                }
                else if (P[i].Contains(currentWord) && currentWord.Length != P[i].Length)
                {
                    //A.Add(currentWord);
                    //P.Add(sourceToAdd[i]);
                    string temp = P[i];
                    ProcessAddToA(currentWord, A, P, T);
                    P.Remove(P.Find(item => item.Equals(temp)));
                    T.Remove(T.Find(item => item.Equals(temp)));
                    isPatternWithIn = true;
                }

            }
            if (!isPatternWithIn && !P.Exists(item => item.Equals(currentWord)))
            {
                P.Add(currentWord);
            }
        }
    }
}
