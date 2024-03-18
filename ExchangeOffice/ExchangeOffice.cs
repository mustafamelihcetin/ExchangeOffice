using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ExchangeOffice
{
    public partial class ExchangeOffice : Form
    {
        private Timer timer;
        public ExchangeOffice()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetCurrency();
        }
        private void InitializeTimer()
        {
            timer = new Timer();
            timer.Interval = 10000;
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            GetCurrency();
        }
        private void GetCurrency()
        {
            string dateOfToday = "https://www.tcmb.gov.tr/kurlar/today.xml";
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(dateOfToday);

            string UsdBuying = xmlDocument.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/BanknoteBuying").InnerXml;
            LblUsdBuy.Text = UsdBuying;
            string UsdSelling = xmlDocument.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/BanknoteSelling").InnerXml;
            LblUsdSell.Text = UsdSelling;

            string EurBuying = xmlDocument.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/BanknoteBuying").InnerXml;
            LblEurBuy.Text = EurBuying;
            string EurSelling = xmlDocument.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/BanknoteSelling").InnerXml;
            LblEurSell.Text = EurSelling;
        }

        private void BtnUsdBuy_Click(object sender, EventArgs e)
        {
            TbRate.Text=LblUsdBuy.Text;
            BtnActionIsBuy();
        }

        private void BtnUsdSell_Click(object sender, EventArgs e)
        {
            TbRate.Text = LblUsdSell.Text;
            BtnActionIsSell();
        }

        private void BtnEurBuy_Click(object sender, EventArgs e)
        {
            TbRate.Text = LblEurBuy.Text;
            BtnActionIsBuy();
        }

        private void BtnEurSell_Click(object sender, EventArgs e)
        {
            TbRate.Text = LblEurSell.Text;
            BtnActionIsSell();
        }

        private void BtnMakeASale_Click(object sender, EventArgs e)
        {
            int amountFromTbAmount;
            if (string.IsNullOrWhiteSpace(TbAmount.Text) || int.Parse(TbAmount.Text) < 0)
            {
                amountFromTbAmount = 1;
                TbAmount.Text = "1";

            }
            else
                amountFromTbAmount = int.Parse(TbAmount.Text);

            float totalToTbTotal = amountFromTbAmount * float.Parse(TbRate.Text);
            string newTotal = totalToTbTotal + "₺";
            TbTotal.Text = newTotal.ToString();

        }

        private void TbRate_TextChanged(object sender, EventArgs e)
        {
            TbRate.Text = TbRate.Text.Replace(".", ",");
        }
        private void BtnActionIsSell()
        {
            BtnAction.Text = "Sell";
            BtnAction.Enabled = true;
        }
        private void BtnActionIsBuy()
        {
            BtnAction.Text = "Buy";
            BtnAction.Enabled = true;
        }
    }
}
