namespace CtpBindingGen
{
    partial class Formmain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageCls = new System.Windows.Forms.TabPage();
            this.tabPageMacro = new System.Windows.Forms.TabPage();
            this.textBoxDir = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBoxCls = new System.Windows.Forms.RichTextBox();
            this.richTextBoxMacro = new System.Windows.Forms.RichTextBox();
            this.buttonGo = new System.Windows.Forms.Button();
            this.tabPageProto = new System.Windows.Forms.TabPage();
            this.tabPageFuncCSharp = new System.Windows.Forms.TabPage();
            this.richTextBoxProto = new System.Windows.Forms.RichTextBox();
            this.richTextBoxCSharpTrdFunc = new System.Windows.Forms.RichTextBox();
            this.tabPageCppFunc = new System.Windows.Forms.TabPage();
            this.richTextBoxCppFunc = new System.Windows.Forms.RichTextBox();
            this.tabPageCppProtoGen = new System.Windows.Forms.TabPage();
            this.richTextBoxCppProtoGen = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageCls.SuspendLayout();
            this.tabPageMacro.SuspendLayout();
            this.tabPageProto.SuspendLayout();
            this.tabPageFuncCSharp.SuspendLayout();
            this.tabPageCppFunc.SuspendLayout();
            this.tabPageCppProtoGen.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonGo);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBoxDir);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(978, 54);
            this.panel1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageCls);
            this.tabControl1.Controls.Add(this.tabPageMacro);
            this.tabControl1.Controls.Add(this.tabPageProto);
            this.tabControl1.Controls.Add(this.tabPageCppProtoGen);
            this.tabControl1.Controls.Add(this.tabPageFuncCSharp);
            this.tabControl1.Controls.Add(this.tabPageCppFunc);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 54);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(978, 491);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPageCls
            // 
            this.tabPageCls.Controls.Add(this.richTextBoxCls);
            this.tabPageCls.Location = new System.Drawing.Point(4, 25);
            this.tabPageCls.Name = "tabPageCls";
            this.tabPageCls.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCls.Size = new System.Drawing.Size(970, 462);
            this.tabPageCls.TabIndex = 0;
            this.tabPageCls.Text = "类";
            this.tabPageCls.UseVisualStyleBackColor = true;
            // 
            // tabPageMacro
            // 
            this.tabPageMacro.Controls.Add(this.richTextBoxMacro);
            this.tabPageMacro.Location = new System.Drawing.Point(4, 25);
            this.tabPageMacro.Name = "tabPageMacro";
            this.tabPageMacro.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMacro.Size = new System.Drawing.Size(970, 462);
            this.tabPageMacro.TabIndex = 1;
            this.tabPageMacro.Text = "宏";
            this.tabPageMacro.UseVisualStyleBackColor = true;
            // 
            // textBoxDir
            // 
            this.textBoxDir.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDir.Location = new System.Drawing.Point(70, 14);
            this.textBoxDir.Name = "textBoxDir";
            this.textBoxDir.Size = new System.Drawing.Size(790, 25);
            this.textBoxDir.TabIndex = 0;
            this.textBoxDir.Text = "G:\\work\\stk\\Yz.Jy\\3rd\\ctplib\\20180109_tradeapi64_windows";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "头文件";
            // 
            // richTextBoxCls
            // 
            this.richTextBoxCls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxCls.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxCls.Name = "richTextBoxCls";
            this.richTextBoxCls.ReadOnly = true;
            this.richTextBoxCls.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.richTextBoxCls.Size = new System.Drawing.Size(964, 456);
            this.richTextBoxCls.TabIndex = 0;
            this.richTextBoxCls.Text = "";
            // 
            // richTextBoxMacro
            // 
            this.richTextBoxMacro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxMacro.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxMacro.Name = "richTextBoxMacro";
            this.richTextBoxMacro.ReadOnly = true;
            this.richTextBoxMacro.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.richTextBoxMacro.Size = new System.Drawing.Size(964, 456);
            this.richTextBoxMacro.TabIndex = 1;
            this.richTextBoxMacro.Text = "";
            // 
            // buttonGo
            // 
            this.buttonGo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGo.Location = new System.Drawing.Point(870, 15);
            this.buttonGo.Name = "buttonGo";
            this.buttonGo.Size = new System.Drawing.Size(86, 23);
            this.buttonGo.TabIndex = 2;
            this.buttonGo.Text = "生成";
            this.buttonGo.UseVisualStyleBackColor = true;
            this.buttonGo.Click += new System.EventHandler(this.buttonGo_Click);
            // 
            // tabPageProto
            // 
            this.tabPageProto.Controls.Add(this.richTextBoxProto);
            this.tabPageProto.Location = new System.Drawing.Point(4, 25);
            this.tabPageProto.Name = "tabPageProto";
            this.tabPageProto.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageProto.Size = new System.Drawing.Size(970, 462);
            this.tabPageProto.TabIndex = 2;
            this.tabPageProto.Text = "Proto定义";
            this.tabPageProto.UseVisualStyleBackColor = true;
            // 
            // tabPageFuncCSharp
            // 
            this.tabPageFuncCSharp.Controls.Add(this.richTextBoxCSharpTrdFunc);
            this.tabPageFuncCSharp.Location = new System.Drawing.Point(4, 25);
            this.tabPageFuncCSharp.Name = "tabPageFuncCSharp";
            this.tabPageFuncCSharp.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFuncCSharp.Size = new System.Drawing.Size(970, 462);
            this.tabPageFuncCSharp.TabIndex = 3;
            this.tabPageFuncCSharp.Text = "函数(C#)";
            this.tabPageFuncCSharp.UseVisualStyleBackColor = true;
            // 
            // richTextBoxProto
            // 
            this.richTextBoxProto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxProto.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxProto.Name = "richTextBoxProto";
            this.richTextBoxProto.ReadOnly = true;
            this.richTextBoxProto.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.richTextBoxProto.Size = new System.Drawing.Size(964, 456);
            this.richTextBoxProto.TabIndex = 2;
            this.richTextBoxProto.Text = "";
            // 
            // richTextBoxCSharpTrdFunc
            // 
            this.richTextBoxCSharpTrdFunc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxCSharpTrdFunc.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxCSharpTrdFunc.Name = "richTextBoxCSharpTrdFunc";
            this.richTextBoxCSharpTrdFunc.ReadOnly = true;
            this.richTextBoxCSharpTrdFunc.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.richTextBoxCSharpTrdFunc.Size = new System.Drawing.Size(964, 456);
            this.richTextBoxCSharpTrdFunc.TabIndex = 2;
            this.richTextBoxCSharpTrdFunc.Text = "";
            // 
            // tabPageCppFunc
            // 
            this.tabPageCppFunc.Controls.Add(this.richTextBoxCppFunc);
            this.tabPageCppFunc.Location = new System.Drawing.Point(4, 25);
            this.tabPageCppFunc.Name = "tabPageCppFunc";
            this.tabPageCppFunc.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCppFunc.Size = new System.Drawing.Size(970, 462);
            this.tabPageCppFunc.TabIndex = 4;
            this.tabPageCppFunc.Text = "函数(C++)";
            this.tabPageCppFunc.UseVisualStyleBackColor = true;
            // 
            // richTextBoxCppFunc
            // 
            this.richTextBoxCppFunc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxCppFunc.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxCppFunc.Name = "richTextBoxCppFunc";
            this.richTextBoxCppFunc.ReadOnly = true;
            this.richTextBoxCppFunc.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.richTextBoxCppFunc.Size = new System.Drawing.Size(964, 456);
            this.richTextBoxCppFunc.TabIndex = 3;
            this.richTextBoxCppFunc.Text = "";
            // 
            // tabPageCppProtoGen
            // 
            this.tabPageCppProtoGen.Controls.Add(this.richTextBoxCppProtoGen);
            this.tabPageCppProtoGen.Location = new System.Drawing.Point(4, 25);
            this.tabPageCppProtoGen.Name = "tabPageCppProtoGen";
            this.tabPageCppProtoGen.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCppProtoGen.Size = new System.Drawing.Size(970, 462);
            this.tabPageCppProtoGen.TabIndex = 5;
            this.tabPageCppProtoGen.Text = "Proto(Cpp)";
            this.tabPageCppProtoGen.UseVisualStyleBackColor = true;
            // 
            // richTextBoxCppProtoGen
            // 
            this.richTextBoxCppProtoGen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxCppProtoGen.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxCppProtoGen.Name = "richTextBoxCppProtoGen";
            this.richTextBoxCppProtoGen.ReadOnly = true;
            this.richTextBoxCppProtoGen.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.richTextBoxCppProtoGen.Size = new System.Drawing.Size(964, 456);
            this.richTextBoxCppProtoGen.TabIndex = 3;
            this.richTextBoxCppProtoGen.Text = "";
            // 
            // Formmain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 545);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Name = "Formmain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CtpBingingGen";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPageCls.ResumeLayout(false);
            this.tabPageMacro.ResumeLayout(false);
            this.tabPageProto.ResumeLayout(false);
            this.tabPageFuncCSharp.ResumeLayout(false);
            this.tabPageCppFunc.ResumeLayout(false);
            this.tabPageCppProtoGen.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageCls;
        private System.Windows.Forms.TabPage tabPageMacro;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxDir;
        private System.Windows.Forms.RichTextBox richTextBoxCls;
        private System.Windows.Forms.RichTextBox richTextBoxMacro;
        private System.Windows.Forms.Button buttonGo;
        private System.Windows.Forms.TabPage tabPageProto;
        private System.Windows.Forms.RichTextBox richTextBoxProto;
        private System.Windows.Forms.TabPage tabPageFuncCSharp;
        private System.Windows.Forms.RichTextBox richTextBoxCSharpTrdFunc;
        private System.Windows.Forms.TabPage tabPageCppFunc;
        private System.Windows.Forms.RichTextBox richTextBoxCppFunc;
        private System.Windows.Forms.TabPage tabPageCppProtoGen;
        private System.Windows.Forms.RichTextBox richTextBoxCppProtoGen;
    }
}

