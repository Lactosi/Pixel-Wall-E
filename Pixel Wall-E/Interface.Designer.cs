namespace PixelWallE
{
    partial class Interface
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            _syntaxHighlighter?.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.splitContainerEditor = new System.Windows.Forms.SplitContainer();
            this.panelCodeEditor = new System.Windows.Forms.Panel();
            this.txtLineNumbers = new System.Windows.Forms.TextBox();
            this.txtCode = new System.Windows.Forms.RichTextBox();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnExecute = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCanvasSize = new System.Windows.Forms.TextBox();
            this.btnResize = new System.Windows.Forms.Button();
            this.pnlCanvas = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerEditor)).BeginInit();
            this.splitContainerEditor.Panel1.SuspendLayout();
            this.splitContainerEditor.Panel2.SuspendLayout();
            this.splitContainerEditor.SuspendLayout();
            this.panelCodeEditor.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.splitContainerEditor);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.pnlCanvas);
            this.splitContainerMain.Size = new System.Drawing.Size(1184, 661);
            this.splitContainerMain.SplitterDistance = 544;
            this.splitContainerMain.TabIndex = 0;
            // 
            // splitContainerEditor
            // 
            this.splitContainerEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerEditor.Location = new System.Drawing.Point(0, 0);
            this.splitContainerEditor.Name = "splitContainerEditor";
            this.splitContainerEditor.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerEditor.Panel1
            // 
            this.splitContainerEditor.Panel1.Controls.Add(this.panelCodeEditor);
            // 
            // splitContainerEditor.Panel2
            // 
            this.splitContainerEditor.Panel2.Controls.Add(this.panelButtons);
            this.splitContainerEditor.Size = new System.Drawing.Size(544, 661);
            this.splitContainerEditor.SplitterDistance = 600;
            this.splitContainerEditor.TabIndex = 0;
            // 
            // panelCodeEditor
            // 
            this.panelCodeEditor.Controls.Add(this.txtLineNumbers);
            this.panelCodeEditor.Controls.Add(this.txtCode);
            this.panelCodeEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCodeEditor.Location = new System.Drawing.Point(0, 0);
            this.panelCodeEditor.Name = "panelCodeEditor";
            this.panelCodeEditor.Size = new System.Drawing.Size(544, 600);
            this.panelCodeEditor.TabIndex = 0;
            // 
            // txtLineNumbers
            // 
            this.txtLineNumbers.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtLineNumbers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLineNumbers.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtLineNumbers.Font = new System.Drawing.Font("Consolas", 10F);
            this.txtLineNumbers.ForeColor = System.Drawing.SystemColors.GrayText;
            this.txtLineNumbers.Location = new System.Drawing.Point(0, 0);
            this.txtLineNumbers.Margin = new System.Windows.Forms.Padding(0);
            this.txtLineNumbers.Multiline = true;
            this.txtLineNumbers.Name = "txtLineNumbers";
            this.txtLineNumbers.ReadOnly = true;
            this.txtLineNumbers.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtLineNumbers.Size = new System.Drawing.Size(40, 600);
            this.txtLineNumbers.TabIndex = 1;
            this.txtLineNumbers.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtLineNumbers.TabStop = false;
            // 
            // txtCode
            // 
            this.txtCode.BackColor = System.Drawing.Color.Black;
            this.txtCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCode.Font = new System.Drawing.Font("Consolas", 10F);
            this.txtCode.ForeColor = System.Drawing.Color.White;
            this.txtCode.Location = new System.Drawing.Point(40, 0);
            this.txtCode.Margin = new System.Windows.Forms.Padding(0);
            this.txtCode.Name = "txtCode";
            this.txtCode.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Both;
            this.txtCode.Size = new System.Drawing.Size(504, 600);
            this.txtCode.TabIndex = 0;
            this.txtCode.Text = "";
            this.txtCode.WordWrap = false;
            this.txtCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top )
   | System.Windows.Forms.AnchorStyles.Left)
   | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.btnExecute);
            this.panelButtons.Controls.Add(this.btnLoad);
            this.panelButtons.Controls.Add(this.btnSave);
            this.panelButtons.Controls.Add(this.label1);
            this.panelButtons.Controls.Add(this.txtCanvasSize);
            this.panelButtons.Controls.Add(this.btnResize);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelButtons.Location = new System.Drawing.Point(0, 0);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(544, 57);
            this.panelButtons.TabIndex = 0;
            // 
            // btnExecute
            // 
            this.btnExecute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExecute.Location = new System.Drawing.Point(12, 17);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(75, 23);
            this.btnExecute.TabIndex = 0;
            this.btnExecute.Text = "Execute";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLoad.Location = new System.Drawing.Point(93, 17);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(174, 17);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(285, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Canvas Size:";
            // 
            // txtCanvasSize
            // 
            this.txtCanvasSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.txtCanvasSize.Location = new System.Drawing.Point(353, 17);
            this.txtCanvasSize.Name = "txtCanvasSize";
            this.txtCanvasSize.Size = new System.Drawing.Size(60, 20);
            this.txtCanvasSize.TabIndex = 4;
            this.txtCanvasSize.Text = "20";
            // 
            // btnResize
            // 
            this.btnResize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnResize.Location = new System.Drawing.Point(419, 15);
            this.btnResize.Name = "btnResize";
            this.btnResize.Size = new System.Drawing.Size(75, 23);
            this.btnResize.TabIndex = 5;
            this.btnResize.Text = "Resize";
            this.btnResize.UseVisualStyleBackColor = true;
            this.btnResize.Click += new System.EventHandler(this.btnResize_Click);
            // 
            // pnlCanvas
            // 
            this.pnlCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCanvas.Location = new System.Drawing.Point(0, 0);
            this.pnlCanvas.Name = "pnlCanvas";
            this.pnlCanvas.Size = new System.Drawing.Size(636, 661);
            this.pnlCanvas.TabIndex = 0;
            // 
            // Interface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.splitContainerMain);
            this.Name = "Interface";
            this.Text = "Pixel Wall-E";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.splitContainerEditor.Panel1.ResumeLayout(false);
            this.splitContainerEditor.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerEditor)).EndInit();
            this.splitContainerEditor.ResumeLayout(false);
            this.panelCodeEditor.ResumeLayout(false);
            this.panelCodeEditor.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.panelButtons.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.SplitContainer splitContainerEditor;
        private System.Windows.Forms.Panel panelCodeEditor;
        private System.Windows.Forms.TextBox txtLineNumbers;
        private System.Windows.Forms.RichTextBox txtCode;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCanvasSize;
        private System.Windows.Forms.Button btnResize;
        private System.Windows.Forms.Panel pnlCanvas;
    }
}