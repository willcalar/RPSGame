using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Library.Attributes;
using Library.Entities;
using Library.Enums;
using Data;
using System.Data;

namespace Library.Logic
{
    public class Game
    {
        SQLiteAdapter RPSGameDBAdapter;
        #region Singleton

        private static volatile Game instance;
        private static object syncRoot = new Object();

        private Game() {
            RPSGameDBAdapter = new SQLiteAdapter();
        }

        public static Game Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new Game();
                    }
                }

                return instance;
            }
        }
        #endregion

        public Play BeginTournament(string playFile)
        {
            try
            {
                Regex replaceAllNot = new Regex(@"[^A-Za-z,\[\]\\""]");
                playFile = replaceAllNot.Replace(playFile, "");
                return ProcessPlays(playFile);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Play ProcessPlays(string playString)
        {
            Play winner = null;
            playString = playString.Substring(1, playString.Length - 2);
            int numberOfCommas = playString.Length - playString.Replace(",", "").Length;
            if (numberOfCommas == 3)
            {
                string[] plays = playString.Replace("[", String.Empty).Replace("]", String.Empty).Replace("\"", String.Empty).Split(',');
                if (plays.Length == 4)
                {
                    Play play1 = new Play(plays[0],plays[1]);
                    Play play2 = new Play(plays[2], plays[3]);
                    return ExecutePlay(play1, play2);
                }
                else
                {
                    throw new Exception("There are missing parameters in one matchup");
                }
            }
            else
            {
                int stringPosition = 0, countElements=0;
                while ((stringPosition = playString.IndexOf(',', stringPosition)) != -1)
                {
                    if (countElements++ == numberOfCommas/2)
                    {
                        break;
                    }
                    stringPosition++;
                }
                winner = ExecutePlay(ProcessPlays(playString.Substring(0, stringPosition)), ProcessPlays(playString.Substring(stringPosition+1, playString.Length - stringPosition-1)));
            }
            return winner;
        }

        private Play ExecutePlay(Play play1, Play play2)
        {
            if (play1.PlayCall == play2.PlayCall)
            {
                //House always wins
                return play1;
            }
            if (play1.PlayCall == GameItems.Rock && play2.PlayCall == GameItems.Scissor)
            {
                return play1;
            }
            if (play1.PlayCall == GameItems.Paper && play2.PlayCall == GameItems.Rock)
            {
                return play1;
            }
            if (play1.PlayCall == GameItems.Scissor && play2.PlayCall == GameItems.Paper)
            {
                return play1;
            }
            return play2;
        }

        private bool UpdateUserTable(Play winnerPlay){
            try
            {
                DataSet result = RPSGameDBAdapter.ExecuteQuery(String.Format("SELECT Username,Wins,Points FROM User WHERE Username='{0}'", winnerPlay.User));
                if (result == null || result.Tables[0].Rows.Count == 0)
                {
                    return RPSGameDBAdapter.ExecuteNonQuery(String.Format("INSERT INTO User (Username,Wins,Points) VALUES('{0}',{1},{2})", winnerPlay.User, 1, 3)); ;
                }
                else
                {
                    int wins = int.Parse(result.Tables[0].Rows[0]["Wins"].ToString())+ 1;
                    int points = int.Parse(result.Tables[0].Rows[0]["Points"].ToString())+3;
                    return RPSGameDBAdapter.ExecuteNonQuery(String.Format("UPDATE TABLE SET Wins = {0},Points={1} WHERE Username='{2}'", wins , points, winnerPlay.User));
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            
        } 

        public bool DeleteAllRecordsUserTable()
        {
            return RPSGameDBAdapter.ExecuteNonQuery(String.Format("DELETE FROM User"));
        }

        public List<Player> GetTopPlayers(int count = 10)
        {
            DataSet result = RPSGameDBAdapter.ExecuteQuery(String.Format("SELECT  Username,Wins,Points FROM User ORDER BY Points DESC LIMIT {0}", count));
            if (!(result == null || result.Tables[0].Rows.Count == 0))
            {
                return (from DataRow row in result.Tables[0].Rows select new Player(row["Username"].ToString(), int.Parse(row["Points"].ToString()))).ToList();
            }
            else
            {
                return null;
            }
        }



    }
}
