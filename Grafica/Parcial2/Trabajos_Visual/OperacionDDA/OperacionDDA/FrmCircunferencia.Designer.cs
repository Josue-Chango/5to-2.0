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
            this.lblPuntos = new System.Windows.Forms.Label();
            this.lblPixeles = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pctGrafico)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pctGrafico
            // 
            this.pctGrafico.Location = new System.Drawing.Point(19, 16);
            this.pctGrafico.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pctGrafico.Name = "pctGrafico";
            this.pctGrafico.Size = new System.Drawing.Size(603, 411);
            this.pctGrafico.TabIndex = 0;
            this.pctGrafico.TabStop = false;
            this.pctGrafico.Paint += new System.Windows.Forms.PaintEventHandler(this.pctGrafico_Paint);
            // 
            // btnDibujar
            // 
            this.btnDibujar.Location = new System.Drawing.Point(665, 71);
            this.btnDibujar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDibujar.Name = "btnDibujar";
            this.btnDibujar.Size = new System.Drawing.Size(83, 31);
            this.btnDibujar.TabIndex = 1;
            this.btnDibujar.Text = "Dibujar";
            this.btnDibujar.UseVisualStyleBackColor = true;
            this.btnDibujar.Click += new System.EventHandler(this.btnDibujar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(648, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Radio";
            // 
            // txtRadio
            // 
            this.txtRadio.Location = new System.Drawing.Point(706, 30);
            this.txtRadio.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtRadio.Name = "txtRadio";
            this.txtRadio.Size = new System.Drawing.Size(65, 22);
            this.txtRadio.TabIndex = 3;
            // 
            // lstPuntos
            // 
            this.lstPuntos.FormattingEnabled = true;
            this.lstPuntos.ItemHeight = 16;
            this.lstPuntos.Location = new System.Drawing.Point(651, 118);
            this.lstPuntos.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lstPuntos.Name = "lstPuntos";
            this.lstPuntos.Size = new System.Drawing.Size(120, 228);
            this.lstPuntos.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.lblPixeles);
            this.panel1.Controls.Add(this.lblPuntos);
            this.panel1.Controls.Add(this.lstPuntos);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnDibujar);
            this.panel1.Controls.Add(this.txtRadio);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(803, 452);
            this.panel1.TabIndex = 5;
            // 
            // lblPuntos
            // 
            this.lblPuntos.AutoSize = true;
            this.lblPuntos.Location = new System.Drawing.Point(651, 375);
            this.lblPuntos.Name = "lblPuntos";
            this.lblPuntos.Size = new System.Drawing.Size(54, 16);
            this.lblPuntos.TabIndex = 5;
            this.lblPuntos.Text = "Puntos: ";
            // 
            // lblPixeles
            // 
            this.lblPixeles.AutoSize = true;
            this.lblPixeles.Location = new System.Drawing.Point(654, 410);
            this.lblPixeles.Name = "lblPixeles";
            this.lblPixeles.Size = new System.Drawing.Size(57, 16);
            this.lblPixeles.TabIndex = 6;
            this.lblPixeles.Text = "Pixeles: ";
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
            this.Text = "Circunferencia";
            ((System.ComponentModel.ISupportInitialize)(this.pctGrafico)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
    }
}