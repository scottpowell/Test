namespace ColFilterToSQL
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnProcess = new System.Windows.Forms.Button();
            this.tbInputFilter = new System.Windows.Forms.TextBox();
            this.tbInputColumn = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbSQLColumn = new System.Windows.Forms.TextBox();
            this.tbSQLExpression = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lbParameters = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // btnProcess
            // 
            this.btnProcess.Location = new System.Drawing.Point(390, 7);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(114, 23);
            this.btnProcess.TabIndex = 0;
            this.btnProcess.Text = "Process Filter";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // tbInputFilter
            // 
            this.tbInputFilter.Location = new System.Drawing.Point(118, 33);
            this.tbInputFilter.Multiline = true;
            this.tbInputFilter.Name = "tbInputFilter";
            this.tbInputFilter.Size = new System.Drawing.Size(386, 78);
            this.tbInputFilter.TabIndex = 1;
            // 
            // tbInputColumn
            // 
            this.tbInputColumn.Location = new System.Drawing.Point(118, 4);
            this.tbInputColumn.Name = "tbInputColumn";
            this.tbInputColumn.Size = new System.Drawing.Size(143, 20);
            this.tbInputColumn.TabIndex = 2;
            this.tbInputColumn.Text = "MyTestColumn";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "SQL Column Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Filter Expression";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 215);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "SQL Expression";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 186);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "SQL Column Name";
            // 
            // tbSQLColumn
            // 
            this.tbSQLColumn.Location = new System.Drawing.Point(118, 183);
            this.tbSQLColumn.Name = "tbSQLColumn";
            this.tbSQLColumn.ReadOnly = true;
            this.tbSQLColumn.Size = new System.Drawing.Size(143, 20);
            this.tbSQLColumn.TabIndex = 6;
            // 
            // tbSQLExpression
            // 
            this.tbSQLExpression.Location = new System.Drawing.Point(118, 212);
            this.tbSQLExpression.Multiline = true;
            this.tbSQLExpression.Name = "tbSQLExpression";
            this.tbSQLExpression.ReadOnly = true;
            this.tbSQLExpression.Size = new System.Drawing.Size(386, 78);
            this.tbSQLExpression.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 300);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Parameters";
            // 
            // lbParameters
            // 
            this.lbParameters.BackColor = System.Drawing.SystemColors.Control;
            this.lbParameters.FormattingEnabled = true;
            this.lbParameters.Location = new System.Drawing.Point(118, 300);
            this.lbParameters.Name = "lbParameters";
            this.lbParameters.Size = new System.Drawing.Size(386, 95);
            this.lbParameters.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(870, 514);
            this.Controls.Add(this.lbParameters);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbSQLColumn);
            this.Controls.Add(this.tbSQLExpression);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbInputColumn);
            this.Controls.Add(this.tbInputFilter);
            this.Controls.Add(this.btnProcess);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.TextBox tbInputFilter;
        private System.Windows.Forms.TextBox tbInputColumn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbSQLColumn;
        private System.Windows.Forms.TextBox tbSQLExpression;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox lbParameters;
    }
}

