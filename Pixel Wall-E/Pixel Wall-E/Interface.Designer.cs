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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.panelCodeEditor = new System.Windows.Forms.Panel();
            this.txtLineNumbers = new System.Windows.Forms.TextBox();
            this.txtCode = new System.Windows.Forms.RichTextBox();
            this.pnlCanvas = new System.Windows.Forms.Panel();
            this.btnExecute = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCanvasSize = new System.Windows.Forms.TextBox();
            this.btnResize = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panelCodeEditor.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(40, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnlCanvas);
            this.splitContainer1.Size = new System.Drawing.Size(1184, 661);
            this.splitContainer1.SplitterDistance = 544;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.panelCodeEditor);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.btnExecute);
            this.splitContainer2.Panel2.Controls.Add(this.btnLoad);
            this.splitContainer2.Panel2.Controls.Add(this.btnSave);
            this.splitContainer2.Panel2.Controls.Add(this.label1);
            this.splitContainer2.Panel2.Controls.Add(this.txtCanvasSize);
            this.splitContainer2.Panel2.Controls.Add(this.btnResize);
            this.splitContainer2.Size = new System.Drawing.Size(544, 661);
            this.splitContainer2.SplitterDistance = 500; // Reducido para dar más espacio a los botones
            this.splitContainer2.TabIndex = 0;
            // 
            // panelCodeEditor
            // 
            this.panelCodeEditor.Controls.Add(this.txtLineNumbers);
            this.panelCodeEditor.Controls.Add(this.txtCode);
            this.panelCodeEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCodeEditor.Location = new System.Drawing.Point(0, 0);
            this.panelCodeEditor.Name = "panelCodeEditor";
            this.panelCodeEditor.Size = new System.Drawing.Size(544, 561);
            this.panelCodeEditor.TabIndex = 2;
            // 
            // txtCode
            // 
            this.txtCode.BackColor = System.Drawing.Color.Black;
            this.txtCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCode.ForeColor = System.Drawing.Color.White;
            this.txtCode.Font = new System.Drawing.Font("Consolas", 10F);
            this.txtCode.Location = new System.Drawing.Point(40, 0);
            this.txtCode.Margin = new System.Windows.Forms.Padding(0);
            this.txtCode.Multiline = true;
            this.txtCode.Name = "txtCode";
            this.txtCode.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Both;
            this.txtCode.Size = new System.Drawing.Size(504, 561);
            this.txtCode.TabIndex = 0;
            this.txtCode.WordWrap = false;
            this.txtCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
    | System.Windows.Forms.AnchorStyles.Left)
    | System.Windows.Forms.AnchorStyles.Right)));

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
            this.txtLineNumbers.Size = new System.Drawing.Size(40, 561);
            this.txtLineNumbers.TabIndex = 1;
            this.txtLineNumbers.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtLineNumbers.TabStop = false;

            // 
            // pnlCanvas
            // 
            this.pnlCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCanvas.Location = new System.Drawing.Point(0, 0);
            this.pnlCanvas.Name = "pnlCanvas";
            this.pnlCanvas.Size = new System.Drawing.Size(596, 661);
            this.pnlCanvas.TabIndex = 0;
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(12, 7);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(75, 23);
            this.btnExecute.TabIndex = 0;
            this.btnExecute.Text = "Execute";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(93, 7);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(174, 7);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(285, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Canvas Size:";
            // 
            // txtCanvasSize
            // 
            this.txtCanvasSize.Location = new System.Drawing.Point(353, 9);
            this.txtCanvasSize.Name = "txtCanvasSize";
            this.txtCanvasSize.Size = new System.Drawing.Size(60, 20);
            this.txtCanvasSize.TabIndex = 4;
            this.txtCanvasSize.Text = "20";
            // 
            // btnResize
            // 
            this.btnResize.Location = new System.Drawing.Point(419, 7);
            this.btnResize.Name = "btnResize";
            this.btnResize.Size = new System.Drawing.Size(75, 23);
            this.btnResize.TabIndex = 5;
            this.btnResize.Text = "Resize";
            this.btnResize.UseVisualStyleBackColor = true;
            this.btnResize.Click += new System.EventHandler(this.btnResize_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MainForm";
            this.Text = "Pixel Wall-E";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.panelCodeEditor.ResumeLayout(false);
            this.panelCodeEditor.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.RichTextBox txtCode;
        private System.Windows.Forms.Panel pnlCanvas;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCanvasSize;
        private System.Windows.Forms.Button btnResize;
        private System.Windows.Forms.TextBox txtLineNumbers;
    }
}