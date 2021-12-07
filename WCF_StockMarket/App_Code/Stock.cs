using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Stock
/// </summary>
public class Stock
{

    private int id;
    private string symbol;
    private string name;
    private string country;
    private string sector;
    private string industry;
    private string exchange;

    public Stock(int id, string symbol, string name, string country, string sector, string industry, string exchange)
    {
        this.id = id;
        this.symbol = symbol;
        this.name = name;
        this.country = country;
        this.sector = sector;
        this.industry = industry;
        this.exchange = exchange;
    }

    private Stock()
    {

    }

    public int Id
    {
        get { return id; }
        set { id = value; }
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public string Symbol
    {
        get { return symbol; }
        set { symbol = value; }
    }

    public string Country
    {
        get { return country; }
        set { country = value; }
    }

    public string Sector
    {
        get { return sector; }
        set { sector = value; }
    }

    public string Industry
    {
        get { return industry; }
        set { industry = value; }
    }

    public string Exchange
    {
        get { return exchange; }
        set { exchange = value; }
    }



}