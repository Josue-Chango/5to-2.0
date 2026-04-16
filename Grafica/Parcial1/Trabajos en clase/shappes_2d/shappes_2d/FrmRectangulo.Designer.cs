namespace shappes_2d
{
    partial class FrmRectangulo
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
            this.lblLargo = new System.Windows.Forms.Label();
            this.txtAncho = new System.Windows.Forms.TextBox();
            this.txtLargo = new System.Windows.Forms.TextBox();
            this.lblAncho = new System.Windows.Forms.Label();
            this.btnDibujar = new System.Windows.Forms.Button();
            this.btnResetear = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.lblGrafico = new System.Windows.Forms.Label();
            this.warning = new System.Windows.Forms.Label();
            this.lblPerimetro = new System.Windows.Forms.Label();
            this.lblArea = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblLargo
            // 
            this.lblLargo.AutoSize = true;
            this.lblLargo.Location = new System.Drawing.Point(9, 43);
            this.lblLargo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLargo.Name = "lblLargo";
            this.lblLargo.Size = new System.Drawing.Size(37, 13);
            this.lblLargo.TabIndex = 1;
            this.lblLargo.Text = "Largo:";
            // 
            // txtAncho
            // 
            this.txtAncho.Location = new System.Drawing.Point(52, 20);
            this.txtAncho.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtAncho.Name = "txtAncho";
            this.txtAncho.Size = new System.Drawing.Size(76, 20);
            this.txtAncho.TabIndex = 2;
            this.txtAncho.TextChanged += new System.EventHandler(this.txtAncho_TextChanged);
            // 
            // txtLargo
            // 
            this.txtLargo.Location = new System.Drawing.Point(52, 43);
            this.txtLargo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtLargo.Name = "txtLargo";
            this.txtLargo.Size = new System.Drawing.Size(76, 20);
            this.txtLargo.TabIndex = 3;
            this.txtLargo.TextChanged += new System.EventHandler(this.txtLargo_TextChanged);
            // 
            // lblAncho
            // 
            this.lblAncho.AutoSize = true;
            this.lblAncho.Location = new System.Drawing.Point(9, 23);
            this.lblAncho.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAncho.Name = "lblAncho";
            this.lblAncho.Size = new System.Drawing.Size(44, 13);
            this.lblAncho.TabIndex = 4;
            this.lblAncho.Text = "Ancho: ";
            // 
            // btnDibujar
            // 
            this.btnDibujar.Location = new System.Drawing.Point(9, 77);
            this.btnDibujar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnDibujar.Name = "btnDibujar";
            this.btnDibujar.Size = new System.Drawing.Size(56, 19);
            this.btnDibujar.TabIndex = 5;
            this.btnDibujar.Text = "Dibujar";
            this.btnDibujar.UseVisualStyleBackColor = true;
            this.btnDibujar.Click += new System.EventHandler(this.btnDibujar_Click);
            // 
            // btnResetear
            // 
            this.btnResetear.Location = new System.Drawing.Point(70, 77);
            this.btnResetear.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnResetear.Name = "btnResetear";
            this.btnResetear.Size = new System.Drawing.Size(56, 19);
            this.btnResetear.TabIndex = 6;
            this.btnResetear.Text = "Resetear";
            this.btnResetear.UseVisualStyleBackColor = true;
            this.btnResetear.Click += new System.EventHandler(this.btnResetear_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(139, 76);
            this.btnSalir.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(56, 19);
            this.btnSalir.TabIndex = 7;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // lblGrafico
            // 
            this.lblGrafico.AutoSize = true;
            this.lblGrafico.Location = new System.Drawing.Point(258, 25);
            this.lblGrafico.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblGrafico.Name = "lblGrafico";
            this.lblGrafico.Size = new System.Drawing.Size(41, 13);
            this.lblGrafico.TabIndex = 9;
            this.lblGrafico.Text = "Grafico";
            // 
            // warning
            // 
            this.warning.AutoSize = true;
            this.warning.Location = new System.Drawing.Point(258, 7);
            this.warning.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.warning.Name = "warning";
            this.warning.Size = new System.Drawing.Size(10, 13);
            this.warning.TabIndex = 10;
            this.warning.Text = " ";
            // 
            // lblPerimetro
            // 
            this.lblPerimetro.AutoSize = true;
            this.lblPerimetro.Location = new System.Drawing.Point(11, 110);
            this.lblPerimetro.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPerimetro.Name = "lblPerimetro";
            this.lblPerimetro.Size = new System.Drawing.Size(57, 13);
            this.lblPerimetro.TabIndex = 11;
            this.lblPerimetro.Text = "Perimetro: ";
            // 
            // lblArea
            // 
            this.lblArea.AutoSize = true;
            this.lblArea.Location = new System.Drawing.Point(11, 134);
            this.lblArea.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblArea.Name = "lblArea";
            this.lblArea.Size = new System.Drawing.Size(35, 13);
            this.lblArea.TabIndex = 12;
            this.lblArea.Text = "Area: ";
            // 
            // FrmRectangulo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.lblArea);
            this.Controls.Add(this.lblPerimetro);
            this.Controls.Add(this.warning);
            this.Controls.Add(this.lblGrafico);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnResetear);
            this.Controls.Add(this.btnDibujar);
            this.Controls.Add(this.lblAncho);
            this.Controls.Add(this.txtLargo);
            this.Controls.Add(this.txtAncho);
            this.Controls.Add(this.lblLargo);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FrmRectangulo";
            this.Text = "Rectángulo";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmRectangulo_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblLargo;
        private System.Windows.Forms.TextBox txtAncho;
        private System.Windows.Forms.TextBox txtLargo;
        private System.Windows.Forms.Label lblAncho;
        private System.Windows.Forms.Button btnDibujar;
        private System.Windows.Forms.Button btnResetear;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Label lblGrafico;
        private System.Windows.Forms.Label warning;
        private System.Windows.Forms.Label lblPerimetro;
        private System.Windows.Forms.Label lblArea;
    }
}