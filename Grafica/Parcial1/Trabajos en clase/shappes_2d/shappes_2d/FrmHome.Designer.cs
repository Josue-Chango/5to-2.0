namespace shappes_2d
{
    partial class FrmHome
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
            this.miGraphicsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miExampleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miRectanguloToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miGraphicsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // miGraphicsToolStripMenuItem
            // 
            this.miGraphicsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miExampleToolStripMenuItem,
            this.miRectanguloToolStripMenuItem});
            this.miGraphicsToolStripMenuItem.Name = "miGraphicsToolStripMenuItem";
            this.miGraphicsToolStripMenuItem.Size = new System.Drawing.Size(80, 24);
            this.miGraphicsToolStripMenuItem.Text = "Graphics";
            // 
            // miExampleToolStripMenuItem
            // 
            this.miExampleToolStripMenuItem.Name = "miExampleToolStripMenuItem";
            this.miExampleToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.miExampleToolStripMenuItem.Text = "Example";
            this.miExampleToolStripMenuItem.Click += new System.EventHandler(this.miExampleToolStripMenuItem_Click);
            // 
            // miRectanguloToolStripMenuItem
            // 
            this.miRectanguloToolStripMenuItem.Name = "miRectanguloToolStripMenuItem";
            this.miRectanguloToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.miRectanguloToolStripMenuItem.Text = "Rectangulo";
            this.miRectanguloToolStripMenuItem.Click += new System.EventHandler(this.rectToolStripMenuItem_Click);
            // 
            // FrmHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmHome";
            this.Text = "Home";
            this.Load += new System.EventHandler(this.FrmHome_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem miGraphicsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miExampleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miRectanguloToolStripMenuItem;
    }
}