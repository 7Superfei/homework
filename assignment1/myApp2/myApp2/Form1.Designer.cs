namespace myApp2
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxNum1 = new System.Windows.Forms.TextBox();
            this.textBoxNum2 = new System.Windows.Forms.TextBox();
            this.radioButtonAdd = new System.Windows.Forms.RadioButton();
            this.radioButtonSubtract = new System.Windows.Forms.RadioButton();
            this.radioButtonMultiply = new System.Windows.Forms.RadioButton();
            this.radioButtonDivide = new System.Windows.Forms.RadioButton();
            this.buttonCalculate = new System.Windows.Forms.Button();
            this.labelResult = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxNum1
            // 
            this.textBoxNum1.Location = new System.Drawing.Point(433, 53);
            this.textBoxNum1.Name = "textBoxNum1";
            this.textBoxNum1.Size = new System.Drawing.Size(135, 28);
            this.textBoxNum1.TabIndex = 0;
            this.textBoxNum1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBoxNum2
            // 
            this.textBoxNum2.Location = new System.Drawing.Point(433, 266);
            this.textBoxNum2.Name = "textBoxNum2";
            this.textBoxNum2.Size = new System.Drawing.Size(135, 28);
            this.textBoxNum2.TabIndex = 1;
            this.textBoxNum2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // radioButtonAdd
            // 
            this.radioButtonAdd.AutoSize = true;
            this.radioButtonAdd.Location = new System.Drawing.Point(146, 167);
            this.radioButtonAdd.Name = "radioButtonAdd";
            this.radioButtonAdd.Size = new System.Drawing.Size(159, 22);
            this.radioButtonAdd.TabIndex = 2;
            this.radioButtonAdd.TabStop = true;
            this.radioButtonAdd.Text = "radioButtonAdd";
            this.radioButtonAdd.UseVisualStyleBackColor = true;
            this.radioButtonAdd.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButtonSubtract
            // 
            this.radioButtonSubtract.AutoSize = true;
            this.radioButtonSubtract.Location = new System.Drawing.Point(339, 167);
            this.radioButtonSubtract.Name = "radioButtonSubtract";
            this.radioButtonSubtract.Size = new System.Drawing.Size(204, 22);
            this.radioButtonSubtract.TabIndex = 3;
            this.radioButtonSubtract.TabStop = true;
            this.radioButtonSubtract.Text = "radioButtonSubtract";
            this.radioButtonSubtract.UseVisualStyleBackColor = true;
            this.radioButtonSubtract.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButtonMultiply
            // 
            this.radioButtonMultiply.AutoSize = true;
            this.radioButtonMultiply.Location = new System.Drawing.Point(518, 167);
            this.radioButtonMultiply.Name = "radioButtonMultiply";
            this.radioButtonMultiply.Size = new System.Drawing.Size(204, 22);
            this.radioButtonMultiply.TabIndex = 4;
            this.radioButtonMultiply.TabStop = true;
            this.radioButtonMultiply.Text = "radioButtonMultiply";
            this.radioButtonMultiply.UseVisualStyleBackColor = true;
            this.radioButtonMultiply.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // radioButtonDivide
            // 
            this.radioButtonDivide.AutoSize = true;
            this.radioButtonDivide.Location = new System.Drawing.Point(726, 167);
            this.radioButtonDivide.Name = "radioButtonDivide";
            this.radioButtonDivide.Size = new System.Drawing.Size(186, 22);
            this.radioButtonDivide.TabIndex = 5;
            this.radioButtonDivide.TabStop = true;
            this.radioButtonDivide.Text = "radioButtonDivide";
            this.radioButtonDivide.UseVisualStyleBackColor = true;
            this.radioButtonDivide.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // buttonCalculate
            // 
            this.buttonCalculate.Location = new System.Drawing.Point(406, 340);
            this.buttonCalculate.Name = "buttonCalculate";
            this.buttonCalculate.Size = new System.Drawing.Size(199, 70);
            this.buttonCalculate.TabIndex = 6;
            this.buttonCalculate.Text = "buttonCalculate";
            this.buttonCalculate.UseVisualStyleBackColor = true;
            this.buttonCalculate.Click += new System.EventHandler(this.buttonCalculate_Click);
            // 
            // labelResult
            // 
            this.labelResult.AutoSize = true;
            this.labelResult.Location = new System.Drawing.Point(452, 447);
            this.labelResult.Name = "labelResult";
            this.labelResult.Size = new System.Drawing.Size(107, 18);
            this.labelResult.TabIndex = 7;
            this.labelResult.Text = "labelResult";
            this.labelResult.Click += new System.EventHandler(this.label1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1039, 618);
            this.Controls.Add(this.labelResult);
            this.Controls.Add(this.buttonCalculate);
            this.Controls.Add(this.radioButtonDivide);
            this.Controls.Add(this.radioButtonMultiply);
            this.Controls.Add(this.radioButtonSubtract);
            this.Controls.Add(this.radioButtonAdd);
            this.Controls.Add(this.textBoxNum2);
            this.Controls.Add(this.textBoxNum1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxNum1;
        private System.Windows.Forms.TextBox textBoxNum2;
        private System.Windows.Forms.RadioButton radioButtonAdd;
        private System.Windows.Forms.RadioButton radioButtonSubtract;
        private System.Windows.Forms.RadioButton radioButtonMultiply;
        private System.Windows.Forms.RadioButton radioButtonDivide;
        private System.Windows.Forms.Button buttonCalculate;
        private System.Windows.Forms.Label labelResult;
    }
}

