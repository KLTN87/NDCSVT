
namespace Grabcut
{
    partial class frmGrabCut
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findContoursToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grabCutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.brightnessAndToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbMinConstast = new System.Windows.Forms.Label();
            this.lbCurrentContrast = new System.Windows.Forms.Label();
            this.lbMaxContrast = new System.Windows.Forms.Label();
            this.lbMaxBrightness = new System.Windows.Forms.Label();
            this.lbCurrentBrightness = new System.Windows.Forms.Label();
            this.lbMinBrightness = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.listBox1 = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureBox1.Location = new System.Drawing.Point(66, 43);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(200, 130);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(854, 24);
            this.menuStrip1.TabIndex = 5;
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
            this.grayToolStripMenuItem,
            this.findContoursToolStripMenuItem,
            this.grabCutToolStripMenuItem,
            this.brightnessAndToolStripMenuItem});
            this.toolToolStripMenuItem.Name = "toolToolStripMenuItem";
            this.toolToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.toolToolStripMenuItem.Text = "Tool";
            // 
            // grayToolStripMenuItem
            // 
            this.grayToolStripMenuItem.Name = "grayToolStripMenuItem";
            this.grayToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.grayToolStripMenuItem.Text = "Gray";
            this.grayToolStripMenuItem.Click += new System.EventHandler(this.grayToolStripMenuItem_Click);
            // 
            // findContoursToolStripMenuItem
            // 
            this.findContoursToolStripMenuItem.Name = "findContoursToolStripMenuItem";
            this.findContoursToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.findContoursToolStripMenuItem.Text = "Find contours";
            this.findContoursToolStripMenuItem.Click += new System.EventHandler(this.findContoursToolStripMenuItem_Click);
            // 
            // grabCutToolStripMenuItem
            // 
            this.grabCutToolStripMenuItem.Name = "grabCutToolStripMenuItem";
            this.grabCutToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.grabCutToolStripMenuItem.Text = "GrabCut";
            // 
            // brightnessAndToolStripMenuItem
            // 
            this.brightnessAndToolStripMenuItem.Name = "brightnessAndToolStripMenuItem";
            this.brightnessAndToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.brightnessAndToolStripMenuItem.Text = "Brightness and Contrast";
            this.brightnessAndToolStripMenuItem.Click += new System.EventHandler(this.brightnessAndToolStripMenuItem_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureBox2.Location = new System.Drawing.Point(286, 43);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(155, 130);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(179, 368);
            this.trackBar1.Maximum = 500;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(463, 45);
            this.trackBar1.TabIndex = 7;
            this.trackBar1.Value = 100;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(741, 77);
            this.trackBar2.Maximum = 100;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar2.Size = new System.Drawing.Size(45, 268);
            this.trackBar2.TabIndex = 8;
            this.trackBar2.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(720, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Brightness";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(160, 352);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Contrast";
            // 
            // lbMinConstast
            // 
            this.lbMinConstast.AutoSize = true;
            this.lbMinConstast.Location = new System.Drawing.Point(179, 399);
            this.lbMinConstast.Name = "lbMinConstast";
            this.lbMinConstast.Size = new System.Drawing.Size(35, 13);
            this.lbMinConstast.TabIndex = 11;
            this.lbMinConstast.Text = "label3";
            // 
            // lbCurrentContrast
            // 
            this.lbCurrentContrast.AutoSize = true;
            this.lbCurrentContrast.Location = new System.Drawing.Point(381, 400);
            this.lbCurrentContrast.Name = "lbCurrentContrast";
            this.lbCurrentContrast.Size = new System.Drawing.Size(35, 13);
            this.lbCurrentContrast.TabIndex = 12;
            this.lbCurrentContrast.Text = "label4";
            // 
            // lbMaxContrast
            // 
            this.lbMaxContrast.AutoSize = true;
            this.lbMaxContrast.Location = new System.Drawing.Point(607, 399);
            this.lbMaxContrast.Name = "lbMaxContrast";
            this.lbMaxContrast.Size = new System.Drawing.Size(35, 13);
            this.lbMaxContrast.TabIndex = 13;
            this.lbMaxContrast.Text = "label5";
            // 
            // lbMaxBrightness
            // 
            this.lbMaxBrightness.AutoSize = true;
            this.lbMaxBrightness.Location = new System.Drawing.Point(778, 77);
            this.lbMaxBrightness.Name = "lbMaxBrightness";
            this.lbMaxBrightness.Size = new System.Drawing.Size(35, 13);
            this.lbMaxBrightness.TabIndex = 14;
            this.lbMaxBrightness.Text = "label6";
            // 
            // lbCurrentBrightness
            // 
            this.lbCurrentBrightness.AutoSize = true;
            this.lbCurrentBrightness.Location = new System.Drawing.Point(778, 194);
            this.lbCurrentBrightness.Name = "lbCurrentBrightness";
            this.lbCurrentBrightness.Size = new System.Drawing.Size(35, 13);
            this.lbCurrentBrightness.TabIndex = 15;
            this.lbCurrentBrightness.Text = "label7";
            // 
            // lbMinBrightness
            // 
            this.lbMinBrightness.AutoSize = true;
            this.lbMinBrightness.Location = new System.Drawing.Point(778, 323);
            this.lbMinBrightness.Name = "lbMinBrightness";
            this.lbMinBrightness.Size = new System.Drawing.Size(35, 13);
            this.lbMinBrightness.TabIndex = 16;
            this.lbMinBrightness.Text = "label8";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Location = new System.Drawing.Point(35, 194);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(417, 100);
            this.panel1.TabIndex = 17;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(469, 101);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(266, 160);
            this.listBox1.TabIndex = 18;
            // 
            // GrabCut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 426);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lbMinBrightness);
            this.Controls.Add(this.lbCurrentBrightness);
            this.Controls.Add(this.lbMaxBrightness);
            this.Controls.Add(this.lbMaxContrast);
            this.Controls.Add(this.lbCurrentContrast);
            this.Controls.Add(this.lbMinConstast);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBar2);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GrabCut";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem grayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findContoursToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem grabCutToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ToolStripMenuItem brightnessAndToolStripMenuItem;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbMinConstast;
        private System.Windows.Forms.Label lbCurrentContrast;
        private System.Windows.Forms.Label lbMaxContrast;
        private System.Windows.Forms.Label lbMaxBrightness;
        private System.Windows.Forms.Label lbCurrentBrightness;
        private System.Windows.Forms.Label lbMinBrightness;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox listBox1;
    }
}

