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
                Console.Write("Enter some string to encode it to Grey code: ");
                string dataToEncode = Console.ReadLine();
                BitArray encodedData = ShowEncodeGrey(dataToEncode);
                ShowDecodeGrey(encodedData);
            }
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
            Console.WriteLine("\n------------------------------------------\n");
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
            Console.WriteLine("------------------------------------------\n\n");
        }
    }
}
