using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingChecker
{
    public class Product
    {
        private string name;
        private string sku;
        private string barcode;
        private int qty = 0;
        private int count = 0;
        private int rowIndex = 0;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Sku
        {
            get { return sku; }
            set { sku = value; }
        }

        public string Barcode
        {
            get { return barcode; }
            set { barcode = value; }
        }

        public int Qty
        {
            get { return qty; }
            set { qty = value; }
        }

        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        public int RowIndex
        {
            get { return rowIndex; }
            set { rowIndex = value; }
        }
    }
}
