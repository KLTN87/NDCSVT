namespace Grabcut
{
    partial class frmHOG
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hOGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hOG36FeatureValuesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hOG144FeatureValuesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hOG576FeatureValuesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hOG2304FeatureValuesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hOG5832FeatureValuesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.hOG9216FeatureValuesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button2 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.imageBox1 = new Emgu.CV.UI.ImageBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolToolStripMenuItem
            // 
            this.toolToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hOGToolStripMenuItem});
            this.toolToolStripMenuItem.Name = "toolToolStripMenuItem";
            this.toolToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.toolToolStripMenuItem.Text = "Tool";
            // 
            // hOGToolStripMenuItem
            // 
            this.hOGToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hOG36FeatureValuesToolStripMenuItem,
            this.hOG144FeatureValuesToolStripMenuItem,
            this.hOG576FeatureValuesToolStripMenuItem,
            this.hOG2304FeatureValuesToolStripMenuItem,
            this.hOG5832FeatureValuesToolStripMenuItem1,
            this.hOG9216FeatureValuesToolStripMenuItem});
            this.hOGToolStripMenuItem.Name = "hOGToolStripMenuItem";
            this.hOGToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.hOGToolStripMenuItem.Text = "HOG";
            // 
            // hOG36FeatureValuesToolStripMenuItem
            // 
            this.hOG36FeatureValuesToolStripMenuItem.Name = "hOG36FeatureValuesToolStripMenuItem";
            this.hOG36FeatureValuesToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.hOG36FeatureValuesToolStripMenuItem.Text = "HOG 36 feature values";
            this.hOG36FeatureValuesToolStripMenuItem.Click += new System.EventHandler(this.hOG36FeatureValuesToolStripMenuItem_Click);
            // 
            // hOG144FeatureValuesToolStripMenuItem
            // 
            this.hOG144FeatureValuesToolStripMenuItem.Name = "hOG144FeatureValuesToolStripMenuItem";
            this.hOG144FeatureValuesToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.hOG144FeatureValuesToolStripMenuItem.Text = "HOG 144 feature values";
            this.hOG144FeatureValuesToolStripMenuItem.Click += new System.EventHandler(this.hOG144FeatureValuesToolStripMenuItem_Click);
            // 
            // hOG576FeatureValuesToolStripMenuItem
            // 
            this.hOG576FeatureValuesToolStripMenuItem.Name = "hOG576FeatureValuesToolStripMenuItem";
            this.hOG576FeatureValuesToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.hOG576FeatureValuesToolStripMenuItem.Text = "HOG 576 feature values";
            this.hOG576FeatureValuesToolStripMenuItem.Click += new System.EventHandler(this.hOG576FeatureValuesToolStripMenuItem_Click);
            // 
            // hOG2304FeatureValuesToolStripMenuItem
            // 
            this.hOG2304FeatureValuesToolStripMenuItem.Name = "hOG2304FeatureValuesToolStripMenuItem";
            this.hOG2304FeatureValuesToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.hOG2304FeatureValuesToolStripMenuItem.Text = "HOG 2304 feature values";
            this.hOG2304FeatureValuesToolStripMenuItem.Click += new System.EventHandler(this.hOG2304FeatureValuesToolStripMenuItem_Click);
            // 
            // hOG5832FeatureValuesToolStripMenuItem1
            // 
            this.hOG5832FeatureValuesToolStripMenuItem1.Name = "hOG5832FeatureValuesToolStripMenuItem1";
            this.hOG5832FeatureValuesToolStripMenuItem1.Size = new System.Drawing.Size(209, 22);
            this.hOG5832FeatureValuesToolStripMenuItem1.Text = "HOG 5832 feature values";
            this.hOG5832FeatureValuesToolStripMenuItem1.Click += new System.EventHandler(this.hOG5832FeatureValuesToolStripMenuItem1_Click);
            // 
            // hOG9216FeatureValuesToolStripMenuItem
            // 
            this.hOG9216FeatureValuesToolStripMenuItem.Name = "hOG9216FeatureValuesToolStripMenuItem";
            this.hOG9216FeatureValuesToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.hOG9216FeatureValuesToolStripMenuItem.Text = "HOG 11664 feature values";
            this.hOG9216FeatureValuesToolStripMenuItem.Click += new System.EventHandler(this.hOG9216FeatureValuesToolStripMenuItem_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 206);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(41, 54);
            this.button2.TabIndex = 10;
            this.button2.Text = " ";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(0, 290);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(800, 160);
            this.richTextBox1.TabIndex = 9;
            this.richTextBox1.Text = "";
            // 
            // imageBox1
            // 
            this.imageBox1.Location = new System.Drawing.Point(114, 30);
            this.imageBox1.Name = "imageBox1";
            this.imageBox1.Size = new System.Drawing.Size(567, 245);
            this.imageBox1.TabIndex = 8;
            this.imageBox1.TabStop = false;
            // 
            // HOG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.imageBox1);
            this.Name = "HOG";
            this.Text = "HOG";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hOGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hOG36FeatureValuesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hOG144FeatureValuesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hOG576FeatureValuesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hOG2304FeatureValuesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hOG5832FeatureValuesToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem hOG9216FeatureValuesToolStripMenuItem;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private Emgu.CV.UI.ImageBox imageBox1;
    }
}