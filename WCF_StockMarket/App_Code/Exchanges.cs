using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Cors;
using System.Web.Services;
using System.Data.SqlClient;
using NPExchange;

/// <summary>
/// Summary description for Exchanges
/// </summary>
[WebService(Namespace = "https://stockmarket-cloud4090.azurewebsites.net/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Exchanges : System.Web.Services.WebService
{
    private SqlConnectionStringBuilder InitConnection()
    {
        SqlConnectionStringBuilder sqlConnection = new SqlConnectionStringBuilder();
        sqlConnection.DataSource = "stockmarket-cloud4090.database.windows.net";
        sqlConnection.UserID = "nowicki-yari";
        sqlConnection.Password = "CLOUD_4090";
        sqlConnection.InitialCatalog = "stockmarket-cloudcomputing";
        return sqlConnection;
    }

    public Exchanges()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    [WebMethod]
    [EnableCors(origins: "https://stockmarket-cloud4090.azurewebsites.net/", headers:"*", methods:"*")]
    public List<Exchange> GetExchanges()
    {
        Exchange exchange;
        List<Exchange> exchanges = new List<Exchange>();
        try
        {
            SqlConnectionStringBuilder builder = InitConnection();
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                string sql = "SELECT * FROM dbo.exchanges";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            exchange = new Exchange(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4));
                            exchanges.Add(exchange);
                        }
                        
                    }
                }
            }
        }
        catch (SqlException e)
        {
            Console.WriteLine(e.ToString());
        }
        return exchanges;
    }

    [WebMethod]
    [EnableCors(origins: "https://stockmarket-cloud4090.azurewebsites.net/", headers: "*", methods: "*")]
    public List<Stock> GetStocksFromExchange(string exchange)
    {
        Stock stock;
        List<Stock> stocks = new List<Stock>();

        try
        {
            SqlConnectionStringBuilder builder = InitConnection();
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                string sql = "SELECT * FROM dbo.stocks WHERE exchange='" + exchange + "'";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {                            
                            stock = new Stock(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6));
                            stocks.Add(stock);
                        }

                    }
                }
            }
        }
        catch (SqlException e)
        {
            Console.WriteLine(e.ToString());
        }
        return stocks;

    }

    [WebMethod]
    [EnableCors(origins: "https://stockmarket-cloud4090.azurewebsites.net/", headers: "*", methods: "*")]
    public List<string> GetListOfSectors()
    {
        List<string> sectors = new List<string>();

        try
        {
            SqlConnectionStringBuilder builder = InitConnection();
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                string sql = "SELECT DISTINCT sector from dbo.stocks WHERE NOT sector=''";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sectors.Add(reader.GetString(0));
                        }

                    }
                }
            }
        }
        catch (SqlException e)
        {
            Console.WriteLine(e.ToString());
        }
        return sectors;
    }

    [WebMethod]
    [EnableCors(origins: "https://stockmarket-cloud4090.azurewebsites.net/", headers: "*", methods: "*")]
    public List<string> GetListOfIndustriesFromSector(string sector)
    {
        List<string> industries = new List<string>();

        try
        {
            SqlConnectionStringBuilder builder = InitConnection();
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                string sql = "SELECT DISTINCT industry from dbo.stocks WHERE NOT industry='' AND sector='" + sector + "'";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            industries.Add(reader.GetString(0));
                        }

                    }
                }
            }
        }
        catch (SqlException e)
        {
            Console.WriteLine(e.ToString());
        }
        return industries;
    }

    [WebMethod]
    [EnableCors(origins: "https://stockmarket-cloud4090.azurewebsites.net/", headers: "*", methods: "*")]
    public List<Stock> GetStocksFromIndustry(string industry)
    {
        Stock stock;
        List<Stock> stocks = new List<Stock>();

        try
        {
            SqlConnectionStringBuilder builder = InitConnection();
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                string sql = "SELECT * FROM dbo.stocks WHERE industry='" + industry + "'";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            stock = new Stock(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6));
                            stocks.Add(stock);
                        }

                    }
                }
            }
        }
        catch (SqlException e)
        {
            Console.WriteLine(e.ToString());
        }
        return stocks;

    }

    [WebMethod]
    [EnableCors(origins: "https://stockmarket-cloud4090.azurewebsites.net/", headers: "*", methods: "*")]
    public List<Stock> GetStocks(string stocks)
    {
        Stock stock;
        List<Stock> favorites = new List<Stock>();
        int count = 1;

        if (string.IsNullOrEmpty(stocks))
        {
            return null;
        }
        string[] arrStocks = stocks.Split(',');
        string sql_string = "WHERE symbol='" + arrStocks[0] + "' ";
        while (arrStocks.Length > count)
        {
            sql_string += "OR symbol='"+ arrStocks[count] + "' ";
            count++;
        }
        sql_string += ";";
        try
        {
            SqlConnectionStringBuilder builder = InitConnection();
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                string sql = "SELECT * FROM dbo.stocks " + sql_string;

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            stock = new Stock(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6));
                            favorites.Add(stock);
                        }

                    }
                }
            }
        }
        catch (SqlException e)
        {
            Console.WriteLine(e.ToString());
        }
        return favorites;

    }

}
