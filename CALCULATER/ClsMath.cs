using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static CALCULATER.Form1;

namespace MATH
{
    public class ClsMath
    {
        public double? number1 = null;
        public double? number2 = null;
        public double? result = null;
        public string type = string.Empty;
        public string speartype = string.Empty;
        public string StrMathMatical = string.Empty;

        struct STtermmath
        {
            public double? number;
            public string optype;
        }

        STtermmath[] ArrtermMath = new STtermmath[100];
        STtermmath termmath = new STtermmath();

        public enum ENSwitchnum
        {
            num1,
            num2
        }
        public enum ENSwitchType
        {
            off,
            on
        }

        public ENSwitchType switchType = ENSwitchType.off;
        ENSwitchnum roundnum = ENSwitchnum.num1;



        short count = 0;

        public void FullArray()
        {
            ArrtermMath[count] = termmath;
            count++;
            termmath.number = null;
            termmath.optype = string.Empty;
        }


        public void ProccessOP(string op)
        {
            if (string.IsNullOrEmpty(type))
            {
                type = op;
            }
            else
            {
                speartype = op;
            }
        }


        public void ProcessKernel()
        {
           Calculate();
           number1 = result;
           number2 = null;
           type = string.Empty;
           ProccessOP(speartype);
        }

        public void ProccessInputs()
        {
            byte counter = 0;

            while ((!string.IsNullOrEmpty(ArrtermMath[counter].optype)) || (ArrtermMath[counter].number != null))
            {
                ProccessOP(ArrtermMath[counter].optype);

                switch (roundnum)
                {
                    case ENSwitchnum.num1:
                        number1 = ArrtermMath[counter].number;
                        roundnum = ENSwitchnum.num2;
                        counter++;
                        break;

                    case ENSwitchnum.num2:
                        number2 = ArrtermMath[counter].number;
                        counter++;
                        break;
                }

                ProcessKernel();
            }
        }

        public void CleanVariableToContionusApp()
        {
            count = 0;
            STtermmath[] NewArrtermMath = new STtermmath[100];
            ArrtermMath = NewArrtermMath;
            number2 = null;
            result = null;
        }

        public bool ExistsOPIs()
        {
            return (this.StrMathMatical.Contains("+") || this.StrMathMatical.Contains("-") ||
                        this.StrMathMatical.Contains("x") || this.StrMathMatical.Contains("/"));
        }

        public int FindFirstOP()
        {
            List<int> finds = new List<int>();

            if (ExistsOPIs())
            {
                if (this.StrMathMatical.Contains("+"))
                {
                    finds.Add(this.StrMathMatical.IndexOf('+'));
                }
                if (this.StrMathMatical.Contains("-"))
                {
                    finds.Add(this.StrMathMatical.IndexOf('-'));
                }
                if (this.StrMathMatical.Contains("/"))
                {
                    finds.Add(this.StrMathMatical.IndexOf('/'));
                }
                if (this.StrMathMatical.Contains("x"))
                {
                    finds.Add(this.StrMathMatical.IndexOf('x'));
                }
                return finds.Min();
            }
            else
            {
                return -1;
            }
        }

        public string SplitString(int index, int endindex)
        {
            string str = string.Empty;
            string newmathmatical = string.Empty;
            if (endindex <= 0)
            {
                str = this.StrMathMatical.Substring(0, this.StrMathMatical.Length);
                this.StrMathMatical = "";
            }
            else
            {
                if (endindex == this.StrMathMatical.Length || this.StrMathMatical.Length == 0)
                {
                    str = this.StrMathMatical;
                    this.StrMathMatical = "";
                }
                else
                {
                    str = this.StrMathMatical.Substring(index, endindex);
                    newmathmatical = this.StrMathMatical.Substring(endindex, (this.StrMathMatical.Length - endindex));
                    this.StrMathMatical = newmathmatical;
                }
            }
            return str;
        }


        public void Calculate()
        {
            switch (type)
            {
                case "+":
                    result = number1 + number2;
                    break;

                case "-":
                    result = number1 - number2;
                    break;

                case "x":
                    result = number1 * number2;
                    break;

                case "/":
                    result = number1 / number2;
                    break;
            }
        }
    }
}