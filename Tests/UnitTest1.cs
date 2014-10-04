using System.Text.RegularExpressions;
using Library.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            
            string playFile = @"[
                                    [
                                    [ [""Armando"", ""P""], [""Dave"", ""S""] ],
                                    [ [""Richard"", ""R""], [""Michael"", ""S""] ]
                                    ],
                                    [
                                    [ [""Allen"", ""S""], [""Omer"", ""P""] ],
                                    [ [""John"", ""R""], [""Robert"", ""P""] ]
                                    ]
                                    ]";
            Regex replaceAllNot = new Regex(@"[^A-Za-z,\[\]\\""]");
            playFile = replaceAllNot.Replace(playFile, "");
            Game.Instance.ProcessPlays(playFile);
        }
    }
}
