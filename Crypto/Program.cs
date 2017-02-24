using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto {
    class Program {
        static void Main(string[] args) {
            while (true){
                Console.Write("Enter some string to encode it to Grey code (please, use only latin letters here): ");
                string to_encode = Console.ReadLine();
                string decoded_data = Crypto.encodeGrey(to_encode);
                //Console.WriteLine("------------------------------------------");
                //string encoded_data = Crypto.decodeGrey(decoded_data);
                //Console.WriteLine("------------------------------------------\n");
            }
        }
    }
}
