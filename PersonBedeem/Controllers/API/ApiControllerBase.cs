using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace PersonBedeem.Controllers.API
{
    public class ApiControllerBase : Controller
    {
        protected IDbConnection _db;
        public ApiControllerBase()
        {
            _db = new SqlConnection("Server=tcp:bedeemperson.database.windows.net,1433;Database=BedeemPerson;User ID=service@bluesun.no@bedeemperson;Password=@Bluesun2015;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

    }
}