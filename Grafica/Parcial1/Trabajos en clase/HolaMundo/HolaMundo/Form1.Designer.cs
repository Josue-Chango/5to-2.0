namespace HolaMundo
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblmensaje = new System.Windows.Forms.Label();
            this.textnombre = new System.Windows.Forms.TextBox();
            this.btnnombre = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblmensaje
            // 
            this.lblmensaje.AutoSize = true;
            this.lblmensaje.Location = new System.Drawing.Point(42, 53);
            this.lblmensaje.Name = "lblmensaje";
            this.lblmensaje.Size = new System.Drawing.Size(118, 16);
            this.lblmensaje.TabIndex = 0;
            this.lblmensaje.Text = "Ingrese su nombre";
            // 
            // textnombre
            // 
            this.textnombre.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.textnombre.Location = new System.Drawing.Point(79, 100);
            this.textnombre.Name = "textnombre";
            this.textnombre.Size = new System.Drawing.Size(100, 22);
            this.textnombre.TabIndex = 1;
            // 
            // btnnombre
            // 
            this.btnnombre.Location = new System.Drawing.Point(85, 157);
            this.btnnombre.Name = "btnnombre";
            this.btnnombre.Size = new System.Drawing.Size(75, 23);
            this.btnnombre.TabIndex = 2;
            this.btnnombre.Text = "salud";
            this.btnnombre.UseVisualStyleBackColor = true;
            this.btnnombre.Click += new System.EventHandler(this.btnnombre_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnnombre);
            this.Controls.Add(this.textnombre);
            this.Controls.Add(this.lblmensaje);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblmensaje;
        private System.Windows.Forms.TextBox textnombre;
        private System.Windows.Forms.Button btnnombre;
    }
}

