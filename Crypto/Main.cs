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
                var items = lines[i].Split(' ');
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
            if(q == 0 || n == 0 || alphabet.Count < 2 || codingType == String.Empty) {
                PrimaryNonBinaryCodesListBox.Items.Add("Please, enter valid data!");
            } else {
                List<string> words = Crypto.getCodeCombinations(alphabet, q, n, codingType, PrimaryNonBinaryCodesListBox);
                PrimaryNonBinaryCodesListBox.Items.Add($"Combinations: {String.Join(", ", words)}");
            }
        }
    }
}
