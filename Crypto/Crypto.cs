using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto {
    class Crypto {
        public static string encodeGrey(string data_to_encode) {
            Console.WriteLine("data_to_encode: " + data_to_encode);
            BitArray bit_array = new BitArray(Encoding.ASCII.GetBytes(data_to_encode));
            return encodeGrey(bit_array);
        }

        public static BitArray encodeGrey(BitArray bit_array) {
            bool[] encoded_data = new bool[];
            encoded_data[0] = binary_code[0];
            foreach(int i = 1; i < binary_code.Length; i++){
                Console.WriteLine("encoded_data: " + encoded_data);
                encoded_data += xor(binary_code[i], binary_code[i - 1]);
            }
            Console.WriteLine("encoded_data: " + encoded_data);
            return encoded_data;
        }

        /*public static string decodeGrey(string encoded_data) {
            Console.WriteLine("encoded_data: " + encoded_data);
            byte[] decoded_data = decodeGreyBinary(encoded_data);
            return BitArrayToStr(new BitArray(decoded_data));
        }

        public static byte[] decodeGreyBinary(string encoded_data) {
            string decoded_data = encoded_data[0].ToString();
            for (int i = 1; i < encoded_data.Length; i++) {
                Console.WriteLine("decoded_data: " + decoded_data);
                decoded_data += xor(decoded_data[i - 1], encoded_data[i]);
            }
            Console.WriteLine("decoded_data: " + decoded_data);
            return decoded_data;
        }

        private static int xor(object first_value, object second_value) {
            Console.WriteLine(first_value + " xor " + second_value + ": " + (first_value == second_value ? 0 : 1));
            return (first_value == second_value ? 0 : 1);
        }

        private static string BitArrayToStr(BitArray ba) {
            byte[] strArr = new byte[ba.Length / 8];
            ASCIIEncoding encoding = new ASCIIEncoding();
            for (int i = 0; i < ba.Length / 8; i++) {
                for (int index = i * 8, m = 1; index < i * 8 + 8; index++, m *= 2) {
                    strArr[i] += ba.Get(index) ? (byte)m : (byte)0;
                }
            }
            return encoding.GetString(strArr);
        }*/
    }
}
