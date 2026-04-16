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
            this.miTrianguloToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(740, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // miGraphicsToolStripMenuItem
            // 
            this.miGraphicsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miExampleToolStripMenuItem,
            this.miRectanguloToolStripMenuItem,
            this.miTrianguloToolStripMenuItem});
            this.miGraphicsToolStripMenuItem.Name = "miGraphicsToolStripMenuItem";
            this.miGraphicsToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.miGraphicsToolStripMenuItem.Text = "Graphics";
            // 
            // miExampleToolStripMenuItem
            // 
            this.miExampleToolStripMenuItem.Name = "miExampleToolStripMenuItem";
            this.miExampleToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.miExampleToolStripMenuItem.Text = "Example";
            this.miExampleToolStripMenuItem.Click += new System.EventHandler(this.miExampleToolStripMenuItem_Click);
            // 
            // miRectanguloToolStripMenuItem
            // 
            this.miRectanguloToolStripMenuItem.Name = "miRectanguloToolStripMenuItem";
            this.miRectanguloToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.miRectanguloToolStripMenuItem.Text = "Rectangulo";
            this.miRectanguloToolStripMenuItem.Click += new System.EventHandler(this.rectToolStripMenuItem_Click);
            // 
            // miTrianguloToolStripMenuItem
            // 
            this.miTrianguloToolStripMenuItem.Name = "miTrianguloToolStripMenuItem";
            this.miTrianguloToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.miTrianguloToolStripMenuItem.Text = "Triangulo";
            this.miTrianguloToolStripMenuItem.Click += new System.EventHandler(this.miTrianguloToolStripMenuItem_Click);
            // 
            // FrmHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 427);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
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
        private System.Windows.Forms.ToolStripMenuItem miTrianguloToolStripMenuItem;
    }
}