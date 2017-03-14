using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Crypto
{
    public class Crypto
    {
        public static List<bool> EncodeGrey(List<bool> dataToEncode, ListBox listBox) {
            var encodedData = new List<bool> { dataToEncode[0] };
            for (var i = 1; i < dataToEncode.Count; i++){
                encodedData.Add(Xor(dataToEncode[i], dataToEncode[i - 1], listBox));
            }
            return encodedData;
        }

        public static List<bool> DecodeGrey(List<bool> encodedData, ListBox listBox) {
            var decodedData = new List<bool> { encodedData[0] };
            Console.WriteLine(encodedData[encodedData.Count - 1] ? 1 : 0);
            for (var i = 1; i < encodedData.Count; i++) {
                decodedData.Add(Xor(decodedData[i-1], encodedData[i], listBox));
            }
            return decodedData;
        }

        public static List<bool> EncodeBcd(string numberToEncode, List<int> bcdCodes) { 
            var digits = new List<char>(numberToEncode.ToCharArray());
            var encodedData = new List<bool>();
            foreach (char digit in digits){
                var rest = (int)Char.GetNumericValue(digit);
                foreach (int bcdCode in bcdCodes){
                    encodedData.Add(rest >= bcdCode);
                    if (rest >= bcdCode){
                        rest -= bcdCode;
                    }
                }
            }
            return encodedData;
        }

        public static int DecodedBdc(List<bool> encodedData, List<int> bcdCodes) {
            var decodedNumber = "";
            for (int i = 0, tmp = 0; i < encodedData.Count; i++) {
                tmp += (encodedData[i] ? 1 : 0) * bcdCodes[i % bcdCodes.Count];
                if ((i + 1) % bcdCodes.Count != 0) 
                    continue;
                decodedNumber += tmp.ToString();
                tmp = 0;
            }
            return int.Parse(decodedNumber);
        }

        public static List<bool> EncodeBerger(List<bool> dataToEncode, ListBox listBox)
        {
            var encodedData = new List<bool>();
            var r = Convert.ToInt32(Math.Round(Math.Log(dataToEncode.Count, 2)));
            listBox.Items.Add($"Amount of symbols: {dataToEncode.Count}");
            listBox.Items.Add($"R: {r}");
            var numOne = 0;
            for (var i = 0; i < dataToEncode.Count; i++){
                if (dataToEncode[i])
                    numOne++;
                encodedData.Add(dataToEncode[i]);
            }
            listBox.Items.Add($"Amount of ones: {numOne}");
            listBox.Items.Add($"Amount of ones in binary code: {Convert.ToString(numOne, 2)}");
            var numOneInvers = Convert.ToString(numOne, 2)
                .PadLeft(r, '0')
                .Aggregate("", (current, c) => current + (c == '1' ? 0 : 1));
            listBox.Items.Add($"Inverse amount of ones in binary code: {numOneInvers}");
            foreach (var c in numOneInvers)
                encodedData.Add(c == '1');
            return encodedData;
        }

        public static List<bool> DecodeBerger(List<bool> encodedData, ListBox listBox)
        {
            var decodedData = new List<bool>();
            var r = Convert.ToInt32(Math.Round(Math.Log(encodedData.Count, 2)));
            listBox.Items.Add($"Amount of symbols: {encodedData.Count}");
            listBox.Items.Add($"R: {r}");

            var oneNum = "";
            for (var i = encodedData.Count - r; i < encodedData.Count; i++)
                oneNum += encodedData[i] ? '1' : '0';

            listBox.Items.Add($"Amount of ones without last {r} symbols in binary code: {oneNum}");
            var numOneInvers = oneNum.Aggregate("", (current, c) => current + (c == '1' ? 0 : 1)).PadLeft(8, '0');
            listBox.Items.Add($"Inverse amount of ones in binary code: {numOneInvers}");
            var countOneInMsg = ToByteArray(numOneInvers)[0];
            listBox.Items.Add($"Inverse amount of ones in binary code: {countOneInMsg}");
            var numOneCheck = 0;
            for (var i = 0; i < encodedData.Count - r; i++){
                if (encodedData[i])
                    numOneCheck++;
                decodedData.Add(encodedData[i]);
            }
            return numOneCheck == countOneInMsg ? decodedData : new List<bool>();
        }

        public static bool[,] EncodeEllayes(bool[,] inputMatrix, ListBox listBox){
            int inputMatrixRowsAmount = inputMatrix.GetLength(0), inputMatrixColumnsAmount = inputMatrix.GetLength(1);
            bool[,] encodedMatrix = new bool[inputMatrixRowsAmount + 1, inputMatrixColumnsAmount + 1];
            for (int i = 0; i < inputMatrixRowsAmount; i++){
                var checkElement = inputMatrix[i, 0];
                encodedMatrix[i, 0] = inputMatrix[i, 0];
                for (int j = 1; j < inputMatrixColumnsAmount; j++){
                    encodedMatrix[i, j] = inputMatrix[i, j];
                    checkElement = Xor(inputMatrix[i, j], checkElement, listBox);
                }
                encodedMatrix[i, inputMatrixColumnsAmount] = checkElement;
            }
            for (int i = 0; i < inputMatrixColumnsAmount; i++){
                var checkElement = inputMatrix[0, i];
                for (int j = 1; j < inputMatrixRowsAmount; j++) {
                    checkElement = Xor(inputMatrix[j, i], checkElement, listBox);
                }
                encodedMatrix[inputMatrixRowsAmount, i] = checkElement;
            }
            return encodedMatrix;
        }

        private static bool Xor(object firstValue, object secondValue, ListBox listBox)
        {
            listBox.Items.Add(
                $"{((bool) firstValue ? 1 : 0)} xor {((bool) secondValue ? 1 : 0)}: {(((bool) firstValue ^ (bool) secondValue) ? 1 : 0)}");
            return (bool) firstValue ^ (bool) secondValue;
        }

        public static byte[] ToByteArray(string str){
            var result = Enumerable.Range(0, str.Length / 8).
                Select(pos => Convert.ToByte(
                        str.Substring(pos * 8, 8),
                        2)
                ).ToArray();
            return result;
        }

        public static string ToString(List<bool> datas)
        {
            return datas.Aggregate(string.Empty,
                (current, data) => current + (data ? "1" : "0"));
        }
    }
}
