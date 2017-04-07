using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Crypto {
    public partial class Main : Form {
        public Main() {
            InitializeComponent();
        }

        private void GreyEncodeButton_Click(object sender, EventArgs e) {
            if(string.IsNullOrWhiteSpace(textBoxCodeGray.Text))
                return;

            listBoxCodeGray.Items.Add("Binary code of data: ");
            var dataToEncode = textBoxCodeGray.Text.Trim().Select(digit => digit == '1').ToList();
            listBoxCodeGray.Items.Add(Crypto.ToString(dataToEncode));
            listBoxCodeGray.Items.Add("");

            var encodedData = Crypto.EncodeGrey(dataToEncode, listBoxCodeGray);
            listBoxCodeGray.Items.Add("");

            listBoxCodeGray.Items.Add("Encoded data: ");
            listBoxCodeGray.Items.Add(Crypto.ToString(encodedData));
            listBoxCodeGray.Items.Add("-------------------------------------------------------------------------");
            textBoxCodeGray.Text = Crypto.ToString(encodedData);
        }

        private void GreyDecodeButton_Click(object sender, EventArgs e) {
            if(string.IsNullOrWhiteSpace(textBoxCodeGray.Text))
                return;

            var dataToDecode = textBoxCodeGray.Text.Trim().Select(digit => digit == '1').ToList();

            listBoxCodeGray.Items.Add("Data to decode: ");
            listBoxCodeGray.Items.Add(textBoxCodeGray.Text);
            listBoxCodeGray.Items.Add("");

            var decodedData = Crypto.DecodeGrey(dataToDecode, listBoxCodeGray);
            listBoxCodeGray.Items.Add("");

            listBoxCodeGray.Items.Add("Decoded data: ");
            listBoxCodeGray.Items.Add(Crypto.ToString(decodedData));
            listBoxCodeGray.Items.Add("-------------------------------------------------------------------------");

            textBoxCodeGray.Text = Crypto.ToString(decodedData);
        }

        private void BerderEncodeButton_Click(object sender, EventArgs e) {
            listBoxBerger.Items.Add("Binary code of data: ");
            listBoxBerger.Items.Add(textBoxBerger.Text);
            listBoxBerger.Items.Add("");

            var encodedData = Crypto.EncodeBerger(textBoxBerger.Text.Trim().Select(digit => digit == '1').ToList(), listBoxBerger);
            listBoxBerger.Items.Add("");

            listBoxBerger.Items.Add("Encoded data: ");
            listBoxBerger.Items.Add(Crypto.ToString(encodedData));
            listBoxBerger.Items.Add("-------------------------------------------------------------------------");

            textBoxBerger.Text = Crypto.ToString(encodedData);
        }

        private void BerderDecodeButton_Click(object sender, EventArgs e) {
            listBoxBerger.Items.Add("Data to decode: ");
            listBoxBerger.Items.Add(textBoxBerger.Text);
            listBoxBerger.Items.Add("");

            var decodedData = Crypto.DecodeBerger(textBoxBerger.Text.Trim().Select(digit => digit == '1').ToList(), listBoxBerger);
            listBoxBerger.Items.Add("");

            listBoxBerger.Items.Add("Decoded data: ");
            listBoxBerger.Items.Add(Crypto.ToString(decodedData));
            listBoxBerger.Items.Add("-------------------------------------------------------------------------");

            textBoxBerger.Text = Crypto.ToString(decodedData);
        }

        private void BCDEncodeButton_Click(object sender, EventArgs e) {
            var numberToEncode = textBoxDdcData.Text.Trim();
            var key = (string)comboBoxDdcKey.SelectedItem;
            if(string.IsNullOrWhiteSpace(key))
                return;

            var tempKey = key.Split('-').Select(val => Convert.ToInt32(val)).ToList();

            listBoxDdc.Items.Add($"Number {numberToEncode} will be encoded using key {key}");
            var encodedData = Crypto.EncodeBcd(numberToEncode, tempKey);
            listBoxDdc.Items.Add("");

            listBoxDdc.Items.Add("Encoded data: ");
            listBoxDdc.Items.Add(Crypto.ToString(encodedData));
            listBoxDdc.Items.Add("");

            for(int i = 0, k = 0; i < encodedData.Count; k++) {
                var terms = new List<string>();
                for(var j = 0; j < tempKey.Count; j++, i++) {
                    var term = $"{tempKey[j]} * {(encodedData[i] ? 1 : 0)}";
                    terms.Add(term);
                }
                listBoxDdc.Items.Add($"{numberToEncode[k]} = {string.Join(" + ", terms)};");
            }
            listBoxDdc.Items.Add("-------------------------------------------------------------------------");

            textBoxDdcData.Text = Crypto.ToString(encodedData);
        }

        private void BCDDecodeButton_Click(object sender, EventArgs e) {
            var numberToDecode = textBoxDdcData.Text.Trim();
            var key = (string)comboBoxDdcKey.SelectedItem;
            if(string.IsNullOrWhiteSpace(key))
                return;

            var tempKey = key.Split('-').Select(val => Convert.ToInt32(val)).ToList();

            listBoxDdc.Items.Add("Data to decode: ");
            listBoxDdc.Items.Add(numberToDecode);
            listBoxDdc.Items.Add($" will be decoded using key {key};");
            var decodedNumber = Crypto.DecodedBdc(numberToDecode.Select(digit => digit == '1').ToList(), tempKey);
            listBoxDdc.Items.Add($"Decoded number is {decodedNumber}");

            textBoxDdcData.Text = decodedNumber.ToString();
        }

        private void EllaysEncodeButton_Click(object sender, EventArgs e) {
            bool[,] matrix = null;
            var lines = richTextBoxEllays.Text.Split('\n');
            for(var i = 0; i < lines.Length; i++) {
                var items = lines[i].Trim().Split(' ');
                if(matrix == null)
                    matrix = new bool[lines.Length, items.Length];
                for(var k = 0; k < items.Length; k++) {
                    matrix[i, k] = items[k] == "1";
                }
            }
            if(matrix == null)
                return;

            var encodedData = Crypto.EncodeEllayes(matrix, null);
            richTextBoxEllays.Clear();
            listBoxEllays.Items.Add("Encoded matrix: ");
            for(var i = 0; i < encodedData.GetLength(0); i++) {
                var line = "";
                for(var j = 0; j < encodedData.GetLength(1); j++) {
                    line += ((encodedData[i, j] ? "1" : "0") + " ");
                    if(j == encodedData.GetLength(1) - 2)
                        line += ("| ");
                }
                listBoxEllays.Items.Add(line);
                richTextBoxEllays.Text += line + '\n';
                if(i == encodedData.GetLength(0) - 2) {
                    line = "";
                    for(var j = 0; j <= encodedData.GetLength(1) + 1; j++)
                        line += ("--");
                    listBoxEllays.Items.Add(line);
                    richTextBoxEllays.Text += line + '\n';
                }
            }
        }

        private void EllaysDecodeButton_Click(object sender, EventArgs e) {
            listBoxEllays.Items.Clear();
            bool[,] encodedMatrix = null;
            var lines = richTextBoxEllays.Text.Trim().Split('\n');
            for(int i = 0, j = 0; i < lines.Length; i++, j++) {
                if(lines[i].Contains("--")) {
                    j--;
                    continue;
                }
                var items = lines[i].Replace("| ", string.Empty).Trim().Split(' ');
                if(encodedMatrix == null)
                    encodedMatrix = new bool[lines.Length - 1, items.Length];
                for(var k = 0; k < items.Length; k++) {
                    encodedMatrix[j, k] = items[k] == "1";
                }
            }
            if(encodedMatrix == null)
                return;

            var fixedMatrix = Crypto.FixMistakesEllayes(encodedMatrix, listBoxEllays);
            richTextBoxEllays.Clear();
            listBoxEllays.Items.Add("\nFixed matrix: ");
            for(var i = 0; i < fixedMatrix.GetLength(0); i++) {
                var line = "";
                for(var j = 0; j < fixedMatrix.GetLength(1); j++) {
                    line += ((fixedMatrix[i, j] ? "1" : "0") + " ");
                    if(j == fixedMatrix.GetLength(1) - 2)
                        line += ("| ");
                }
                listBoxEllays.Items.Add(line);
                richTextBoxEllays.Text += line + '\n';
                if(i == fixedMatrix.GetLength(0) - 2) {
                    line = "";
                    for(var j = 0; j <= fixedMatrix.GetLength(1) + 1; j++)
                        line += ("--");
                    listBoxEllays.Items.Add(line);
                    richTextBoxEllays.Text += line + '\n';
                }
            }
        }

        private void PrimaryNonBinaryCodesEnodeButton_Click(object sender, EventArgs e) {
            int q = Int32.Parse(PrimaryNonBinaryCodesQTextBox.Text.Trim());
            int n = Int32.Parse(PrimaryNonBinaryCodesNTextBox.Text.Trim());
            List<char> alphabet = PrimaryNonBinaryCodesAlphabetTextBox.Text.ToCharArray().ToArray().ToList();
            string codingType = PrimaryNonBinaryCodesTypeCombobox.Text;
            if(q == 0 || n == 0 || alphabet.Count < 2 || codingType == string.Empty) {
                PrimaryNonBinaryCodesListBox.Items.Add("Please, enter valid data!");
            } else {
                List<string> words = Crypto.GetCodeCombinations(alphabet, q, n, codingType, PrimaryNonBinaryCodesListBox);
                PrimaryNonBinaryCodesListBox.Items.Add($"Combinations: {string.Join(", ", words)}");
            }
            PrimaryNonBinaryCodesListBox.Items.Add("-------------------------------------------------------------------------");
        }

        private void PrimaryNonBinaryCodesCheckButton_Click(object sender, EventArgs e) {
            int q = Int32.Parse(PrimaryNonBinaryCodesQTextBox.Text.Trim());
            int n = Int32.Parse(PrimaryNonBinaryCodesNTextBox.Text.Trim());
            List<char> alphabet = PrimaryNonBinaryCodesAlphabetTextBox.Text.ToCharArray().ToArray().ToList();
            string codingType = PrimaryNonBinaryCodesTypeCombobox.Text;
            String combinationToCheck = PrimaryNonBinaryCodesCombinationToCheckTextBox.Text;
            if(q == 0 || n == 0 || alphabet.Count < 2 || codingType == String.Empty) {
                PrimaryNonBinaryCodesListBox.Items.Add("Please, enter valid data!");
            } else {
                List<string> words = Crypto.GetCodeCombinations(alphabet, q, n, codingType, PrimaryNonBinaryCodesListBox);
                PrimaryNonBinaryCodesListBox.Items.Add($"Combinations: {string.Join(", ", words)}");
                String correct = words.Contains(combinationToCheck) ? string.Empty : string.Copy("not ");
                PrimaryNonBinaryCodesListBox.Items.Add($"Combination {combinationToCheck} is {correct}correct for this code.");
            }
            PrimaryNonBinaryCodesListBox.Items.Add("-------------------------------------------------------------------------");
        }

        private void CodeWithModuleQTestEncodeButton_Click(object sender, EventArgs e) {
            int q = Int32.Parse(CodeWithModuleQTestQTextBox.Text.Trim());
            List<char> combination = CodeWithModuleQTestCombinationTextBox.Text.ToCharArray().ToList();
            List<char> encodedCombination = Crypto.CodeWithModuleQTestEncode(q, combination, CodeWithModuleQTestListBox);
            CodeWithModuleQTestListBox.Items.Add($"Encoded combination: {string.Join("", encodedCombination)}");
            CodeWithModuleQTestListBox.Items.Add("-------------------------------------------------------------------------");
        }

        private void CodeWithModuleQTestCheckButton_Click(object sender, EventArgs e) {
            int q = Int32.Parse(CodeWithModuleQTestQTextBox.Text.Trim());
            List<char> combination = CodeWithModuleQTestCombinationTextBox.Text.ToCharArray().ToList();
            bool combinationIsValid = Crypto.CodeWithModuleQTestCheckValidCombination(q, combination, CodeWithModuleQTestListBox);
            string correct = combinationIsValid ? string.Empty : string.Copy("not ");
            CodeWithModuleQTestListBox.Items.Add($"Combination {string.Join("", combination)} is {correct}correct for this q : {q}.");
            CodeWithModuleQTestListBox.Items.Add("-------------------------------------------------------------------------");
        }

        private void CodeWithSimpleRepetitionEncodeButton_Click(object sender, EventArgs e) {
            string combination = CodeWithSimpleRepetitionCombinationTextBox.Text;
            string encodedCombination = Crypto.CodeWithSimpleRepetitionEncode(combination);
            Clipboard.SetText(encodedCombination);
            CodeWithSimpleRepetitionListBox.Items.Add($"Encoded combination: {encodedCombination}");
            CodeWithSimpleRepetitionListBox.Items.Add("-------------------------------------------------------------------------");
        }

        private void CodeWithSimpleRepetitionDecodeButton_Click(object sender, EventArgs e) {
            string combination = CodeWithSimpleRepetitionCombinationTextBox.Text;
            string decodedCombination = string.Empty;
            decodedCombination = Crypto.CodeWithSimpleRepetitionDecode(combination);
            Clipboard.SetText(decodedCombination);
            CodeWithSimpleRepetitionListBox.Items.Add($"Decoded combination: {decodedCombination}");
            CodeWithSimpleRepetitionListBox.Items.Add("-------------------------------------------------------------------------");
        }

        private void IterativeСodeEncodeButton_Click(object sender, EventArgs e) {
            int q;
            if (!Int32.TryParse(IterativeСodeQTextBox.Text, out q)) {
                DisplayErrorMessage(IterativeСodeListBox,
                    new List<string> { "Please, enter valid data for q!\n It can be only number!" });
                return;
            }
            string message = IterativeСodeMessageTextBox.Text;
            char[,] matrix = getCharMatrix(message);
            if(matrix == null) {
                DisplayErrorMessage(IterativeСodeListBox, new List<string> { "Please, enter valid data into message!", "It must be only numbers and multiplying k by inself must be lenght of message" });
                return;
            }
            char[,] encodedMatrix = Crypto.IterativeСodeEncode(matrix, q);
            IterativeСodeListBox.Items.Add("Encoded matrix: ");
            DisplayMatrixWithLastAdditionalSymbol(IterativeСodeListBox, encodedMatrix);
            IterativeСodeListBox.Items.Add($"Encoded message: {GetMessageFromMatrix(encodedMatrix)}");
            IterativeСodeListBox.Items.Add("--------------------------------------------------------------------------------------------------");
        }

        private void IterativeСodeFixButton_Click(object sender, EventArgs e) {
            int q;
            if(!Int32.TryParse(IterativeСodeQTextBox.Text, out q)) {
                DisplayErrorMessage(IterativeСodeListBox,
                    new List<string> { "Please, enter valid data for q!\n It can be only number!" });
                return;
            }
            string message = IterativeСodeMessageTextBox.Text;
            char[,] matrix = getCharMatrix(message);
            if(matrix == null) {
                DisplayErrorMessage(IterativeСodeListBox, new List<string> { "Please, enter valid data into message!", "It must be only numbers and multiplying k by inself must be lenght of message" });
                return;
            }
            Crypto.IterativeСodeFixMistakes(matrix, q, IterativeСodeListBox);
            IterativeСodeListBox.Items.Add("--------------------------------------------------------------------------------------------------");
        }

        private void AmountOfInformationFindButton_Click(object sender, EventArgs e) {
            List<string> probabilitiesOfMessages = getProbabilitiesOfMessagesAmountOfInformation(AmountOfInformationPXiTextBox, AmountOfInformationListBox, ';');
            List<int> ensembleOrder = getEnsembleOrder();
            int amountOfInformation = Crypto.getAmountOfInformation(probabilitiesOfMessages, ensembleOrder);
            AmountOfInformationListBox.Items.Add($"Amount of infromation for ensemle A with specified probabilities is {amountOfInformation} bits");
        }

        private void UnconditionalAndMaximumEntropyFindButton_Click(object sender, EventArgs e) {
            List<string> probabilitiesOfMessages = getProbabilitiesOfMessagesAmountOfInformation(UnconditionalAndMaximumEntropyPXiTextBox, UnconditionalAndMaximumEntropyListBox, ';');
            double unconditionalEntropy = Crypto.getUnconditionalEntropy(probabilitiesOfMessages);
            UnconditionalAndMaximumEntropyListBox.Items.Add($"Unconditional Entropy for this source: {unconditionalEntropy}");
            double maximumEntropy = Crypto.getMaximumEntropy(probabilitiesOfMessages);
            UnconditionalAndMaximumEntropyListBox.Items.Add($"Maximum Entropy for this source: {maximumEntropy}");
        }

        private void ConditionalEntropyFindButton_Click(object sender, EventArgs e) {
            double[,] conditionalProbabilitiesMatrix = getConditionalProbabilitiesMatrix();
            if(conditionalProbabilitiesMatrix == null)
                return;
            List<double> probabilitiesOfMessages = getProbabilitiesOfMessagesAmountOfInformationDouble(ConditionalEntropyMessagesProbabilitiesTextBox, ConditionalEntropyListBox, ';');
            double conditionalEntropy = Crypto.getConditionalEntropy(conditionalProbabilitiesMatrix);
            ConditionalEntropyListBox.Items.Add($"Conditional Entropy for this source: {conditionalEntropy}");
        }

        private void ShannonFanoCodeEncodeButton_Click(object sender, EventArgs e) {
            List<double> probabilitiesOfMessages = getProbabilitiesOfMessagesAmountOfInformationDouble(ShannonFanoCodePXiTextBox, ShannonFanoCodeListBox, ';');
            if(probabilitiesOfMessages == null)
                return;
            List<string> encodedProbabilities = Crypto.EncodeProbabilities(probabilitiesOfMessages, null);
            for(int i = 0; i < encodedProbabilities.Count; i++) {
                ShannonFanoCodeListBox.Items.Add($"P{i} = {probabilitiesOfMessages[i]} = {encodedProbabilities[i]};");
            }
            ShannonFanoCodeListBox.Items.Add("---------------------------------------------------------------------");
        }

        private double[,] getConditionalProbabilitiesMatrix() {
            double[,] conditionalProbabilitiesMatrix = null;
            try {
                var lines = ConditionalEntropyProbabilityMatrixRichBox.Text.Trim().Split('\n');
                for(int i = 0; i < lines.Length; i++) {
                    var items = lines[i].Trim().Split(' ');
                    if(conditionalProbabilitiesMatrix == null)
                        conditionalProbabilitiesMatrix = new double[lines.Length, items.Length];
                    for(var k = 0; k < items.Length; k++) {
                        conditionalProbabilitiesMatrix[i, k] = double.Parse(items[k]);
                    }
                }
                return conditionalProbabilitiesMatrix;
            } catch(Exception e) {
                DisplayErrorMessage(ConditionalEntropyListBox, new List<string> { "Conditional probabilities matrix is not correct. Please, write matrix in input field: use new line for each new row, use whitespace to separate elements in one row." });
                return null;
            }
        }

        private List<int> getEnsembleOrder() {
            try {
                List<int> ensembleOrder = (new List<string>(AmountOfInformationATextBox.Text.Replace(" ", String.Empty).Split(';'))).Select(x => Int32.Parse(x.Substring(1, 1))).ToList();
                return ensembleOrder;
            } catch(Exception ex) {
                DisplayErrorMessage(AmountOfInformationListBox, new List<string> { "Ensemble isn't correct! Please, specify it like order of messages. Example: \'X1;X2;X3;X9;X8;X5;X5;X4\'" });
                return null;
            }
        }

        private List<string> getProbabilitiesOfMessagesAmountOfInformation(TextBox sourceTextBox, ListBox listBoxForErrorsDisplay, char separator) {
            try {
                List<string> probabilitiesOfMessages = new List<string>(sourceTextBox.Text.Replace(" ", String.Empty).Split(separator));
                return probabilitiesOfMessages;
            } catch(Exception ex) {
                DisplayErrorMessage(listBoxForErrorsDisplay, new List<string> { "Probabilities for messages P(Xi) aren't correct. Please, write numbers separeted by \'" + separator + "\'. You can also use fractions in format: 1/8." });
                return null;
            }
        }

        private List<double> getProbabilitiesOfMessagesAmountOfInformationDouble(TextBox sourceTextBox, ListBox listBoxForErrorsDisplay, char separator) {
            try {
                List<double> probabilitiesOfMessages = new List<string>(sourceTextBox.Text.Replace(" ", String.Empty).Split(separator)).Select(x => Double.Parse(x)).ToList();
                return probabilitiesOfMessages;
            } catch(Exception ex) {
                DisplayErrorMessage(listBoxForErrorsDisplay, new List<string> { "Probabilities for messages P(Xi) aren't correct. Please, write numbers separeted by \'" + separator + "\'." });
                return null;
            }
        }

        private void DisplayErrorMessage(ListBox listBox, List<string> errorMessages) {
            foreach(string errorMessage in errorMessages) {
                listBox.Items.Add(errorMessage);
            }
            listBox.Items.Add("--------------------------------------------------------------------------------------------------");
        }

        private int[,] GetMatrix(string message) {
            try {
                int columnsAmount = (int)Math.Sqrt(message.Length), rowsAmount = (int)Math.Sqrt(message.Length);
                int[,] matrix = new int[columnsAmount, rowsAmount];
                for(int i = 0; i < rowsAmount; i++) {
                    for(int j = 0; j < columnsAmount; j++) {
                        matrix[i, j] = Int32.Parse(message[i * columnsAmount + j].ToString());
                    }
                }
                return matrix;
            } catch(Exception e) {
                return null;
            }
        }

        private char[,] getCharMatrix(String message) {
            try {
                int columnsAmount = (int)Math.Sqrt(message.Length), rowsAmount = (int)Math.Sqrt(message.Length);
                char[,] matrix = new char[columnsAmount, rowsAmount];
                for (int i = 0; i < rowsAmount; i++) {
                    for (int j = 0; j < columnsAmount; j++) {
                        matrix[i, j] = (char)message[i * columnsAmount + j];
                    }
                }
                return matrix;
            } catch (Exception e) {
                return null;
            }
        }

        private void DisplayMatrixWithLastAdditionalSymbol(ListBox listBox, int[,] matrix) {
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

        private void DisplayMatrixWithLastAdditionalSymbol(ListBox listBox, char[,] matrix) {
            for (var i = 0; i < matrix.GetLength(0); i++) {
                var line = "";
                for (var j = 0; j < matrix.GetLength(1); j++) {
                    line += matrix[i, j] + "  ";
                    if (j == matrix.GetLength(1) - 2)
                        line += ("| ");
                }
                listBox.Items.Add(line);
                if (i == matrix.GetLength(0) - 2) {
                    line = "";
                    for (var j = 0; j <= matrix.GetLength(1) + 1; j++)
                        line += ("--");
                    listBox.Items.Add(line);
                }
            }
        }

        private string GetMessageFromMatrix(int[,] matrix) {
            string message = "";
            for(int i = 0; i < matrix.GetLength(0); i++) {
                for(int j = 0; j < matrix.GetLength(1); j++) {
                    message += matrix[i, j].ToString();
                }
            }
            return message;
        }

        private string GetMessageFromMatrix(char[,] matrix) {
            string message = "";
            for (int i = 0; i < matrix.GetLength(0); i++) {
                for (int j = 0; j < matrix.GetLength(1); j++) {
                    message += matrix[i, j].ToString();
                }
            }
            return message;
        }
    }
}
