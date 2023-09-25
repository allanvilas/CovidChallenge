using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Security.Cryptography;


namespace ChallengeInsert
{
    public class Connection
    {
        string ConnString = "PleaseContatcMe";
        public void InsertOnDB(Data[] data, List<string> New_Hashs)
        {
            Console.WriteLine("[CNN]: Executing insert function");
            using (NpgsqlConnection Conn = new NpgsqlConnection(ConnString))
            {
                Conn.Open();
                foreach (Data touple in data)
                {
                    using (MD5 md5 = MD5.Create())
                    {
                        string strToHash = $"{touple.country}{touple.country_code}{touple.continent}{touple.population}{touple.indicator}{touple.year_week}{touple.source}{touple.note}{touple.weekly_count.ToString(CultureInfo.InvariantCulture)}{touple.cumulative_count.ToString(CultureInfo.InvariantCulture)}{touple.rate_14_day.ToString(CultureInfo.InvariantCulture)}";
                        string tohash = Convert.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(strToHash)));

                        if (!New_Hashs.Contains(tohash))
                        {
                            continue;
                        }
                    }
                        
                    string insertSql = "INSERT INTO cases (country,country_code,continent,population,indicator,year_week,source,note,weekly_count,cumulative_count,rate_14_day) VALUES " +
                    $"('{touple.country}','{touple.country_code}','{touple.continent}',{touple.population},'{touple.indicator}','{touple.year_week}','{touple.source}','{touple.note}',{touple.weekly_count.ToString(CultureInfo.InvariantCulture)},{touple.cumulative_count.ToString(CultureInfo.InvariantCulture)},{touple.rate_14_day.ToString(CultureInfo.InvariantCulture)});";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(insertSql, Conn))
                    {
                        cmd.ExecuteNonQuery();
                    }  
                }
                Conn.Close();
            }   
        }
        
        public List<string> FetchHashs()
        {
            List<string> Hashs = new();

            using (NpgsqlConnection Conn = new NpgsqlConnection(ConnString))
            {
                string SQL = "SELECT MD5(country::text || country_code::text || continent::text || population::text || indicator::text || year_week::text || source::text || note::text || weekly_count::text || cumulative_count::text || rate_14_day::text) AS HASH FROM CASES;";
                Conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(SQL, Conn))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string value = reader["hash"].ToString();
                            Hashs.Add(value);
                        }                            
                    }
                }
                Conn.Close();
            }
            return Hashs;
        }
    }
}
