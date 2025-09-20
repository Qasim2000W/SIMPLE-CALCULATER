using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using MATH;
using static MATH.ClsMath;


namespace CALCULATER
{
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        ClsMath math = new ClsMath();
        string strnumber1 = string.Empty;
        string strnumber2 = string.Empty;

        public enum EnModeApp
        {
            startly,
            continous
        }

        EnModeApp _Mode = EnModeApp.startly;

        public void APPeariend(Button button)
        {
            textBox1.Text += button.Tag.ToString();
        }

       public void GetType(Button button)
       {
            if (math.switchType == ClsMath.ENSwitchType.on)
            {
                APPeariend(button);
                math.switchType = ClsMath.ENSwitchType.off;
            }
            else
            {
                MessageBox.Show("the operation not avaliable", "wrong", MessageBoxButtons.OK);
            }
       }


        public void GetUpDataToVariableClass()
        {
            if (math.number1 == null)
            {
                math.number1 = Convert.ToDouble(strnumber1);
                math.number2 = Convert.ToDouble(strnumber2);
            }
            else
            {
                math.number2 = Convert.ToDouble(strnumber2);
            }
            strnumber1 = string.Empty;
            strnumber2 = string.Empty;
        }


        private void FullStrNumber2InCaseOpEmpty()
        {
            int IndexOp = 0;

            if (math.StrMathMatical.IndexOf("-") == 0 || math.StrMathMatical.IndexOf("+") == 0
                       || math.StrMathMatical.IndexOf("/") == 0 || math.StrMathMatical.IndexOf("x") == 0)
            {
                math.type = math.SplitString(0, 1);
                IndexOp = math.FindFirstOP();
                strnumber2 = math.SplitString(0, IndexOp);
            }
            else
            {
                IndexOp = math.FindFirstOP();
                strnumber2 = math.SplitString(0, IndexOp);
                math.type = math.SplitString(0, 1);
            }
        }

        private void FullStrNumber2()
        {
            int IndexOp = 0;

            if (math.ExistsOPIs())
            {
                if (string.IsNullOrEmpty(math.type))
                {
                    FullStrNumber2InCaseOpEmpty();
                }
                else
                {
                    IndexOp = math.FindFirstOP();
                    strnumber2 = math.SplitString(0, IndexOp);
                }

            }
            else
            {
                strnumber2 = math.SplitString(0, math.StrMathMatical.Length);
                math.StrMathMatical = "";
            }
        }

        private void FullStrNumbersInFirstSub()
        {
            int IndexOp = 0;

            math.type = math.SplitString(0, 1);

            if (math.number1 == null)
            {
                IndexOp = math.FindFirstOP();
                strnumber1 = math.SplitString(0, IndexOp);
                FullStrNumber2();
            }
            else
            {
                FullStrNumber2();
            }
        }

        private void FullStrNumbersInNormalMode()
        {
            int IndexOp = 0;

            if (math.number1 == null)
            {
                IndexOp = math.FindFirstOP();
                strnumber1 = math.SplitString(0, IndexOp);
                math.type = math.SplitString(0, 1);
                FullStrNumber2();
            }
            else
            {
                FullStrNumber2();
            }
        }

        public void ConvertSTRMathToDecimal()
        { 
            while (!string.IsNullOrEmpty(math.StrMathMatical))
            {
                if (math.StrMathMatical.IndexOf("-")==0)
                {
                    FullStrNumbersInFirstSub();
                }
                else
                {
                    FullStrNumbersInNormalMode();
                }

                GetUpDataToVariableClass();
                math.Calculate();
                math.ProcessKernel();
            }

            textBox1.Text = math.result.ToString();
        }

        private void button_Click(object sender, EventArgs e)
        {
            APPeariend((Button)sender);
            math.switchType = ClsMath.ENSwitchType.on;
        }

        
        private void buttonOP_Click(object sender, EventArgs e)
        {
          GetType((Button)sender);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            math.StrMathMatical = textBox1.Text;
            ConvertSTRMathToDecimal();
            _Mode = EnModeApp.continous;

            if (_Mode== EnModeApp.continous)
            {
                math.result = null;
                math.number1 = null;
                math.number2 = null;
                strnumber1 = string.Empty;
                strnumber1 = string.Empty;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            math.result = null;
            math.number1 = null;
            math.number2 = null;
            strnumber1 = string.Empty;
            strnumber1 = string.Empty;
            math.speartype = string.Empty;
            math.type = string.Empty;
            _Mode =EnModeApp.startly;
        }
    }
}