namespace OperacionDDA
{
    partial class FrmRecorte
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
            this.pctGrafico = new System.Windows.Forms.PictureBox();
            this.txtX1 = new System.Windows.Forms.TextBox();
            this.txtY1 = new System.Windows.Forms.TextBox();
            this.txtX2 = new System.Windows.Forms.TextBox();
            this.txtY2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnRecortar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rtbFormula = new System.Windows.Forms.RichTextBox();
            this.btnLiang = new System.Windows.Forms.Button();
            this.btnParametrico = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pctGrafico)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pctGrafico
            // 
            this.pctGrafico.Location = new System.Drawing.Point(16, 17);
            this.pctGrafico.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pctGrafico.Name = "pctGrafico";
            this.pctGrafico.Size = new System.Drawing.Size(403, 332);
            this.pctGrafico.TabIndex = 0;
            this.pctGrafico.TabStop = false;
            this.pctGrafico.Paint += new System.Windows.Forms.PaintEventHandler(this.pctGrafico_Paint);
            // 
            // txtX1
            // 
            this.txtX1.Location = new System.Drawing.Point(453, 28);
            this.txtX1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtX1.Name = "txtX1";
            this.txtX1.Size = new System.Drawing.Size(46, 20);
            this.txtX1.TabIndex = 1;
            // 
            // txtY1
            // 
            this.txtY1.Location = new System.Drawing.Point(542, 28);
            this.txtY1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtY1.Name = "txtY1";
            this.txtY1.Size = new System.Drawing.Size(44, 20);
            this.txtY1.TabIndex = 2;
            // 
            // txtX2
            // 
            this.txtX2.Location = new System.Drawing.Point(454, 67);
            this.txtX2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtX2.Name = "txtX2";
            this.txtX2.Size = new System.Drawing.Size(44, 20);
            this.txtX2.TabIndex = 3;
            // 
            // txtY2
            // 
            this.txtY2.Location = new System.Drawing.Point(542, 67);
            this.txtY2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtY2.Name = "txtY2";
            this.txtY2.Size = new System.Drawing.Size(42, 20);
            this.txtY2.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(423, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "x1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(510, 31);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "y1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(423, 70);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "x2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(510, 70);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(18, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "y2";
            // 
            // btnRecortar
            // 
            this.btnRecortar.Location = new System.Drawing.Point(453, 116);
            this.btnRecortar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnRecortar.Name = "btnRecortar";
            this.btnRecortar.Size = new System.Drawing.Size(58, 25);
            this.btnRecortar.TabIndex = 9;
            this.btnRecortar.Text = "Cohen";
            this.btnRecortar.UseVisualStyleBackColor = true;
            this.btnRecortar.Click += new System.EventHandler(this.btnRecortar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rtbFormula);
            this.groupBox1.Location = new System.Drawing.Point(440, 233);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(144, 116);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Algoritmo";
            // 
            // rtbFormula
            // 
            this.rtbFormula.Location = new System.Drawing.Point(8, 15);
            this.rtbFormula.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rtbFormula.Name = "rtbFormula";
            this.rtbFormula.Size = new System.Drawing.Size(135, 101);
            this.rtbFormula.TabIndex = 0;
            this.rtbFormula.Text = "";
            // 
            // btnLiang
            // 
            this.btnLiang.Location = new System.Drawing.Point(528, 116);
            this.btnLiang.Name = "btnLiang";
            this.btnLiang.Size = new System.Drawing.Size(58, 25);
            this.btnLiang.TabIndex = 11;
            this.btnLiang.Text = "Liang";
            this.btnLiang.UseVisualStyleBackColor = true;
            this.btnLiang.Click += new System.EventHandler(this.btnLiang_Click);
            // 
            // btnParametrico
            // 
            this.btnParametrico.Location = new System.Drawing.Point(486, 147);
            this.btnParametrico.Name = "btnParametrico";
            this.btnParametrico.Size = new System.Drawing.Size(74, 25);
            this.btnParametrico.TabIndex = 12;
            this.btnParametrico.Text = "Parametrico";
            this.btnParametrico.UseVisualStyleBackColor = true;
            this.btnParametrico.Click += new System.EventHandler(this.btnParametrico_Click);
            // 
            // FrmRecorte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.btnParametrico);
            this.Controls.Add(this.btnLiang);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnRecortar);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtY2);
            this.Controls.Add(this.txtX2);
            this.Controls.Add(this.txtY1);
            this.Controls.Add(this.txtX1);
            this.Controls.Add(this.pctGrafico);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FrmRecorte";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmRecorte";
            ((System.ComponentModel.ISupportInitialize)(this.pctGrafico)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pctGrafico;
        private System.Windows.Forms.TextBox txtX1;
        private System.Windows.Forms.TextBox txtY1;
        private System.Windows.Forms.TextBox txtX2;
        private System.Windows.Forms.TextBox txtY2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnRecortar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox rtbFormula;
        private System.Windows.Forms.Button btnLiang;
        private System.Windows.Forms.Button btnParametrico;
    }
}