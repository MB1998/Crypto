using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto {
    class Program {
        static void Main(string[] args) {
            while (true){
                Console.WriteLine("Chose code type to start: \n1)Grey code;\n2)BCD;");
                string chosenOption = Console.ReadLine();
                int chosenOptionNumber = 0;
                if (!Int32.TryParse(chosenOption, out chosenOptionNumber) || !Enumerable.Range(1, 2).Contains(chosenOptionNumber)) {
                    Console.WriteLine("Please, enter valid number of coding's type!\n-------------------------------------------------------------------------\n");
                    continue;
                }
                switch (chosenOptionNumber){
                    case 1:
                        ShowGreyCode();
                        break;
                    case 2:
                        ShowBCDCode();
                        break;
                }
                Console.WriteLine("\n-------------------------------------------------------------------------\n");
            }
        }

        private static void ShowGreyCode() {
            Console.Write("\nEnter some string to encode it to Grey code (press 'enter' to exit to main manu): ");
            string input = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(input)) { return; }
            BitArray encodedData = ShowEncodeGrey(input);
            ShowDecodeGrey(encodedData);
        }

        private static BitArray ShowEncodeGrey(string dataToEncode ) {
            BitArray bitArray = new BitArray(Encoding.ASCII.GetBytes(dataToEncode));
            Console.Write("Binary code of data: ");
            foreach (bool bit in bitArray) {
                Console.Write(bit ? 1 : 0);
            }
            Console.WriteLine();
            BitArray encodedData = Crypto.EncodeGrey(bitArray);
            Console.Write("Encoded data: ");
            foreach (bool bit in encodedData) {
                Console.Write(bit ? 1 : 0);
            }
            Console.WriteLine("\n-------------------------------------------------------------------------\n");
            return encodedData;
        }

        private static void ShowDecodeGrey(BitArray encodedData) {
            Console.Write("Data to decode: ");
            foreach (bool bit in encodedData) {
                Console.Write(bit ? 1 : 0);
            }
            Console.WriteLine();
            BitArray decodedData = Crypto.DecodeGrey(encodedData);
            Console.Write("Decoded data: ");
            foreach (bool bit in decodedData) {
                Console.Write(bit ? 1 : 0);
            }
            Console.WriteLine("\nText which was encoded using Grey code: " + Crypto.BitArrayToStr(decodedData));
            Console.WriteLine("-------------------------------------------------------------------------\n\n");
        }

        private static void ShowBCDCode(){
            while (true) { 
                Console.Write("\nEnter number you would like to code and key for BCD coding separeted by ';' (press 'enter' to exit to main manu): ");
                string input = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(input)) {return;}
                string[] inputNumbers = input.Split(';');
                int numberToEncode = 0, keyForBCDcoding = 0;
                if (!Int32.TryParse(inputNumbers[0].Trim(), out numberToEncode) || !Int32.TryParse(inputNumbers[1].Trim(), out keyForBCDcoding)) {
                    Console.WriteLine("Please, enter valid number to code it!\n-------------------------------------------------------------------------\n");
                    continue;
                }
                ShowBCDEncode(inputNumbers[0].Trim(), inputNumbers[1].Trim());
                Console.WriteLine("-------------------------------------------------------------------------\n");
                return;
            }
        }

        private static void ShowBCDEncode(string numberToEncode, string BCDcodes) {
            ArrayList BCDcodesArrayList = new ArrayList();
            foreach (char BCDcode in BCDcodes){
                BCDcodesArrayList.Add(Convert.ToByte(BCDcode));
            }
            Console.WriteLine("Number {0} will be encoded using key {1}", numberToEncode, BCDcodes);
            BitArray encodedData = Crypto.EncodeBCD(numberToEncode, BCDcodesArrayList);
            for (int i = 0, k = 0; i < encodedData.Count; k++) {
                ArrayList terms = new ArrayList();
                for (int j = 0; i < BCDcodes.Length; j++, i++){
                    string term = String.Format("{0} * {1}", BCDcodes[j], (encodedData.Get(i) ? 1 : 0));
                    Console.WriteLine(term);
                    terms.Add(term);
                }
                Console.WriteLine("{0} = {1};", numberToEncode[k], String.Join(" + ", terms));
            }
        }
    }
}
