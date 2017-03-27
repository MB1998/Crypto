using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Crypto {
    public class Crypto {
        public static List<bool> EncodeGrey(List<bool> dataToEncode, ListBox listBox) {
            var encodedData = new List<bool> { dataToEncode[0] };
            for(var i = 1; i < dataToEncode.Count; i++) {
                encodedData.Add(Xor(dataToEncode[i], dataToEncode[i - 1], listBox));
            }
            return encodedData;
        }

        public static List<bool> DecodeGrey(List<bool> encodedData, ListBox listBox) {
            var decodedData = new List<bool> { encodedData[0] };
            Console.WriteLine(encodedData[encodedData.Count - 1] ? 1 : 0);
            for(var i = 1; i < encodedData.Count; i++) {
                decodedData.Add(Xor(decodedData[i - 1], encodedData[i], listBox));
            }
            return decodedData;
        }

        public static List<bool> EncodeBcd(string numberToEncode, List<int> bcdCodes) {
            var digits = new List<char>(numberToEncode.ToCharArray());
            var encodedData = new List<bool>();
            foreach(char digit in digits) {
                var rest = (int)Char.GetNumericValue(digit);
                foreach(int bcdCode in bcdCodes) {
                    encodedData.Add(rest >= bcdCode);
                    if(rest >= bcdCode) {
                        rest -= bcdCode;
                    }
                }
            }
            return encodedData;
        }

        public static int DecodedBdc(List<bool> encodedData, List<int> bcdCodes) {
            var decodedNumber = "";
            for(int i = 0, tmp = 0; i < encodedData.Count; i++) {
                tmp += (encodedData[i] ? 1 : 0) * bcdCodes[i % bcdCodes.Count];
                if((i + 1) % bcdCodes.Count != 0)
                    continue;
                decodedNumber += tmp.ToString();
                tmp = 0;
            }
            return int.Parse(decodedNumber);
        }

        public static List<bool> EncodeBerger(List<bool> dataToEncode, ListBox listBox) {
            var encodedData = new List<bool>();
            var r = Convert.ToInt32(Math.Round(Math.Log(dataToEncode.Count, 2)));
            listBox.Items.Add($"Amount of symbols: {dataToEncode.Count}");
            listBox.Items.Add($"R: {r}");
            var numOne = 0;
            for(var i = 0; i < dataToEncode.Count; i++) {
                if(dataToEncode[i])
                    numOne++;
                encodedData.Add(dataToEncode[i]);
            }
            listBox.Items.Add($"Amount of ones: {numOne}");
            listBox.Items.Add($"Amount of ones in binary code: {Convert.ToString(numOne, 2)}");
            var numOneInvers = Convert.ToString(numOne, 2)
                .PadLeft(r, '0')
                .Aggregate("", (current, c) => current + (c == '1' ? 0 : 1));
            listBox.Items.Add($"Inverse amount of ones in binary code: {numOneInvers}");
            foreach(var c in numOneInvers)
                encodedData.Add(c == '1');
            return encodedData;
        }

        public static List<bool> DecodeBerger(List<bool> encodedData, ListBox listBox) {
            var decodedData = new List<bool>();
            var r = Convert.ToInt32(Math.Round(Math.Log(encodedData.Count, 2)));
            listBox.Items.Add($"Amount of symbols: {encodedData.Count}");
            listBox.Items.Add($"R: {r}");

            var oneNum = "";
            for(var i = encodedData.Count - r; i < encodedData.Count; i++)
                oneNum += encodedData[i] ? '1' : '0';

            listBox.Items.Add($"Amount of ones without last {r} symbols in binary code: {oneNum}");
            var numOneInvers = oneNum.Aggregate("", (current, c) => current + (c == '1' ? 0 : 1)).PadLeft(8, '0');
            listBox.Items.Add($"Inverse amount of ones in binary code: {numOneInvers}");
            var countOneInMsg = ToByteArray(numOneInvers)[0];
            listBox.Items.Add($"Inverse amount of ones in binary code: {countOneInMsg}");
            var numOneCheck = 0;
            for(var i = 0; i < encodedData.Count - r; i++) {
                if(encodedData[i])
                    numOneCheck++;
                decodedData.Add(encodedData[i]);
            }
            return numOneCheck == countOneInMsg ? decodedData : new List<bool>();
        }

        public static bool[,] EncodeEllayes(bool[,] inputMatrix, ListBox listBox) {
            int inputMatrixRowsAmount = inputMatrix.GetLength(0), inputMatrixColumnsAmount = inputMatrix.GetLength(1);
            bool[,] encodedMatrix = new bool[inputMatrixRowsAmount + 1, inputMatrixColumnsAmount + 1];
            for(int i = 0; i < inputMatrixRowsAmount; i++) {
                var checkElement = inputMatrix[i, 0];
                encodedMatrix[i, 0] = inputMatrix[i, 0];
                for(int j = 1; j < inputMatrixColumnsAmount; j++) {
                    encodedMatrix[i, j] = inputMatrix[i, j];
                    checkElement = Xor(inputMatrix[i, j], checkElement, listBox);
                }
                encodedMatrix[i, inputMatrixColumnsAmount] = checkElement;
            }
            for(int i = 0; i < inputMatrixColumnsAmount; i++) {
                var checkElement = inputMatrix[0, i];
                for(int j = 1; j < inputMatrixRowsAmount; j++) {
                    checkElement = Xor(inputMatrix[j, i], checkElement, listBox);
                }
                encodedMatrix[inputMatrixRowsAmount, i] = checkElement;
            }
            return encodedMatrix;
        }

        public static bool[,] FixMistakesEllayes(bool[,] inputMatrix, ListBox listBox) {
            var encodedMatrix = EncodeEllayes(inputMatrix, null);
            int rowWithMistake = 0, columnWithMistake = 0;
            for(var i = 0; i < encodedMatrix.GetLength(0); i++) {
                if(encodedMatrix[i, encodedMatrix.GetLength(1) - 1] && i < encodedMatrix.GetLength(0) - 2) {
                    rowWithMistake = i + 1;
                    break;
                }
            }
            for(var i = 0; i < encodedMatrix.GetLength(1); i++) {
                if(encodedMatrix[encodedMatrix.GetLength(0) - 1, i] && i < encodedMatrix.GetLength(1) - 2) {
                    columnWithMistake = i + 1;
                    break;
                }
            }
            if(rowWithMistake != 0 && columnWithMistake != 0) {
                listBox.Items.Add(
                    $"Mistake was found in row {rowWithMistake} and column {columnWithMistake} : {(inputMatrix[rowWithMistake - 1, columnWithMistake - 1] ? "1" : "0")} => {(!inputMatrix[rowWithMistake - 1, columnWithMistake - 1] ? "1" : "0")}");
                inputMatrix[rowWithMistake - 1, columnWithMistake - 1] =
                    !inputMatrix[rowWithMistake - 1, columnWithMistake - 1];
            }
            return inputMatrix;
        }

        public static List<String> GetCodeCombinations(List<char> alphabet, int q, int n, string codeType, ListBox listBox) {
            long N = 0;
            List<String> combinations = new List<string>();
            switch(codeType) {
                case "на перестановки":
                    N = Factorial(n);
                    combinations = getAllCombinations(String.Empty, alphabet, n, false, false);
                    break;
                case "на размещение":
                    N = Factorial(q) / Factorial(q - n);
                    combinations = getAllCombinations(String.Empty, alphabet, n, false, false);
                    break;
                case "на определенные сочетания":
                    N = Factorial(q) / (Factorial(q - n) * Factorial(n));
                    combinations = getAllCombinations(String.Empty, alphabet, n, true, false);
                    break;
                case "на все сочетания":
                    N = (int)Math.Pow(q, n);
                    combinations = getAllCombinations(String.Empty, alphabet, n, false, true);
                    break;
                case "сменно-качественный":
                    N = q * (int)Math.Pow(q - 1, n - 1);
                    combinations = getAllCombinations(String.Empty, alphabet, n, false, false);
                    break;
            }
            listBox.Items.Add($"Overall amount of combinations N = {N}");
            return combinations;
        }

        public static List<char> CodeWithModuleQTestEncode(int q, List<char> combination, ListBox listBox) {
            int checkBit = q - (SumOfList(combination, q) % q);
            checkBit = checkBit == q ? 0 : checkBit;
            listBox.Items.Add($"Check bit: {q} - ({SumOfList(combination, q)} mod {q}) = {checkBit}");
            combination.Add(checkBit < 10 ? checkBit.ToString().ToCharArray()[0] : LettersToNumbers.FirstOrDefault(x => x.Value == checkBit).Key);
            return combination;
        }

        public static bool CodeWithModuleQTestCheckValidCombination(int q, List<char> combination, ListBox listBox) {
            return (SumOfList(combination, q) % q == 0);
        }

        public static string CodeWithSimpleRepetitionEncode(string combination) {
            return string.Concat(combination, combination);
        }

        public static string CodeWithSimpleRepetitionDecode(string combination) {
            for(int i = 0; i < combination.Length; i++) {
                if(combination == CodeWithSimpleRepetitionEncode(combination.Substring(0, i))) {
                    return combination.Substring(0, i);
                }
            }
            return null;
        }

        public static char[,] IterativeСodeEncode(char[,] inputMatrix, int q) {
            int inputMatrixRowsAmount = inputMatrix.GetLength(0), inputMatrixColumnsAmount = inputMatrix.GetLength(1);
            char[,] encodedMatrix = new char[inputMatrixRowsAmount + 1, inputMatrixColumnsAmount + 1];
            for(int i = 0; i < inputMatrixRowsAmount; i++) {
                List<char> listToSum = new List<char>();
                for(int j = 0; j < inputMatrixColumnsAmount; j++) {
                    listToSum.Add(inputMatrix[i, j]);
                    encodedMatrix[i, j] = inputMatrix[i, j];
                }
                int sumOfRow = SumOfList(listToSum, q);
                int checkBit = (q - (sumOfRow % q)) == q ? 0 : (q - (sumOfRow % q));
                encodedMatrix[i, inputMatrixColumnsAmount] = Convert.ToChar(checkBit.ToString());
            }
            for(int i = 0; i < inputMatrixColumnsAmount + 1; i++) {
                List<char> listToSum = new List<char>();
                for (int j = 0; j < inputMatrixRowsAmount; j++) {
                    listToSum.Add(encodedMatrix[j, i]);
                }
                int sumOfColumn = SumOfList(listToSum, q);
                int checkBit = (q - (sumOfColumn % q)) == q ? 0 : (q - (sumOfColumn % q));
                encodedMatrix[inputMatrixRowsAmount, i] = Convert.ToChar(checkBit.ToString());
            }
            return encodedMatrix;
        }

        public static void IterativeСodeFixMistakes(char[,] inputMatrix, int q, ListBox listBox) {
            char[,] encodedMatrix = Crypto.IterativeСodeEncode(inputMatrix, q);
            int columnOfError = 0, rowOfError = 0, checkNumber = 0;
            for (int i = 0; i < encodedMatrix.GetLength(0) - 2; i++) {
                if(encodedMatrix[i, encodedMatrix.GetLength(1) - 1] != '0') {
                    rowOfError = i + 1;
                    if(!Int32.TryParse(encodedMatrix[i, encodedMatrix.GetLength(1) - 1].ToString(), out checkNumber))
                        checkNumber = LettersToNumbers[encodedMatrix[i, encodedMatrix.GetLength(1) - 1].ToString().ToUpper().ToCharArray()[0]];
                }
            }
            for(int i = 0; i < encodedMatrix.GetLength(1) - 2; i++) {
                if(encodedMatrix[encodedMatrix.GetLength(0) - 1, i] != '0') {
                    columnOfError = i + 1;
                }
            }
            if(columnOfError != 0 && rowOfError != 0) {
                int falseValue;
                if(!Int32.TryParse(encodedMatrix[rowOfError - 1, columnOfError - 1].ToString(), out falseValue))
                    falseValue = LettersToNumbers[encodedMatrix[rowOfError - 1, columnOfError - 1].ToString().ToUpper().ToCharArray()[0]];
                int realValue = (falseValue + (checkNumber % q)) == q ? 0 : (falseValue + (checkNumber % q));
                char realValueChar = realValue <= 10 ? Convert.ToChar(realValue.ToString()) : LettersToNumbers.FirstOrDefault(x => x.Value == (checkNumber % q)).Key;
                inputMatrix[rowOfError - 1, columnOfError - 1] = realValueChar;
                listBox.Items.Add(
                    $"Mistake was found in column {columnOfError}, row {rowOfError}: {encodedMatrix[rowOfError - 1, columnOfError - 1]} -> {inputMatrix[rowOfError - 1, columnOfError - 1]}");
                listBox.Items.Add("Fixed matrix: ");
                DisplayMatrixWithLastAdditionalSymbol(listBox, inputMatrix);
                listBox.Items.Add($"Fixed message: {GetMessageFromMatrix(inputMatrix)}");
            } else {
                listBox.Items.Add("Mistake wasn't found");
            }
        }

        private static int SumOfList(List<char> numbers, int notation) {
            List<int> translatedNumbers = new List<int>();
            foreach(char number in numbers) {
                int translatedNumber;
                if (!Int32.TryParse(Convert.ToString(number), out translatedNumber)) {
                    translatedNumber = LettersToNumbers[number.ToString().ToUpper().ToCharArray()[0]];
                }
                translatedNumbers.Add(translatedNumber);
            }
            return translatedNumbers.Sum();
        }

        private static Dictionary<char, int> LettersToNumbers = new Dictionary<char, int>()
        {
            { 'A' , 10 }, { 'B' , 11 }, { 'C' , 12 }, { 'D' , 13 }, { 'E' , 14 }, { 'F' , 15 }, { 'G' , 16 }, { 'H' , 17 },
            { 'I' , 18 }, { 'J' , 19 }, { 'K' , 20 }, { 'L' , 21 }, { 'M' , 22 }, { 'N' , 23 }, { 'O' , 24 }, { 'P' , 25 },
            { 'Q' , 26 }, { 'R' , 27 }, { 'S' , 28 }, { 'T' , 29 }, { 'U' , 30 }, { 'V' , 31 }, { 'W' , 32 }, { 'X' , 33 },
            { 'Y' , 34 }, { 'Z' , 35 }
        };

        private static List<String> getAllCombinations(String currentCombination, List<char> alphabet, int lengthOfWord, bool useOnlyForwardDirection, bool allowReiteration) {
            List<String> combinations = new List<string>();
            List<char> alphabetForResursion = DeepClone(alphabet);
            for(int i = 0; i < alphabet.Count; i++) {
                currentCombination += alphabet[i];
                if(!allowReiteration)
                    alphabetForResursion.Remove(alphabet[i]);
                if(currentCombination.Length == lengthOfWord) {
                    combinations.Add(currentCombination);
                } else {
                    combinations.AddRange(getAllCombinations(currentCombination, alphabetForResursion, lengthOfWord, useOnlyForwardDirection, allowReiteration));
                }
                currentCombination = currentCombination.Substring(0, currentCombination.Length - 1);
                if(!useOnlyForwardDirection && !allowReiteration)
                    alphabetForResursion.Add(alphabet[i]);
            }
            return combinations;
        }

        private static bool Xor(object firstValue, object secondValue, ListBox listBox) {
            if(listBox != null)
                listBox.Items.Add(
                    $"{((bool)firstValue ? 1 : 0)} xor {((bool)secondValue ? 1 : 0)}: {(((bool)firstValue ^ (bool)secondValue) ? 1 : 0)}");
            return (bool)firstValue ^ (bool)secondValue;
        }

        public static byte[] ToByteArray(string str) {
            var result = Enumerable.Range(0, str.Length / 8).
                Select(pos => Convert.ToByte(
                        str.Substring(pos * 8, 8),
                        2)
                ).ToArray();
            return result;
        }

        public static string ToString(List<bool> datas) {
            return datas.Aggregate(string.Empty,
                (current, data) => current + (data ? "1" : "0"));
        }

        static long Factorial(long x) {
            return (x == 0) ? 1 : x * Factorial(x - 1);
        }

        static List<char> DeepClone(List<char> listToClone) {
            List<char> clonedList = new List<char>();
            foreach(char element in listToClone) {
                clonedList.Add(element);
            }
            return clonedList;
        }

        private static void DisplayMatrixWithLastAdditionalSymbol(ListBox listBox, char[,] matrix) {
            for(var i = 0; i < matrix.GetLength(0); i++) {
                var line = "";
                for(var j = 0; j < matrix.GetLength(1); j++) {
                    line += matrix[i, j] + "  ";
                    if(j == matrix.GetLength(1) - 2)
                        line += ("| ");
                }
                listBox.Items.Add(line);
                if(i == matrix.GetLength(0) - 2) {
                    line = "";
                    for(var j = 0; j <= matrix.GetLength(1) + 1; j++)
                        line += ("--");
                    listBox.Items.Add(line);
                }
            }
        }

        private static string GetMessageFromMatrix(char[,] matrix) {
            string message = "";
            for(int i = 0; i < matrix.GetLength(0); i++) {
                for(int j = 0; j < matrix.GetLength(1); j++) {
                    message += matrix[i, j].ToString();
                }
            }
            return message;
        }
    }
}
