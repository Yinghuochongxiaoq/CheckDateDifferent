using System;
using System.Text;
using System.Windows.Forms;

namespace CheckDateDifferent
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Windows.Forms.Design;

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        #region [1、比较两个文件中差异数据]
        /// <summary>
        /// 实现对比方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckDateClick(object sender, EventArgs e)
        {
            var time = new Stopwatch();
            time.Start();
            UiContrller(false);
            var needFileOne = txtFileAPath.Text;
            var needFileTwo = txtFileBPath.Text;
            var resulteA = SaveResulteA.Text + @"\" + DateTime.Now.ToString("yyyy-MM-dd") + @"ResulteA.txt";
            var resulteB = SaveResulteA.Text + @"\" + DateTime.Now.ToString("yyyy-MM-dd") + @"ResulteB.txt";
            ResulteModel resulteModel = this.CheckFilePath(needFileOne, needFileTwo, resulteA, resulteB);
            if (resulteModel.Code != 0) return;
            var aFile = SetExcept(needFileOne, needFileTwo);
            var bFile = SetExcept(needFileTwo, needFileOne);

            var a = WirteFile(aFile.ResulteList, resulteA);

            var b = WirteFile(bFile.ResulteList, resulteB);

            if (a.Code == 0 && b.Code == 0)
            {
                var runTime = time.Elapsed;
                runningTime.Text = string.Format("处理结果：A文件数据量{0}条,B文件数据量{1}条. 一共耗时：{2}毫秒", bFile.SetACount, aFile.SetACount, runTime.Milliseconds);
                resulteLable.Text = @"A文件中包含：" + a.Count + @"条；B文件中包含：" + b.Count + @"条";
                MessageBox.Show(@"处理完成", @"处理成功", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            else if (a.Code == -1 || b.Code == -1)
            {
                MessageBox.Show(a.Msg + @"  " + b.Msg, @"异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (a.Code == 1 || b.Code == 1)
            {
                var messageStr = (a.Code == 1 && b.Code == 1) ? "A和B文件一致" : ((a.Code == 1 && b.Code != 1) ? "A文件都包含在B中" : "B文件都包含在A中");

                var runTime = time.Elapsed;
                runningTime.Text = string.Format("处理结果：A文件数据量{0}条,B文件数据量{1}条. 一共耗时：{2}毫秒", bFile.SetACount, aFile.SetACount, runTime.Milliseconds);
                resulteLable.Text = @"A文件中包含：" + a.Count + @"条；B文件中包含：" + b.Count + @"条";
                MessageBox.Show(@"处理完成：" + messageStr, @"成功", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            UiTxtClear();
            UiContrller(true);
            time.Stop();
        }

        /// <summary>
        /// 写结果文件
        /// </summary>
        /// <param name="hashTable"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public ResulteModel WirteFile(List<string> hashTable, string fileName)
        {
            var resulteModel = new ResulteModel { Code = 0, Msg = "" };
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
            if (hashTable.Count > 0)
            {
                FileStream fs;
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
                var sw = new StreamWriter(fs, Encoding.Default);
                var hashList = hashTable.GetEnumerator();
                resulteModel.Count = hashTable.Count;
                while (hashList.MoveNext())
                {
                    sw.WriteLine(hashList.Current);
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
        /// 求集合差集
        /// </summary>
        /// <param name="needFileOne"></param>
        /// <param name="needFileTwo"></param>
        /// <returns></returns>
        private SetCompareResulted<string> SetExcept(string needFileOne, string needFileTwo)
        {
            var setReslute = new SetCompareResulted<string>();
            //读取B文件
            StreamReader stream;
            try
            {
                stream = File.OpenText(needFileTwo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UiContrller(true);
                return setReslute;
            }
            #region [1、一遍读取文件获得B与A的差集]
            var aFile = new Set<string>();
            string aLine;
            int aCount = 0;
            while (!stream.EndOfStream)
            {
                aLine = stream.ReadLine();
                if (!string.IsNullOrEmpty(aLine) && !aFile.Contains(aLine))
                {
                    aFile.Add(aLine);
                }
                aCount++;
            }
            //读取A文件
            try
            {
                stream = File.OpenText(needFileOne);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UiContrller(true);
                return setReslute;
            }
            int bCount = 0;
            var bFile = new Set<string>();
            while (!stream.EndOfStream)
            {
                aLine = stream.ReadLine();
                if (aFile.Add(aLine))
                {
                    bFile.Add(aLine);
                    setReslute.ResulteList.Add(aLine);
                }
                bCount++;
            }
            setReslute.SetACount = aCount;
            setReslute.SetBCount = bCount;
            return setReslute;
            #endregion
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

            txtFileAPath.Enabled = canDo;
            txtFileBPath.Enabled = canDo;
            SaveResulteA.Enabled = canDo;
        }

        /// <summary>
        /// 清除最后工作
        /// </summary>
        private void UiTxtClear()
        {
            txtFileAPath.Text = string.Empty;
            txtFileBPath.Text = string.Empty;
            SaveResulteA.Text = string.Empty;
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
                resulteModel.Msg = "处理结果A文件不能为空";
                SaveResulteA.Focus();
            }
            else if (string.IsNullOrEmpty(resulteB))
            {
                resulteModel.Code = 2;
                resulteModel.Msg = "处理结果B文件不能为空";
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

        #region [2、单个文件找到重复数据]
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
            var repeatSet = new Set<string>();
            //读取文件
            var itxtListA = new Set<string>();
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
                var runTime = time.Elapsed;
                SignRunTime.Text = string.Format("处理结果：待处理文件数据量{0}条,重复数据项{1}条. 一共耗时：{2}毫秒", itemCount, repeatCount, runTime.Milliseconds);
                MessageBox.Show(@"处理完成，该文件中没有重复数据", @"处理成功", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            else
            {
                var a = WriteSignFile(repeatList, resulteA, code);

                if (a.Code == 0)
                {
                    var runTime = time.Elapsed;
                    SignRunTime.Text = string.Format("处理结果：待处理文件数据量{0}条,重复数据项{1}条. 一共耗时：{2}毫秒", itemCount, repeatCount, runTime.Milliseconds);
                    MessageBox.Show(@"处理完成", @"处理成功", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
                else if (a.Code == -1)
                {
                    MessageBox.Show(a.Msg, @"异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (a.Code == 1)
                {
                    var runTime = time.Elapsed;
                    SignRunTime.Text = string.Format("处理结果：待处理文件数据量{0}条,重复数据项{1}条. 一共耗时：{2}毫秒", itemCount, repeatCount, runTime.Milliseconds);
                    const string MessageStr = "未找到重复项";
                    MessageBox.Show(@"处理完成：" + MessageStr, @"成功", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
            }
            time.Stop();
        }

        #region [2.1 获得文件编码格式]
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

        /// <summary>
        /// 写单个文件
        /// </summary>
        /// <param name="listItem"></param>
        /// <param name="fileName"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        private ResulteModel WriteSignFile(IList<string> listItem, string fileName,Encoding code)
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
            if (listItem.Count>0)
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
        #endregion
    }

    #region [3、单例使用文件夹选项]
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

    #region [4、返回结果实体]
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
        /// 处理总条数
        /// </summary>
        public int Count { get; set; }
    }
    #endregion

    #region [5、集合比较结果实体]
    /// <summary>
    /// 集合比较结果实体
    /// </summary>
    /// <typeparam name="TElement"></typeparam>
    public class SetCompareResulted<TElement>
    {
        /// <summary>
        /// A文件的数量
        /// </summary>
        public int SetACount { get; set; }

        /// <summary>
        /// B文件的数量
        /// </summary>
        public int SetBCount { get; set; }

        /// <summary>
        /// 结果集合
        /// </summary>
        public List<TElement> ResulteList = new List<TElement>();
    }
    #endregion
}
