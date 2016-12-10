using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckDateDifferent
{
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Windows.Forms.Design;
    using Excel = Microsoft.Office.Interop.Excel;
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.CBHasOfficeDll.SelectedIndex = 0;
        }

        #region [1、处理对比事件]
        /// <summary>
        /// 实现对比方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckDateClick(object sender, EventArgs e)
        {
            var sss = DealSVGWeight();
            DataTabletoExcel(sss, "D:\\MyConfiguration\\qxb09889\\Desktop\\测试资料\\10.xlsx");
            foreach (DataRow temp in sss.Rows)
            {
                var ssss = temp[1];
                Console.WriteLine(ssss);
            }

            var outFileOuter = new NpoiHelper("D:\\MyConfiguration\\qxb09889\\Desktop\\测试资料\\10.xlsx");
            var dataCountNumber = outFileOuter.DataTableToExcel(sss, "youme", true);
            return;

            UiContrller(false);
            var needFileOne = txtFileAPath.Text;
            var needFileTwo = txtFileBPath.Text;
            var resulteA = SaveResulteA.Text + @"\" + DateTime.Now.ToString("yyyy-MM-dd") + @"ResulteA.txt";
            var resulteB = SaveResulteA.Text + @"\" + DateTime.Now.ToString("yyyy-MM-dd") + @"ResulteB.txt";
            ResulteModel resulteModel = this.CheckFilePath(needFileOne, needFileTwo, resulteA, resulteB);
            if (resulteModel.Code != 0) return;

            //#region [1、系统内置集合方法]
            SystemSetWay(needFileOne, needFileTwo, resulteA, resulteB);
            //#endregion

            UiTxtClear();
            UiContrller(true);
        }

        /// <summary>
        /// 选择A文件地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectFileAClick(object sender, EventArgs e)
        {
            DialogResult dr = OFOne.ShowDialog();
            if (dr == DialogResult.OK)
            {
                txtFileAPath.Text = OFOne.FileName;
            }
        }

        /// <summary>
        /// 选择B文件地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectFileBClick(object sender, EventArgs e)
        {
            DialogResult dr = OFTwo.ShowDialog();
            if (dr == DialogResult.OK)
            {
                txtFileBPath.Text = OFTwo.FileName;
            }
        }

        /// <summary>
        /// 实时时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimernewTick(object sender, EventArgs e)
        {
            var showTime = DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss");
            TimeNew.Text = showTime;
            SignTimeNew.Text = showTime;
            MergeShowTime.Text = showTime;
        }

        /// <summary>
        /// 窗体加载初始化方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1Load(object sender, EventArgs e)
        {
            timernew.Start();
        }

        /// <summary>
        /// 选择保存A文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveAFileClick(object sender, EventArgs e)
        {
            var aa = FolderDialog.GetInstance();
            aa.DisplayDialog();
            SaveResulteA.Text = aa.Path;
        }

        /// <summary>
        /// UI控件控制
        /// </summary>
        /// <param name="canDo">true：可以使用；false不可使用</param>
        private void UiContrller(bool canDo)
        {
            CheckDate.Enabled = canDo;
            SelectFileA.Enabled = canDo;
            SelectFileB.Enabled = canDo;
            SaveAFile.Enabled = canDo;
            //SaveBFile.Enabled = canDo;

            txtFileAPath.Enabled = canDo;
            txtFileBPath.Enabled = canDo;
            SaveResulteA.Enabled = canDo;
            //SaveResulteB.Enabled = canDo;
        }

        /// <summary>
        /// 清除最后工作
        /// </summary>
        private void UiTxtClear()
        {
            txtFileAPath.Text = string.Empty;
            txtFileBPath.Text = string.Empty;
            SaveResulteA.Text = string.Empty;
            //SaveResulteB.Text = string.Empty;
        }

        /// <summary>
        /// 检测文件有效性，是否一致性
        /// </summary>
        /// <returns></returns>
        private ResulteModel CheckFilePath(string needFileOne, string needFileTwo, string resulteA, string resulteB)
        {
            var resulteModel = new ResulteModel { Code = 0, Msg = "" };
            #region 文件路径判空
            if (string.IsNullOrEmpty(needFileOne))
            {
                resulteModel.Code = 1;
                resulteModel.Msg = "待处理文件A地址不能为空";
                txtFileAPath.Focus();
            }
            else if (string.IsNullOrEmpty(needFileTwo))
            {
                resulteModel.Code = 1;
                resulteModel.Msg = "待处理文件B地址不能为空";
                txtFileBPath.Focus();
            }
            else if (string.IsNullOrEmpty(resulteA))
            {
                resulteModel.Code = 2;
                resulteModel.Msg = "处理结果A文件地址不能为空";
                SaveResulteA.Focus();
            }
            else if (string.IsNullOrEmpty(resulteB))
            {
                resulteModel.Code = 2;
                resulteModel.Msg = "处理结果B文件地址不能为空";
            }
            else if (!File.Exists(needFileOne))
            {
                resulteModel.Code = 3;
                resulteModel.Msg = "待处理A文件不存在！";
                txtFileAPath.Text = "";
                txtFileAPath.Focus();
            }
            else if (!File.Exists(needFileTwo))
            {
                resulteModel.Code = 3;
                resulteModel.Msg = "待处理B文件不存在！";
                txtFileBPath.Text = "";
                txtFileBPath.Focus();
            }
            #endregion

            #region 文件地址判是否相等

            else if (needFileOne == needFileTwo)
            {
                resulteModel.Code = 4;
                resulteModel.Msg = "待处理A B文件不能为同一个文件！";
                txtFileBPath.Text = "";
                txtFileBPath.Focus();
            }
            else if (resulteA == resulteB)
            {
                resulteModel.Code = 4;
                resulteModel.Msg = "处理结果文件名不能为同一个文件！";
            }
            else if (needFileOne == resulteA || needFileOne == resulteB || needFileTwo == resulteA || needFileTwo == resulteB)
            {
                resulteModel.Code = 5;
                resulteModel.Msg = "待处理文件不能作为结果文件存在！";
            }
            #endregion
            if (resulteModel.Code != 0)
            {
                MessageBox.Show(resulteModel.Msg, @"错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                UiContrller(true);
            }
            return resulteModel;
        }
        #endregion

        #region [2、系统内置集合方法]
        /// <summary>
        /// 系统内置集合方法
        /// </summary>
        /// <param name="needFileOne"></param>
        /// <param name="needFileTwo"></param>
        /// <param name="resulteA"></param>
        /// <param name="resulteB"></param>
        private void SystemSetWay(string needFileOne, string needFileTwo, string resulteA, string resulteB)
        {
            var time = new Stopwatch();
            time.Start();
            //读取A文件
            var itxtListA = new ConcurrentQueue<string>();
            StreamReader stream;
            try
            {
                stream = File.OpenText(needFileOne);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UiContrller(true);
                return;
            }
            while (!stream.EndOfStream)
            {
                itxtListA.Enqueue(stream.ReadLine());
            }
            //读取B文件
            var itxtListB = new ConcurrentQueue<string>();
            try
            {
                stream = File.OpenText(needFileTwo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UiContrller(true);
                return;
            }
            while (!stream.EndOfStream)
            {
                itxtListB.Enqueue(stream.ReadLine());
            }
            var a = Task.Factory.StartNew(() =>
            WirteFile(itxtListA, itxtListB, resulteA));

            var b = Task.Factory.StartNew(() =>
            WirteFile(itxtListB, itxtListA, resulteB));

            if (a.Result.Code == 0 && b.Result.Code == 0)
            {
                MessageBox.Show(@"处理完成", @"处理成功", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            else if (a.Result.Code == -1 || b.Result.Code == -1)
            {
                MessageBox.Show(a.Result.Msg + @"  " + b.Result.Msg, @"异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (a.Result.Code == 1 || b.Result.Code == 1)
            {
                var messageStr = (a.Result.Code == 1 && b.Result.Code == 1) ? "A和B文件一致" : ((a.Result.Code == 1 && b.Result.Code != 1) ? "A文件都包含在B中" : "B文件都包含在A中");
                MessageBox.Show(@"处理完成：" + messageStr, @"成功", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            time.Stop();
            var runTime = time.Elapsed;
            runningTime.Text = string.Format("处理结果：A文件数据量{0}条,B文件数据量{1}条. 一共耗时：{2}毫秒", itxtListA.Count, itxtListB.Count, runTime.Milliseconds);
            resulteLable.Text = @"A文件中包含：" + a.Result.DataCount + @"条；B文件中包含：" + b.Result.DataCount + @"条";
        }

        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="itxtListB"></param>
        /// <param name="itxtListA"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public ResulteModel WirteFile(IEnumerable<string> itxtListB, IEnumerable<string> itxtListA, string fileName)
        {
            var resulteModel = new ResulteModel { Code = 0, Msg = "" };
            FileStream fs;
            if (string.IsNullOrEmpty(fileName))
            {
                resulteModel.Code = -1;
                resulteModel.Msg = "结果文件名不能为空！";
                return resulteModel;
            }
            var position = fileName.LastIndexOf('.');
            if (position == -1)
            {
                fileName += @"\" + DateTime.Now.Millisecond.ToString(CultureInfo.InvariantCulture) + @"Resulte.txt";
            }
            else
            {
                var fileExt = fileName.Substring(position);
                if (string.IsNullOrEmpty(fileExt))
                {
                    fileName += @"\" + DateTime.Now.Millisecond.ToString(CultureInfo.InstalledUICulture) + @"Resulte.txt";
                }
                else if (fileExt != ".txt")
                {
                    fileName += ".txt";
                }
            }
            var ss = itxtListB.Except(itxtListA);
            int i = 0;
            if (ss.Any())
            {
                try
                {
                    fs = new FileStream(fileName, FileMode.Create);
                    resulteModel.DataCount = ss.Count();
                }
                catch (Exception ex)
                {
                    resulteModel.Code = -1;
                    resulteModel.Msg = ex.Message;
                    resulteModel.DataCount = 0;
                    return resulteModel;
                }
                var sw = new StreamWriter(fs, Encoding.Default);
                foreach (var txta in ss)
                {
                    i++;
                    sw.WriteLine(txta);
                }
                sw.Close();
                fs.Close();
                resulteModel.Msg = "处理完成";
            }
            else
            {
                resulteModel.Code = 1;
                resulteModel.Msg = "都存在元素";
            }
            return resulteModel;
        }

        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="itxtListB"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public ResulteModel WirteFile(DataTable itxtListB, string fileName)
        {
            var resulteModel = new ResulteModel { Code = 0, Msg = "" };
            FileStream fs;
            if (string.IsNullOrEmpty(fileName))
            {
                resulteModel.Code = -1;
                resulteModel.Msg = "结果文件名不能为空！";
                return resulteModel;
            }
            var position = fileName.LastIndexOf('.');
            if (position == -1)
            {
                fileName += @"\" + DateTime.Now.Millisecond.ToString(CultureInfo.InvariantCulture) + @"Resulte.txt";
            }
            else
            {
                var fileExt = fileName.Substring(position);
                if (string.IsNullOrEmpty(fileExt))
                {
                    fileName += @"\" + DateTime.Now.Millisecond.ToString(CultureInfo.InstalledUICulture) + @"Resulte.txt";
                }
                else if (fileExt != ".txt")
                {
                    fileName += ".txt";
                }
            }
            if (itxtListB != null && itxtListB.Rows.Count > 0)
            {
                try
                {
                    fs = new FileStream(fileName, FileMode.Create);
                    resulteModel.DataCount = itxtListB.Rows.Count;
                }
                catch (Exception ex)
                {
                    resulteModel.Code = -1;
                    resulteModel.Msg = ex.Message;
                    resulteModel.DataCount = 0;
                    return resulteModel;
                }
                var sw = new StreamWriter(fs, Encoding.Default);
                int j;
                for (j = 0; j < itxtListB.Rows.Count; j++)
                {
                    sw.WriteLine(itxtListB.Rows[j][0]);
                }
                sw.Close();
                fs.Close();
                resulteModel.Msg = "处理完成";
            }
            else
            {
                resulteModel.Code = 1;
                resulteModel.Msg = "都存在元素";
            }
            return resulteModel;
        }
        #endregion

        #region [3、单个文件查重]
        /// <summary>
        /// 选择文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChoessSighFileClick(object sender, EventArgs e)
        {
            DialogResult dr = OFThree.ShowDialog();
            if (dr == DialogResult.OK)
            {
                SignFileTxt.Text = OFThree.FileName;
            }
        }

        /// <summary>
        /// 单个文件保存地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveSignFileResulteClick(object sender, EventArgs e)
        {
            var aa = FolderDialog.GetInstance();
            aa.DisplayDialog();
            SignResulteTxt.Text = aa.Path;
        }

        /// <summary>
        /// 处理对比
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BeginSearchClick(object sender, EventArgs e)
        {
            SignUiContrller(false);
            var needFileOne = SignFileTxt.Text;
            var resulteA = SignResulteTxt.Text + @"\" + DateTime.Now.ToString("yyyy-MM-dd") + @"SignResulteA.txt";
            ResulteModel resulteModel = this.SignCheckFilePath(needFileOne, resulteA);
            if (resulteModel.Code != 0) return;

            //开始比较
            SeachRepeatItem(needFileOne, resulteA);

            SignUiTxtClear();
            SignUiContrller(true);
        }

        /// <summary>
        /// 开始比较
        /// </summary>
        private void SeachRepeatItem(string needFileOne, string resulteA)
        {
            var time = new Stopwatch();
            time.Start();
            //数据量
            var itemCount = 0;
            //重复的数据量
            var repeatCount = 0;
            //结果列表
            var repeatList = new List<string>();
            //结果集合，防止结果集合重复
            ISet<string> repeatSet = new HashSet<string>();
            //读取文件
            ISet<string> itxtListA = new HashSet<string>();
            StreamReader stream;
            //获得编码格式
            var code = GetEncoding(needFileOne);
            try
            {
                stream = new StreamReader(needFileOne, code);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SignUiContrller(true);
                return;
            }
            while (!stream.EndOfStream)
            {
                var lineStr = stream.ReadLine();
                if (string.IsNullOrEmpty(lineStr)) continue;
                itemCount++;
                if (!itxtListA.Add(lineStr))
                {
                    if (repeatSet.Add(lineStr))
                    {
                        repeatCount++;
                        repeatList.Add(lineStr);
                    }
                }
            }
            stream.Close();
            if (repeatCount == 0)
            {
                MessageBox.Show(@"处理完成，该文件中没有重复数据", @"处理成功", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            else
            {
                var a = WriteSignFile(repeatList, resulteA, code);

                if (a.Code == 0)
                {
                    MessageBox.Show(@"处理完成", @"处理成功", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
                else if (a.Code == -1)
                {
                    MessageBox.Show(a.Msg, @"异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (a.Code == 1)
                {
                    const string MessageStr = "未找到重复项";
                    MessageBox.Show(@"处理完成：" + MessageStr, @"成功", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
            }
            time.Stop();
            var runTime = time.Elapsed;
            SignRunTime.Text = string.Format("处理结果：待处理文件数据量{0}条,重复数据项{1}条. 一共耗时：{2}毫秒", itemCount, repeatCount, runTime.Milliseconds);
        }

        /// <summary>
        /// 写单个文件
        /// </summary>
        /// <param name="listItem"></param>
        /// <param name="fileName"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        private ResulteModel WriteSignFile(IList<string> listItem, string fileName, Encoding code)
        {
            var resulteModel = new ResulteModel { Code = 0, Msg = "" };
            FileStream fs;
            if (string.IsNullOrEmpty(fileName))
            {
                resulteModel.Code = -1;
                resulteModel.Msg = "结果文件名不能为空！";
                return resulteModel;
            }
            var position = fileName.LastIndexOf('.');
            if (position == -1)
            {
                fileName += @"\" + DateTime.Now.Millisecond.ToString(CultureInfo.InvariantCulture) + @"SignResulte.txt";
            }
            else
            {
                var fileExt = fileName.Substring(position);
                if (string.IsNullOrEmpty(fileExt))
                {
                    fileName += @"\" + DateTime.Now.Millisecond.ToString(CultureInfo.InstalledUICulture) + @"SignResulte.txt";
                }
                else if (fileExt != ".txt")
                {
                    fileName += ".txt";
                }
            }
            if (listItem.Any())
            {
                try
                {
                    fs = new FileStream(fileName, FileMode.Create);
                }
                catch (Exception ex)
                {
                    resulteModel.Code = -1;
                    resulteModel.Msg = ex.Message;
                    return resulteModel;
                }
                var sw = new StreamWriter(fs, code);
                foreach (var txta in listItem)
                {
                    sw.WriteLine(txta);
                }
                sw.Close();
                fs.Close();
                resulteModel.Msg = "处理完成";
            }
            else
            {
                resulteModel.Code = 1;
                resulteModel.Msg = "元素为空";
            }
            return resulteModel;
        }

        /// <summary>
        /// 文件路径检测
        /// </summary>
        /// <returns></returns>
        private ResulteModel SignCheckFilePath(string needFile, string resulte)
        {
            var resulteModel = new ResulteModel { Code = 0, Msg = "" };
            #region 文件路径判空
            if (string.IsNullOrEmpty(needFile))
            {
                resulteModel.Code = 1;
                resulteModel.Msg = "待处理文件地址不能为空";
                txtFileAPath.Focus();
            }
            else if (string.IsNullOrEmpty(resulte))
            {
                resulteModel.Code = 2;
                resulteModel.Msg = "处理结果A文件地址不能为空";
                SaveResulteA.Focus();
            }
            else if (!File.Exists(needFile))
            {
                resulteModel.Code = 3;
                resulteModel.Msg = "待处理文件不存在！";
                txtFileAPath.Text = "";
                txtFileAPath.Focus();
            }
            #endregion

            #region 文件地址判是否相等
            if (resulte == needFile)
            {
                resulteModel.Code = 5;
                resulteModel.Msg = "待处理文件不能作为结果文件存在！";
            }
            #endregion
            if (resulteModel.Code != 0)
            {
                MessageBox.Show(resulteModel.Msg, @"错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                SignUiContrller(true);
            }
            return resulteModel;
        }

        /// <summary>
        /// UI控件控制
        /// </summary>
        /// <param name="canDo">true：可以使用；false不可使用</param>
        private void SignUiContrller(bool canDo)
        {
            ChoessSighFile.Enabled = canDo;
            SaveSignFileResulte.Enabled = canDo;
            SignFileTxt.Enabled = canDo;
            SignResulteTxt.Enabled = canDo;
            BeginSearch.Enabled = canDo;
        }

        /// <summary>
        /// 输入清空
        /// </summary>
        private void SignUiTxtClear()
        {
            SignFileTxt.Text = string.Empty;
            SignResulteTxt.Text = string.Empty;
        }

        #region [3.1 获得文件编码格式]
        /// <summary>
        /// 取得一个文本文件的编码方式。如果无法在文件头部找到有效的前导符将被返回。
        /// </summary>
        /// <param name="fileName">文件名。</param>
        /// <returns></returns>
        public Encoding GetEncoding(string fileName)
        {
            return GetEncoding(fileName, Encoding.Default);
        }

        /// <summary>
        /// 取得一个文本文件的编码方式。
        /// </summary>
        /// <param name="fileName">文件名。</param>
        /// <param name="defaultEncoding">默认编码方式。当该方法无法从文件的头部取得有效的前导符时，将返回该编码方式。</param>
        /// <returns></returns>
        public static Encoding GetEncoding(string fileName, Encoding defaultEncoding)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open);
            Encoding targetEncoding = GetEncoding(fs, defaultEncoding);
            fs.Close();
            return targetEncoding;
        }

        /// <summary>
        /// 取得一个文本文件流的编码方式。
        /// </summary>
        /// <param name="stream">文本文件流。</param>
        /// <param name="defaultEncoding">默认编码方式。当该方法无法从文件的头部取得有效的前导符时，将返回该编码方式。</param>
        /// <returns></returns>
        public static Encoding GetEncoding(FileStream stream, Encoding defaultEncoding)
        {
            Encoding targetEncoding = defaultEncoding;
            if (stream != null && stream.Length >= 2)
            {
                //保存文件流的前4个字节
                byte byte1 = 0;
                byte byte2 = 0;
                byte byte3 = 0;
                byte byte4 = 0;
                //保存当前Seek位置
                long origPos = stream.Seek(0, SeekOrigin.Begin);
                stream.Seek(0, SeekOrigin.Begin);

                int nByte = stream.ReadByte();
                byte1 = Convert.ToByte(nByte);
                byte2 = Convert.ToByte(stream.ReadByte());
                if (stream.Length >= 3)
                {
                    byte3 = Convert.ToByte(stream.ReadByte());
                }
                if (stream.Length >= 4)
                {
                    byte4 = Convert.ToByte(stream.ReadByte());
                }

                //根据文件流的前4个字节判断Encoding
                //Unicode {0xFF, 0xFE};
                //BE-Unicode {0xFE, 0xFF};
                //UTF8 = {0xEF, 0xBB, 0xBF};
                if (byte1 == 0xFE && byte2 == 0xFF)//UnicodeBe
                {
                    targetEncoding = Encoding.BigEndianUnicode;
                }
                if (byte1 == 0xFF && byte2 == 0xFE && byte3 != 0xFF)//Unicode
                {
                    targetEncoding = Encoding.Unicode;
                }
                if (byte1 == 0xEF && byte2 == 0xBB && byte3 == 0xBF)//UTF8
                {
                    targetEncoding = Encoding.UTF8;
                }

                //恢复Seek位置      
                stream.Seek(origPos, SeekOrigin.Begin);
            }
            return targetEncoding;
        }
        #endregion
        #endregion

        #region [4、NPOI处理excel文件合并]
        /// <summary>
        /// 处理excel文件合并
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MergeExcelFile(object sender, EventArgs e)
        {
            MergeFileUiContrller(false);
            var time = new Stopwatch();
            time.Start();
            var directoryPath = TxtNeedDealFilePath.Text;
            var resulteDirectoryPath = TxtDealResulteSaveFilePath.Text + "\\";
            if (!Directory.Exists(directoryPath))
            {
                MessageBox.Show(@"文件夹不存在", @"异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MergeFileUiContrller(true);
                return;
            }
            //获得外层文件列表
            var needMergeOuterFileList = Directory.GetFiles(directoryPath, "*.xls*");
            //内层需要合并的文件列表
            var needMergeItemFilePath = new List<string>();
            needMergeItemFilePath = GetSubDirectoryFile(directoryPath, needMergeItemFilePath);
            //文件列表判断
            if (!needMergeOuterFileList.Any() && !needMergeItemFilePath.Any())
            {
                MessageBox.Show(@"文件夹中不存在任何待处理文件", @"异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MergeFileUiContrller(true);
                return;
            }
            var dateTime = DateTime.Now;
            var fileOuterName = string.Format("顺风{0}汇总结果.xlsx", dateTime.ToString("MM月dd日"));
            var fileInName = string.Format("顺风{0}子文件合并结果.xlsx", dateTime.ToString("MM月dd日"));

            var hasOffice = CBHasOfficeDll.SelectedIndex;
            //安装office软件
            if (hasOffice == 0)
            {
                int inNumber;
                int outNumber;
                var reslute = DoMerge(needMergeItemFilePath.ToList(), needMergeOuterFileList.ToList(), resulteDirectoryPath, fileInName, fileOuterName, out inNumber, out outNumber);
                time.Stop();
                var runTime = time.Elapsed;
                MergeMessge.Text = string.Format("处理结果：处理耗时：{0}毫秒", runTime.Milliseconds);
                ShowMergeNumber.Text = string.Format("完成合并总数据量{0}条,子文件夹合并数量{1}条", outNumber, inNumber);
                MessageBox.Show(reslute ? "合并成功" : "合并失败，请重试", @"运行成功", MessageBoxButtons.OK, reslute ? MessageBoxIcon.Asterisk : MessageBoxIcon.Error);
            }
            else
            {
                #region [没有安装office软件]

                #region 首先合并外层层文件
                var dataTableOutItem = new DataTable();
                int needExportDataNumberOut = 0;
                if (needMergeOuterFileList.Any())
                {
                    for (var item = 0; item < needMergeOuterFileList.Count(); item++)
                    {
                        var npoi = new NpoiHelper(needMergeOuterFileList[item]);
                        string errorMessage;
                        var tempData = npoi.ExcelToDataTable(0, true, out errorMessage);
                        if (tempData != null && tempData.Rows.Count > 0)
                        {
                            if (item == 0)
                            {
                                dataTableOutItem = tempData;
                                needExportDataNumberOut = tempData.Rows.Count;
                            }
                            else
                            {
                                dataTableOutItem.Merge(tempData);
                                needExportDataNumberOut += tempData.Rows.Count;
                            }
                        }
                    }
                }
                #endregion

                #region 再次合并内层文件
                var dataTableItem = new DataTable();
                int needExportDataNumberIn = 0;
                if (needMergeItemFilePath.Any())
                {
                    for (var item = 0; item < needMergeItemFilePath.Count(); item++)
                    {
                        var npoi = new NpoiHelper(needMergeItemFilePath[item]);
                        string errorMessage;
                        var tempData = npoi.ExcelToDataTable(0, true, out errorMessage);
                        if (tempData != null && tempData.Rows.Count > 0)
                        {
                            if (item == 0)
                            {
                                dataTableItem = tempData;
                                needExportDataNumberIn = tempData.Rows.Count;
                            }
                            else
                            {
                                dataTableItem.Merge(tempData);
                                needExportDataNumberIn += tempData.Rows.Count;
                            }
                            dataTableOutItem.Merge(tempData);
                        }
                    }
                }
                needExportDataNumberOut += needExportDataNumberIn;
                #endregion

                //导出文件
                var dataCountNumber = 0;
                var dataCountNumberIn = 0;
                //存在内层文件
                //if (needExportDataNumberIn > 0)
                //{
                //    var outFileInt = new NpoiHelper(resulteDirectoryPath + fileInName);
                //    dataCountNumberIn = outFileInt.DataTableToExcel(dataTableItem, "子文件表", true);
                //}
                var b = Task.Factory.StartNew(() =>
                ExportDataTableToExcle(needExportDataNumberIn, resulteDirectoryPath + fileInName, dataTableItem, "子文件表"));
                //存在外层文件
                //if (needExportDataNumberOut > 0)
                //{
                //    var outFileOuter = new NpoiHelper(resulteDirectoryPath + fileOuterName);
                //    dataCountNumber = outFileOuter.DataTableToExcel(dataTableOutItem, "汇总表", true);
                //}
                var a = Task.Factory.StartNew(() =>
                ExportDataTableToExcle(needExportDataNumberOut, resulteDirectoryPath + fileOuterName, dataTableOutItem, "汇总表"));

                dataCountNumber = a.Result;
                dataCountNumberIn = b.Result;
                time.Stop();
                var runTime = time.Elapsed;

                MergeMessge.Text = string.Format("处理结果：合并成功.处理耗时：{0}毫秒", runTime.Milliseconds);
                ShowMergeNumber.Text = string.Format("需要合并总数据量{0}条,子文件夹合并数据量{1}条.完成合并总数据量{2}条,子文件夹合并数量{3}条", needExportDataNumberOut > 0 ? needExportDataNumberOut + 1 : needExportDataNumberOut, needExportDataNumberIn > 0 ? needExportDataNumberIn + 1 : needExportDataNumberIn, dataCountNumber, dataCountNumberIn);
                var boxMessage = "合并成功.";
                if (needExportDataNumberIn <= 0)
                {
                    boxMessage += " 子文件数据为空.";
                }
                if (needExportDataNumberOut <= 0)
                {
                    boxMessage += " 无数据需要合并.";
                }
                MessageBox.Show(boxMessage, @"成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                #endregion
            }
            MergeFileUiContrller(true);
        }

        /// <summary>
        /// 使用深度递归获得子文件夹中的所有子文件集合
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileList"></param>
        /// <returns></returns>
        private List<string> GetSubDirectoryFile(string path, List<string> fileList)
        {
            //获得目录
            var fileDirectory = Directory.GetDirectories(path);
            if (!fileDirectory.Any())
            {
                return fileList;
            }
            else
            {
                fileDirectory.ToList().ForEach(
                    m =>
                    {
                        fileList.AddRange(Directory.GetFiles(m, "*.xls*"));
                        fileList = GetSubDirectoryFile(m, fileList);
                    });
                return fileList;
            }
        }

        /// <summary>
        /// 选择结果文件存放路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtDealResulteSaveFilePathClick(object sender, EventArgs e)
        {
            var aa = FolderDialog.GetInstance();
            aa.DisplayDialog();
            TxtDealResulteSaveFilePath.Text = aa.Path;
        }

        /// <summary>
        /// 处理文件路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtNeedDealFilePathClick(object sender, EventArgs e)
        {
            var aa = FolderDialog.GetInstance();
            aa.DisplayDialog();
            TxtNeedDealFilePath.Text = aa.Path;
        }

        /// <summary>
        /// UI控件控制
        /// </summary>
        /// <param name="canDo">true：可以使用；false不可使用</param>
        private void MergeFileUiContrller(bool canDo)
        {
            BtNeedDealFilePath.Enabled = canDo;
            TxtNeedDealFilePath.Enabled = canDo;
            BtDealResulteSaveFilePath.Enabled = canDo;
            TxtDealResulteSaveFilePath.Enabled = canDo;
            BeginMerge.Enabled = canDo;
        }

        /// <summary>
        /// 导出文件
        /// </summary>
        /// <param name="needExportDataNumberOut"></param>
        /// <param name="filePath"></param>
        /// <param name="dataTableOutItem"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        private int ExportDataTableToExcle(int needExportDataNumberOut, string filePath, DataTable dataTableOutItem, string sheetName)
        {
            var dataCountNumber = 0;
            //存在外层文件
            if (needExportDataNumberOut > 0)
            {
                var outFileOuter = new NpoiHelper(filePath);
                dataCountNumber = outFileOuter.DataTableToExcel(dataTableOutItem, sheetName, true);
            }
            return dataCountNumber;
        }
        #endregion

        #region [7、office原生自带处理excel文件合并]

        /// <summary>
        /// 合并需要的列表中的文件
        /// </summary>
        /// <param name="fileInList"></param>
        /// <param name="fileOutList"></param>
        /// <param name="resulteFilePath"></param>
        /// <param name="fileInName"></param>
        /// <param name="fileOutName"></param>
        /// <param name="inNumber"></param>
        /// <param name="outNumber"></param>
        /// <returns></returns>
        public bool DoMerge(List<string> fileInList, List<string> fileOutList, string resulteFilePath, string fileInName, string fileOutName, out int inNumber, out int outNumber)
        {
            inNumber = outNumber = 0;
            bool b = false;
            bool resulte = false;
            var app = new Excel.Application { Visible = false, UserControl = true };
            Excel.Workbooks workbooks = app.Workbooks;
            Excel.Workbook workbookIn = workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);//生成Excel工作表
            Excel.Workbook workbookOut = workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);//生成Excel工作表
            Excel.Sheets sheetsIn = workbookIn.Sheets;
            Excel.Sheets sheetsOut = workbookOut.Sheets;
            var worksheetIn = (Excel.Worksheet)sheetsIn.Item[1];//生成sheet
            var worksheetOut = (Excel.Worksheet)sheetsOut.Item[1];//生成sheet
            if (string.IsNullOrEmpty(resulteFilePath))
            {
                MessageBox.Show(@"存储地址不能为空", @"异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            try
            {
                //处理内层
                foreach (string file in fileInList)//循环把每个要合并的Excel复制到目标Excel上
                {
                    Excel.Worksheet sheetsource = OpenExcel(file, app);
                    if (b == false)
                    {
                        CopyHeader(worksheetIn, sheetsource);
                        CopyHeader(worksheetOut, sheetsource);
                        b = true;
                    }
                    CopyData(worksheetIn, sheetsource);
                    CopyData(worksheetOut, sheetsource);
                }
                app.DisplayAlerts = false;//文件覆盖时不弹出警告
                //保存Excel
                inNumber = worksheetIn.UsedRange.Rows.Count;
                fileInName = fileInName.Substring(0, fileInName.LastIndexOf(".", StringComparison.Ordinal)) + inNumber + "票" + fileInName.Substring(fileInName.LastIndexOf(".", StringComparison.Ordinal));
                workbookIn.SaveAs(resulteFilePath + fileInName, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

                //处理外层
                foreach (string file in fileOutList)//循环把每个要合并的Excel复制到目标Excel上
                {
                    Excel.Worksheet sheetsource = OpenExcel(file, app);
                    if (b == false)
                    {
                        CopyHeader(worksheetOut, sheetsource);
                        b = true;
                    }
                    CopyData(worksheetOut, sheetsource);
                }
                //保存Excel
                outNumber = worksheetOut.UsedRange.Rows.Count;
                fileOutName = fileOutName.Substring(0, fileOutName.LastIndexOf(".", StringComparison.Ordinal)) + outNumber + "票" + fileOutName.Substring(fileOutName.LastIndexOf(".", StringComparison.Ordinal));
                workbookOut.SaveAs(resulteFilePath + fileOutName, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

                resulte = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //释放资源
                workbookIn.Close(false, Missing.Value, Missing.Value);
                app.Quit();
                Process[] proc = Process.GetProcessesByName("Excel");//循环删除所有Excel进程
                foreach (Process process in proc)
                {
                    if (!process.CloseMainWindow())
                    {
                        process.Kill();
                    }
                }
                GC.Collect();//资源回收
            }
            return resulte;
        }

        private Excel.Worksheet OpenExcel(string filename, Excel.Application app)
        {
            Excel.Workbook workbook = app.Workbooks._Open(filename, Missing.Value, Missing.Value, Missing.Value, Missing.Value
                , Missing.Value, Missing.Value, Missing.Value, Missing.Value
                , Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            var worksheet = workbook.Worksheets[1] as Excel.Worksheet;
            return worksheet;
        }

        private void CopyHeader(Excel.Worksheet worksheet, Excel.Worksheet sheetsource)
        {
            int scc = sheetsource.UsedRange.Columns.Count;
            int src = sheetsource.UsedRange.Rows.Count;
            if (src >= 1)
            {
                Excel.Range range = sheetsource.Range[sheetsource.Cells[1, 1], sheetsource.Cells[1, scc]];
                range.Copy(worksheet.Range["A1", Missing.Value]);
            }
        }

        private void CopyData(Excel.Worksheet worksheet, Excel.Worksheet sheetsource)
        {
            int src = sheetsource.UsedRange.Rows.Count;
            int scc = sheetsource.UsedRange.Columns.Count;
            int drc = worksheet.UsedRange.Rows.Count;
            if (src >= 2)
            {
                Excel.Range range = sheetsource.Range[sheetsource.Cells[2, 1], sheetsource.Cells[src, scc]];
                range.Copy(worksheet.Range[worksheet.Cells[drc + 1, 1], worksheet.Cells[drc + src - 1, scc]]);
            }
        }
        #endregion

        #region [8、处理计件平均重量]
        /// <summary>
        /// 处理计件平均重量
        /// </summary>
        private bool DealSVGWeight(List<int> inputNumberList, string inputFilePath,bool isInstallOffice)
        {
            //去除重复元素
            ISet<decimal> iListA = new HashSet<decimal>();
            //结果集排序
            var resulteDecimalList = new List<decimal>();
            //参数元
            const string FirstName = "1PKG";
            //界限
            const decimal BaseNumber = 5M;
            //数据源
            var needDealNumber = new List<int> { 25, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 66, 52, 25, 32, 37, 27, 35, 26, 33 };
            //创建数据结构
            var resulteDataTable = new DataTable();
            resulteDataTable.Columns.Add(new DataColumn("数量") { DefaultValue = FirstName });
            resulteDataTable.Columns.Add(new DataColumn("平均值") { DefaultValue = "0KG/PKG" });

            foreach (var temp in needDealNumber)
            {
                #region 计算平均值
                var resulteA = BaseNumber / temp;
                decimal svgResulte;
                if (temp < BaseNumber)
                {
                    svgResulte = temp;
                }
                else if (temp < 25)
                {
                    svgResulte = temp * (decimal)0.2;
                }
                else
                {
                    resulteA = Math.Round(resulteA, 4);
                    //判断是否是有理数
                    var surplus = (int)(resulteA * 10000) % 100;
                    if (surplus == 0)
                    {
                        resulteA = resulteA - 0.01M;
                    }
                    else
                    {
                        resulteA = ((int)(resulteA * 100)) / (decimal)100;
                    }
                    svgResulte = resulteA * temp;
                }
                #endregion
                //去重
                if (iListA.Add(svgResulte))
                {
                    resulteDecimalList.Add(svgResulte);
                }
            }
            //没有数据
            if (!resulteDecimalList.Any()) return false;
            resulteDecimalList = resulteDecimalList.OrderBy(m => m).ToList();
            if (isInstallOffice)
            {
                SaveDataToExcelFile(resulteDecimalList, "D:\\MyConfiguration\\qxb09889\\Desktop\\测试资料\\10.xlsx");
            }
            else
            {
                var tempValue = new object[2];
                foreach (var @decimal in resulteDecimalList)
                {
                    tempValue[0] = FirstName;
                    tempValue[1] = @decimal + "KG/PKG";
                    resulteDataTable.Rows.Add(tempValue);
                }
            }
            return true;
        }

        /// <summary>
        /// 将DataTable导出到excel中
        /// </summary>
        /// <param name="tmpDataTable"></param>
        /// <param name="strFileName"></param>
        public static void DataTabletoExcel(DataTable tmpDataTable, string strFileName)
        {
            if (tmpDataTable == null)
                return;
            int rowNum = tmpDataTable.Rows.Count;
            int columnNum = tmpDataTable.Columns.Count;
            int rowIndex = 1;
            int columnIndex = 0;

            var xlApp = new Excel.Application { DefaultFilePath = "", DisplayAlerts = true, SheetsInNewWorkbook = 1 };
            Excel.Workbook xlBook = xlApp.Workbooks.Add(true);

            //将DataTable的列名导入Excel表第一行
            foreach (DataColumn dc in tmpDataTable.Columns)
            {
                columnIndex++;
                xlApp.Cells[rowIndex, columnIndex] = dc.ColumnName;
            }

            //将DataTable中的数据导入Excel中
            for (int i = 0; i < rowNum; i++)
            {
                rowIndex++;
                columnIndex = 0;
                for (int j = 0; j < columnNum; j++)
                {
                    columnIndex++;
                    xlApp.Cells[rowIndex, columnIndex] = tmpDataTable.Rows[i][j].ToString();
                }
            }
            xlBook.SaveCopyAs(strFileName);
        }

        /// <summary>
        /// 直接存储
        /// </summary>
        /// <param name="dataList"></param>
        /// <param name="filePath"></param>
        private void SaveDataToExcelFile(List<decimal> dataList, string filePath)
        {
            object misValue = Missing.Value;
            var xlApp = new Excel.Application();
            var xlWorkBook = xlApp.Workbooks.Add(misValue);
            var xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.Item[1];
            var nameList = new List<string> { "单位", "平均值" };
            for (int i = 0; i < 2; i++)
            {
                xlWorkSheet.Cells[1, i + 1] = nameList[i];
            }
            for (int i = 0; i < dataList.Count; i++)
            {
                xlWorkSheet.Cells[i + 2, 1] = "1PKG";
                xlWorkSheet.Cells[i + 2, 2] = dataList[i] + "KG/PKG";
            }
            try
            {
                xlWorkBook.SaveAs(filePath, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();
            }
            catch
            { }

        }
        #endregion
    }

    #region [5、文件夹选项单例]
    /// <summary>
    /// 文件夹选项类
    /// </summary>
    public class FolderDialog : FolderNameEditor
    {
        /// <summary>
        /// 唯一实例
        /// </summary>
        private static FolderDialog instance;

        /// <summary>
        /// 程序运行时创建一个静态只读的进程辅助对象
        /// </summary>
        private static readonly object SyncRoot = new object();

        private readonly FolderBrowser fDialog = new FolderBrowser();

        private FolderDialog()
        {
        }

        public DialogResult DisplayDialog()
        {
            return DisplayDialog("请选择一个文件夹");
        }

        public DialogResult DisplayDialog(string description)
        {
            fDialog.Description = description;
            return fDialog.ShowDialog();
        }

        public string Path
        {
            get
            {
                return fDialog.DirectoryPath;
            }
        }

        ~FolderDialog()
        {
            fDialog.Dispose();
        }

        /// <summary>
        /// 对外入口
        /// </summary>
        /// <returns></returns>
        public static FolderDialog GetInstance()
        {
            //双重锁定
            //先判断实例是否存在，不存在再加锁处理
            if (instance == null)
            {
                //在同一个时刻加了锁的那部分程序只有一个线程可以进入
                lock (SyncRoot)
                {
                    if (instance == null)
                    {
                        instance = new FolderDialog();
                    }
                }
            }
            return instance;
        }

    }
    #endregion

    #region [6、返回结果实体]
    /// <summary>
    /// 返回结果实例
    /// </summary>
    public class ResulteModel
    {
        /// <summary>
        /// 处理结果0:成功；其他：失败
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 处理数据量
        /// </summary>
        public int DataCount { get; set; }
    }
    #endregion
}
