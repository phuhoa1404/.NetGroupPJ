using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupPJ.Classes
{
    class Bill
    {
        private static int id, idTable, status;
        private static DateTime? dateCheckIn, dateCheckOut;
        public Bill(int id, DateTime? dateCheckin, DateTime? dateCheckOut, int status, int idTable)
        {
            this.Id = id;
            this.DateCheckIn = dateCheckin;
            this.DateCheckOut = dateCheckOut;
            this.Status = status;
            this.IdTable = idTable;
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public int IdTable
        {
            get { return idTable; }
            set { idTable = value; }
        }
        public int Status
        {
            get { return status; }
            set { status = value; }
        }
        public DateTime? DateCheckIn
        {
            get { return dateCheckIn; }
            set { dateCheckIn = value; }
        }
        public DateTime? DateCheckOut
        {
            get { return dateCheckOut; }
            set { dateCheckOut = value; }
        }
    }
}
