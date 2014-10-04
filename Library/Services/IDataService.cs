using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Library.Entities;

namespace Library.Services
{
    [ServiceContract]
    interface IDataService
    {
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/api/championship/result")]
        [OperationContract]
        bool Result(string players);

        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/api/championship/top?count={count}")]
        [OperationContract]
        List<Player> GetTopPlayers(string count);

        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/api/championship/new")]
        [OperationContract]
        Play NewGame(string data);

        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/api/championship/delete")]
        [OperationContract]
        bool DeleteAllData();
    }
}
