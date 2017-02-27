using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Crypto
{
    public class Crypto
    {
        public static BitArray EncodeGrey(BitArray bitArray) {
            var encodedBitArray = new ArrayList {bitArray.Get(bitArray.Count - 1) };
            Console.WriteLine(bitArray.Get(bitArray.Count - 1) ? 1 : 0);
            for (var i = bitArray.Count - 1; i > 0; i--){
                encodedBitArray.Add(Xor(bitArray.Get(i), bitArray.Get(i - 1)));
            }
            encodedBitArray.Reverse();
            return new BitArray((bool[])encodedBitArray.ToArray(typeof(bool)));
        }

        public static BitArray DecodeGrey(BitArray encodedBitArray) {
            var decodedBitArray = new ArrayList {encodedBitArray.Get(encodedBitArray.Count - 1) };
            Console.WriteLine(encodedBitArray.Get(encodedBitArray.Count - 1) ? 1 : 0);
            for (int i = encodedBitArray.Count - 2; i >= 0; i--) {
                decodedBitArray.Add(Xor(decodedBitArray[encodedBitArray.Count - i - 2], encodedBitArray.Get(i)));
            }
            decodedBitArray.Reverse();
            return new BitArray((bool[])decodedBitArray.ToArray(typeof(bool)));
        }

        public static BitArray EncodeBcd(string numberToEncode, ArrayList bcdCodes) { 
            var digits = new ArrayList(numberToEncode.ToCharArray());
            var encodedData = new ArrayList();
            foreach (char digit in digits){
                var rest = (int)Char.GetNumericValue(digit);
                foreach (int bcdCode in bcdCodes){
                    encodedData.Add(rest >= bcdCode);
                    if (rest >= bcdCode){
                        rest -= bcdCode;
                    }
                }
            }
            return new BitArray((bool[])encodedData.ToArray(typeof(bool)));
        }

        public static int DecodedBdc(BitArray encodedData, ArrayList bcdCodes) {
            string decodedNumber = "";
            int tmp = 0;
            for (int i = 0; i < encodedData.Count; i++) {
                tmp += (encodedData.Get(i) ? 1 : 0) * (int)bcdCodes[i % bcdCodes.Count];
                if((i+1) % bcdCodes.Count == 0) {
                    decodedNumber += tmp.ToString();
                    tmp = 0;
                }
            }
            return Int32.Parse(decodedNumber);
        }

        public static BitArray EncodeBerger(BitArray dataToEncode){
            var encodedData = new ArrayList();
            var r = Convert.ToInt32(Math.Ceiling(Math.Log(dataToEncode.Count, 2)));
            Console.WriteLine($"Amount of symbols: {dataToEncode.Count}");
            Console.WriteLine($"R: {r}");
            var numOne = 0;
            for (var i = 0; i < dataToEncode.Count; i++){
                if (dataToEncode.Get(i))
                    numOne++;
                encodedData.Add(dataToEncode.Get(i));
            }
            Console.WriteLine($"Amount of ones: {numOne}");
            Console.WriteLine($"Amount of ones in binary code: {Convert.ToString(numOne, 2)}");
            var numOneInvers = Convert.ToString(numOne, 2)
                .PadLeft(r, '0')
                .Aggregate("", (current, c) => current + (c == '1' ? 0 : 1));
            Console.WriteLine($"Inverse amount of ones in binary code: {numOneInvers}");
            foreach (var c in numOneInvers)
                encodedData.Add(c == '1');
            return new BitArray((bool[])encodedData.ToArray(typeof(bool)));
        }

        public static BitArray DecodeBerger(BitArray encodedData){
            var decodedData = new ArrayList();
            var r = Convert.ToInt32(Math.Round(Math.Log(encodedData.Count, 2)));
            Console.WriteLine($"Amount of symbols: {encodedData.Count}");
            Console.WriteLine($"R: {r}");

            var oneNum = "";
            for (var i = encodedData.Count - r; i < encodedData.Count; i++)
                oneNum += encodedData.Get(i) ? '1' : '0';
            
            Console.WriteLine($"Amount of ones without last {r} symbols in binary code: {oneNum}");
            var numOneInvers = oneNum.Aggregate("", (current, c) => current + (c == '1' ? 0 : 1)).PadLeft(8, '0');
            Console.WriteLine($"Inverse amount of ones in binary code: {numOneInvers}");
            var countOneInMsg = ToByteArray(numOneInvers)[0];
            Console.WriteLine($"Inverse amount of ones in binary code: {countOneInMsg}");
            var numOneCheck = 0;
            for (var i = 0; i < encodedData.Count - r; i++){
                if (encodedData.Get(i))
                    numOneCheck++;
                decodedData.Add(encodedData.Get(i));
            }
            return numOneCheck == countOneInMsg ? new BitArray((bool[])decodedData.ToArray(typeof(bool))) : new BitArray(0);
        }

        public static bool[,] EncodeEllayes(bool[,] inputMatrix){
            int inputMatrixRowsAmount = inputMatrix.GetLength(0), inputMatrixColumnsAmount = inputMatrix.GetLength(1);
            bool[,] encodedMatrix = new bool[inputMatrixRowsAmount + 1, inputMatrixColumnsAmount + 1];
            for (int i = 0; i < inputMatrixRowsAmount; i++){
                var checkElement = inputMatrix[i, 0];
                encodedMatrix[i, 0] = inputMatrix[i, 0];
                for (int j = 1; j < inputMatrixColumnsAmount; j++){
                    encodedMatrix[i, j] = inputMatrix[i, j];
                    checkElement = Xor(inputMatrix[i, j], checkElement);
                }
                encodedMatrix[i, inputMatrixColumnsAmount] = checkElement;
            }
            for (int i = 0; i < inputMatrixColumnsAmount; i++){
                var checkElement = inputMatrix[0, i];
                for (int j = 1; j < inputMatrixRowsAmount; j++) {
                    checkElement = Xor(inputMatrix[j, i], checkElement);
                }
                encodedMatrix[inputMatrixRowsAmount, i] = checkElement;
            }
            return encodedMatrix;
        }
        
        private static bool Xor(object firstValue, object secondValue){
            Console.WriteLine($"{((bool)firstValue ? 1 : 0)} xor {((bool) secondValue? 1 : 0)}: {(((bool)firstValue ^ (bool)secondValue) ? 1 : 0)}");
            return ((bool)firstValue ^ (bool)secondValue);
        }

        public static string BitArrayToStr(BitArray bitArray){
            var strArr = new byte[bitArray.Length / 8];
            var encoding = new ASCIIEncoding();
            for (var i = 0; i < bitArray.Length / 8; i++){
                for (int index = i * 8, m = 1; index < i * 8 + 8; index++, m *= 2){
                    strArr[i] += bitArray.Get(index) ? (byte)m : (byte)0;
                }
            }
            return encoding.GetString(strArr);
        }

        public static byte[] ToByteArray(string str){
            var result = Enumerable.Range(0, str.Length / 8).
                Select(pos => Convert.ToByte(
                        str.Substring(pos * 8, 8),
                        2)
                ).ToArray();
            return result;
        }
    }
}
