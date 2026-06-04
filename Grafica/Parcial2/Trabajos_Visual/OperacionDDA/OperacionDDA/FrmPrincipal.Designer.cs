namespace OperacionDDA
{
    partial class FrmPrincipal
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lstPuntos = new System.Windows.Forms.ListBox();
            this.txtX1 = new System.Windows.Forms.TextBox();
            this.txtY1 = new System.Windows.Forms.TextBox();
            this.txtX2 = new System.Windows.Forms.TextBox();
            this.txtY2 = new System.Windows.Forms.TextBox();
            this.btnDibujar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.lblPasos = new System.Windows.Forms.Label();
            this.btnPuntoMedio = new System.Windows.Forms.Button();
            this.btnBresenham = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rtbFormula = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(20, 18);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(573, 430);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // lstPuntos
            // 
            this.lstPuntos.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstPuntos.FormattingEnabled = true;
            this.lstPuntos.ItemHeight = 21;
            this.lstPuntos.Location = new System.Drawing.Point(620, 346);
            this.lstPuntos.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lstPuntos.Name = "lstPuntos";
            this.lstPuntos.Size = new System.Drawing.Size(230, 67);
            this.lstPuntos.TabIndex = 1;
            this.lstPuntos.SelectedIndexChanged += new System.EventHandler(this.lstPuntos_SelectedIndexChanged);
            // 
            // txtX1
            // 
            this.txtX1.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtX1.Location = new System.Drawing.Point(673, 18);
            this.txtX1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtX1.Name = "txtX1";
            this.txtX1.Size = new System.Drawing.Size(64, 29);
            this.txtX1.TabIndex = 7;
            // 
            // txtY1
            // 
            this.txtY1.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtY1.Location = new System.Drawing.Point(802, 17);
            this.txtY1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtY1.Name = "txtY1";
            this.txtY1.Size = new System.Drawing.Size(67, 29);
            this.txtY1.TabIndex = 8;
            // 
            // txtX2
            // 
            this.txtX2.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtX2.Location = new System.Drawing.Point(672, 67);
            this.txtX2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtX2.Name = "txtX2";
            this.txtX2.Size = new System.Drawing.Size(64, 29);
            this.txtX2.TabIndex = 9;
            this.txtX2.TextChanged += new System.EventHandler(this.txtX2_TextChanged);
            // 
            // txtY2
            // 
            this.txtY2.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtY2.Location = new System.Drawing.Point(802, 67);
            this.txtY2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtY2.Name = "txtY2";
            this.txtY2.Size = new System.Drawing.Size(67, 29);
            this.txtY2.TabIndex = 10;
            // 
            // btnDibujar
            // 
            this.btnDibujar.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnDibujar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDibujar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnDibujar.Location = new System.Drawing.Point(619, 224);
            this.btnDibujar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDibujar.Name = "btnDibujar";
            this.btnDibujar.Size = new System.Drawing.Size(116, 39);
            this.btnDibujar.TabIndex = 11;
            this.btnDibujar.Text = "DDA";
            this.btnDibujar.UseVisualStyleBackColor = false;
            this.btnDibujar.Click += new System.EventHandler(this.btnDibujar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(620, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 23);
            this.label1.TabIndex = 3;
            this.label1.Text = "x1:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(753, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 23);
            this.label2.TabIndex = 4;
            this.label2.Text = "y1:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(619, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 23);
            this.label3.TabIndex = 5;
            this.label3.Text = "x2:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(753, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 23);
            this.label4.TabIndex = 6;
            this.label4.Text = "y2:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.AliceBlue;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btnLimpiar);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtY2);
            this.panel1.Controls.Add(this.lblPasos);
            this.panel1.Controls.Add(this.txtX2);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnPuntoMedio);
            this.panel1.Controls.Add(this.btnBresenham);
            this.panel1.Controls.Add(this.lstPuntos);
            this.panel1.Controls.Add(this.btnDibujar);
            this.panel1.Controls.Add(this.txtY1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(967, 516);
            this.panel1.TabIndex = 12;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.LightCoral;
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnLimpiar.Location = new System.Drawing.Point(741, 274);
            this.btnLimpiar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(109, 39);
            this.btnLimpiar.TabIndex = 15;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // lblPasos
            // 
            this.lblPasos.AutoSize = true;
            this.lblPasos.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblPasos.Location = new System.Drawing.Point(622, 424);
            this.lblPasos.Name = "lblPasos";
            this.lblPasos.Size = new System.Drawing.Size(53, 23);
            this.lblPasos.TabIndex = 3;
            this.lblPasos.Text = "Pasos";
            // 
            // btnPuntoMedio
            // 
            this.btnPuntoMedio.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnPuntoMedio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPuntoMedio.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnPuntoMedio.Location = new System.Drawing.Point(619, 274);
            this.btnPuntoMedio.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPuntoMedio.Name = "btnPuntoMedio";
            this.btnPuntoMedio.Size = new System.Drawing.Size(116, 39);
            this.btnPuntoMedio.TabIndex = 14;
            this.btnPuntoMedio.Text = "Pto Medio";
            this.btnPuntoMedio.UseVisualStyleBackColor = false;
            this.btnPuntoMedio.Click += new System.EventHandler(this.btnPuntoMedio_Click);
            // 
            // btnBresenham
            // 
            this.btnBresenham.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnBresenham.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBresenham.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnBresenham.Location = new System.Drawing.Point(741, 224);
            this.btnBresenham.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnBresenham.Name = "btnBresenham";
            this.btnBresenham.Size = new System.Drawing.Size(109, 39);
            this.btnBresenham.TabIndex = 13;
            this.btnBresenham.Text = "Bresenham";
            this.btnBresenham.UseVisualStyleBackColor = false;
            this.btnBresenham.Click += new System.EventHandler(this.btnBresenham_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rtbFormula);
            this.groupBox1.Location = new System.Drawing.Point(612, 101);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(257, 118);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información del Algoritmo";
            // 
            // rtbFormula
            // 
            this.rtbFormula.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbFormula.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbFormula.Location = new System.Drawing.Point(6, 16);
            this.rtbFormula.Name = "rtbFormula";
            this.rtbFormula.ReadOnly = true;
            this.rtbFormula.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbFormula.Size = new System.Drawing.Size(232, 96);
            this.rtbFormula.TabIndex = 0;
            this.rtbFormula.Text = "";
            // 
            // FrmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(882, 494);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtX1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FrmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Principal";
            this.Load += new System.EventHandler(this.FrmPrincipal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ListBox lstPuntos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtX1;
        private System.Windows.Forms.TextBox txtY1;
        private System.Windows.Forms.TextBox txtX2;
        private System.Windows.Forms.TextBox txtY2;
        private System.Windows.Forms.Button btnDibujar;
        private System.Windows.Forms.Button btnBresenham;
        private System.Windows.Forms.Button btnPuntoMedio;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblPasos;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox rtbFormula;
    }
}