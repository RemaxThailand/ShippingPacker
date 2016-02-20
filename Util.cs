using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShippingChecker
{
    public class Util
    {
        static string apiUrl = "https://api-test.remaxthailand.co.th";
        public static string token = string.Empty;
        public static string orderNo = string.Empty;
        public static bool hasShippingCost = false;
        public static List<string> orderId;
        public static Dictionary<string, Product> orderDict;
        public static Dictionary<string, string> serialDict;
        public static Dictionary<string, string> skuDict;
        public static Dictionary<string, string> barcodeDict;
        public static System.Windows.Forms.Panel mainPanel;

        public static string DownloadWeb(string url, string param)
        {
            string html = string.Empty;
            using (WebClient client = new WebClient())
            {
                //client.Headers["Accept-Encoding"] = "gzip";
                client.Encoding = System.Text.Encoding.UTF8;
                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                try
                {
                    html = client.UploadString(apiUrl + url, param);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + " : " + ex.StackTrace);
                }
            }
            return html;
        }
        public static void LoadScreen(string screen)
        {
            mainPanel.Controls.Clear();
            UserControl uc = new UserControl();

            if (screen == "search")
                uc = new UcSearchOrder();
            if (screen == "detail")
                uc = new UcChecking();

            uc.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(uc);

        }

        public static void LoadToken()
        {
            string html = string.Empty;
            JObject json;
            html = DownloadWeb("/api/token/request",
                "apiKey=" + "1234" +
                "&secretKey=" + "5678");
            json = JObject.Parse(html);
            if ((bool)json["success"])
            {
                token = json["token"].ToString();
            }
        }

        public static bool LoadOrderDetail(string orderNo)
        {
            bool success = false;

            if (token == string.Empty)
                LoadToken();

            string html = string.Empty;
            JObject json;

            orderId = new List<string>();
            orderDict = new Dictionary<string, Product>();
            serialDict = new Dictionary<string, string>();
            skuDict = new Dictionary<string, string>();
            barcodeDict = new Dictionary<string, string>();

            html = DownloadWeb("/order/barcode/checker",
                "token=" + token +
                "&orderNo=" + orderNo);
            json = JObject.Parse(html);
            if ((bool)json["success"])
            {
                JArray array = (JArray)json["summary"];
                for (int i = 0; i < array.Count; i++)
                {
                    if (array[i]["id"].ToString() == "940") {
                        hasShippingCost = true;
                    }
                    else {
                        Product p = new Product();
                        orderId.Add(array[i]["id"].ToString());
                        p.RowIndex = i;
                        p.Name = array[i]["name"].ToString();
                        p.Qty = int.Parse(array[i]["qty"].ToString());
                        p.Sku = array[i]["sku"].ToString();
                        p.Barcode = array[i]["barcode"].ToString();
                        orderDict.Add(array[i]["id"].ToString(), p);
                        skuDict.Add(array[i]["sku"].ToString(), array[i]["id"].ToString());
                        if (array[i]["barcode"].ToString() != "")
                        {
                            barcodeDict.Add(array[i]["barcode"].ToString(), array[i]["id"].ToString());
                            string[] sp = array[i]["barcode"].ToString().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                            if (barcodeDict.ContainsKey(sp[0]))
                            {
                                if (sp.Length == 1)
                                    skuDict.Add(sp[0], array[i]["id"].ToString());
                                else {
                                    barcodeDict[sp[0]] += sp[1] + "," + array[i]["id"].ToString() + "|";
                                }
                            }
                            else
                            {
                                barcodeDict.Add(sp[0], sp[1] + "," + array[i]["id"].ToString() + "|");
                            }
                        }
                    }
                }

                array = (JArray)json["barcode"];
                for (int i = 0; i < array.Count; i++)
                {
                    serialDict.Add(array[i]["serialNo"].ToString(), array[i]["id"].ToString());
                    if (orderDict[array[i]["id"].ToString()].Sku != ""
                        && skuDict.ContainsKey(orderDict[array[i]["id"].ToString()].Sku))
                        skuDict.Remove(orderDict[array[i]["id"].ToString()].Sku);
                    if (orderDict[array[i]["id"].ToString()].Barcode != ""
                        && barcodeDict.ContainsKey(orderDict[array[i]["id"].ToString()].Barcode))
                        barcodeDict.Remove(orderDict[array[i]["id"].ToString()].Barcode);
                }
                success = orderDict.Count > 0;
            }

            return success;
        }
    }
}
