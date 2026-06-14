namespace OperacionDDA
{
    partial class FrmMenu
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
            this.algoritmosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dDAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.circunferenciaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rellenoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recorteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recorteLineaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recorteFiguraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.curvasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bezierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bezier3PToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.algoritmosToolStripMenuItem,
            this.recorteToolStripMenuItem,
            this.curvasToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(922, 30);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // algoritmosToolStripMenuItem
            // 
            this.algoritmosToolStripMenuItem.BackColor = System.Drawing.Color.Bisque;
            this.algoritmosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dDAToolStripMenuItem,
            this.circunferenciaToolStripMenuItem,
            this.rellenoToolStripMenuItem});
            this.algoritmosToolStripMenuItem.Name = "algoritmosToolStripMenuItem";
            this.algoritmosToolStripMenuItem.Size = new System.Drawing.Size(97, 24);
            this.algoritmosToolStripMenuItem.Text = "Algoritmos";
            // 
            // dDAToolStripMenuItem
            // 
            this.dDAToolStripMenuItem.Name = "dDAToolStripMenuItem";
            this.dDAToolStripMenuItem.Size = new System.Drawing.Size(186, 26);
            this.dDAToolStripMenuItem.Text = "DDA";
            this.dDAToolStripMenuItem.Click += new System.EventHandler(this.dDAToolStripMenuItem_Click);
            // 
            // circunferenciaToolStripMenuItem
            // 
            this.circunferenciaToolStripMenuItem.Name = "circunferenciaToolStripMenuItem";
            this.circunferenciaToolStripMenuItem.Size = new System.Drawing.Size(186, 26);
            this.circunferenciaToolStripMenuItem.Text = "Circunferencia";
            this.circunferenciaToolStripMenuItem.Click += new System.EventHandler(this.circunferenciaToolStripMenuItem_Click);
            // 
            // rellenoToolStripMenuItem
            // 
            this.rellenoToolStripMenuItem.Name = "rellenoToolStripMenuItem";
            this.rellenoToolStripMenuItem.Size = new System.Drawing.Size(186, 26);
            this.rellenoToolStripMenuItem.Text = "Relleno";
            this.rellenoToolStripMenuItem.Click += new System.EventHandler(this.rellenoToolStripMenuItem_Click);
            // 
            // recorteToolStripMenuItem
            // 
            this.recorteToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.recorteLineaToolStripMenuItem,
            this.recorteFiguraToolStripMenuItem});
            this.recorteToolStripMenuItem.Name = "recorteToolStripMenuItem";
            this.recorteToolStripMenuItem.Size = new System.Drawing.Size(74, 24);
            this.recorteToolStripMenuItem.Text = "Recorte";
            this.recorteToolStripMenuItem.Click += new System.EventHandler(this.recorteToolStripMenuItem_Click);
            // 
            // recorteLineaToolStripMenuItem
            // 
            this.recorteLineaToolStripMenuItem.Name = "recorteLineaToolStripMenuItem";
            this.recorteLineaToolStripMenuItem.Size = new System.Drawing.Size(188, 26);
            this.recorteLineaToolStripMenuItem.Text = "Recorte linea";
            this.recorteLineaToolStripMenuItem.Click += new System.EventHandler(this.recorteLineaToolStripMenuItem_Click);
            // 
            // recorteFiguraToolStripMenuItem
            // 
            this.recorteFiguraToolStripMenuItem.Name = "recorteFiguraToolStripMenuItem";
            this.recorteFiguraToolStripMenuItem.Size = new System.Drawing.Size(188, 26);
            this.recorteFiguraToolStripMenuItem.Text = "Recorte Figura";
            this.recorteFiguraToolStripMenuItem.Click += new System.EventHandler(this.recorteFiguraToolStripMenuItem_Click);
            // 
            // curvasToolStripMenuItem
            // 
            this.curvasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bezierToolStripMenuItem,
            this.bezier3PToolStripMenuItem});
            this.curvasToolStripMenuItem.Name = "curvasToolStripMenuItem";
            this.curvasToolStripMenuItem.Size = new System.Drawing.Size(66, 24);
            this.curvasToolStripMenuItem.Text = "Curvas";
            // 
            // bezierToolStripMenuItem
            // 
            this.bezierToolStripMenuItem.Name = "bezierToolStripMenuItem";
            this.bezierToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.bezierToolStripMenuItem.Text = "Bezier2P";
            this.bezierToolStripMenuItem.Click += new System.EventHandler(this.bezierToolStripMenuItem_Click);
            // 
            // bezier3PToolStripMenuItem
            // 
            this.bezier3PToolStripMenuItem.Name = "bezier3PToolStripMenuItem";
            this.bezier3PToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.bezier3PToolStripMenuItem.Text = "Bezier3P";
            this.bezier3PToolStripMenuItem.Click += new System.EventHandler(this.bezier3PToolStripMenuItem_Click);
            // 
            // FrmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.ClientSize = new System.Drawing.Size(922, 637);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Segoe Script", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menu";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem algoritmosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dDAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem circunferenciaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rellenoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recorteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recorteLineaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recorteFiguraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem curvasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bezierToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bezier3PToolStripMenuItem;
    }
}