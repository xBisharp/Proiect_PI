using Blazorise;
using FarmaHelp.Pages;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace FarmaHelp.Data
{
    public static class DataAccessService
    {
        private static SqlConnection conn;

        public static void InitializeConnection()
        {

            string connectionString = "Server=DESKTOP-PI7PT95\\SQLEXPRESS;Database=FarmaHelp;User Id=user;Password=user;Pooling=false;MultipleActiveResultSets=true;";
            conn = new SqlConnection(connectionString);

        }

        public static List<Medicine> getMedicines()
        {
            List<Medicine> medicines = new List<Medicine>();
            SqlDataAdapter adapter = new SqlDataAdapter();
            string sql = "select * from dbo.medicamente";
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            //int rowsaffected = cmd.ExecuteNonQuery(); //pt update si delete si insert
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var record = (IDataRecord)reader;
                medicines.Add(new Medicine()
                {
                    name = record.GetString(0),
                    id = record.GetInt32(1),
                    pret = record.GetDouble(2)
                });
            }
            conn.Close();
            return medicines;

        }
        public static List<User> getUsers()
        {
            List<User> users = new List<User>();
            SqlDataAdapter adapter = new SqlDataAdapter();
            string sql = "select * from dbo.utilizator";
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            //int rowsaffected = cmd.ExecuteNonQuery(); //pt update si delete si insert
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var record = (IDataRecord)reader;
                users.Add(new User()
                {
                    Name = record.GetString(0),
                    PreName= record.GetString(1),
                    Email = record.GetString(5),
                    Password = record.GetString(2),
                    esteAdmin = record.GetBoolean(3),
                    PhoneNumber = record.GetString(4)
                });
            }
            conn.Close();
            return users;

        }
        public static void insertUser(string name, string prenume, string parola, bool esteAdmin, string phoneNumber, string Email)
        {
            List<User> users = new List<User>();
            string sql = "INSERT INTO [dbo].[utilizator]\r\n" +
                $"([Nume]\r\n,[Prenume]\r\n,[Parola]\r\n,[esteAdmin]\r\n,[PhoneNumber],[Email])\r\n  VALUES\r\n('{name}'\r\n,'{prenume}'\r\n,'{parola}'\r\n,'{esteAdmin}'\r\n,'{phoneNumber}','{Email}')";
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            try
            {
                int rowsaffected = cmd.ExecuteNonQuery(); //pt update si delete si insert

            }
            catch (Exception ex)
            {

            }

            conn.Close();

        }

        public static List<Cart> getCos()
        {
            List<Cart> coses= new List<Cart>();
            SqlDataAdapter adapter = new SqlDataAdapter();
            string sql = "SELECT [idMedicamente],[Cantitate],Nume, medicamente.Pret FROM [FarmaHelp].[dbo].[cos] join medicamente on medicamente.id = idMedicamente";
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            //int rowsaffected = cmd.ExecuteNonQuery(); //pt update si delete si insert
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var record = (IDataRecord)reader;
                coses.Add(new Cart()
                {
                    idMedicament= record.GetInt32(0),
                    Cantitate = record.GetInt32(1),
                    numeMedicament = record.GetString(2),
                    pret = record.GetDouble(3)
                });
            }
            conn.Close();
            return coses;

        }

        public static void insertCos(int idMedicament, int cantitate)
        {
            string sql;
            List<Medicine> medicines = getMedicines();
            List<Cart> coses = getCos();
            var selectedmedicine = medicines.Find(el => el.id == idMedicament);
            var selectedCos = coses.FirstOrDefault((el) => el.idMedicament == selectedmedicine.id);
            if (selectedCos == null)
                sql = $"INSERT INTO [dbo].[cos]\r\n([idMedicamente]\r\n,[Cantitate])\r\nVALUES\r\n({selectedmedicine.id}\r\n,{cantitate})";
            else
                sql = $"INSERT INTO [dbo].[cos]\r\n([idMedicamente]\r\n,[Cantitate])\r\nVALUES\r\n({selectedmedicine.id}\r\n,{selectedCos.Cantitate + cantitate})";
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            try
            {
                int rowsaffected = cmd.ExecuteNonQuery(); //pt update si delete si insert

            }
            catch (Exception ex)
            {

            }
            conn.Close();
        }

        public static void EmptyCos()
        {
            string sql;
            sql = $"DELETE FROM cos";
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            try
            {
                int rowsaffected = cmd.ExecuteNonQuery(); //pt update si delete si insert
            }
            catch (Exception ex)
            {

            }
            conn.Close();
        }
    }
}
