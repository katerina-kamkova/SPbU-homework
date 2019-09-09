using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace _6._1
{
    /// <summary>
    /// Class that provides the reaction to clicks on Calculator
    /// </summary>
    public partial class CalculatorForm : Form
    {
        private ICalculator calculator = new Calculator();
        private List<string> input = new List<string>();

        public CalculatorForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Function that describes reactions to pressing buttons 0-9 or comma
        /// Shall add this symbol in inpurOutput.Text 
        /// and create new or complement already existing number in input - list of strings
        /// </summary>
        /// <param name="button">Pressed button</param>
        private void NumberButtons(Button button)
        {
            if (inputOutput.Text == "Wrong order of elements" || inputOutput.Text == "Wrong symbol")
            {
                inputOutput.Text = "";
            }

            var size = input.Count();
            if (size > 0 && double.TryParse(input[size - 1], out var number))
            {
                input[size - 1] += button.Text;
            }
            else
            {
                input.Add(button.Text);
            }
            inputOutput.Text += button.Text;
        }

        /// <summary>
        /// Event that describes the reaction to pressing button with number or comma
        /// </summary>
        private void ClickOnNumberButtons(object sender, EventArgs e)
        {
            NumberButtons((Button)sender);
        }

        /// <summary>
        /// Funtion that describes reaction to pressing buttons with operations or brackets
        /// Shall add this symbol in inpurOutput.Text 
        /// and create new string in input - array of strings
        /// </summary>
        /// <param name="button">Pressed button</param>
        private void OtherButtons(Button button)
        {
            if (inputOutput.Text == "Wrong order of elements" || inputOutput.Text == "Wrong symbol")
            {
                inputOutput.Text = "";
            }

            input.Add(button.Text);
            inputOutput.Text += button.Text;
        }

        /// <summary>
        /// Event that describes the reaction to pressing (, ), +, -, *, /
        /// </summary>
        private void ClickOnOperationsOrBrackets(object sender, EventArgs e)
        {
            OtherButtons((Button)sender);
        }

        /// <summary>
        /// Event that describes the reaction to pressing Del
        /// Shall delete last symbol from inputOutput.Text and input
        /// </summary>
        private void ClickOnDel(object sender, EventArgs e)
        {
            var size = input.Count();
            if (size == 0)
            {
                return;
            }

            if (input[size - 1] == "-0" || !(double.TryParse(input[size - 1], out var number) && input[size - 1].Length != 1))
            {
                input.Remove(input[size - 1]);
            }
            else if (!Equals(input[size - 1][input[size - 1].Length - 1], ","))
            {
                string newStr = null;
                for (int i = 0; i < input[size - 1].Length - 1; ++i)
                {
                    newStr += input[size - 1][i];
                }
                input[size - 1] = newStr;
            }

            string newString = null;
            for (int i = 0; i < inputOutput.Text.Length - 1; ++i)
            {
                newString += inputOutput.Text[i];
            }
            inputOutput.Text = newString;
        }

        /// <summary>
        /// Event that describes the reaction to pressing Equals
        /// Shall calculate the input expression and show the answer in inputOutput.Text
        /// </summary>
        private void ClickOnEquals(object sender, EventArgs e)
        {
            try
            {
                inputOutput.Text = Convert.ToString(calculator.Calculate(input));
            }
            catch (WrongExpressionException exception)
            {
                inputOutput.Text = exception.Message;
            }

            input = new List<string>();
            input.Add(inputOutput.Text);
        }

        /// <summary>
        /// Event that describes the reaction to pressing ChangeSign
        /// </summary>
        private void ClickOnChangeSign(object sender, EventArgs e)
        {
            if (inputOutput.Text == "Wrong order of elements" || inputOutput.Text == "Wrong symbol")
            {
                inputOutput.Text = "";
            }

            var size = input.Count();
            if (size != 0 && double.TryParse(input[size - 1], out var number))
            {
                input[size - 1] = "-" + input[size - 1];

                string newStr = null;
                for (int i = 0; i < inputOutput.Text.Length; i++)
                {
                    if (i == inputOutput.Text.Length - input[size - 1].Length + 1)
                    {
                        newStr += "-";
                    }
                    newStr += inputOutput.Text[i];
                }
                inputOutput.Text = newStr;
            }
            else
            {
                input.Add("-0");
                inputOutput.Text += "-";
            }
        }
    }
}