using System;
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
            var dataToEncode = textBoxCodeGray.Text.Select(digit => digit == '1').ToList();
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

            var dataToDecode = textBoxCodeGray.Text.Select(digit => digit == '1').ToList();

            listBoxCodeGray.Items.Add("Data to decode: ");
            listBoxCodeGray.Items.Add(Crypto.ToString(dataToDecode));
            listBoxCodeGray.Items.Add("");

            var decodedData = Crypto.DecodeGrey(dataToDecode, listBoxCodeGray);
            listBoxCodeGray.Items.Add("");

            listBoxCodeGray.Items.Add("Decoded data: ");
            listBoxCodeGray.Items.Add(Crypto.ToString(decodedData));
            listBoxCodeGray.Items.Add("-------------------------------------------------------------------------");

            textBoxCodeGray.Text = Crypto.ToString(decodedData);
        }
    }
}
