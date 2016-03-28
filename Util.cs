using Microsoft.Synchronization;
using Microsoft.Synchronization.Data;
using Microsoft.Synchronization.Data.SqlServer;
using Microsoft.Synchronization.Data.SqlServerCe;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Data.SqlServerCe;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zen.Barcode;

namespace ShippingChecker
{
    public class Util
    {
        static string apiUrl = "https://api.remaxthailand.co.th";
        public static string token = string.Empty;
        public static string orderNo = string.Empty;
        public static bool hasShippingCost = false;
        public static List<string> orderId;
        public static Dictionary<string, Product> orderDict;
        public static Dictionary<string, string> serialDict;
        public static Dictionary<string, string> skuDict;
        public static Dictionary<string, string> barcodeDict;
        public static System.Windows.Forms.Panel mainPanel;

        public static JObject jsonAddress;

        private const string SQLiteFileName = "System.dll";
        private static SQLiteConnection SQLiteConnection;

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

                var d = 0;
                StringBuilder sb = new StringBuilder(@"INSERT OR REPLACE INTO SellHeader (orderNo, product, qty, name, barcode, sku) ");

                for (int i = 0; i < array.Count; i++)
                {
                    if (array[i]["id"].ToString() == "940")
                    {
                        hasShippingCost = true;
                    }
                    else {
                        Product p = new Product();
                        p.Id = array[i]["id"].ToString();
                        p.RowIndex = d;
                        p.Name = array[i]["name"].ToString();
                        p.Qty = int.Parse(array[i]["qty"].ToString());
                        p.Sku = array[i]["sku"].ToString();
                        p.Barcode = array[i]["barcode"].ToString();
                        orderId.Add(p.Id);
                        orderDict.Add(p.Id, p);
                        skuDict.Add(array[i]["sku"].ToString(), p.Id);
                        if (array[i]["barcode"].ToString() != "")
                        {
                            barcodeDict.Add(array[i]["barcode"].ToString(), p.Id);
                            string[] sp = array[i]["barcode"].ToString().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                            if (barcodeDict.ContainsKey(sp[0]))
                            {
                                if (sp.Length == 1)
                                    if (!skuDict.ContainsKey(sp[0]))
                                        skuDict.Add(sp[0], p.Id);
                                    else if (sp.Length >= 2)
                                    {
                                        barcodeDict[sp[0]] += sp[1] + "," + p.Id + "|";
                                    }
                            }
                            else
                            {
                                if (!barcodeDict.ContainsKey(sp[0]))
                                    barcodeDict.Add(sp[0], sp[1] + "," + p.Id + "|");
                            }
                        }

                        if (d != 0) sb.Append(" UNION ALL ");
                        sb.Append(string.Format(@" SELECT '{0}', '{1}', {2}, '{3}', '{4}', '{5}'", orderNo, p.Id, p.Qty, p.Name, p.Barcode, p.Sku));
                        d++;
                    }
                }
                DBExecute(sb.ToString());

                d = 0;
                sb = new StringBuilder(@"INSERT OR REPLACE INTO SellDetail (orderNo, product, serial, isChecked) ");

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

                    if (d % 100 == 0 && d >= 100)
                    {
                        DBExecute(sb.ToString());
                        sb = new StringBuilder(@"INSERT OR REPLACE INTO SellDetail (orderNo, product, serial, isChecked) ");
                    }
                    if (d % 100 != 0) sb.Append(" UNION ALL ");
                    sb.Append(string.Format(@" SELECT '{0}', '{1}', '{2}', {3}", orderNo, array[i]["id"].ToString(), array[i]["serialNo"].ToString(), array[i]["isChecked"].ToString()));

                    d++;

                }
                success = orderDict.Count > 0;

                DBExecute(sb.ToString());
            }

            return success;
        }



        public static bool LoadOrderAddress(string orderNo)
        {
            bool success = false;

            if (token == string.Empty)
                LoadToken();

            string html = string.Empty;
            JObject json;

            html = DownloadWeb("/order/address",
                "token=" + token +
                "&orderNo=" + orderNo);
            json = JObject.Parse(html);
            if ((bool)json["success"])
            {
                jsonAddress = (JObject)json["result"];
                success = true;
            }

            return success;
        }

        public static void ConnectSQLiteDatabase()
        {
            if (!File.Exists(SQLiteFileName))
            {
                SQLiteConnection.CreateFile(SQLiteFileName);
            }
            SQLiteConnection = new SQLiteConnection("Data Source=" + SQLiteFileName + ";Version=3;New=True;Compress=True;");
            SQLiteConnection.Open();

            DBExecute(@"CREATE TABLE IF NOT EXISTS SellHeader (
                orderNo  NVARCHAR(20) NOT NULL,
                product  NVARCHAR(10) NOT NULL,
                qty INT NOT NULL,
                name  NVARCHAR(256) NOT NULL,
                barcode  NVARCHAR(128),
                sku  NVARCHAR(16),
                PRIMARY KEY (orderNo, product))");

            DBExecute(@"CREATE TABLE IF NOT EXISTS SellDetail (
                orderNo  NVARCHAR(20) NOT NULL,
                product  NVARCHAR(10) NOT NULL,
                serial  NVARCHAR(256) NOT NULL,
                isChecked  BOOL DEFAULT 0,
                PRIMARY KEY (orderNo, product, serial))");

            /*
            CREATE TABLE SellHeader (orderNo VARCHAR NOT NULL, product VARCHAR NOT NULL, sku VARCHAR, barcode VARCHAR, name VARCHAR, qty INTEGER, PRIMARY KEY (orderNo, product)) */
        }

        public static DataTable DBQuery(string sql)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            DataTable dt = new DataTable();
            try
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, SQLiteConnection);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.Message);
                WriteErrorLog(ex.StackTrace);
            }
            return dt;
        }

        public static void DBExecute(string sql)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            try
            {
                SQLiteCommand command = new SQLiteCommand(sql, SQLiteConnection);
                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.Message);
                WriteErrorLog(ex.StackTrace);
            }
        }

        public static void WriteErrorLog(string message)
        {
            string filename = "error-" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            StreamWriter sw = new StreamWriter(filename, true);
            sw.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "\t" + message);
            sw.Close();
        }

        public static void PrintAddress(PrintPageEventArgs g, string orderNo, int boxStart, int boxCount, string shipping, bool isCash)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            for (int x = boxStart; x <= boxCount; x++)
            {
                var pX = 60;
                var pY = (x%2 == 1) ? 60 : 640;

                Image image = Image.FromFile("logo.png");
                Rectangle destRect = new Rectangle(pX, pY, 134, 32);
                g.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);

                SolidBrush brush = new SolidBrush(Color.Black);
                Font stringFont = new Font("DilleniaUPC", 16);
                g.Graphics.DrawString("โทรศัพท์ 081-2673-388", stringFont, brush, new PointF(pX - 2, pY + 35));

                g.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Gray)), pX + 430, pY, 270, 50);
                
                Code128BarcodeDraw bdw = BarcodeDrawFactory.Code128WithChecksum;
                Image img = bdw.Draw(orderNo.ToUpper(), 24);
                g.Graphics.DrawImage(img, new Point(pX + 435, pY + 5));

                stringFont = new Font("Calibri", 10);
                g.Graphics.DrawString(orderNo, stringFont, brush, new PointF(pX + 432, pY + 31));

                DateTime now = DateTime.Now;
                g.Graphics.DrawString(now.ToString("ddMMHHmm"), stringFont, brush, new PointF(pX + 517, pY + 31));


                stringFont = new Font("Calibri", 30, FontStyle.Bold);
                string measureString = x + "/" + boxCount;
                SizeF stringSize = g.Graphics.MeasureString(measureString, stringFont);
                g.Graphics.DrawString(measureString, stringFont, brush, new PointF(pX + 577 + ((124 - stringSize.Width) / 2), pY - 2));


                stringFont = new Font("DilleniaUPC", 18);
                if (isCash) g.Graphics.DrawString("เก็บเงินสดปลายทาง", stringFont, brush, new PointF(pX + 425, pY + 47));
                stringFont = new Font("DilleniaUPC", 18, FontStyle.Bold);
                stringSize = g.Graphics.MeasureString(shipping, stringFont);
                g.Graphics.DrawString(shipping, stringFont, brush, new PointF(pX + 705 - stringSize.Width, pY + 47));


                pX += 200;
                pY += 170;
                stringFont = new Font("DilleniaUPC", 20);
                g.Graphics.DrawString("กรุณาส่ง", stringFont, brush, new PointF(pX - 50, pY));
                g.Graphics.DrawString("โทรศัพท์ "+ jsonAddress["mobile"].ToString().Substring(0,3) + "-" + 
                    jsonAddress["mobile"].ToString().Substring(3, 4) + "-" + 
                    jsonAddress["mobile"].ToString().Substring(7), 
                    stringFont, brush, new PointF(pX + 47, pY));
                pY += 50;
                stringFont = new Font("DilleniaUPC", 26, FontStyle.Bold);
                g.Graphics.DrawString("คุณ "+ jsonAddress["firstname"].ToString() + " " + jsonAddress["lastname"].ToString(), stringFont, brush, new PointF(pX, pY));
                pY += 42;
                pX += 47;
                if (jsonAddress["shopName"].ToString().Trim() != "")
                {
                    stringFont = new Font("DilleniaUPC", 24, FontStyle.Bold);
                    g.Graphics.DrawString(jsonAddress["shopName"].ToString(), stringFont, brush, new PointF(pX, pY));
                    pY += 40;
                }
                stringFont = new Font("DilleniaUPC", 20);
                g.Graphics.DrawString(jsonAddress["address"].ToString(), stringFont, brush, new PointF(pX, pY));
                pY += 35;
                if (jsonAddress["address2"].ToString().Trim() != "")
                {
                    g.Graphics.DrawString(jsonAddress["address2"].ToString(), stringFont, brush, new PointF(pX, pY));
                    pY += 35;
                }

                bool isBkk = jsonAddress["province"].ToString() == "กรุงเทพมหานคร";
                g.Graphics.DrawString((isBkk ? "แขวง" : "ต.") + jsonAddress["subDistrict"].ToString() +
                    (isBkk ? " เขต" : " อ.") + jsonAddress["district"].ToString() +
                    (isBkk ? " " : " จ.") + jsonAddress["province"].ToString() + "", 
                    stringFont, brush, new PointF(pX, pY));
                pY += 35;
                g.Graphics.DrawString("รหัสไปรษณีย์", stringFont, brush, new PointF(pX + 30, pY));
                stringFont = new Font("Calibri", 20, FontStyle.Bold);
                g.Graphics.DrawString(jsonAddress["zipcode"].ToString(), stringFont, brush, new PointF(pX + 140, pY));

                if (x % 2 == 0) break;
            }


        }


            /*public static void LoadDatabase()
            {
                sqlSrvConnectionString = @"Data Source=111;Initial Catalog=R;User ID=sa;Password=P";
                sqlCeConnectionString = string.Format("DataSource=\"{0}\"; Password='{1}'", sqlCeFileName, sqlCePassword);

                if (!File.Exists(sqlCeFileName))
                {
                    SqlCeEngine engine = new SqlCeEngine(sqlCeConnectionString);
                    engine.CreateDatabase();
                    engine.Dispose();
                }
            }

            public static void Setup()
            {
                //try
                //{
                    SqlCeConnection sqlCeConn = new SqlCeConnection(sqlCeConnectionString);
                    SqlConnection sqlAzureConn = new SqlConnection(sqlSrvConnectionString);
                    DbSyncScopeDescription myScope = new DbSyncScopeDescription("alltablesyncgroup");
                    DbSyncTableDescription Customer = SqlSyncDescriptionBuilder.GetDescriptionForTable("Province", sqlAzureConn);
                    DbSyncTableDescription Product = SqlSyncDescriptionBuilder.GetDescriptionForTable("District", sqlAzureConn);
                    DbSyncTableDescription Barcode = SqlSyncDescriptionBuilder.GetDescriptionForTable("Product", sqlAzureConn);

                    // Add the tables from above to the scope
                    myScope.Tables.Add(Customer);
                    myScope.Tables.Add(Product);
                    myScope.Tables.Add(Barcode);

                    // Setup SQL Azure for sync
                    SqlSyncScopeProvisioning sqlAzureProv = new SqlSyncScopeProvisioning(sqlAzureConn, myScope);
                    if (!sqlAzureProv.ScopeExists("alltablesyncgroup"))
                    {
                        // Apply the scope provisioning.
                        Console.WriteLine("Provisioning SQL Azure for sync " +DateTime.Now);
                        sqlAzureProv.Apply();
                        Console.WriteLine("Done Provisioning SQL Azure for sync " +DateTime.Now);
                    }

                    else
                        Console.WriteLine("SQL Azure Database server already provisioned for sync " +DateTime.Now);


                    SqlCeSyncScopeProvisioning sqlCeProv = new SqlCeSyncScopeProvisioning(sqlCeConn, myScope);

                    if (!sqlCeProv.ScopeExists("alltablesyncgroup"))
                    {
                        // Apply the scope provisioning.
                        Console.WriteLine("Provisioning SQL CE for sync " +DateTime.Now);
                        sqlCeProv.Apply();
                        Console.WriteLine("Done Provisioning SQL CE for sync " +DateTime.Now);
                    }
                    else
                        Console.WriteLine("SQL CE Database server already provisioned for sync " +DateTime.Now);

                    sqlAzureConn.Close();
                    sqlCeConn.Close();

                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine(ex);
                //}

            }

            public static void Sync()
            {

                //try

                //{



                    SqlCeConnection sqlCeConn = new SqlCeConnection(sqlCeConnectionString);

                    SqlConnection sqlAzureConn = new SqlConnection(sqlSrvConnectionString);

                    SyncOrchestrator orch = new SyncOrchestrator

                    {

                        RemoteProvider = new SqlSyncProvider("alltablesyncgroup", sqlAzureConn),
                        LocalProvider = new SqlCeSyncProvider("alltablesyncgroup", sqlCeConn),
                        Direction = SyncDirectionOrder.UploadAndDownload

                    };

                    Console.WriteLine("ScopeName ={0} ", "alltablesyncgroup");

                    Console.WriteLine("Starting Sync " +DateTime.Now);

                    ShowStatistics(orch.Synchronize());



                    sqlAzureConn.Close();

                    sqlCeConn.Close();

                //}

                //catch (Exception ex)

                //{

                //    Console.WriteLine(ex);

                //}

            }



            public static void ShowStatistics(SyncOperationStatistics syncStats)

            {

                string message;



                message = "\tSync Start Time:" +syncStats.SyncStartTime.ToString();

                Console.WriteLine(message);

                message = "\tSync End Time:" +syncStats.SyncEndTime.ToString();

                Console.WriteLine(message);

                message = "\tUpload Changes Applied:" +syncStats.UploadChangesApplied.ToString();

                Console.WriteLine(message);

                message = "\tUpload Changes Failed:" +syncStats.UploadChangesFailed.ToString();

                Console.WriteLine(message);

                message = "\tUpload Changes Total:" +syncStats.UploadChangesTotal.ToString();

                Console.WriteLine(message);

                message = "\tDownload Changes Applied:" +syncStats.DownloadChangesApplied.ToString();

                Console.WriteLine(message);

                message = "\tDownload Changes Failed:" +syncStats.DownloadChangesFailed.ToString();

                Console.WriteLine(message);

                message = "\tDownload Changes Total:" +syncStats.DownloadChangesTotal.ToString();

                Console.WriteLine(message);

            }*/
        }
    }
