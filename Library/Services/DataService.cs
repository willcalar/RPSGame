using System.Collections.Generic;
using Library.Entities;
using Library.Logic;

namespace Library.Services
{
    public class DataService:IDataService
    {
        public bool Result(string players)
        {
            //throw new NotImplementedException();
            return true;
        }

        public List<Player> GetTopPlayers(string count)
        {
            return Game.Instance.GetTopPlayers(count == null ? 10 : int.Parse(count));
        }

        public Play NewGame(string data)
        {
            return Game.Instance.BeginTournament(data);
        }

        public bool DeleteAllData()
        {
            return Game.Instance.DeleteAllRecordsUserTable();
        }
    }
}
