namespace OperacionDDA
{
    partial class FrmCircunferencia
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
            this.btnDibujar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRadio = new System.Windows.Forms.TextBox();
            this.lstPuntos = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtY = new System.Windows.Forms.TextBox();
            this.txtX = new System.Windows.Forms.TextBox();
            this.lblCentro = new System.Windows.Forms.Label();
            this.btnDibujar3 = new System.Windows.Forms.Button();
            this.btnDibujar2 = new System.Windows.Forms.Button();
            this.lblPixeles = new System.Windows.Forms.Label();
            this.lblPuntos = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rtbFormula = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pctGrafico)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pctGrafico
            // 
            this.pctGrafico.Location = new System.Drawing.Point(19, 16);
            this.pctGrafico.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pctGrafico.Name = "pctGrafico";
            this.pctGrafico.Size = new System.Drawing.Size(523, 411);
            this.pctGrafico.TabIndex = 0;
            this.pctGrafico.TabStop = false;
            this.pctGrafico.Paint += new System.Windows.Forms.PaintEventHandler(this.pctGrafico_Paint);
            // 
            // btnDibujar
            // 
            this.btnDibujar.Location = new System.Drawing.Point(556, 97);
            this.btnDibujar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDibujar.Name = "btnDibujar";
            this.btnDibujar.Size = new System.Drawing.Size(102, 31);
            this.btnDibujar.TabIndex = 1;
            this.btnDibujar.Text = "Punto medio";
            this.btnDibujar.UseVisualStyleBackColor = true;
            this.btnDibujar.Click += new System.EventHandler(this.btnDibujar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(605, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Radio";
            // 
            // txtRadio
            // 
            this.txtRadio.Location = new System.Drawing.Point(669, 23);
            this.txtRadio.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtRadio.Name = "txtRadio";
            this.txtRadio.Size = new System.Drawing.Size(53, 22);
            this.txtRadio.TabIndex = 3;
            // 
            // lstPuntos
            // 
            this.lstPuntos.FormattingEnabled = true;
            this.lstPuntos.ItemHeight = 16;
            this.lstPuntos.Location = new System.Drawing.Point(563, 328);
            this.lstPuntos.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lstPuntos.Name = "lstPuntos";
            this.lstPuntos.Size = new System.Drawing.Size(208, 68);
            this.lstPuntos.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtY);
            this.panel1.Controls.Add(this.txtX);
            this.panel1.Controls.Add(this.lblCentro);
            this.panel1.Controls.Add(this.btnDibujar3);
            this.panel1.Controls.Add(this.btnDibujar2);
            this.panel1.Controls.Add(this.lblPixeles);
            this.panel1.Controls.Add(this.lblPuntos);
            this.panel1.Controls.Add(this.lstPuntos);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnDibujar);
            this.panel1.Controls.Add(this.txtRadio);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(803, 452);
            this.panel1.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(720, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 16);
            this.label3.TabIndex = 13;
            this.label3.Text = "y:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(615, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 16);
            this.label2.TabIndex = 12;
            this.label2.Text = "x:";
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(739, 60);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(49, 22);
            this.txtY.TabIndex = 11;
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(637, 60);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(48, 22);
            this.txtX.TabIndex = 10;
            // 
            // lblCentro
            // 
            this.lblCentro.AutoSize = true;
            this.lblCentro.Location = new System.Drawing.Point(548, 63);
            this.lblCentro.Name = "lblCentro";
            this.lblCentro.Size = new System.Drawing.Size(46, 16);
            this.lblCentro.TabIndex = 9;
            this.lblCentro.Text = "Centro";
            // 
            // btnDibujar3
            // 
            this.btnDibujar3.Location = new System.Drawing.Point(608, 134);
            this.btnDibujar3.Margin = new System.Windows.Forms.Padding(4);
            this.btnDibujar3.Name = "btnDibujar3";
            this.btnDibujar3.Size = new System.Drawing.Size(129, 28);
            this.btnDibujar3.TabIndex = 8;
            this.btnDibujar3.Text = "Parametrico";
            this.btnDibujar3.UseVisualStyleBackColor = true;
            this.btnDibujar3.Click += new System.EventHandler(this.btnDibujar3_Click);
            // 
            // btnDibujar2
            // 
            this.btnDibujar2.Location = new System.Drawing.Point(683, 97);
            this.btnDibujar2.Margin = new System.Windows.Forms.Padding(4);
            this.btnDibujar2.Name = "btnDibujar2";
            this.btnDibujar2.Size = new System.Drawing.Size(102, 28);
            this.btnDibujar2.TabIndex = 7;
            this.btnDibujar2.Text = "Bresenham";
            this.btnDibujar2.UseVisualStyleBackColor = true;
            this.btnDibujar2.Click += new System.EventHandler(this.btnDibujar2_Click);
            // 
            // lblPixeles
            // 
            this.lblPixeles.AutoSize = true;
            this.lblPixeles.Location = new System.Drawing.Point(680, 411);
            this.lblPixeles.Name = "lblPixeles";
            this.lblPixeles.Size = new System.Drawing.Size(57, 16);
            this.lblPixeles.TabIndex = 6;
            this.lblPixeles.Text = "Pixeles: ";
            // 
            // lblPuntos
            // 
            this.lblPuntos.AutoSize = true;
            this.lblPuntos.Location = new System.Drawing.Point(560, 411);
            this.lblPuntos.Name = "lblPuntos";
            this.lblPuntos.Size = new System.Drawing.Size(54, 16);
            this.lblPuntos.TabIndex = 5;
            this.lblPuntos.Text = "Puntos: ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rtbFormula);
            this.groupBox1.Location = new System.Drawing.Point(553, 169);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(231, 146);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Formulas";
            // 
            // rtbFormula
            // 
            this.rtbFormula.Location = new System.Drawing.Point(8, 23);
            this.rtbFormula.Name = "rtbFormula";
            this.rtbFormula.Size = new System.Drawing.Size(222, 122);
            this.rtbFormula.TabIndex = 0;
            this.rtbFormula.Text = "";
            // 
            // FrmCircunferencia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pctGrafico);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FrmCircunferencia";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Circunferencia";
            ((System.ComponentModel.ISupportInitialize)(this.pctGrafico)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pctGrafico;
        private System.Windows.Forms.Button btnDibujar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRadio;
        private System.Windows.Forms.ListBox lstPuntos;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblPuntos;
        private System.Windows.Forms.Label lblPixeles;
        private System.Windows.Forms.Button btnDibujar2;
        private System.Windows.Forms.Button btnDibujar3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtY;
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.Label lblCentro;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox rtbFormula;
    }
}