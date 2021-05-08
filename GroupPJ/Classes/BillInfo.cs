using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupPJ.Classes
{
    class BillInfo
    {
        public BillInfo(int id, int billID, int drinkID, int count)
        {
            this.ID = id;
            this.BillID = billID;
            this.FoodID = foodID;
            this.Count = count;
        }

        private int count;

        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        private int foodID;

        public int FoodID
        {
            get { return foodID; }
            set { foodID = value; }
        }

        private int billID;

        public int BillID
        {
            get { return billID; }
            set { billID = value; }
        }

        private int iD;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
    }
}
