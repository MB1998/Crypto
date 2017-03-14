using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Crypto
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxCodeGray.Text))
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxCodeGray.Text))
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

        private void button4_Click(object sender, EventArgs e)
        {
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

        private void button3_Click(object sender, EventArgs e)
        {
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

        private void button6_Click(object sender, EventArgs e)
        {
            var numberToEncode = textBoxDdcData.Text.Trim();
            var key = (string)comboBoxDdcKey.SelectedItem;
            if (string.IsNullOrWhiteSpace(key))
                return;

            var tempKey = key.Split('-').Select(val => Convert.ToInt32(val)).ToList();

            listBoxDdc.Items.Add($"Number {numberToEncode} will be encoded using key {key}");
            var encodedData = Crypto.EncodeBcd(numberToEncode, tempKey);
            listBoxDdc.Items.Add("");

            listBoxDdc.Items.Add("Encoded data: ");
            listBoxDdc.Items.Add(Crypto.ToString(encodedData));
            listBoxDdc.Items.Add("");

            for (int i = 0, k = 0; i < encodedData.Count; k++)
            {
                var terms = new List<string>();
                for (var j = 0; j < tempKey.Count; j++, i++)
                {
                    var term = $"{tempKey[j]} * {(encodedData[i] ? 1 : 0)}";
                    terms.Add(term);
                }
                listBoxDdc.Items.Add($"{numberToEncode[k]} = {string.Join(" + ", terms)};");
            }
            listBoxDdc.Items.Add("-------------------------------------------------------------------------");

            textBoxDdcData.Text = Crypto.ToString(encodedData);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var numberToDecode = textBoxDdcData.Text.Trim();
            var key = (string)comboBoxDdcKey.SelectedItem;
            if (string.IsNullOrWhiteSpace(key))
                return;

            var tempKey = key.Split('-').Select(val => Convert.ToInt32(val)).ToList();

            listBoxDdc.Items.Add("Data to decode: ");
            listBoxDdc.Items.Add(numberToDecode);
            listBoxDdc.Items.Add($" will be decoded using key {key};");
            var decodedNumber = Crypto.DecodedBdc(numberToDecode.Select(digit => digit == '1').ToList(), tempKey);
            listBoxDdc.Items.Add($"Decoded number is {decodedNumber}");

            textBoxDdcData.Text = decodedNumber.ToString();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            bool[,] matrix = null;
            var lines = richTextBoxEllays.Text.Split('\n');
            for (var i = 0; i < lines.Length; i++)
            {
                var items = lines[i].Split(' ');
                if (matrix == null)
                    matrix = new bool[lines.Length, items.Length];
                for (var k = 0; k < items.Length; k++)
                {
                    matrix[i, k] = items[k] == "1";
                }
            }
            if (matrix == null)
                return;

            var key = new List<bool>();
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                var line = matrix[i, 0];
                for (var k = 1; k < matrix.GetLength(1); k++)
                    line = line ^ matrix[i, k];
                key.Add(line);
            }
            for (var i = 0; i < matrix.GetLength(1); i++)
            {
                var line = matrix[0, i];
                for (var k = 1; k < matrix.GetLength(0); k++)
                    line = line ^ matrix[k, i];
                key.Add(line);
            }

            listBoxEllays.Items.Add("Keys: ");
            foreach (var b in key)
            {
                listBoxEllays.Items.Add(b ? "1" : "0");
            }

            /*var encodedData = Crypto.EncodeEllayes(matrix, listBoxEllays);
            listBoxEllays.Items.Add("Encoded matrix: ");
            for (var i = 0; i < encodedData.GetLength(0); i++)
            {
                var line = "";
                for (var j = 0; j < encodedData.GetLength(1); j++)
                {
                    line += ((encodedData[i, j] ? "1" : "0") + " ");
                    if (j == encodedData.GetLength(1) - 1)
                        line += ("| ");
                }
                listBoxEllays.Items.Add(line);
                if (i != encodedData.GetLength(0) - 1)
                    continue;
                var end = "";
                for (var j = 0; j <= encodedData.GetLength(1) + 1; j++)
                    end += ("--");
                listBoxEllays.Items.Add(end);
            }*/

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }
    }
}
