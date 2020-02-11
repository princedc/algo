using System;
using System.Collections.Generic;

namespace Algo
{
    class Program
    {
        static void Main(string[] args)
        {
            List<String> S = new List<String> { "aaaccc", "aaabb", "aaaaaccccc", "aaaaabbb", "aaacb", "ac", "aaaaacbcc", "ab" };
            List<String> T = new List<String>();
            List<String> P = new List<String>();
            List<String> A = new List<String>();
            string pattern = null, axiom = null;

            string itemToAdd = string.Empty;
            string currentWord = String.Empty;
            bool foundPattern = false;

            while (S.Count > 0)
            {
                DebugPrint(S, A, P, T);
                foundPattern = false;
                currentWord = S[0];
                S.Remove(currentWord);

                Console.WriteLine("Start Processing: " + currentWord);

                T.Insert(0, currentWord);
                axiom = pattern = null;
                for (int ti = 1; ti < T.Count; ti++)
                {
                    if (T[0].Contains(T[ti]))
                    {
                        pattern = T[0];
                        axiom = T[ti];
                        ProcessAddToA(axiom, A, P, T);
                        ProcessAddToP(pattern, A, P, T, false);
                      
                        foundPattern = true;
                    }
                    else if (T[ti].Contains(T[0]))
                    {
                        axiom = T[0];
                        pattern = T[ti];
                        ProcessAddToA(axiom, A, P, T);
                        ProcessAddToP(pattern, A, P, T, false);
                        foundPattern = true;
                    }
                }
                if(axiom != null)
                {
                    T.Remove(T.Find(item => item.Equals(axiom)));
                }
            }

            if (T.Count == 1)
            {
                ProcessAddToP(T[0], A, P, T, false);
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
                            ProcessAddToP(pattern, A, P, T, false);
                            ProcessAddToA(axiom, A, P, T);

                        }
                        else if (T[i + 1].Contains(T[i]))
                        {
                            pattern = T[i + 1];
                            axiom = T[i];
                            ProcessAddToP(pattern, A, P, T, false);
                            ProcessAddToA(axiom, A, P, T);
                        }
                    }
                }

            DebugPrint(S, A, P, T);
            Console.WriteLine("-----------------------------------------------------------");

            foreach (var w in A)
            {
                for (int i = 0; i < P.Count; i++)
                    P[i] = P[i].Replace(w, "~");
            }



            Console.WriteLine("--------------------------Final Result---------------------------------");
            DebugPrint(S, A, P, T);
            Console.WriteLine("-----------------------------------------------------------");
            Console.Read();
        }

        public static void ProcessAddToA(string currentWord, List<String> A, List<String> P, List<String> T)
        {
            bool isPatternWithIn = false;
            string pattern, axiom;
            int w1Len = 0, w2Len = 0;
             w1Len = currentWord.Length;
            for (int i = 0; i < A.Count; i++)
            {
                w2Len = A[i].Length;
                if( w1Len > w2Len && currentWord.Contains(A[i]))
                {
                    ProcessAddToP(currentWord, A, P, T, true);
                    isPatternWithIn = true;
                }
                else if (w1Len < w2Len && A[i].Contains(currentWord))
                {
                    pattern = A[i];
                    axiom = currentWord;


                    ProcessAddToP(pattern, A, P, T, true);

                    Console.WriteLine("Remove " + pattern + " from A.");
                    A.Remove(A.Find(item => item.Equals(pattern)));
                    A.Insert(0, currentWord);
                    isPatternWithIn = true;
                }
            }
            if (!isPatternWithIn && !A.Exists(item => item.Equals(currentWord)))
            {
                Console.WriteLine("Adding " + currentWord + " to A.");
                A.Add(currentWord);
            }
        }
        public static void ProcessAddToP(string currentWord, List<String> A, List<String> P, List<String> T, bool processT)
        {
            bool isPatternWithIn = false;
            string axiom;

            for (int i = 0; i < P.Count; i++)
            {
                if (currentWord.Length != P[i].Length && currentWord.Contains(P[i]))
                {
                    axiom = P[i];
                    ProcessAddToA(axiom, A, P, T);


                    if (processT)
                    {
                        Console.WriteLine("Removing " + currentWord + " from P.");
                        P.Remove(P.Find(item => item.Equals(currentWord)));
                        Console.WriteLine("Removing " + currentWord + " from T.");
                        T.Remove(T.Find(item => item.Equals(currentWord)));
                    }
                    isPatternWithIn = true;
                }
                else if (P[i].Contains(currentWord) && currentWord.Length != P[i].Length)
                {
                    string temp = P[i];
                    if(!processT)
                    ProcessAddToA(currentWord, A, P, T);

                    if (processT)
                    {
                        Console.WriteLine("Removing " + temp + " from P.");
                        P.Remove(P.Find(item => item.Equals(temp)));
                        Console.WriteLine("Removing " + temp + " from T.");
                        T.Remove(T.Find(item => item.Equals(temp)));
                    }
                    isPatternWithIn = true;
                }

            }
            if (!isPatternWithIn && !P.Exists(item => item.Equals(currentWord)))
            {
                Console.WriteLine("Adding " + currentWord + " to P.");
                P.Add(currentWord);
            }
        }


        public static void DebugPrint(List<string> S, List<string> A, List<String> P, List<String> T)
        {
            Console.WriteLine("-----------------------------------------------------------");
            Console.Write("S: ");
            S.ForEach(item => Console.Write(item + ","));
            Console.WriteLine("");
            Console.Write("A: ");
            A.ForEach(item => Console.Write(item + ","));
            Console.WriteLine("");
            Console.Write("P: ");
            P.ForEach(item => Console.Write(item + ","));
            Console.WriteLine("");
            Console.Write("T: ");
            T.ForEach(item => Console.Write(item + ","));
            Console.WriteLine("");

        }
    }


}




