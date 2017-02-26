using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Crypto
{
    public class Crypto
    {
        public static BitArray EncodeGrey(BitArray bitArray) {
            var encodedBitArray = new ArrayList {bitArray.Get(0)};
            for (var i = 1; i < bitArray.Count; i++){
                encodedBitArray.Add(Xor(bitArray.Get(i), bitArray.Get(i - 1)));
            }
            return new BitArray((bool[])encodedBitArray.ToArray(typeof(bool)));
        }

        public static BitArray DecodeGrey(BitArray encodedBitArray) {
            var decodedBitArray = new ArrayList {encodedBitArray.Get(0)};
            for (var i = 1; i < encodedBitArray.Count; i++) {
                decodedBitArray.Add(Xor(decodedBitArray[i-1], encodedBitArray.Get(i)));
            }
            return new BitArray((bool[])decodedBitArray.ToArray(typeof(bool)));
        }

        public static BitArray EncodeBcd(string numberToEncode, ArrayList bcDcodes) { 
            var digits = new ArrayList(numberToEncode.ToCharArray());
            var encodedData = new ArrayList();
            foreach (char digit in digits){
                var rest = Convert.ToByte(digit);
                foreach (byte bcDcode in bcDcodes){
                    encodedData.Add(rest >= bcDcode);
                    if (rest >= bcDcode){
                        rest -= bcDcode;
                    }
                }
            }
            return new BitArray((bool[])encodedData.ToArray(typeof(bool)));
        }

        public static BitArray EncodeBerger(BitArray data)
        {
            var temp = new ArrayList();
            var k = data.Count;
            var r = Convert.ToInt32(Math.Round(Math.Log(k, 2)));
            var numOne = 0;
            for (var i = 0; i < k; i++)
            {
                var num = data.Get(i);
                if (num)
                    numOne++;
                temp.Add(num);
            }
            var numOneInvers = Convert.ToString(numOne, 2)
                .PadLeft(r, '0')
                .Aggregate("", (current, c) => current + (c == '1' ? 0 : 1));
            foreach (var c in numOneInvers)
                temp.Add(c == '1');
            return new BitArray((bool[])temp.ToArray(typeof(bool)));
        }

        public static BitArray DecodeBerger(BitArray data)
        {
            var temp = new ArrayList();
            var k = data.Count;
            var r = Convert.ToInt32(Math.Round(Math.Log(k, 2)));

            var oneNum = "";
            for (var i = data.Count - r; i < data.Count; i++)
                oneNum += data.Get(i) ? '1' : '0';

            var numOneInvers = oneNum.Aggregate("", (current, c) => current + (c == '1' ? 0 : 1)).PadLeft(8, '0');
            var countOneInMsg = ToByteArray(numOneInvers)[0];
            var numOneCheck = 0;
            for (var i = 0; i < data.Count - r; i++)
            {
                var num = data.Get(i);
                if (num)
                    numOneCheck++;
                temp.Add(num);
            }
            return numOneCheck == countOneInMsg ? new BitArray((bool[]) temp.ToArray(typeof(bool))) : new BitArray(0);
        }


        private static bool Xor(object firstValue, object secondValue)
        {
            firstValue = Convert.ToByte(firstValue);
            secondValue = Convert.ToByte(secondValue);
            var temp = (byte)firstValue ^ (byte)secondValue;
            Console.WriteLine($"{secondValue} xor {secondValue}: {temp}");
            return Convert.ToBoolean(temp);
        }

        public static string BitArrayToStr(BitArray bitArray)
        {
            var strArr = new byte[bitArray.Length / 8];
            var encoding = new ASCIIEncoding();
            for (var i = 0; i < bitArray.Length / 8; i++)
            {
                for (int index = i * 8, m = 1; index < i * 8 + 8; index++, m *= 2)
                {
                    strArr[i] += bitArray.Get(index) ? (byte)m : (byte)0;
                }
            }
            return encoding.GetString(strArr);
        }

        public static byte[] ToByteArray(string str)
        {
            var result = Enumerable.Range(0, str.Length / 8).
                Select(pos => Convert.ToByte(
                        str.Substring(pos * 8, 8),
                        2)
                ).ToArray();
            return result;
        }
    }
}
