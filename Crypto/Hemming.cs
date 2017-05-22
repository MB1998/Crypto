using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crypto
{
    class Hemming
    {
        public static int[] Encoder(int[] codingSequence)
        {
            if (codingSequence.Length != 11)
                throw new ArgumentException("Длина последовательности должна быть 11");

            int[] cs = InitializeArray(codingSequence);

            cs[0] = (cs[2] + cs[4] + cs[6] + cs[8] + cs[10] + cs[12] + cs[14]) % 2;
            cs[1] = (cs[2] + cs[5] + cs[6] + cs[9] + cs[10] + cs[13] + cs[14]) % 2;
            cs[3] = (cs[4] + cs[5] + cs[6] + cs[11] + cs[12] + cs[13] + cs[14]) % 2;
            cs[7] = (cs[8] + cs[9] + cs[10] + cs[11] + cs[12] + cs[13] + cs[14]) % 2;

            return cs;
        }

        public static int[] Decoder(int[] decodingSequence)
        {
            int bagBitNumber = Check(decodingSequence);
            if (bagBitNumber > 0)
                decodingSequence[bagBitNumber - 1] = (decodingSequence[bagBitNumber - 1] + 1) % 2;
            List<int> resultSequence = new List<int>(decodingSequence);
            resultSequence.RemoveAt(7);
            resultSequence.RemoveAt(3);
            resultSequence.RemoveAt(1);
            resultSequence.RemoveAt(0);
            return resultSequence.ToArray();
        }

        private static int Check(int[] sequence)
        {
            if (sequence.Length != 15)
                throw new ArgumentException("Длина последовательности должна быть 15");
            int s1 = (sequence[0] + sequence[2] + sequence[4] + sequence[6] + sequence[8] + sequence[10] + sequence[12] + sequence[14]) % 2;
            int s2 = (sequence[1] + sequence[2] + sequence[5] + sequence[6] + sequence[9] + sequence[10] + sequence[13] + sequence[14]) % 2;
            int s3 = (sequence[3] + sequence[5] + sequence[4] + sequence[6] + sequence[11] + sequence[13] + sequence[12] + sequence[14]) % 2;
            int s4 = (sequence[7] + sequence[8] + sequence[9] + sequence[11] + sequence[13] + sequence[10] + sequence[12] + sequence[14]) % 2;
            return Convert.ToInt32((s4 * 1000 + s3 * 100 + s2 * 10 + s1).ToString(), 2);
        }

        private static int[] InitializeArray(int[] codingSequence)
        {
            if (codingSequence.Length != 11)
                throw new ArgumentException();
            int[] array = new int[15];
            array[0] = array[1] = 0;
            array[2] = codingSequence[0];
            array[3] = 0;
            array[4] = codingSequence[1];
            array[5] = codingSequence[2];
            array[6] = codingSequence[3];
            array[7] = 0;
            array[8] = codingSequence[4];
            for (int i = 8; i < 15; i++)
            {
                array[i] = codingSequence[i - 4];
            }
            return array;
        }
    }
}
