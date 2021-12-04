using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Exchange
/// </summary>
/// 

namespace NPExchange
{
    public class Exchange
    {
        private int id;
        private string name;
        private string shortName;
        private string country;
        private string currency;


        public Exchange(int id, string name, string shortName, string country, string currency)
        {
            this.id = id;
            this.name = name;
            this.shortName = shortName;
            this.country = country;
            this.currency = currency;
        }

        private Exchange()
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

        public string ShortName
        {
            get { return shortName; }
            set { shortName = value; }
        }

        public string Country
        {
            get { return country; }
            set { country = value; }
        }

        public string Currency
        {
            get { return currency; }
            set { currency = value; }
        }
    }
}