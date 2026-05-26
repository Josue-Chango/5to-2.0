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
            ((System.ComponentModel.ISupportInitialize)(this.pctGrafico)).BeginInit();
            this.SuspendLayout();
            // 
            // pctGrafico
            // 
            this.pctGrafico.Location = new System.Drawing.Point(19, 16);
            this.pctGrafico.Name = "pctGrafico";
            this.pctGrafico.Size = new System.Drawing.Size(602, 411);
            this.pctGrafico.TabIndex = 0;
            this.pctGrafico.TabStop = false;
            this.pctGrafico.Paint += new System.Windows.Forms.PaintEventHandler(this.pctGrafico_Paint);
            // 
            // btnDibujar
            // 
            this.btnDibujar.Location = new System.Drawing.Point(665, 133);
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
            this.label1.Location = new System.Drawing.Point(647, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Radio";
            // 
            // txtRadio
            // 
            this.txtRadio.Location = new System.Drawing.Point(704, 66);
            this.txtRadio.Name = "txtRadio";
            this.txtRadio.Size = new System.Drawing.Size(66, 22);
            this.txtRadio.TabIndex = 3;
            // 
            // lstPuntos
            // 
            this.lstPuntos.FormattingEnabled = true;
            this.lstPuntos.ItemHeight = 16;
            this.lstPuntos.Location = new System.Drawing.Point(650, 188);
            this.lstPuntos.Name = "lstPuntos";
            this.lstPuntos.Size = new System.Drawing.Size(120, 228);
            this.lstPuntos.TabIndex = 4;
            // 
            // FrmCircunferencia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lstPuntos);
            this.Controls.Add(this.txtRadio);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDibujar);
            this.Controls.Add(this.pctGrafico);
            this.Name = "FrmCircunferencia";
            this.Text = "Circunferencia";
            ((System.ComponentModel.ISupportInitialize)(this.pctGrafico)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pctGrafico;
        private System.Windows.Forms.Button btnDibujar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRadio;
        private System.Windows.Forms.ListBox lstPuntos;
    }
}