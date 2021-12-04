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
[WebService(Namespace = "http://stockmarketviewer.azurewebsites.net/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Exchanges : System.Web.Services.WebService
{

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
    [EnableCors(origins: "http://stockmarketviewer.azurewebsites.net/", headers:"*", methods:"*")]
    public List<Exchange> GetExchanges()
    {
        Exchange exchange;
        List<Exchange> exchanges = new List<Exchange>();
        try
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "stockmarket-cloudcomputing.database.windows.net";
            builder.UserID = "nowicki-yari";
            builder.Password = "CLOUD_4090";
            builder.InitialCatalog = "StockMarketViewer";

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = "SELECT * FROM dbo.exchanges";

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

}
