using System;
using System.Collections;
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

        private static bool Xor(object firstValue, object secondValue) {
            firstValue = Convert.ToByte(firstValue);
            secondValue = Convert.ToByte(secondValue);
            var temp = (byte)firstValue ^ (byte)secondValue;
            Console.WriteLine($"{secondValue} xor {secondValue}: {temp}");
            return Convert.ToBoolean(temp);
        }

        public static string BitArrayToStr(BitArray bitArray) {
            var strArr = new byte[bitArray.Length / 8];
            var encoding = new ASCIIEncoding();
            for (var i = 0; i < bitArray.Length / 8; i++) {
                for (int index = i * 8, m = 1; index < i * 8 + 8; index++, m *= 2) {
                    strArr[i] += bitArray.Get(index) ? (byte)m : (byte)0;
                }
            }
            return encoding.GetString(strArr);
        }
    }
}
