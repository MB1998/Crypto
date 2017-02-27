using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Crypto{
    internal class Program{

        private static void Main(){
            while (true){
                Console.WriteLine("Chose code type to start: \n1)Grey code;\n2)BCD;\n3)BCD code for text;\n4)Berger;\n5)Ellayes's code;");
                var chosenOption = Console.ReadLine();
                int chosenOptionNumber;
                if (!int.TryParse(chosenOption, out chosenOptionNumber) ||
                    !Enumerable.Range(1, 4).Contains(chosenOptionNumber)){
                    Console.WriteLine("Please, enter valid number of coding's type!\n-------------------------------------------------------------------------\n");
                    continue;
                }
                switch (chosenOptionNumber){
                    case 1:
                        ShowGreyCode();
                        break;
                    case 2:
                        ShowBcdCode();
                        break;
                    case 3:
                        ShowBCDcodeLiteralEncode();
                        break;
                    case 4:
                        ShowBergerCode();
                        break;
                    case 5:
                        ShowEllayesCode();
                        break;
                    default:
                        return;
                }
                Console.WriteLine("\n-------------------------------------------------------------------------\n");
            }
        }

        private static void ShowGreyCode(){
            Console.Write("\nEnter some string to encode it to Grey code (press 'enter' to exit to main manu): ");
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) return;
            var encodedData = ShowEncodeGrey(input);
            ShowDecodeGrey(encodedData);
        }

        private static BitArray ShowEncodeGrey(string dataToEncode){
            var bitArray = new BitArray(Encoding.ASCII.GetBytes(dataToEncode));
            Console.Write("Binary code of data: ");
            foreach (bool bit in bitArray) Console.Write(bit ? 1 : 0);
            Console.WriteLine();
            var encodedData = Crypto.EncodeGrey(bitArray);
            Console.Write("Encoded data: ");
            foreach (bool bit in encodedData) Console.Write(bit ? 1 : 0);
            Console.WriteLine("\n-------------------------------------------------------------------------\n");
            return encodedData;
        }

        private static void ShowDecodeGrey(BitArray encodedData){
            Console.Write("Data to decode: ");
            foreach (bool bit in encodedData) Console.Write(bit ? 1 : 0);
            Console.WriteLine();
            var decodedData = Crypto.DecodeGrey(encodedData);
            Console.Write("Decoded data: ");
            foreach (bool bit in decodedData) Console.Write(bit ? 1 : 0);
            Console.WriteLine("\nText which was encoded using Grey code: " + Crypto.BitArrayToStr(decodedData));
            Console.WriteLine("-------------------------------------------------------------------------\n\n");
        }

        private static void ShowBcdCode(){
            while (true){
                Console.Write("\nEnter number you would like to code and key for BCD coding separeted by ';' (press 'enter' to exit to main manu): ");
                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input)) return;
                var inputNumbers = input.Split(';');
                int numberToEncode, keyForBcDcoding;
                if (!int.TryParse(inputNumbers[0].Trim(), out numberToEncode) ||
                    !int.TryParse(inputNumbers[1].Trim(), out keyForBcDcoding)){
                    Console.WriteLine(
                        "Please, enter valid number to code it!\n-------------------------------------------------------------------------\n");
                    continue;
                }
                var bcdCodes = inputNumbers[1].Trim();
                var bcdCodesArrayList = new ArrayList();
                foreach (var bcdCode in bcdCodes) bcdCodesArrayList.Add((int)Char.GetNumericValue(bcdCode));
                var encodedData = ShowBcdEncode(inputNumbers[0].Trim(), bcdCodesArrayList, bcdCodes);
                ShowBcdDecode(encodedData, bcdCodesArrayList, bcdCodes);
                Console.WriteLine("-------------------------------------------------------------------------\n");
                break;
            }
        }

        private static void ShowBCDcodeLiteralEncode() {
            while (true) {
                Console.Write("\nEnter any text you would like to encode using BCD code and key for it separeted by ';' (press 'enter' to exit to main manu): ");
                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input)) return;
                var inputNumbers = input.Split(';');
                var textToEncode = inputNumbers[0].Trim();
                int keyForBcDcoding;
                if (inputNumbers.Length != 2 || !int.TryParse(inputNumbers[1].Trim(), out keyForBcDcoding)) {
                    Console.WriteLine("Please, enter valid number to code it!\n-------------------------------------------------------------------------\n");
                    continue;
                }
                var bcdCodes = inputNumbers[1].Trim();
                var bcdCodesArrayList = new ArrayList();
                foreach (var bcdCode in bcdCodes) bcdCodesArrayList.Add((int)Char.GetNumericValue(bcdCode));
                var bitArray = new BitArray(Encoding.ASCII.GetBytes(textToEncode));
                Console.Write("Binary code of data: ");
                foreach (bool bit in bitArray) Console.Write(bit ? 1 : 0);
                Console.WriteLine();
                string decodedNumber = ShowBcdDecode(bitArray, bcdCodesArrayList, bcdCodes).ToString();
                BitArray resultingArray = ShowBcdEncode(decodedNumber, bcdCodesArrayList, bcdCodes);
                Console.WriteLine("\nText which was encoded using BCD code: " + Crypto.BitArrayToStr(resultingArray));
                Console.WriteLine("-------------------------------------------------------------------------\n");
                return;
            }
        }

        private static BitArray ShowBcdEncode(string numberToEncode, ArrayList bcdCodesArrayList, string bcdCodes){
            Console.WriteLine($"Number {numberToEncode} will be encoded using key {bcdCodes}");
            var encodedData = Crypto.EncodeBcd(numberToEncode, bcdCodesArrayList);
            Console.Write("Encoded data: ");
            foreach (bool bit in encodedData) Console.Write(bit ? 1 : 0);
            Console.WriteLine();
            for (int i = 0, k = 0; i < encodedData.Count; k++){
                var terms = new ArrayList();
                for (var j = 0; j < bcdCodes.Length; j++, i++){
                    var term = $"{bcdCodes[j]} * {(encodedData.Get(i) ? 1 : 0)}";
                    terms.Add(term);
                }
                Console.WriteLine($"{numberToEncode[k]} = {string.Join(" + ", (string[])terms.ToArray(Type.GetType("System.String")))};");
            }
            return encodedData;
        }

        private static int ShowBcdDecode(BitArray encodedData, ArrayList bcdCodesArrayList, string bcdCodes){
            Console.Write("\nData to decode: ");
            foreach (bool bit in encodedData) Console.Write(bit ? 1 : 0);
            Console.WriteLine($" will be decoded using key {bcdCodes};");
            var decodedNumber = Crypto.DecodedBdc(encodedData, bcdCodesArrayList);
            Console.WriteLine($"Decoded number is {decodedNumber}");
            return decodedNumber;
        }

        private static void ShowBergerCode() {
            Console.Write("\nEnter some string to encode it to Berger code (press 'enter' to exit to main manu): ");
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) return;
            var encodedData = ShowBergerEncode(input);
            ShowBergerDecode(encodedData);
        }

        private static BitArray ShowBergerEncode(String dataToEncode) {
            var bitArray = new BitArray(Encoding.ASCII.GetBytes(dataToEncode));
            Console.Write("Binary code of data: ");
            foreach (bool bit in bitArray) Console.Write(bit ? 1 : 0);
            Console.WriteLine();
            var encodedData = Crypto.EncodeBerger(bitArray);
            Console.Write("Encoded data: ");
            foreach (bool bit in encodedData) Console.Write(bit ? 1 : 0);
            Console.WriteLine("\n-------------------------------------------------------------------------\n");
            return encodedData;
        }

        private static void ShowBergerDecode(BitArray encodedData) {
            Console.Write("Data to decode: ");
            foreach (bool bit in encodedData) Console.Write(bit ? 1 : 0);
            Console.WriteLine();
            var decodedData = Crypto.DecodeBerger(encodedData);
            Console.Write("Decoded data: ");
            foreach (bool bit in decodedData) Console.Write(bit ? 1 : 0);
            Console.WriteLine("\nText which was encoded using Berder code: " + Crypto.BitArrayToStr(decodedData));
            Console.WriteLine("-------------------------------------------------------------------------\n\n");
        }

        private static void ShowEllayesCode(){
            while (true) {
                Console.Write("Enter size of matrix to be encoded (amount of rows and columns separeted by ';'): ");
                var input = Console.ReadLine();
                int rowsAmount = 0, columnsAmount = 0;
                if (string.IsNullOrWhiteSpace(input)) return;
                var amountNumber = input.Split(';');
                if (amountNumber.Length != 2 || !Int32.TryParse(amountNumber[0].Trim(), out rowsAmount) ||
                    !Int32.TryParse(amountNumber[1].Trim(), out columnsAmount)){
                    Console.WriteLine("Please, enter valid number to code it!\n-------------------------------------------------------------------------\n");
                    continue;
                }
                bool[,] encodedData = ShowEllayesCodeEncode(rowsAmount, columnsAmount);
                break;
            }
        }

        private static bool[,] ShowEllayesCodeEncode(int rowsAmount, int columnsAmount) {
            bool[,] dataToEncode = new bool[rowsAmount, columnsAmount];
            Random ran = new Random();
            Console.WriteLine("\nWe are going to encode following matrix using Ellayes's code (matrix is created automatically): ");
            for (int i = 0; i < rowsAmount; i++) {
                for (int j = 0; j < columnsAmount; j++) {
                    dataToEncode[i, j] = (ran.Next(0, 2) == 1);
                    Console.Write("{0} ", (dataToEncode[i, j] ? 1 : 0));
                }
                Console.WriteLine();
            }
            bool[,] encodedData = Crypto.EncodeEllayes(dataToEncode);
            Console.WriteLine("Encoded matrix: ");
            for (int i = 0; i < encodedData.GetLength(0); i++) {
                for (int j = 0; j < encodedData.GetLength(1); j++) {
                    Console.Write("{0} ", (encodedData[i, j] ? 1 : 0));
                    if(j == columnsAmount - 1) Console.Write("| ");
                }
                Console.WriteLine();
                if (i == rowsAmount - 1){
                    for(int j = 0; j <= columnsAmount + 1; j++)
                        Console.Write("--");
                    Console.WriteLine();
                }
            }
            return encodedData;
        }
    }
}