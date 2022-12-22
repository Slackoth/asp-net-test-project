using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ATP.DataAccess.Repository.Employee
{
    public class EmployeeRepositoryImpl : EmployeeRepository
    {
        private readonly IConfiguration Config;
        private readonly ILogger Logger;

        public EmployeeRepositoryImpl(IConfiguration Config, ILogger<EmployeeRepositoryImpl> Logger)
        {
            this.Config = Config; this.Logger = Logger;
        }

        public int Add(string LastName, string FirstName, string Phone, string ZipCode)
        {
            int Rows = 0;
            SqlConnection Connection = new();

            try
            {
                Connection = GetDbConnection();
                SqlCommand Command = new(
                    "insert into dbo.Employee(EmployeeLastName, EmployeeFirstName, EmployeePhone, EmployeeZip, HireDate) values(@last, @name, @phone, @zip, @date)",
                    Connection
                );

                Connection.Open();
                Command.CommandType = CommandType.Text;
                Command.Parameters.Add("@last", SqlDbType.VarChar, 155).Value = LastName; Command.Parameters.Add("@name", SqlDbType.VarChar, 155).Value = FirstName;
                Command.Parameters.Add("@phone", SqlDbType.VarChar, 11).Value = Phone; Command.Parameters.Add("@zip", SqlDbType.VarChar, 9).Value = ZipCode;
                Command.Parameters.Add("@date", SqlDbType.Date, 9).Value = DateTime.Now;

                Rows = Command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.LogError("ERROR CREATING EMPLOYEE", e);
            }
            finally { if (Connection.State == ConnectionState.Open) Connection.Close(); }

            return Rows;
        }

        public int Delete(long Id)
        {
            int Rows = 0;
            SqlConnection Connection = new();

            try
            {
                Connection = GetDbConnection();
                SqlCommand Command = new(
                    "delete from dbo.Employee where EmployeeID = @id",
                    Connection
                );

                Connection.Open();
                Command.CommandType = CommandType.Text;
                Command.Parameters.Add("@id", SqlDbType.BigInt).Value = Id;

                Rows = Command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.LogError("ERROR UPDATING EMPLOYEE " + Id, e);
            }
            finally { if (Connection.State == ConnectionState.Open) Connection.Close(); }

            return Rows;
        }

        public int Edit(string LastName, string FirstName, string Phone, string ZipCode, long Id)
        {
            int Rows = 0;
            SqlConnection Connection = new();

            try
            {
                Connection = GetDbConnection();
                SqlCommand Command = new(
                    "update dbo.Employee set EmployeeLastName = @last, EmployeeFirstName = @name, EmployeePhone = @phone, EmployeeZip = @zip where EmployeeID = @id",
                    Connection
                );

                Connection.Open();
                Command.CommandType = CommandType.Text;
                Command.Parameters.Add("@last", SqlDbType.VarChar, 155).Value = LastName; Command.Parameters.Add("@name", SqlDbType.VarChar, 155).Value = FirstName;
                Command.Parameters.Add("@phone", SqlDbType.VarChar, 11).Value = Phone; Command.Parameters.Add("@zip", SqlDbType.VarChar, 9).Value = ZipCode;
                Command.Parameters.Add("@id", SqlDbType.BigInt).Value = Id;

                Rows = Command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.LogError("ERROR UPDATING EMPLOYEE " + Id, e);
            }
            finally { if (Connection.State == ConnectionState.Open) Connection.Close(); }

            return Rows;
        }

        public DataTable FindAll()
        {
            DataTable DataTable = new();
            SqlConnection Connection = new();

            try
            {
                Connection = GetDbConnection();
                SqlCommand Command = new("select * from dbo.Employee order by HireDate desc", Connection);
                Command.CommandType = CommandType.Text;

                Connection.Open();
                DataTable.Load(Command.ExecuteReader());
            }
            catch (Exception e)
            {
                Logger.LogError("ERROR OBTAINING EMPLOYEES", e);
            }
            finally { if (Connection.State == ConnectionState.Open) Connection.Close(); }


            return DataTable;
        }

        public DataTable FindByEmployeeId(long Id)
        {
            DataTable DataTable = new();
            SqlConnection Connection = new();

            try
            {
                Connection = GetDbConnection();
                SqlCommand Command = new("select * from dbo.Employee where EmployeeID = @id", Connection);
                Command.CommandType = CommandType.Text;
                Command.Parameters.Add("@id", SqlDbType.BigInt).Value = Id;

                Connection.Open();
                DataTable.Load(Command.ExecuteReader());
            }
            catch (Exception e)
            {
                Logger.LogError("ERROR OBTAINING EMPLOYEE", e);
            }
            finally { if(Connection.State == ConnectionState.Open) Connection.Close(); }
            
            return DataTable;
        }

        public DataTable FindByEmployeeLastNameOrEmployeePhone(string Like)
        {
            DataTable DataTable = new();
            SqlConnection Connection = new();
            try
            {
                Connection = GetDbConnection();
                SqlCommand Command = new("select * from dbo.Employee where lower(EmployeeLastName) like @like or lower(EmployeePhone) like @like order by HireDate desc", Connection);
                Command.CommandType = CommandType.Text;
                Command.Parameters.Add("@like", SqlDbType.VarChar).Value = Like;

                Connection.Open();
                DataTable.Load(Command.ExecuteReader());
            }
            catch (Exception e)
            {
                Logger.LogError("ERROR OBTAINING EMPLOYEES", e);
            }
            finally { if (Connection.State == ConnectionState.Open) Connection.Close(); }

            return DataTable;
        }

        private SqlConnection GetDbConnection()
        {
            return new SqlConnection(Config.GetConnectionString("DefaultConnection"));
        }
    }
}
