namespace ExamenChangoJosue
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.miPrimeraFiguraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miSegundaFiguraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Location = new System.Drawing.Point(0, 28);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuStrip2
            // 
            this.menuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miPrimeraFiguraToolStripMenuItem,
            this.miSegundaFiguraToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(800, 28);
            this.menuStrip2.TabIndex = 1;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // miPrimeraFiguraToolStripMenuItem
            // 
            this.miPrimeraFiguraToolStripMenuItem.Name = "miPrimeraFiguraToolStripMenuItem";
            this.miPrimeraFiguraToolStripMenuItem.Size = new System.Drawing.Size(115, 24);
            this.miPrimeraFiguraToolStripMenuItem.Text = "PrimeraFigura";
            this.miPrimeraFiguraToolStripMenuItem.Click += new System.EventHandler(this.miPrimeraFiguraToolStripMenuItem_Click);
            // 
            // miSegundaFiguraToolStripMenuItem
            // 
            this.miSegundaFiguraToolStripMenuItem.Name = "miSegundaFiguraToolStripMenuItem";
            this.miSegundaFiguraToolStripMenuItem.Size = new System.Drawing.Size(122, 24);
            this.miSegundaFiguraToolStripMenuItem.Text = "SegundaFigura";
            this.miSegundaFiguraToolStripMenuItem.Click += new System.EventHandler(this.miSegundaFiguraToolStripMenuItem_Click);
            // 
            // FrmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.menuStrip2);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmPrincipal";
            this.Text = "FrmPrincipal";
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem miPrimeraFiguraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miSegundaFiguraToolStripMenuItem;
    }
}