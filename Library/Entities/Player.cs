using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entities
{
    [DataContract]
    public class Player
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Points { get; set; }

        public Player(string name, int points)
        {
            Name = name;
            Points = points;
        }
    }
}
