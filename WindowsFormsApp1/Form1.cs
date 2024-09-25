using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        string path = "";//图片地址
        string filename ;//图片水印文字
        string picpath;//图片名称
        int x = 0;//定时循环计时
        string data = string.Empty;
        string checkBoxChecked;//选择
        string FileNamePath = @"D:\\自动截图\设置.csv";
        public Form1()
        {
            InitializeComponent();
            csh();
        }
        private void csh()//初始化
        {
           path = textBox1.Text;
            if (!Directory.Exists(path))//判断没有此文件夹时在D盘下新建文件夹
                 {
                    Directory.CreateDirectory(path);
                 }
            label4.Text = System.DateTime.Now.ToString("f");//显示时间
            timer2.Start();//计时器开始计时
            checkBox2.Checked = true;//
            checkBox3.Checked = true;//
            checkBox4.Checked = true;//默认开始选择时间段
            if (!File.Exists(FileNamePath))
            {
                Addcsh();
            }
            else if (File.Exists(FileNamePath))
            {
                readCsv();
            }
          
            if (checkBox1.Checked) timer1.Start();
 
        }
        private static Bitmap GetScreenCapture()
        {
            Rectangle tScreenRect = new Rectangle(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Bitmap tSrcBmp = new Bitmap(tScreenRect.Width, tScreenRect.Height); // 用于屏幕原始图片保存
            Graphics gp = Graphics.FromImage(tSrcBmp);
            gp.CopyFromScreen(0, 0, 0, 0, tScreenRect.Size);
            gp.DrawImage(tSrcBmp, 0, 0, tScreenRect, GraphicsUnit.Pixel);
            return tSrcBmp;
        }//屏幕截图
        private void bcpic()
        {
            picpath = System.DateTime.Now.ToString("yyyy年MM月dd HH时mm分ss秒");
            if (checkBox7.Checked) picpath = textBox7.Text.ToString() + picpath.ToString();
            Bitmap bitmap = GetScreenCapture();
            if (checkBox8.Checked)
            {
                try {
                    filename = textBox8.Text;
                    int width = bitmap.Width, height = bitmap.Height;
                    Graphics g = Graphics.FromImage(bitmap);
                    g.DrawImage(bitmap, 0, 0);

                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                    g.DrawImage(bitmap, new Rectangle(0, 0, width, height), 0, 0, width, height, GraphicsUnit.Pixel);

                    Font crFont = new Font("微软雅黑", 40, FontStyle.Bold);
                    SizeF crSize = new SizeF();
                    crSize = g.MeasureString(filename, crFont);
                    SolidBrush semiTransBrush = new SolidBrush(Color.Black);
                    //SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(120, 177, 171, 171));
                    g.TranslateTransform(0, 0);
                    g.RotateTransform(0);
                    // Font font = new Font(theString.ToString(), 20, FontStyle.Bold);
                    g.DrawString(filename, crFont, semiTransBrush, new PointF(0, 0));
                }
                catch
                {
                    MessageBox.Show("处理图片水印异常");
                }
                
            }
            else if (checkBox9.Checked)
            {
                try {
                    filename = textBox8.Text.ToString() + System.DateTime.Now.ToString("yyyy年MM月dd HH时mm分ss秒");
                    int width = bitmap.Width, height = bitmap.Height;
                    Graphics g = Graphics.FromImage(bitmap);
                    g.DrawImage(bitmap, 0, 0);
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.DrawImage(bitmap, new Rectangle(0, 0, width, height), 0, 0, width, height, GraphicsUnit.Pixel);
                    Font crFont = new Font("微软雅黑", 40, FontStyle.Bold);
                    SizeF crSize = new SizeF();
                    crSize = g.MeasureString(filename, crFont);
                    SolidBrush semiTransBrush = new SolidBrush(Color.Red);
                    g.TranslateTransform(0, 0);
                    g.RotateTransform(0);
                    // Font font = new Font(theString.ToString(), 20, FontStyle.Bold);
                    g.DrawString(filename, crFont, semiTransBrush, new PointF(0, 0));
                }
                catch
                {
                    MessageBox.Show("处理图片水印异常");
                }
                

            }
            bitmap.Save(@"D:\\自动截图\" + picpath.ToString() + ".jpg");//在此路径下保存图片
        }//保存图片

        private void timer1_Tick(object sender, EventArgs e)
        {
           
            if (checkBox1.Checked)
            {
                x = Convert.ToInt32(textBox2.Text) * 60 * 1000;//定时截图时间
                timer1.Interval = x;
                bcpic();
                timer1.Stop();
                timer1.Start();

            }
            else if (!checkBox1.Checked)
            {
                timer1.Stop();
            }
        }//循环截图
        private void timer2_Tick(object sender, EventArgs e)//循环判断判断是否到固定时间是否截图
        {
            label4.Text = System.DateTime.Now.ToString("f");
            if(!checkBox1.Checked) timer1.Stop();
            if (checkBox2.Checked&& checkBox3.Checked&&(System.DateTime.Now.ToString("t").ToString()== textBox3.Text))
            {
                bcpic();
            }else if (checkBox2.Checked && checkBox4.Checked && (System.DateTime.Now.ToString("t").ToString() == textBox5.Text))
            {
                bcpic();
            }
            else if (checkBox2.Checked && checkBox5.Checked && (System.DateTime.Now.ToString("t").ToString() == textBox4.Text))
            {
                bcpic();
            }
            else if (checkBox2.Checked && checkBox6.Checked && (System.DateTime.Now.ToString("t").ToString() == textBox6.Text))
            {
                bcpic();
            }
        }

        private void button2_Click(object sender, EventArgs e)//保存图片
        {
            bcpic();
            Addcsh();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try {
                x = Convert.ToInt32(textBox2.Text) * 60 * 1000;
            }
            catch
            {
                textBox2.Text = "1";
                MessageBox.Show("请输入整数分钟数，暂不支持小数");
            }

        }//判断非int类型清空并弹窗
        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Explorer.exe", "D:\\自动截图");
        }//点击路径栏打开文件夹
        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox8.Checked) checkBox9.Checked = false;
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox9.Checked) checkBox8.Checked = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                timer1.Start();
            }
            else if (!checkBox1.Checked)
            {
                timer1.Stop();
            }
        }
        private void Addcsh()
        {
            
            if (!File.Exists(FileNamePath)) //如果文件不存在
            {
                File.Create(FileNamePath).Close(); //先创建文件并关闭该文件，否则程序占用会导致文件没法被其他语句访问和读写操作   
            }
            FileStream fileStream = new FileStream(FileNamePath, FileMode.Create, FileAccess.Write); //实例化文件流类，并指定为创建模式和写
            StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8); //实例化流写类，WriteLine()将以UTF8的编码方式写入数据
            //------组装是否选择-----
            if (checkBox7.Checked) { 
                checkBoxChecked = "1"; 
                     } else if (!checkBox7.Checked)
            {
                checkBoxChecked = "0";
            }
            if (checkBox8.Checked)
            {
                checkBoxChecked = checkBoxChecked.ToString()+","+ "1";
            }
            else if (!checkBox8.Checked)
            {
                checkBoxChecked = checkBoxChecked.ToString() + "," + "0";
            }
            if (checkBox9.Checked)
            {
                checkBoxChecked = checkBoxChecked.ToString() + "," + "1";
            }
            else if (!checkBox9.Checked)
            {
                checkBoxChecked = checkBoxChecked.ToString() + "," + "0";
            }
            if (checkBox1.Checked)
            {
                checkBoxChecked = checkBoxChecked.ToString() + "," + "1";
            }
            else if (!checkBox1.Checked)
            {
                checkBoxChecked = checkBoxChecked.ToString() + "," + "0";
            }
            if (checkBox2.Checked)
            {
                checkBoxChecked = checkBoxChecked.ToString() + "," + "1";
            }
            else if (!checkBox2.Checked)
            {
                checkBoxChecked = checkBoxChecked.ToString() + "," + "0";
            }
            if (checkBox3.Checked)
            {
                checkBoxChecked = checkBoxChecked.ToString() + "," + "1";
            }
            else if (!checkBox3.Checked)
            {
                checkBoxChecked = checkBoxChecked.ToString() + "," + "0";
            }
            if (checkBox4.Checked)
            {
                checkBoxChecked = checkBoxChecked.ToString() + "," + "1";
            }
            else if (!checkBox4.Checked)
            {
                checkBoxChecked = checkBoxChecked.ToString() + "," + "0";
            }
            if (checkBox5.Checked)
            {
                checkBoxChecked = checkBoxChecked.ToString() + "," + "1";
            }
            else if (!checkBox5.Checked)
            {
                checkBoxChecked = checkBoxChecked.ToString() + "," + "0";
            }
            if (checkBox6.Checked)
            {
                checkBoxChecked = checkBoxChecked.ToString() + "," + "1";
            }
            else if (!checkBox6.Checked)
            {
                checkBoxChecked = checkBoxChecked.ToString() + "," + "0";
            }
//------组装是否选择-----
            data = checkBoxChecked.ToString()+","+textBox7.Text+","+ textBox8.Text+","+ textBox9.Text+","+ textBox2.Text+","+ textBox3.Text+","
                + textBox4.Text+","+ textBox5.Text+","+ textBox6.Text+ ","+textBox1.Text;//把字符串格式的数据用","连接起来以便写入CSV时按逗号分隔为4列
            streamWriter.WriteLine(data);
            streamWriter.Close(); //先关闭流写，写完一定要记得关闭，否则文件会被程序打开占用
            fileStream.Close(); //再关闭文件流写，写完一定要记得关闭，否则文件会被程序打开占用


        }//保存设置
        private void readCsv()
        {
           // List<string> datas;
            string filePath = @"D:\\自动截图\设置.csv"; 
            // 读取CSV文件的所有行
            string[] csvLines = File.ReadAllLines(filePath);
            foreach (string line in csvLines)
            {
                string[] rowData = line.Split(',');
                textBox7.Text = rowData[9].ToString();
                textBox8.Text = rowData[10].ToString();
                textBox9.Text = rowData[11].ToString();
                textBox2.Text = rowData[12].ToString();
                textBox3.Text = rowData[13].ToString();
                textBox4.Text = rowData[14].ToString();
                textBox5.Text = rowData[15].ToString();
                textBox6.Text = rowData[16].ToString();
                if (rowData[0].ToString()=="0")
                {
                    checkBox7.Checked = false;
                }else if (rowData[0].ToString() == "1")
                {
                    checkBox7.Checked = true;
                }else
                {
                    checkBox7.Checked = false;
                }
                if (rowData[1].ToString() == "0")
                {
                    checkBox8.Checked = false;
                }
                else if (rowData[1].ToString() == "1")
                {
                    checkBox8.Checked = true;
                }
                else
                {
                    checkBox8.Checked = false;
                }
                if (rowData[2].ToString() == "0")
                {
                    checkBox9.Checked = false;
                }
                else if (rowData[2].ToString() == "1")
                {
                    checkBox9.Checked = true;
                }
                else
                {
                    checkBox9.Checked = false;
                }
                if (rowData[3].ToString() == "0")
                {
                    checkBox1.Checked = false;
                }
                else if (rowData[3].ToString() == "1")
                {
                    checkBox1.Checked = true;
                }
                else
                {
                    checkBox1.Checked = false;
                }
                if (rowData[4].ToString() == "0")
                {
                    checkBox2.Checked = false;
                }
                else if (rowData[4].ToString()== "1")
                {
                    checkBox2.Checked = true;
                }
                else
                {
                    checkBox2.Checked = false;
                }
                if (rowData[5].ToString() == "0")
                {
                    checkBox3.Checked = false;
                }
                else if (rowData[5].ToString() == "1")
                {
                    checkBox3.Checked = true;
                }
                else
                {
                    checkBox3.Checked = false;
                }
                if (rowData[6].ToString() == "0")
                {
                    checkBox4.Checked = false;
                }
                else if (rowData[6].ToString() == "1")
                {
                    checkBox4.Checked = true;
                }
                else
                {
                    checkBox4.Checked = false;
                }
                if (rowData[7].ToString() == "0")
                {
                    checkBox5.Checked = false;
                }
                else if (rowData[7].ToString() == "1")
                {
                    checkBox5.Checked = true;
                }
                else
                {
                    checkBox5.Checked = false;
                }
                if (rowData[8].ToString() == "0")
                {
                    checkBox6.Checked = false;
                }
                else if (rowData[8].ToString() == "1")
                {
                    checkBox6.Checked = true;
                }
                else
                {
                    checkBox6.Checked = false;
                }

            }

        }//写入设置
    }
}
