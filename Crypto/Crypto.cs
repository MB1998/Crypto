using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto {
    class Crypto {
        public static BitArray EncodeGrey(BitArray bitArray) {
            ArrayList encodedBitArray = new ArrayList();
            encodedBitArray.Add(bitArray.Get(0));
            for (int i = 1; i < bitArray.Count; i++){
                encodedBitArray.Add(Xor(bitArray.Get(i), bitArray.Get(i - 1)));
            }
            return new BitArray((bool[])encodedBitArray.ToArray(typeof(bool)));
        }

        public static BitArray DecodeGrey(BitArray encodedBitArray) {
            ArrayList decodedBitArray = new ArrayList();
            decodedBitArray.Add(encodedBitArray.Get(0));
            for (int i = 1; i < encodedBitArray.Count; i++) {
                decodedBitArray.Add(Xor(decodedBitArray[i-1], encodedBitArray.Get(i)));
            }
            return new BitArray((bool[])decodedBitArray.ToArray(typeof(bool))); ;
        }

        public static BitArray EncodeBCD(string numberToEncode, ArrayList BCDcodes) { 
            ArrayList digits = new ArrayList(numberToEncode.ToCharArray());
            ArrayList encodedData = new ArrayList();
            foreach (char digit in digits){
                byte rest = Convert.ToByte(digit);
                foreach (byte BCDcode in BCDcodes){
                    encodedData.Add(rest >= BCDcode);
                    if (rest >= BCDcode){
                        rest -= BCDcode;
                    }
                }
            }
            return new BitArray((bool[])encodedData.ToArray(typeof(bool)));
        }

        private static bool Xor(object firstValue, object secondValue) {
            Console.WriteLine(((bool)firstValue ? 1 : 0) + " xor " + ((bool)secondValue ? 1 : 0) + ": " + ((bool)firstValue != (bool)secondValue ? 1 : 0));
            return ((bool)firstValue != (bool)secondValue);
        }

        public static string BitArrayToStr(BitArray bitArray) {
            byte[] strArr = new byte[bitArray.Length / 8];
            ASCIIEncoding encoding = new ASCIIEncoding();
            for (int i = 0; i < bitArray.Length / 8; i++) {
                for (int index = i * 8, m = 1; index < i * 8 + 8; index++, m *= 2) {
                    strArr[i] += bitArray.Get(index) ? (byte)m : (byte)0;
                }
            }
            return encoding.GetString(strArr);
        }
    }
}
