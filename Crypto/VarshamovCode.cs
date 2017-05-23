using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crypto
{
    class VarshamovCode
    {
        static byte[,] g = new byte[,]
        {
            { 1, 0, 0, 0, 0, 0, 0, 0, 1, 1},
        {0,1,0,0,0,0,0,1,0,1 },
        {0,0,1,0,0,0,1,0,0,1 },
        {0,0,0,1,0,0,0,1,1,0 },
        {0,0,0,0,1,0,1,0,1,0 },
        {0,0,0,0,0,1,1,1,0,0 }
        };

        static byte[,] h = new byte[,]
            {
                {0,0,1,0,1,1,1,0,0,0 },
            {0,1,0,1,0,1,0,1,0,0 },
            {1,0,0,1,1,0,0,0,1,0 },
            {1,1,1,0,0,0,0,0,0,1 }
            };

        static Dictionary<string, byte> cindroms = new Dictionary<string, byte>();
        public static byte[,] H { get { return h; } }
        public byte[,] G { get { return g; } }
        public static int GetT(int n, int d)
        {
            return  (int)Math.Floor((double)(d - 1) / 2);
        }

        static int Combination(int lowerIndex, int upperIndex)
        {
            double upper = Fac(lowerIndex);
            int low_min_kigh = lowerIndex - upperIndex;
            long n_min_k = Fac(low_min_kigh);
            long k = Fac(upperIndex);            
            double lower = k * n_min_k;
            double result = upper / lower;
            return (int)result;
        }

        public static int GetR(int n, int d)
        {
            int r;
            int sum = 0;
            for (int i = 1; i <= d - 2; i++)
            {
                sum += Combination(n - 1, i);
            }
            sum++;
            r = (int)Math.Ceiling(Math.Log(sum, 2));
            return r;
        }

        static long Fac(int value)
        {
            Console.WriteLine("Factorial of " + value);
            long result = 1;
            for (int i = 1; i <= value; i++)
            {
                result *= i;
                Console.WriteLine(result);
            }
            return result;
        }

        public static int GetRows(int n, int d)
        {
            int r = GetR(n, d);
            return n - r;
        }

        public static int GetColumns(int n)
        {
            return n;
        }

        public static string Encode(string num)
        {
            char[] dights = num.ToCharArray();
            int[] result = null;
            for (int i = 0; i < dights.Length; i++)
            {
                if(dights[i] == '1')
                {
                    if (result == null)
                    {
                        result = new int[g.GetLength(1)];
                        for (int j = 0; j < g.GetLength(1); j++)
                        {
                            result[j] = g[i, j];
                        }
                    }
                    else
                    {
                        for (int j = 0; j < g.GetLength(1); j++)
                        {
                            result[j] = result[j] ^ g[i, j];
                        }
                    }
                }
            }
            string resStr = "";
            foreach (int el in result)
                resStr += el.ToString();
            return resStr;
        }

        public static string CorrectCode(string code)
        {
            FillCindroms();
            char[] chars = code.ToCharArray();
            byte[] nums = new byte[chars.Length];
            for (int i = 0; i < chars.Length; i++)
            {
                nums[i] = Convert.ToByte(chars[i]);
            }
            string cindrom = "";
            for (int i = 0; i < 4; i++)
            {
                int dight = nums[0] & h[i, 0];
                for (int j = 1; j < 6; j++)
                {
                    dight = dight ^ (nums[j] & h[i, j]);
                }
                dight = dight ^ nums[6 + i];
                cindrom += (char)dight;
            }
            if (cindroms.Keys.Contains(cindrom))
            {
                int index = cindroms[cindrom];
                chars[index] = chars[index] == '0' ? '1' : '0';
            }
            return String.Concat(chars);
        }

        public static void FillCindroms()
        {
            if (cindroms.Keys.Count == 0)
            {
                cindroms.Add("0011", 0);
                cindroms.Add("0101", 1);
                cindroms.Add("1001", 2);
                cindroms.Add("0110", 3);
                cindroms.Add("1010", 4);
                cindroms.Add("1100", 5);
                cindroms.Add("1000", 6);
                cindroms.Add("0100", 7);
                cindroms.Add("0010", 8);
                cindroms.Add("0001", 9);
            }
        }
    }
}
