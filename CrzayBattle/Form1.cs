using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.Media;

namespace CrzayBattle { 
    public partial class Form1 : Form
    {
        List<string> letterList = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "J", "K", "L", "M" };
        List<string> numbList = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
        List<Button> buttonList = new List<Button>();
        //  Horizontal 
        Dictionary<string, List<string>> myDic = new Dictionary<string, List<string>>();
        Dictionary<string, List<string>> ShipDicHorizontal = new Dictionary<string, List<string>>();
        Dictionary<string, List<string>> ShipDicRemoveHorizontal = new Dictionary<string, List<string>>();
        Dictionary<string, List<string>> ShipDicHorizontalFull = new Dictionary<string, List<string>>();
        List<int> valukeysaveList = new List<int>();
        //   Vertical 
        Dictionary<string, List<string>> ShipDicVertical = new Dictionary<string, List<string>>();
        Dictionary<string, List<string>> ShipDicRemoveVertical = new Dictionary<string, List<string>>();
        Dictionary<string, List<string>> ShipDicVerticalFull = new Dictionary<string, List<string>>();

        List<int> LetterListindex = new List<int>();
        int count = 0;
        int clickCount = 0;

        List<string> horizontal = new List<string>();
        List<string> vertical = new List<string>();
        Random rm = new Random();
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            createMethod();
            createShipListMethot();
        }
        public void createMethod()
        {
            int top = 90;
            for (int i = 0; i < 12; i++)
            {
                myDic.Add(letterList[i], numbList);
                int left = 60;
                for (int j = 0; j < 12; j++)
                {
                    Button btn = new Button();
                    btn.Name = "" + letterList[i] + numbList[j];
                    btn.Top = top;
                    btn.Left = left;
                    btn.Height = 41;
                    btn.Width = 41;
                    left += 40;
                    btn.Click += btnClickMethodHorizontal;
                    btn.Click += btnClickMethodVertical;
                    buttonList.Add(btn);
                    this.Controls.Add(btn);
                }
                Label lbl1 = new Label();
                lbl1.Name = letterList[i];
                lbl1.Text = lbl1.Name;
                lbl1.Width = 30;
                lbl1.Height = 40;
                lbl1.Top = top + 12;
                lbl1.Left = 20;
                this.Controls.Add(lbl1);
                top += 40;
                Label lbl2 = new Label();
                lbl2.Name = numbList[i];
                lbl2.Text = lbl2.Name;
                lbl2.Width = 40;
                lbl2.Height = 40;
                lbl2.Top = 60;
                lbl2.Left = top - 67;
                this.Controls.Add(lbl2);
            }
        }
        public void btnClickMethodHorizontal(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            ++clickCount;
            label8.Text = clickCount.ToString();
            foreach (var item in ShipDicHorizontal)
            {
                for (int i = 0; i < item.Value.Count; i++)
                {
                    if ((item.Key + item.Value[i]) == btn.Name)
                    {
                        btn.Enabled = false;
                        btn.BackColor = Color.Red;
                        foreach (var item1 in ShipDicRemoveHorizontal)
                        {
                            for (int j = 0; j < item1.Value.Count; j++)
                            {
                                if (item1.Key + item1.Value[j] == btn.Name)
                                {
                                    if (item1.Value.Count < 2)
                                    {
                                        ++count;
                                        label3.Text = "" + (7 - count);
                                        if (clickCount > 100)
                                        {
                                            string g = "   Məğlub    oldunuz :( ";
                                            loseWinMsj(g);
                                        }
                                        else if (count > 6)
                                        {
                                            if (clickCount < Convert.ToInt32(label4.Text) || label4.Text == "0")
                                            {
                                                label4.Text = "" + clickCount;
                                            }
                                            string g = "Qalib  Gəldiniz XAL:" + clickCount.ToString();
                                            loseWinMsj(g);
                                        }
                                        else
                                            MessageBox.Show("Gəmi partladıldı", "Xəbərdarlıq");
                                    }
                                    else
                                    {
                                        item1.Value.Remove(item1.Value[j]);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        btn.Enabled = false;
                    }
                }
            }
        }
        // Horizontal *********************************************
        public void createShipMethod(int vk, int bg, int hl)
        {
            List<string> numbList1 = new List<string>();
            List<string> numbList2 = new List<string>();
            for (int i = bg; i < bg + hl; i++)
            {
                numbList1.Add(numbList[i]);
            }
            for (int i = ((bg > 0) ? (bg - 1) : 0); i < ((bg + hl < 12) ? (bg + hl + 1) : 12); i++)
            {
                numbList2.Add(numbList[i]);
            }
            ShipDicHorizontal.Add(letterList[vk], numbList1);
            ShipDicRemoveHorizontal.Add(letterList[vk], numbList1);
            ShipDicHorizontalFull.Add(letterList[vk], numbList2);
            for (int i = 0; i < numbList2.Count; i++)
            {
                horizontal.Add(letterList[vk] + numbList2[i]);
                if (vk > 0)
                    horizontal.Add((letterList[vk - 1] + numbList2[i]));
                if (vk < 11)
                    horizontal.Add((letterList[vk + 1] + numbList2[i]));
            }
        }
        public void valuekeysametestMethod(ref int valuekey)
        {
            bool z = true;
            foreach (var item in valukeysaveList)
            {
                if ((item == valuekey))
                {
                    z = false;
                    break;
                }
            }
            if (z == false)
            {
                valuekey = rm.Next(0, 11);
                valuekeysametestMethod(ref valuekey);
            }
            if (z == true)
            {
                valukeysaveList.Add(valuekey);
                valukeysaveList.Add(valuekey - 1);
                valukeysaveList.Add(valuekey + 1);
            }
        }
        public void createShipListMethot()
        {
            int valuekey = 99;
            for (int i = 0; i < 4; i++)
            {
                valukeysaveList.Add(valuekey);
                valuekeysametestMethod(ref valuekey);

                int bigining = 12;
                int howlong = 12;
                while (bigining + howlong > 12)
                {
                    bigining = rm.Next(0, 8);
                    howlong = rm.Next(3, 6);
                }
                createShipMethod(valuekey, bigining, howlong);
            }
            createShipListMethotVertical();
        }
        // vertical *****************************************
        public void btnClickMethodVertical(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            foreach (var item in ShipDicVertical)
            {
                for (int i = 0; i < item.Value.Count; i++)
                {
                    if ((item.Value[i] + item.Key) == btn.Name)
                    {
                        btn.Enabled = false;
                        btn.BackColor = Color.Red;
                        foreach (var item1 in ShipDicRemoveVertical)
                        {
                            for (int j = 0; j < item1.Value.Count; j++)
                            {
                                if (item1.Value[j] + item1.Key == btn.Name)
                                {
                                    if (item1.Value.Count < 2)
                                    {
                                        ++count;
                                        label3.Text = "" + (7 - count);
                                        if (clickCount > 100)
                                        {
                                            string g = "   Məğlub    oldunuz)";
                                            loseWinMsj(g);
                                        }
                                        else if (count > 6)
                                        {
                                            if (clickCount < Convert.ToInt32(label4.Text) || label4.Text == "0")
                                            {
                                                label4.Text = "" + clickCount;
                                            }
                                            string g = "Qalib Gəldiniz  XAL:" + clickCount.ToString();
                                            loseWinMsj(g);
                                        }
                                        else
                                            MessageBox.Show("Gəmi partladıldı", "Xəbərdarlıq");
                                    }
                                    else
                                    {
                                        item1.Value.Remove(item1.Value[j]);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public void createShipMethodVertical(int numberStart, int bg, int hl)
        {
            List<string> letterList3 = new List<string>();
            for (int i = bg; i < bg + hl; i++)
            {
                letterList3.Add(letterList[i]);
            }
            ShipDicVertical.Add(numbList[numberStart], letterList3);
            ShipDicRemoveVertical.Add(numbList[numberStart], letterList3);
            for (int i = 0; i < letterList3.Count; i++)
            {
                vertical.Add(letterList3[i] + numbList[numberStart]);
            }
        }
        public void numberStartsametestMethodVertical(ref int numberStart)
        {
            bool x = true;
            foreach (var item in LetterListindex)
            {
                if (item == numberStart)
                {
                    x = false;
                    break;
                }
            }
            if (x == false)
            {
                numberStart = rm.Next(0, 11);
                numberStartsametestMethodVertical(ref numberStart);
            }
            else
            {
                LetterListindex.Add(numberStart);
                LetterListindex.Add(numberStart - 1);
                LetterListindex.Add(numberStart + 1);
            }
        }
        public void createShipListMethotVertical()
        {
            int numberStart = 99;
            for (int k = 0; k < 3; k++)
            {
                LetterListindex.Add(numberStart);
                numberStartsametestMethodVertical(ref numberStart);
                int biginingletter = 12;
                int howlong = 12;
                while (biginingletter + howlong > 12)
                {
                    biginingletter = rm.Next(0, 9);
                    howlong = rm.Next(2, 5);
                }
                createShipMethodVertical(numberStart, biginingletter, howlong);
            }
            testVerticalSameHorizontal(ref LetterListindex);
        }
        public void testVerticalSameHorizontal(ref List<int> LetterListindex)
        {
            bool a = true;
            foreach (var item in horizontal)
            {
                foreach (var item2 in vertical)
                {
                    if (item == item2)
                    {
                        a = false;
                        break;
                    }
                }
                if (a == false)
                {
                    break;
                }
            }
            if (a == false)
            {
                ShipDicVertical.Clear();
                ShipDicRemoveVertical.Clear();
                vertical.Clear();
                LetterListindex.Clear();
                createShipListMethotVertical();
            }
        }
        public void loseWinMsj(string g)
        {
            foreach (var item3 in buttonList)
            {
                item3.Enabled = false;
                item3.BackColor = Color.LightGray;
            }
            label6.Text = g;
            label6.Visible = true;
        }
        private void label1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    this.Controls.RemoveByKey("" + letterList[i] + numbList[j]);

                }
            }
            foreach (var item in letterList)
            {
                this.Controls.RemoveByKey("" + item);
            }
            foreach (var item in numbList)
            {
                this.Controls.RemoveByKey("" + item);
            }
            buttonList.Clear();
            myDic.Clear();
            count = 0;
            clickCount = 0;
            ShipDicHorizontal.Clear();
            ShipDicRemoveHorizontal.Clear();
            ShipDicHorizontalFull.Clear();
            valukeysaveList.Clear();
            ShipDicVertical.Clear();
            ShipDicRemoveVertical.Clear();
            ShipDicVerticalFull.Clear();
            LetterListindex.Clear();
            horizontal.Clear();
            vertical.Clear();
            label6.Visible = false;
            label3.Text = "7";
            label8.Text = "0";
            createMethod();
            createShipListMethot();
        }
        public void btnClickMethod(Button btn, Dictionary<string, List<string>> ShipDicVertical, Dictionary<string, List<string>> ShipDicRemoveVertical)
        {
            foreach (var item in ShipDicVertical)
            {
                for (int i = 0; i < item.Value.Count; i++)
                {
                    if ((item.Value[i] + item.Key) == btn.Name)
                    {
                        btn.Enabled = false;
                        btn.BackColor = Color.Red;
                        foreach (var item1 in ShipDicRemoveVertical)
                        {
                            for (int j = 0; j < item1.Value.Count; j++)
                            {
                                if (item1.Value[j] + item1.Key == btn.Name)
                                {
                                    if (item1.Value.Count < 2)
                                    {
                                        ++count;
                                        label3.Text = "" + (7 - count);
                                        if (clickCount > 100)
                                        {
                                            string g = "   Məğlub    oldunuz)";
                                            SoundPlayer play = new SoundPlayer();
                                            loseWinMsj(g);
                                        }
                                        else if (count > 6)
                                        {
                                            if (clickCount < Convert.ToInt32(label4.Text) || label4.Text == "0")
                                            {
                                                label4.Text = "" + clickCount;
                                            }
                                            string g = "Qalib Gəldiniz  XAL:" + clickCount.ToString();
                                            loseWinMsj(g);
                                        }
                                        else
                                            MessageBox.Show("Gəmi partladıldı", "Xəbərdarlıq");
                                    }
                                    else
                                    {
                                        item1.Value.Remove(item1.Value[j]);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}












