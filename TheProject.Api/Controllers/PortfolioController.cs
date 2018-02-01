using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TheProject.Model;

namespace TheProject.Api.Controllers
{
    public class PortfolioController : ApiController
    {
        public Portfolio UpdatePortfolio(Portfolio portfolio)
        {
            return new Portfolio();
        }

        public Portfolio AddPortfolio(Portfolio portfolio)
        {
            return new Portfolio();
        }

        public IEnumerable<Portfolio> GetPortfolios()
        {
            return new List<Portfolio>();
        }
    }
}
