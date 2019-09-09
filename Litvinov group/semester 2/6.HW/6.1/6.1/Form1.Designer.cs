using System.Windows.Forms;
using System.Drawing;

namespace _6._1
{
    partial class CalculatorForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.inputOutput = new System.Windows.Forms.TextBox();
            this.comma = new System.Windows.Forms.Button();
            this.button0 = new System.Windows.Forms.Button();
            this.changeSign = new System.Windows.Forms.Button();
            this.subtract = new System.Windows.Forms.Button();
            this.equals = new System.Windows.Forms.Button();
            this.divide = new System.Windows.Forms.Button();
            this.multiply = new System.Windows.Forms.Button();
            this.delLastSymb = new System.Windows.Forms.Button();
            this.openBracket = new System.Windows.Forms.Button();
            this.closeBracket = new System.Windows.Forms.Button();
            this.add = new System.Windows.Forms.Button();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(5, 163);
            this.button1.Margin = new System.Windows.Forms.Padding(5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(70, 69);
            this.button1.TabIndex = 3;
            this.button1.Text = "1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.ClickOnNumberButtons);
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.Location = new System.Drawing.Point(85, 163);
            this.button2.Margin = new System.Windows.Forms.Padding(5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(70, 69);
            this.button2.TabIndex = 4;
            this.button2.Text = "2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.ClickOnNumberButtons);
            // 
            // button3
            // 
            this.button3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button3.Location = new System.Drawing.Point(165, 163);
            this.button3.Margin = new System.Windows.Forms.Padding(5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(70, 69);
            this.button3.TabIndex = 5;
            this.button3.Text = "3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.ClickOnNumberButtons);
            // 
            // button4
            // 
            this.button4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button4.Location = new System.Drawing.Point(5, 242);
            this.button4.Margin = new System.Windows.Forms.Padding(5);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(70, 69);
            this.button4.TabIndex = 6;
            this.button4.Text = "4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.ClickOnNumberButtons);
            // 
            // button5
            // 
            this.button5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button5.Location = new System.Drawing.Point(85, 242);
            this.button5.Margin = new System.Windows.Forms.Padding(5);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(70, 69);
            this.button5.TabIndex = 7;
            this.button5.Text = "5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.ClickOnNumberButtons);
            // 
            // button6
            // 
            this.button6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button6.Location = new System.Drawing.Point(165, 242);
            this.button6.Margin = new System.Windows.Forms.Padding(5);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(70, 69);
            this.button6.TabIndex = 8;
            this.button6.Text = "6";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.ClickOnNumberButtons);
            // 
            // button7
            // 
            this.button7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button7.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button7.Location = new System.Drawing.Point(5, 321);
            this.button7.Margin = new System.Windows.Forms.Padding(5);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(70, 69);
            this.button7.TabIndex = 9;
            this.button7.Text = "7";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.ClickOnNumberButtons);
            // 
            // button8
            // 
            this.button8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button8.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button8.Location = new System.Drawing.Point(85, 321);
            this.button8.Margin = new System.Windows.Forms.Padding(5);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(70, 69);
            this.button8.TabIndex = 10;
            this.button8.Text = "8";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.ClickOnNumberButtons);
            // 
            // button9
            // 
            this.button9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button9.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button9.Location = new System.Drawing.Point(165, 321);
            this.button9.Margin = new System.Windows.Forms.Padding(5);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(70, 69);
            this.button9.TabIndex = 11;
            this.button9.Text = "9";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.ClickOnNumberButtons);
            // 
            // inputOutput
            // 
            this.inputOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel.SetColumnSpan(this.inputOutput, 4);
            this.inputOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.inputOutput.Location = new System.Drawing.Point(5, 15);
            this.inputOutput.Margin = new System.Windows.Forms.Padding(5, 15, 5, 15);
            this.inputOutput.MaximumSize = new System.Drawing.Size(730, 100);
            this.inputOutput.Multiline = true;
            this.inputOutput.Name = "inputOutput";
            this.inputOutput.Size = new System.Drawing.Size(310, 49);
            this.inputOutput.TabIndex = 18;
            // 
            // comma
            // 
            this.comma.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comma.Font = new System.Drawing.Font("Microsoft Sans Serif", 27F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comma.Location = new System.Drawing.Point(165, 400);
            this.comma.Margin = new System.Windows.Forms.Padding(5);
            this.comma.Name = "comma";
            this.comma.Size = new System.Drawing.Size(70, 74);
            this.comma.TabIndex = 22;
            this.comma.Text = ",";
            this.comma.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.comma.UseVisualStyleBackColor = true;
            this.comma.Click += new System.EventHandler(this.ClickOnNumberButtons);
            // 
            // button0
            // 
            this.button0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button0.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button0.Location = new System.Drawing.Point(85, 400);
            this.button0.Margin = new System.Windows.Forms.Padding(5);
            this.button0.Name = "button0";
            this.button0.Size = new System.Drawing.Size(70, 74);
            this.button0.TabIndex = 21;
            this.button0.Text = "0";
            this.button0.UseVisualStyleBackColor = true;
            this.button0.Click += new System.EventHandler(this.ClickOnNumberButtons);
            // 
            // changeSign
            // 
            this.changeSign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.changeSign.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.changeSign.Location = new System.Drawing.Point(5, 400);
            this.changeSign.Margin = new System.Windows.Forms.Padding(5);
            this.changeSign.Name = "changeSign";
            this.changeSign.Size = new System.Drawing.Size(70, 74);
            this.changeSign.TabIndex = 20;
            this.changeSign.Text = "+/-";
            this.changeSign.UseVisualStyleBackColor = true;
            this.changeSign.Click += new System.EventHandler(this.ClickOnChangeSign);
            // 
            // subtract
            // 
            this.subtract.Dock = System.Windows.Forms.DockStyle.Fill;
            this.subtract.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.subtract.Location = new System.Drawing.Point(245, 163);
            this.subtract.Margin = new System.Windows.Forms.Padding(5);
            this.subtract.Name = "subtract";
            this.subtract.Size = new System.Drawing.Size(70, 69);
            this.subtract.TabIndex = 23;
            this.subtract.Text = "-";
            this.subtract.UseVisualStyleBackColor = true;
            this.subtract.Click += new System.EventHandler(this.ClickOnOperationsOrBrackets);
            // 
            // equals
            // 
            this.equals.Dock = System.Windows.Forms.DockStyle.Fill;
            this.equals.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.equals.Location = new System.Drawing.Point(245, 400);
            this.equals.Margin = new System.Windows.Forms.Padding(5);
            this.equals.Name = "equals";
            this.equals.Size = new System.Drawing.Size(70, 74);
            this.equals.TabIndex = 24;
            this.equals.Text = "=";
            this.equals.UseVisualStyleBackColor = true;
            this.equals.Click += new System.EventHandler(this.ClickOnEquals);
            // 
            // divide
            // 
            this.divide.Dock = System.Windows.Forms.DockStyle.Fill;
            this.divide.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.divide.Location = new System.Drawing.Point(245, 321);
            this.divide.Margin = new System.Windows.Forms.Padding(5);
            this.divide.Name = "divide";
            this.divide.Size = new System.Drawing.Size(70, 69);
            this.divide.TabIndex = 25;
            this.divide.Text = "/";
            this.divide.UseVisualStyleBackColor = true;
            this.divide.Click += new System.EventHandler(this.ClickOnOperationsOrBrackets);
            // 
            // multiply
            // 
            this.multiply.Dock = System.Windows.Forms.DockStyle.Fill;
            this.multiply.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.multiply.Location = new System.Drawing.Point(245, 242);
            this.multiply.Margin = new System.Windows.Forms.Padding(5);
            this.multiply.Name = "multiply";
            this.multiply.Size = new System.Drawing.Size(70, 69);
            this.multiply.TabIndex = 26;
            this.multiply.Text = "*";
            this.multiply.UseVisualStyleBackColor = true;
            this.multiply.Click += new System.EventHandler(this.ClickOnOperationsOrBrackets);
            // 
            // delLastSymb
            // 
            this.delLastSymb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.delLastSymb.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.delLastSymb.Location = new System.Drawing.Point(5, 84);
            this.delLastSymb.Margin = new System.Windows.Forms.Padding(5);
            this.delLastSymb.Name = "delLastSymb";
            this.delLastSymb.Size = new System.Drawing.Size(70, 69);
            this.delLastSymb.TabIndex = 27;
            this.delLastSymb.Text = "del";
            this.delLastSymb.UseVisualStyleBackColor = true;
            this.delLastSymb.Click += new System.EventHandler(this.ClickOnDel);
            // 
            // openBracket
            // 
            this.openBracket.Dock = System.Windows.Forms.DockStyle.Fill;
            this.openBracket.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.openBracket.Location = new System.Drawing.Point(85, 84);
            this.openBracket.Margin = new System.Windows.Forms.Padding(5);
            this.openBracket.Name = "openBracket";
            this.openBracket.Size = new System.Drawing.Size(70, 69);
            this.openBracket.TabIndex = 28;
            this.openBracket.Text = "(";
            this.openBracket.UseVisualStyleBackColor = true;
            this.openBracket.Click += new System.EventHandler(this.ClickOnOperationsOrBrackets);
            // 
            // closeBracket
            // 
            this.closeBracket.Dock = System.Windows.Forms.DockStyle.Fill;
            this.closeBracket.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.closeBracket.Location = new System.Drawing.Point(165, 84);
            this.closeBracket.Margin = new System.Windows.Forms.Padding(5);
            this.closeBracket.Name = "closeBracket";
            this.closeBracket.Size = new System.Drawing.Size(70, 69);
            this.closeBracket.TabIndex = 29;
            this.closeBracket.Text = ")";
            this.closeBracket.UseVisualStyleBackColor = true;
            this.closeBracket.Click += new System.EventHandler(this.ClickOnOperationsOrBrackets);
            // 
            // add
            // 
            this.add.Dock = System.Windows.Forms.DockStyle.Fill;
            this.add.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.add.Location = new System.Drawing.Point(245, 84);
            this.add.Margin = new System.Windows.Forms.Padding(5);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(70, 69);
            this.add.TabIndex = 30;
            this.add.Text = "+";
            this.add.UseVisualStyleBackColor = true;
            this.add.Click += new System.EventHandler(this.ClickOnOperationsOrBrackets);
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel.ColumnCount = 4;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel.Controls.Add(this.button1, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.inputOutput, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.divide, 3, 4);
            this.tableLayoutPanel.Controls.Add(this.multiply, 3, 3);
            this.tableLayoutPanel.Controls.Add(this.delLastSymb, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.button9, 2, 4);
            this.tableLayoutPanel.Controls.Add(this.openBracket, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.button8, 1, 4);
            this.tableLayoutPanel.Controls.Add(this.subtract, 3, 2);
            this.tableLayoutPanel.Controls.Add(this.button7, 0, 4);
            this.tableLayoutPanel.Controls.Add(this.add, 3, 1);
            this.tableLayoutPanel.Controls.Add(this.button6, 2, 3);
            this.tableLayoutPanel.Controls.Add(this.closeBracket, 2, 1);
            this.tableLayoutPanel.Controls.Add(this.button5, 1, 3);
            this.tableLayoutPanel.Controls.Add(this.button4, 0, 3);
            this.tableLayoutPanel.Controls.Add(this.button2, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.button3, 2, 2);
            this.tableLayoutPanel.Controls.Add(this.changeSign, 0, 5);
            this.tableLayoutPanel.Controls.Add(this.button0, 1, 5);
            this.tableLayoutPanel.Controls.Add(this.comma, 2, 5);
            this.tableLayoutPanel.Controls.Add(this.equals, 3, 5);
            this.tableLayoutPanel.Location = new System.Drawing.Point(14, 14);
            this.tableLayoutPanel.Margin = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 6;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(320, 479);
            this.tableLayoutPanel.TabIndex = 31;
            // 
            // CalculatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 508);
            this.Controls.Add(this.tableLayoutPanel);
            this.MaximumSize = new System.Drawing.Size(740, 1128);
            this.MinimumSize = new System.Drawing.Size(370, 564);
            this.Name = "CalculatorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Calculator";
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
        private Button button8;
        private Button button9;
        private TextBox inputOutput;
        private Button comma;
        private Button button0;
        private Button changeSign;
        private Button subtract;
        private Button equals;
        private Button divide;
        private Button multiply;
        private Button delLastSymb;
        private Button openBracket;
        private Button closeBracket;
        private Button add;
        private TableLayoutPanel tableLayoutPanel;
    }
}