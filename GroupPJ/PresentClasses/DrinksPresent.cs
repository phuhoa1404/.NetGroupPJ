using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroupPJ.Classes;
using System.Data.SqlClient;

namespace GroupPJ.PresentClasses
{
    class DrinksPresent
    {
        private static DrinksPresent instance;
        public static DrinksPresent Instance
        {
            get { if (instance == null) instance = new DrinksPresent(); return DrinksPresent.instance; }
            set { DrinksPresent.instance = value; }
        }

        public List<Drinks> LoadDrinksList()
        {
            List<Drinks> drinksList = new List<Drinks>();
            ClsDatabase.OpenConnection();

            SqlCommand command = new SqlCommand("SELECT * FROM Drinks", ClsDatabase.con);
            SqlDataReader rd = command.ExecuteReader();
            while (rd.Read())
            {
                Drinks drink = new Drinks();
                drink.Id = rd.GetInt32(0);
                drink.Name = rd.GetString(1);
                drink.Price =(float) rd.GetDouble(2);
                drinksList.Add(drink);
            }
            ClsDatabase.CloseConnection();
            return drinksList;
        } 
    }
}
