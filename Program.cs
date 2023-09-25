using System.Globalization;

namespace ChallengeInsert
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine($"[Program]: Iniciando");

            FetchData FD = new FetchData();
            Data[]? data = FD.Main();
            Connection con = new Connection();

            List<string> Hashs = con.FetchHashs();

            CheckForDuplicatedHashs CheckDP = new();

            List<string> NewHashs = CheckDP.CheckDuplicated_ReturnNew(data, Hashs);

            con.InsertOnDB(data, NewHashs);
            
        }
    }
}