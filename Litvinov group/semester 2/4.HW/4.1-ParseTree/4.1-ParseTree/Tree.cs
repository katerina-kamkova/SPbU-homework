using static System.Console;

namespace ParseTree
{
    /// <summary>
    /// Parse tree
    /// </summary>
    public class Tree : ITree
    {
        /// <summary>
        /// Pointer to the top element
        /// </summary>
        private Operation head;

        /// <summary>
        /// Fill the tree with expression
        /// </summary>
        /// <param name="input">Expression</param>
        public void FillTree(string[] input)
        {
            var fillTreeClass = new FillTreeClass(input);
            fillTreeClass.FillTreeFunc(ref head);
        }

        /// <summary>
        /// Print the expression by tree
        /// </summary>
        public void PrintTree()
        {
            Write("The expression: ");

            head.PrintChar();
        }

        /// <summary>
        /// Calculate the answer
        /// </summary>
        /// <returns>Answer</returns>
        public int CalculateAnswer() => head.Calculate();

        /// <summary>
        /// Class that provides all necessary for FillTree functions
        /// </summary>
        private class FillTreeClass
        {
            private int index;
            private string[] input;

            public FillTreeClass(string[] input)
            {
                this.input = input;
            }

            /// <summary>
            /// Fill the tree with expression
            /// </summary>
            public void FillTreeFunc(ref Operation head)
            {
                FixStr(input);
                AddNode(ref head, true, ref head);
            }

            /// <summary>
            /// To free input expression from brackets
            /// </summary>
            private void FixStr(string[] input)
            {
                int length = input.Length;
                for (int i = 0; i < length; ++i)
                {
                    string str = input[i];
                    input[i] = "";
                    for (int j = 0; j < str.Length; ++j)
                    {
                        if (!Equals(str[j], '(') && !Equals(str[j], ')'))
                        {
                            input[i] += str[j];
                        }
                    }
                }
            }

            /// <summary>
            /// To define the operation and create new element for it
            /// </summary>
            private Operation DefineOperation(string operation)
            {
                switch (operation)
                {
                    case "+":
                        return new Add();
                    case "-":
                        return new Subtract();
                    case "*":
                        return new Multiply();
                    case "/":
                        return new Divide();
                    default:
                        throw new WrongInputException("Wrong expression");
                }
            }

            /// <summary>
            /// Add the node to the tree
            /// </summary>
            private void AddNode(ref Operation parent, bool child, ref Operation head)
            {
                if (!int.TryParse(input[index], out int number))
                {
                    Operation current;
                    if (head == null)
                    {
                        head = DefineOperation(input[index]);
                        current = head;
                    }
                    else
                    {
                        if (child)
                        {
                            parent.Left = DefineOperation(input[index]);
                            current = (Operation)parent.Left;
                        }
                        else
                        {
                            parent.Right = DefineOperation(input[index]);
                            current = (Operation)parent.Right;
                        }
                    }

                    ++index;
                    AddNode(ref current, true, ref head);
                    AddNode(ref current, false, ref head);
                }
                else
                {
                    if (head == null)
                    {
                        throw new WrongInputException("Wrong expression");
                    }
                    else if (child)
                    {
                        parent.Left = new Number(number);
                    }
                    else
                    {
                        parent.Right = new Number(number);
                    }
                    ++index;
                }
            }
        }
    }
}