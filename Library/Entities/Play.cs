using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Library.Attributes;
using Library.Enums;

namespace Library.Entities
{

    [DataContract]
    public class Play
    {
        [DataMember]
        public string User { get; set; }

        [DataMember]
        public GameItems PlayCall { get; set; }

        public Play(string user, string playCall)
        {
            try
            {
                User = user;

                PlayCall = StringValueAttributeExtensionMethods.GetValueFromStringValue<GameItems>(playCall);
            }
            catch (Exception)
            {
                throw new Exception("Play doesn't exist");
            }
            
        }

        public override string ToString()
        {
            return String.Format("[\"{0}\", \"{1}\"]", User, PlayCall.GetStringValue());
        }
    }
}

