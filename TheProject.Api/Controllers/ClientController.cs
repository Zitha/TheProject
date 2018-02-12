using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TheProject.Data;
using TheProject.Model;

namespace TheProject.Api.Controllers
{
    public class ClientController : ApiController
    {

        [HttpGet]
        public List<Client> GetClients()
        {
            using (ApplicationUnit unit = new ApplicationUnit())
            {
                List<Client> clients = unit.Clients.GetAll().ToList();
                return clients;
            }
        }


        [HttpGet]
        public List<Portfolio> GetPotfoliosbyClientId(int clientId)
        {
            using (ApplicationUnit unit = new ApplicationUnit())
            {
                List<Portfolio> clients = unit.Portfolios.GetAll()
                    .Where(cl => cl.Client.Id == clientId).ToList();
                return clients;
            }
        }
    }
}
