﻿namespace CheckDateDifferent
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.SignRunTime = new System.Windows.Forms.Label();
            this.txtFileBPath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SignTimeNew = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.BeginSearch = new System.Windows.Forms.Button();
            this.txtFileAPath = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.SignResulteTxt = new System.Windows.Forms.TextBox();
            this.SaveSignFileResulte = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.SignFileTxt = new System.Windows.Forms.TextBox();
            this.ChoessSighFile = new System.Windows.Forms.Button();
            this.timernew = new System.Windows.Forms.Timer(this.components);
            this.OFTwo = new System.Windows.Forms.OpenFileDialog();
            this.OFThree = new System.Windows.Forms.OpenFileDialog();
            this.SelectFileA = new System.Windows.Forms.Button();
            this.OFOne = new System.Windows.Forms.OpenFileDialog();
            this.runningTime = new System.Windows.Forms.Label();
            this.CheckDate = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SelectFileB = new System.Windows.Forms.Button();
            this.SaveResulteA = new System.Windows.Forms.TextBox();
            this.SaveAFile = new System.Windows.Forms.Button();
            this.resulteLable = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.RightShow = new System.Windows.Forms.Label();
            this.TimeNew = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tpTwoFileCompare = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpOneFileCompare = new System.Windows.Forms.TabPage();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tpTwoFileCompare.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpOneFileCompare.SuspendLayout();
            this.SuspendLayout();
            // 
            // SignRunTime
            // 
            this.SignRunTime.AutoSize = true;
            this.SignRunTime.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SignRunTime.ForeColor = System.Drawing.Color.Red;
            this.SignRunTime.Location = new System.Drawing.Point(12, 208);
            this.SignRunTime.Name = "SignRunTime";
            this.SignRunTime.Size = new System.Drawing.Size(0, 12);
            this.SignRunTime.TabIndex = 28;
            // 
            // txtFileBPath
            // 
            this.txtFileBPath.Location = new System.Drawing.Point(212, 84);
            this.txtFileBPath.Name = "txtFileBPath";
            this.txtFileBPath.Size = new System.Drawing.Size(538, 21);
            this.txtFileBPath.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(485, 12);
            this.label4.TabIndex = 26;
            this.label4.Text = "温馨提示：请确定您对所选文件或者文件夹有读写权限。结果文件如果已经存在将被覆盖！";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label5.Location = new System.Drawing.Point(454, 290);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(305, 12);
            this.label5.TabIndex = 25;
            this.label5.Text = "免责申明：如果您有任何问题请联系满哥，通通给你搞定";
            // 
            // SignTimeNew
            // 
            this.SignTimeNew.AutoSize = true;
            this.SignTimeNew.Location = new System.Drawing.Point(76, 286);
            this.SignTimeNew.Name = "SignTimeNew";
            this.SignTimeNew.Size = new System.Drawing.Size(0, 12);
            this.SignTimeNew.TabIndex = 24;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 287);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 23;
            this.label7.Text = "当前时间：";
            // 
            // BeginSearch
            // 
            this.BeginSearch.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.BeginSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BeginSearch.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.BeginSearch.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BeginSearch.ForeColor = System.Drawing.SystemColors.ControlText;
            this.BeginSearch.Location = new System.Drawing.Point(635, 235);
            this.BeginSearch.Name = "BeginSearch";
            this.BeginSearch.Size = new System.Drawing.Size(124, 52);
            this.BeginSearch.TabIndex = 22;
            this.BeginSearch.Text = "开始查找";
            this.BeginSearch.UseVisualStyleBackColor = false;
            this.BeginSearch.Click += new System.EventHandler(this.BeginSearchClick);
            // 
            // txtFileAPath
            // 
            this.txtFileAPath.Location = new System.Drawing.Point(212, 28);
            this.txtFileAPath.Name = "txtFileAPath";
            this.txtFileAPath.Size = new System.Drawing.Size(538, 21);
            this.txtFileAPath.TabIndex = 3;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.SignResulteTxt);
            this.groupBox4.Controls.Add(this.SaveSignFileResulte);
            this.groupBox4.ForeColor = System.Drawing.Color.Blue;
            this.groupBox4.Location = new System.Drawing.Point(3, 74);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(756, 67);
            this.groupBox4.TabIndex = 16;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "选择或输入处理结果文件地址";
            // 
            // SignResulteTxt
            // 
            this.SignResulteTxt.Location = new System.Drawing.Point(212, 30);
            this.SignResulteTxt.Name = "SignResulteTxt";
            this.SignResulteTxt.Size = new System.Drawing.Size(538, 21);
            this.SignResulteTxt.TabIndex = 3;
            // 
            // SaveSignFileResulte
            // 
            this.SaveSignFileResulte.ForeColor = System.Drawing.Color.Coral;
            this.SaveSignFileResulte.Location = new System.Drawing.Point(11, 28);
            this.SaveSignFileResulte.Name = "SaveSignFileResulte";
            this.SaveSignFileResulte.Size = new System.Drawing.Size(182, 23);
            this.SaveSignFileResulte.TabIndex = 1;
            this.SaveSignFileResulte.Text = "选择结果文件存放路径";
            this.SaveSignFileResulte.UseVisualStyleBackColor = true;
            this.SaveSignFileResulte.Click += new System.EventHandler(this.SaveSignFileResulteClick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.SignFileTxt);
            this.groupBox3.Controls.Add(this.ChoessSighFile);
            this.groupBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.groupBox3.Location = new System.Drawing.Point(3, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(756, 62);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "选择或输入需要查重的文本路径";
            // 
            // SignFileTxt
            // 
            this.SignFileTxt.Location = new System.Drawing.Point(212, 28);
            this.SignFileTxt.Name = "SignFileTxt";
            this.SignFileTxt.Size = new System.Drawing.Size(538, 21);
            this.SignFileTxt.TabIndex = 3;
            // 
            // ChoessSighFile
            // 
            this.ChoessSighFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ChoessSighFile.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ChoessSighFile.Location = new System.Drawing.Point(11, 28);
            this.ChoessSighFile.Name = "ChoessSighFile";
            this.ChoessSighFile.Size = new System.Drawing.Size(182, 21);
            this.ChoessSighFile.TabIndex = 1;
            this.ChoessSighFile.Text = "选择查重文件：";
            this.ChoessSighFile.UseVisualStyleBackColor = true;
            this.ChoessSighFile.Click += new System.EventHandler(this.ChoessSighFileClick);
            // 
            // timernew
            // 
            this.timernew.Tick += new System.EventHandler(this.TimernewTick);
            // 
            // OFTwo
            // 
            this.OFTwo.Filter = "(*.txt)|*.txt";
            // 
            // OFThree
            // 
            this.OFThree.FileName = "OFThree";
            // 
            // SelectFileA
            // 
            this.SelectFileA.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SelectFileA.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SelectFileA.Location = new System.Drawing.Point(11, 28);
            this.SelectFileA.Name = "SelectFileA";
            this.SelectFileA.Size = new System.Drawing.Size(182, 21);
            this.SelectFileA.TabIndex = 1;
            this.SelectFileA.Text = "选择对比文件A：";
            this.SelectFileA.UseVisualStyleBackColor = true;
            this.SelectFileA.Click += new System.EventHandler(this.SelectFileAClick);
            // 
            // OFOne
            // 
            this.OFOne.Filter = "(*.txt)|*.txt";
            // 
            // runningTime
            // 
            this.runningTime.AutoSize = true;
            this.runningTime.ForeColor = System.Drawing.Color.LimeGreen;
            this.runningTime.Location = new System.Drawing.Point(12, 243);
            this.runningTime.Name = "runningTime";
            this.runningTime.Size = new System.Drawing.Size(0, 12);
            this.runningTime.TabIndex = 20;
            // 
            // CheckDate
            // 
            this.CheckDate.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.CheckDate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CheckDate.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CheckDate.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CheckDate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.CheckDate.Location = new System.Drawing.Point(635, 235);
            this.CheckDate.Name = "CheckDate";
            this.CheckDate.Size = new System.Drawing.Size(124, 52);
            this.CheckDate.TabIndex = 13;
            this.CheckDate.Text = "开始对比";
            this.CheckDate.UseVisualStyleBackColor = false;
            this.CheckDate.Click += new System.EventHandler(this.CheckDateClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtFileBPath);
            this.groupBox1.Controls.Add(this.txtFileAPath);
            this.groupBox1.Controls.Add(this.SelectFileA);
            this.groupBox1.Controls.Add(this.SelectFileB);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.groupBox1.Location = new System.Drawing.Point(3, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(756, 131);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择或输入需要处理的文本路径";
            // 
            // SelectFileB
            // 
            this.SelectFileB.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SelectFileB.Location = new System.Drawing.Point(11, 84);
            this.SelectFileB.Name = "SelectFileB";
            this.SelectFileB.Size = new System.Drawing.Size(182, 21);
            this.SelectFileB.TabIndex = 2;
            this.SelectFileB.Text = "选择对比文件B：";
            this.SelectFileB.UseVisualStyleBackColor = true;
            this.SelectFileB.Click += new System.EventHandler(this.SelectFileBClick);
            // 
            // SaveResulteA
            // 
            this.SaveResulteA.Location = new System.Drawing.Point(212, 30);
            this.SaveResulteA.Name = "SaveResulteA";
            this.SaveResulteA.Size = new System.Drawing.Size(538, 21);
            this.SaveResulteA.TabIndex = 3;
            // 
            // SaveAFile
            // 
            this.SaveAFile.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SaveAFile.Location = new System.Drawing.Point(11, 28);
            this.SaveAFile.Name = "SaveAFile";
            this.SaveAFile.Size = new System.Drawing.Size(182, 23);
            this.SaveAFile.TabIndex = 1;
            this.SaveAFile.Text = "选择结果文件存放路径";
            this.SaveAFile.UseVisualStyleBackColor = true;
            this.SaveAFile.Click += new System.EventHandler(this.SaveAFileClick);
            // 
            // resulteLable
            // 
            this.resulteLable.AutoSize = true;
            this.resulteLable.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.resulteLable.ForeColor = System.Drawing.Color.Red;
            this.resulteLable.Location = new System.Drawing.Point(12, 263);
            this.resulteLable.Name = "resulteLable";
            this.resulteLable.Size = new System.Drawing.Size(0, 12);
            this.resulteLable.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 223);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(485, 12);
            this.label2.TabIndex = 19;
            this.label2.Text = "温馨提示：请确定您对所选文件或者文件夹有读写权限。结果文件如果已经存在将被覆盖！";
            // 
            // RightShow
            // 
            this.RightShow.AutoSize = true;
            this.RightShow.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.RightShow.Location = new System.Drawing.Point(454, 290);
            this.RightShow.Name = "RightShow";
            this.RightShow.Size = new System.Drawing.Size(305, 12);
            this.RightShow.TabIndex = 18;
            this.RightShow.Text = "免责申明：如果您有任何问题请联系满哥，通通给你搞定";
            // 
            // TimeNew
            // 
            this.TimeNew.AutoSize = true;
            this.TimeNew.Location = new System.Drawing.Point(76, 286);
            this.TimeNew.Name = "TimeNew";
            this.TimeNew.Size = new System.Drawing.Size(0, 12);
            this.TimeNew.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 287);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 16;
            this.label1.Text = "当前时间：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.SaveResulteA);
            this.groupBox2.Controls.Add(this.SaveAFile);
            this.groupBox2.ForeColor = System.Drawing.Color.Blue;
            this.groupBox2.Location = new System.Drawing.Point(3, 143);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(756, 67);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "选择或输入处理结果文件地址";
            // 
            // tpTwoFileCompare
            // 
            this.tpTwoFileCompare.Controls.Add(this.resulteLable);
            this.tpTwoFileCompare.Controls.Add(this.label2);
            this.tpTwoFileCompare.Controls.Add(this.RightShow);
            this.tpTwoFileCompare.Controls.Add(this.TimeNew);
            this.tpTwoFileCompare.Controls.Add(this.label1);
            this.tpTwoFileCompare.Controls.Add(this.groupBox2);
            this.tpTwoFileCompare.Controls.Add(this.runningTime);
            this.tpTwoFileCompare.Controls.Add(this.CheckDate);
            this.tpTwoFileCompare.Controls.Add(this.groupBox1);
            this.tpTwoFileCompare.Location = new System.Drawing.Point(4, 22);
            this.tpTwoFileCompare.Name = "tpTwoFileCompare";
            this.tpTwoFileCompare.Padding = new System.Windows.Forms.Padding(3);
            this.tpTwoFileCompare.Size = new System.Drawing.Size(765, 305);
            this.tpTwoFileCompare.TabIndex = 0;
            this.tpTwoFileCompare.Text = "两个数据文件去重";
            this.tpTwoFileCompare.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpTwoFileCompare);
            this.tabControl1.Controls.Add(this.tpOneFileCompare);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(773, 331);
            this.tabControl1.TabIndex = 14;
            // 
            // tpOneFileCompare
            // 
            this.tpOneFileCompare.Controls.Add(this.SignRunTime);
            this.tpOneFileCompare.Controls.Add(this.label4);
            this.tpOneFileCompare.Controls.Add(this.label5);
            this.tpOneFileCompare.Controls.Add(this.SignTimeNew);
            this.tpOneFileCompare.Controls.Add(this.label7);
            this.tpOneFileCompare.Controls.Add(this.BeginSearch);
            this.tpOneFileCompare.Controls.Add(this.groupBox4);
            this.tpOneFileCompare.Controls.Add(this.groupBox3);
            this.tpOneFileCompare.Location = new System.Drawing.Point(4, 22);
            this.tpOneFileCompare.Name = "tpOneFileCompare";
            this.tpOneFileCompare.Padding = new System.Windows.Forms.Padding(3);
            this.tpOneFileCompare.Size = new System.Drawing.Size(765, 305);
            this.tpOneFileCompare.TabIndex = 1;
            this.tpOneFileCompare.Text = "单个文件查重";
            this.tpOneFileCompare.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 346);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "FreshManCheckDataDifferent-Xp(Win7)";
            this.Load += new System.EventHandler(this.Form1Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tpTwoFileCompare.ResumeLayout(false);
            this.tpTwoFileCompare.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tpOneFileCompare.ResumeLayout(false);
            this.tpOneFileCompare.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label SignRunTime;
        private System.Windows.Forms.TextBox txtFileBPath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label SignTimeNew;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button BeginSearch;
        private System.Windows.Forms.TextBox txtFileAPath;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox SignResulteTxt;
        private System.Windows.Forms.Button SaveSignFileResulte;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox SignFileTxt;
        private System.Windows.Forms.Button ChoessSighFile;
        private System.Windows.Forms.Timer timernew;
        private System.Windows.Forms.OpenFileDialog OFTwo;
        private System.Windows.Forms.OpenFileDialog OFThree;
        private System.Windows.Forms.Button SelectFileA;
        private System.Windows.Forms.OpenFileDialog OFOne;
        private System.Windows.Forms.Label runningTime;
        private System.Windows.Forms.Button CheckDate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button SelectFileB;
        private System.Windows.Forms.TextBox SaveResulteA;
        private System.Windows.Forms.Button SaveAFile;
        private System.Windows.Forms.Label resulteLable;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label RightShow;
        private System.Windows.Forms.Label TimeNew;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TabPage tpTwoFileCompare;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpOneFileCompare;

    }
}

