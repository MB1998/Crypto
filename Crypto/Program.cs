using System;
using System.Windows.Forms;

namespace Crypto
{
    internal class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }

        /*private static void Main(){
            while (true){
                Console.WriteLine("Chose code type to start: \n1)Grey code;\n2)BCD;\n3)Berger;\n4)Ellayes's code;");
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
                        ShowBergerCode();
                        break;
                    case 4:
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

        private static List<bool> ShowEncodeGrey(string input){
            var dataToEncode = new List<bool>();
            Console.Write("Binary code of data: ");
            foreach (char digit in input){
                dataToEncode.Add(digit == '1');
                Console.Write(digit == '1' ? 1 : 0);
            }
            Console.WriteLine();
            var encodedData = Crypto.EncodeGrey(dataToEncode);
            Console.Write("Encoded data: ");
            foreach (bool bit in encodedData) Console.Write(bit ? 1 : 0);
            Console.WriteLine("\n-------------------------------------------------------------------------\n");
            return encodedData;
        }

        private static void ShowDecodeGrey(List<bool> encodedData){
            Console.Write("Data to decode: ");
            foreach (bool bit in encodedData) Console.Write(bit ? 1 : 0);
            Console.WriteLine();
            var decodedData = Crypto.DecodeGrey(encodedData);
            Console.Write("Decoded data: ");
            foreach (bool bit in decodedData) Console.Write(bit ? 1 : 0);
            Console.WriteLine("\n-------------------------------------------------------------------------\n\n");
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
                var bcdCodesList = new List<int>();
                foreach (var bcdCode in bcdCodes) bcdCodesList.Add((int)Char.GetNumericValue(bcdCode));
                var encodedData = ShowBcdEncode(inputNumbers[0].Trim(), bcdCodesList, bcdCodes);
                ShowBcdDecode(encodedData, bcdCodesList, bcdCodes);
                Console.WriteLine("-------------------------------------------------------------------------\n");
                break;
            }
        }

        private static List<bool> ShowBcdEncode(string numberToEncode, List<int> bcdCodesList, string bcdCodes){
            Console.WriteLine($"Number {numberToEncode} will be encoded using key {bcdCodes}");
            var encodedData = Crypto.EncodeBcd(numberToEncode, bcdCodesList);
            Console.Write("Encoded data: ");
            foreach (bool bit in encodedData) Console.Write(bit ? 1 : 0);
            Console.WriteLine();
            for (int i = 0, k = 0; i < encodedData.Count; k++){
                var terms = new List<string>();
                for (var j = 0; j < bcdCodes.Length; j++, i++){
                    var term = $"{bcdCodes[j]} * {(encodedData[i] ? 1 : 0)}";
                    terms.Add(term);
                }
                Console.WriteLine($"{numberToEncode[k]} = {string.Join(" + ", terms)};");
            }
            return encodedData;
        }

        private static int ShowBcdDecode(List<bool> encodedData, List<int> bcdCodesArrayList, string bcdCodes){
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

        private static List<bool> ShowBergerEncode(string dataToEncode) {
            var bitArray = new List<bool>();
            Console.Write("Binary code of data: ");
            foreach (char digit in dataToEncode)
            {
                bitArray.Add(digit == '1');
                Console.Write(digit == '1' ? 1 : 0);
            }
            Console.WriteLine();
            var encodedData = Crypto.EncodeBerger(bitArray);
            Console.Write("Encoded data: ");
            foreach (bool bit in encodedData) Console.Write(bit ? 1 : 0);
            Console.WriteLine("\n-------------------------------------------------------------------------\n");
            return encodedData;
        }

        private static void ShowBergerDecode(List<bool> encodedData) {
            Console.Write("Data to decode: ");
            foreach (bool bit in encodedData) Console.Write(bit ? 1 : 0);
            Console.WriteLine();
            var decodedData = Crypto.DecodeBerger(encodedData);
            Console.Write("Decoded data: ");
            foreach (bool bit in decodedData) Console.Write(bit ? 1 : 0);
            Console.WriteLine("\n-------------------------------------------------------------------------\n\n");
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
        }*/
    }
}