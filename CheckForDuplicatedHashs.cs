using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace ChallengeInsert
{
    public class CheckForDuplicatedHashs
    {
        public List<string> CheckDuplicated_ReturnNew(Data[] date, List<string> DB_Hashs)
        {
            List<string> JSON_Hases = new();
            using (MD5 md5 = MD5.Create())
            {
                foreach (Data t in date)
                {
                    string strToHash = $"{t.country}{t.country_code}{t.continent}{t.population}{t.indicator}{t.year_week}{t.source}{t.note}{t.weekly_count}{t.cumulative_count}{t.rate_14_day}";
                    byte[] tohash = md5.ComputeHash(Encoding.UTF8.GetBytes(strToHash));
                    JSON_Hases.Add(Convert.ToString(tohash));
                }
            }

            List<string> NoDuplicates = JSON_Hases.Concat(DB_Hashs.Except(JSON_Hases)).ToList();
            
            if(NoDuplicates.Count() <= 0)
            {
                Console.WriteLine("[CFDH] - No new records to add to database,");
                return null;
            }
            else
            {
                Console.WriteLine($"[CFDH] - Have {NoDuplicates.Count()} new records");
                return NoDuplicates;
            }
        }
    }
}
