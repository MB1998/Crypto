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

        public static int getAmountOfInformation(List<string> probabilitiesOfMessages, List<int> ensembleOrder) {
            int amountOfInfromation = 0;
            foreach(int ensembleMember in ensembleOrder) {
                List<int> deviders = (new List<string>(probabilitiesOfMessages[ensembleMember-1].Split('/'))).Select(x => Int32.Parse(x)).ToList();
                amountOfInfromation += (int)Math.Log(1 / deviders[0] * deviders[1], 2);
            }
            return amountOfInfromation;
        }

        public static double getUnconditionalEntropy(List<string> probabilitiesOfMessages) {
            double unconditionalEntropy = 0;
            foreach(string probabilityOfMessage in probabilitiesOfMessages) {
                unconditionalEntropy += Double.Parse(probabilityOfMessage) * Math.Log(Double.Parse(probabilityOfMessage), 2);
            }
            unconditionalEntropy *= -1;
            return unconditionalEntropy;
        }

        public static double getMaximumEntropy(List<string> probabilitiesOfMessages) {
            return Math.Log(probabilitiesOfMessages.Count, 2);
        }

        public static double getConditionalEntropy(double[,] conditionalProbabilitiesMatrix) {
            double conditionalEntropy = 0;
            for(int i = 0; i < conditionalProbabilitiesMatrix.GetLength(0); i++) {
                for(int j = 0; j < conditionalProbabilitiesMatrix.GetLength(1); j++) {
                    conditionalEntropy += conditionalProbabilitiesMatrix[i, j] * Math.Log(conditionalProbabilitiesMatrix[i, j], 2);
                }
            }
            conditionalEntropy *= -1;
            return conditionalEntropy;
        }

        public static List<string> EncodeShannonFanoCode(List<double> probabilitiesOfMessages, List<string> encodedProbabilities) {
            if(encodedProbabilities == null) {
                encodedProbabilities = new List<string>(probabilitiesOfMessages.Count);
                encodedProbabilities.Add("0");
                for(int i = 0; i < probabilitiesOfMessages.Count - 1; i++) {
                    encodedProbabilities.Add(String.Empty);
                }
            } else {
                encodedProbabilities[0] += '0';
            }
            int firstSectionLength = 1;
            for(int i = 0; i < probabilitiesOfMessages.Count - 1; i++) {
                double diffSumOfSections = Math.Abs(probabilitiesOfMessages.GetRange(0, i + 1).Sum() - probabilitiesOfMessages.GetRange(i + 1, probabilitiesOfMessages.Count - 1 - i).Sum());
                double potensialdiffSumOfSections = Math.Abs(probabilitiesOfMessages.GetRange(0, i + 2).Sum() - probabilitiesOfMessages.GetRange(i + 2, probabilitiesOfMessages.Count - 2 - i).Sum());
                if(diffSumOfSections >= potensialdiffSumOfSections) {
                    encodedProbabilities[i + 1] += '0';
                    firstSectionLength++;
                } else {
                    encodedProbabilities[i + 1] += '1';
                }
            }
            if(firstSectionLength != 1) {
                List<string> finalCodeForFirstSection = EncodeShannonFanoCode(probabilitiesOfMessages.GetRange(0, firstSectionLength), encodedProbabilities.GetRange(0, firstSectionLength));
                for(int i = 0; i < firstSectionLength; i++) {
                    encodedProbabilities[i] = finalCodeForFirstSection[i];
                }
            }
            if(probabilitiesOfMessages.Count - firstSectionLength != 1) {
                List<string> finalCodeForSecondSection = EncodeShannonFanoCode(probabilitiesOfMessages.GetRange(firstSectionLength, probabilitiesOfMessages.Count - firstSectionLength), encodedProbabilities.GetRange(firstSectionLength, encodedProbabilities.Count - firstSectionLength));
                for(int i = firstSectionLength, j = 0; j < finalCodeForSecondSection.Count; i++, j++) {
                    encodedProbabilities[i] = finalCodeForSecondSection[j];
                }
            }
            return encodedProbabilities;
        }

        public static List<string> EncodeHaffman(List<double> probabilitiesOfMessages, ListBox listBoxToShow) {
            List<List<double>> auxiliaryGroupsHaffman = new List<List<double>>();
            for(int i = 0; i < probabilitiesOfMessages.Count - 1; i++) {
                if(probabilitiesOfMessages[i] == probabilitiesOfMessages[i + 1]) {
                    probabilitiesOfMessages[i + 1] += 0.0001 * i;
                    probabilitiesOfMessages[i] -= 0.0001 * i;
                }
            }
            List<double> initialProbabilitiesOfMessages = probabilitiesOfMessages.Select(x => x).ToList();
            probabilitiesOfMessages.Sort();
            probabilitiesOfMessages.Reverse();
            List<string> encodedProbabilities = new List<string>();
            for (int i = 0; i < probabilitiesOfMessages.Count; i++) {
                encodedProbabilities.Add(String.Empty);
            }
            auxiliaryGroupsHaffman.Add(probabilitiesOfMessages.Select(x => Math.Round(x, 3)).ToList());
            int initialListLength = probabilitiesOfMessages.Count;
            for(int i = 0; i < initialListLength - 1; i++) {
                double smallestProbability = probabilitiesOfMessages[probabilitiesOfMessages.Count - 1];
                double secondFromEndProbability = probabilitiesOfMessages[probabilitiesOfMessages.Count - 2];
                double sumOfSmallestProbabilities = smallestProbability + secondFromEndProbability;
                probabilitiesOfMessages[probabilitiesOfMessages.Count - 2] = sumOfSmallestProbabilities;
                probabilitiesOfMessages.Remove(smallestProbability);
                if (smallestProbability != secondFromEndProbability) {
                    for (int j = 0; j < initialProbabilitiesOfMessages.Count; j++) {
                        if (initialProbabilitiesOfMessages[j] == smallestProbability)
                            encodedProbabilities[j] += "0";
                        else if (initialProbabilitiesOfMessages[j] == secondFromEndProbability)
                            encodedProbabilities[j] += "1";
                    }
                } else {
                    int count = 0;
                    for (int j = 0; j < initialProbabilitiesOfMessages.Count; j++) {
                        if (initialProbabilitiesOfMessages[j] == smallestProbability)
                            count++;
                    }
                    for (int j = 0; j < initialProbabilitiesOfMessages.Count; j++) {
                        if (initialProbabilitiesOfMessages[j] == smallestProbability && i < count / 2)
                            encodedProbabilities[j] += "0";
                        else if (initialProbabilitiesOfMessages[j] == smallestProbability && i >= count / 2)
                            encodedProbabilities[j] += "1";
                    }
                }
                initialProbabilitiesOfMessages = initialProbabilitiesOfMessages.Select(x => x == smallestProbability || x == secondFromEndProbability ? sumOfSmallestProbabilities : x).ToList();
                probabilitiesOfMessages.Sort();
                probabilitiesOfMessages.Reverse();
                auxiliaryGroupsHaffman.Add(probabilitiesOfMessages.Select(x => Math.Round(x, 3)).ToList());
            }
            DiplsayAuxiliaryGroupsHaffman(auxiliaryGroupsHaffman, listBoxToShow);
            for (int i = 0; i < encodedProbabilities.Count; i++) {
                char[] charArray = encodedProbabilities[i].ToCharArray();
                Array.Reverse(charArray);
                encodedProbabilities[i] = new string(charArray);
            }
            return encodedProbabilities;
        }

        public static void EncodeVarshamovaCode(int codeLength, int minCodeDistance, String toEncode, ListBox listBox) {
            int amountOfColumns = codeLength;
            int amountOfFixedBugs = getVarshamoveCodeAmountOfColumns(minCodeDistance);
            int amountOfCheckDigits = getAmountOfCheckDigits(codeLength, minCodeDistance);
            int amountOfRows = codeLength - amountOfCheckDigits;
            listBox.Items.Add($"Amount of columns of addishional matrix: {amountOfColumns}.");
            listBox.Items.Add($"Amount of bugs which could be fixed: {amountOfFixedBugs}.");
            listBox.Items.Add($"Amount of rows of addishional matrix: {amountOfRows}.");
            listBox.Items.Add($"Amount of check digits: {amountOfCheckDigits}.");
            listBox.Items.Add("-------------------------------------------------------------------------");
            listBox.Items.Add("Additional matrix: ");
            foreach(String matrixRow in getAdditionalMatrixVarshamova()) {
                listBox.Items.Add(matrixRow);
            }
            listBox.Items.Add("-------------------------------------------------------------------------");
            String encodedCombination = VarshamovCode.Encode(toEncode);
            listBox.Items.Add("Encoded combination: ");
            listBox.Items.Add(encodedCombination);
            listBox.Items.Add("-------------------------------------------------------------------------");
            listBox.Items.Add("Checking matrix matrix: ");
            foreach (String matrixRow in getHMatrixVarshamova()) {
                listBox.Items.Add(matrixRow);
            }
            listBox.Items.Add("-------------------------------------------------------------------------");
        }

        public static void FixMistakesVarshamovaCode(String combinationToFix, ListBox listBox) {
            listBox.Items.Add("Combination with mistakes: ");
            listBox.Items.Add(combinationToFix);
            listBox.Items.Add("-------------------------------------------------------------------------");
            listBox.Items.Add("Fixed combination: ");
            listBox.Items.Add(VarshamovCode.CorrectCode(combinationToFix));
            listBox.Items.Add("-------------------------------------------------------------------------");

        }

        private static List<String> getAdditionalMatrixVarshamova() {
            List<String> additionalMatrixVarshamova = new List<String>();
            for(int i = 0; i < AdditionalMatrixVarshamova.GetLength(0); i++) {
                string matrixRow = "";
                for(int j = 0; j < AdditionalMatrixVarshamova.GetLength(1); j++) {
                    matrixRow += AdditionalMatrixVarshamova[i, j] + "   ";
                    if(j == 5)
                        matrixRow += "|  ";
                }
                additionalMatrixVarshamova.Add(matrixRow);
            }
            return additionalMatrixVarshamova;
        }

        private static List<String> getHMatrixVarshamova() {
            List<String> additionalMatrixVarshamova = new List<String>();
            for (int i = 0; i < h.GetLength(0); i++) {
                string matrixRow = "";
                for (int j = 0; j < h.GetLength(1); j++) {
                    matrixRow += h[i, j] + "   ";
                    if (j == 5)
                        matrixRow += "|  ";
                }
                additionalMatrixVarshamova.Add(matrixRow);
            }
            return additionalMatrixVarshamova;
        }

        static byte[,] AdditionalMatrixVarshamova = new byte[,]
        {
            { 1, 0, 0, 0, 0, 0, 0, 0, 1, 1},
        {0,1,0,0,0,0,0,1,0,1 },
        {0,0,1,0,0,0,1,0,0,1 },
        {0,0,0,1,0,0,0,1,1,0 },
        {0,0,0,0,1,0,1,0,1,0 },
        {0,0,0,0,0,1,1,1,0,0 }
        };

        static byte[,] h = new byte[,]
            {
                {0,0,1,0,1,1,1,0,0,0 },
            {0,1,0,1,0,1,0,1,0,0 },
            {1,0,0,1,1,0,0,0,1,0 },
            {1,1,1,0,0,0,0,0,0,1 }
            };

        private static int getVarshamoveCodeAmountOfColumns(int minCodeDistance) {
            return (int)Math.Floor((double)((minCodeDistance - 1) / 2));
        }

        private static int getAmountOfCheckDigits(int codeLength, int minCodeDistance) {
            double valueToCompare = 1;
            for(int i = 1; i <= minCodeDistance - 2; i++) {
                valueToCompare += (Factorial(codeLength - 1) / (Factorial(i) * Factorial(codeLength - 1 - i)));
            }
             return (int)Math.Floor(Math.Log(valueToCompare, 2)) + 1;
        }

        private static void DiplsayAuxiliaryGroupsHaffman(List<List<double>> auxiliaryGroupsHaffman, ListBox listBox) {
            listBox.Items.Add("Auxiliary Groups of Haffman for enterred possibilities: ");
            for (int i = 0; i < auxiliaryGroupsHaffman[0].Count; i++) {
                string listBoxItem = $"P(x{i + 1})\t";
                for (int j = 0; j < auxiliaryGroupsHaffman.Count - i; j++) {
                    listBoxItem += $"{auxiliaryGroupsHaffman[j][i]}\t";
                }
                listBox.Items.Add(listBoxItem);
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

        public static string EncodeAbramsonaCode(List<bool> dataToEncode, List<bool> polynomial, ListBox listBox) {
            listBox.Items.Add("Message: " + ToString(dataToEncode));
            listBox.Items.Add("Polynomial: " + ToString(polynomial));
            List<bool> formingPolynomial = GetFormingPolynomial(polynomial);
            listBox.Items.Add("Forming polinomial: " + ToString(formingPolynomial));
            List<bool> remainderOfDivision = devide(dataToEncode, formingPolynomial, true);
            dataToEncode.AddRange(remainderOfDivision);
            listBox.Items.Add("Encoded message: " + ToString(dataToEncode));
            listBox.Items.Add("-------------------------------------------------------------------------");
            return ToString(dataToEncode);
        }

        private static List<bool> GetFormingPolynomial(List<bool> polynomial) {
            List<bool> tempPolinomial = polynomial.Select(digit => digit).ToList();
            tempPolinomial.Add(false);
            return xor(polynomial, tempPolinomial);
        }

        private static List<bool> xor (List<bool> code1, List<bool> code2) {
            List<bool> result = new List<bool>();
            for(int i = 0; i < (code1.Count > code2.Count ? code1.Count : code2.Count); i++ ) {
                bool firstDigit = (code1.Count <= i ? false : code1[code1.Count - 1 - i]);
                bool secondDigit = (code2.Count <= i ? false : code2[code2.Count - 1 - i]);
                result.Add(firstDigit ^ secondDigit);
            }
            result.Reverse();
            return result;
        }

        private static List<bool> devide(List<bool> dividend, List<bool> divider, bool multiplyX5) {
            dividend = deleteFirstZeros(dividend);
            if(multiplyX5) {
                List<bool> addintionalZerosToEnd = new List<bool>();
                for(int i = 0; i < divider.Count - 1; i++) {
                    addintionalZerosToEnd.Add(false);
                }
                dividend.AddRange(addintionalZerosToEnd);
            }
            List<bool> remainderOfDivision = new List<bool>();
            int nextIndexOfDevident = divider.Count;
            remainderOfDivision = dividend.GetRange(0, divider.Count);
            while(true) {
                remainderOfDivision = deleteFirstZeros(xor(remainderOfDivision, divider));
                if(nextIndexOfDevident + (divider.Count - remainderOfDivision.Count) > dividend.Count) {
                    remainderOfDivision.AddRange(dividend.GetRange(nextIndexOfDevident, dividend.Count - nextIndexOfDevident));
                    return remainderOfDivision;
                }
                int amountOfSymbolsToAdd = divider.Count - remainderOfDivision.Count;
                remainderOfDivision.AddRange(dividend.GetRange(nextIndexOfDevident, amountOfSymbolsToAdd));
                nextIndexOfDevident += amountOfSymbolsToAdd;
            }
        }

        private static List<bool> deleteFirstZeros(List<bool> message) {
            List<bool> validatedMessage = message.Select(i => i).ToList();
            for(int i = 0; i < message.Count; i++) {
                if(!message[i]) {
                    validatedMessage.Remove(false);
                } else {
                    break;
                }
            }
            return validatedMessage;
        }

        public static string DecodeAbramsonaCode(List<bool> dataToDecode, List<bool> polynomial, ListBox listBox) {
            listBox.Items.Add("Encoded: " + ToString(dataToDecode));
            listBox.Items.Add("Polynomial: " + ToString(polynomial));
            List<bool> formingPolynomial = GetFormingPolynomial(polynomial);
            listBox.Items.Add("Forming polinomial: " + ToString(formingPolynomial));
            int weightOfError = 0, amountOfShifts = 0;
            List<bool> remainderOfDivision = devide(dataToDecode, formingPolynomial, false);
            while((weightOfError = GetWeightOfError(remainderOfDivision)) != 0) {
                listBox.Items.Add("Error has been found!");
                listBox.Items.Add("Weight of error: " + weightOfError);
                if(weightOfError == 1 || (weightOfError == 2 && deleteFirstZeros(remainderOfDivision)[0] && deleteFirstZeros(remainderOfDivision)[1])) {
                    dataToDecode = xor(dataToDecode, remainderOfDivision);
                    for(int i = 0; i < amountOfShifts; i++) {
                        dataToDecode = ShiftCombinationToRight(dataToDecode);
                    }
                    break;
                } else {
                    dataToDecode = ShiftCombinationToLeft(dataToDecode);
                    listBox.Items.Add("Shifted combination: " + ToString(dataToDecode));
                    listBox.Items.Add("-------------------------------------------------------------------------");
                    remainderOfDivision = devide(dataToDecode, formingPolynomial, false);
                    amountOfShifts++;
                }
            }
            for(int i = 0; i < 5; i++) {
                dataToDecode.RemoveAt(dataToDecode.Count - 1);
            }
            listBox.Items.Add("-------------------------------------------------------------------------");
            listBox.Items.Add("Decoded combination: " + ToString(dataToDecode));
            listBox.Items.Add("-------------------------------------------------------------------------");
            return ToString(dataToDecode);
        }

        private static int GetWeightOfError(List<bool> remainderOfDivision) {
            int weightOfError = 0;
            foreach(bool digit in remainderOfDivision) {
                if(digit)
                    weightOfError++;
            }
            return weightOfError;
        }

        private static List<bool> ShiftCombinationToLeft(List<bool> combination) {
            combination.Add(combination[0]);
            combination.Remove(combination[0]);
            return combination;
        }

        private static List<bool> ShiftCombinationToRight(List<bool> combination) {
            List<bool> shifterCombination = new List<bool>(combination.Count);
            shifterCombination.Add(combination[combination.Count - 1]);
            for(int i = 0; i < combination.Count - 1; i++) {
                shifterCombination.Add(combination[i]);
            }
            return shifterCombination;
        }
    }
}
