using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using System.IO;

namespace Proga
{
    public static class Logic
    {
        private static NpgsqlConnection? connection;

        public static NpgsqlConnection Connection
        {
            get
            {
                if (connection is null)
                {
                    connection = new("Server=192.168.0.10;Port=5432;Database=TellCompany;User Id=User;Password=1234;");
                }
                return connection;
            }
        }

        public static List<Abonent> Abonents
        {
            get
            {
                return Connection.Query<Abonent>("select * from abonent inner join adress on abonent.adress = adress.id inner join streets on adress.streetname = streets.id inner join phonenumber on abonent.phonenumber = phonenumber.id").ToList();
            }
        }

        public static List<Abonent> FindByPhone(string phone)
        {
            return Connection.Query<Abonent>($"select * from abonent inner join adress on abonent.adress = adress.id inner join streets on adress.streetname = streets.id inner join phonenumber on abonent.phonenumber = phonenumber.id where phonenumber.mobile = '{phone}' or phonenumber.work = '{phone}' or phonenumber.home = '{phone}'").ToList();
        }

        public static List<Street> Streets
        {
            get
            {
                return Connection.Query<Street>("select count(abonent.id), streets.streetname from abonent join adress on abonent.adress = adress.id join streets on adress.streetname = streets.id group by streets.streetname").ToList();
            }
        }

        public static List<string> UniqueFirstNames
        {
            get
            {
                return Connection.Query<string>("select distinct firstname from abonent").ToList();
            }
        }

        public static List<string> UniqueLastNames
        {
            get
            {
                return Connection.Query<string>("select distinct lastname from abonent").ToList();
            }
        }

        public static List<string> UniqueFatherNames
        {
            get
            {
                return Connection.Query<string>("select distinct fathername from abonent").ToList();
            }
        }

        public static List<string> UniqueStreetNames
        {
            get
            {
                return Connection.Query<string>("select distinct streetname from streets").ToList();
            }
        }

        public static List<string> UniqueHouseNumbers
        {
            get
            {
                return Connection.Query<string>("select distinct housenumber from adress").ToList();
            }
        }

        public static List<string> UniqueHome
        {
            get
            {
                return Connection.Query<string>("select distinct home from phonenumber").ToList();
            }
        }

        public static List<string> UniqueWork
        {
            get
            {
                return Connection.Query<string>("select distinct work from phonenumber").ToList();
            }
        }

        public static List<string> UniqueMobile
        {
            get
            {
                return Connection.Query<string>("select distinct mobile from phonenumber").ToList();
            }
        }

        public static void SaveAbonentsCsv(IEnumerable<Abonent> abonents, string filename)
        {
            CsvWriter writer = new(new StreamWriter(filename+".csv"), System.Globalization.CultureInfo.CurrentCulture);
            writer.WriteRecords(abonents);
            writer.Dispose();
        }
    }

    public class Abonent
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string StreetName { get; set; }
        public string HouseNumber { get; set; }
        public string Home { get; set; }
        public string Work { get; set; }
        public string Mobile { get; set; }
    }

    public class Street
    {
        public string StreetName { get; set; }
        public int Count { get; set;}
    }
}
