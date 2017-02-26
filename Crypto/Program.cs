using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Crypto
{
    internal class Program
    {
        private static void Main()
        {
            while (true)
            {
                Console.WriteLine("Chose code type to start: \n1)Grey code;\n2)BCD;");
                var chosenOption = Console.ReadLine();
                int chosenOptionNumber;
                if (!int.TryParse(chosenOption, out chosenOptionNumber) ||
                    !Enumerable.Range(1, 2).Contains(chosenOptionNumber))
                {
                    Console.WriteLine(
                        "Please, enter valid number of coding's type!\n-------------------------------------------------------------------------\n");
                    continue;
                }
                switch (chosenOptionNumber)
                {
                    case 1:
                        ShowGreyCode();
                        break;
                    case 2:
                        ShowBcdCode();
                        break;
                    default:
                        return;
                }
                Console.WriteLine("\n-------------------------------------------------------------------------\n");
            }
        }

        private static void ShowGreyCode()
        {
            Console.Write("\nEnter some string to encode it to Grey code (press 'enter' to exit to main manu): ");
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) return;
            var encodedData = ShowEncodeGrey(input);
            ShowDecodeGrey(encodedData);
        }

        private static BitArray ShowEncodeGrey(string dataToEncode)
        {
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

        private static void ShowDecodeGrey(BitArray encodedData)
        {
            Console.Write("Data to decode: ");
            foreach (bool bit in encodedData) Console.Write(bit ? 1 : 0);
            Console.WriteLine();
            var decodedData = Crypto.DecodeGrey(encodedData);
            Console.Write("Decoded data: ");
            foreach (bool bit in decodedData) Console.Write(bit ? 1 : 0);
            Console.WriteLine("\nText which was encoded using Grey code: " + Crypto.BitArrayToStr(decodedData));
            Console.WriteLine("-------------------------------------------------------------------------\n\n");
        }

        private static void ShowBcdCode()
        {
            while (true)
            {
                Console.Write(
                    "\nEnter number you would like to code and key for BCD coding separeted by ';' (press 'enter' to exit to main manu): ");
                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input)) return;
                var inputNumbers = input.Split(';');
                int numberToEncode, keyForBcDcoding;
                if (!int.TryParse(inputNumbers[0].Trim(), out numberToEncode) ||
                    !int.TryParse(inputNumbers[1].Trim(), out keyForBcDcoding))
                {
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

        private static void ShowBcdDecode(BitArray encodedData, ArrayList bcdCodesArrayList, string bcdCodes){
            Console.Write("\nData to decode: ");
            foreach (bool bit in encodedData) Console.Write(bit ? 1 : 0);
            Console.WriteLine($" will be decoded using key {bcdCodes};");
            var decodedNumber = Crypto.DecodedBdc(encodedData, bcdCodesArrayList);
            Console.WriteLine($"Decoded number is {decodedNumber}");
        }
    }
}