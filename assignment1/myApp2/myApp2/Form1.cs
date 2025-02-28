using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace myApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            double n1, n2, res = 0;
            if (double.TryParse(textBoxNum1.Text, out n1) && double.TryParse(textBoxNum2.Text, out n2))
            {
                if (radioButtonAdd.Checked)
                {
                    res = n1 + n2;
                }
                else if (radioButtonSubtract.Checked)
                {
                    res = n1 - n2;
                }
                else if (radioButtonMultiply.Checked)
                {
                    res = n1 * n2;
                }
                else if (radioButtonDivide.Checked)
                {
                    if (n2 != 0)
                        res = n1 / n2;
                    else
                    {
                        MessageBox.Show("除数不能为零", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                labelResult.Text = "结果: " + res.ToString();
            }
            else
            {
                MessageBox.Show("请输入有效的数字", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
