using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TheProject.Data;
using TheProject.Model;

namespace TheProject.Api.Controllers
{
    public class PortfolioController : ApiController
    {
        //public Portfolio UpdatePortfolio(Portfolio portfolio)
        //{
        //    return new Portfolio();
        //}

        [HttpPost]
        public Portfolio AddPortfolio(Portfolio portfolio)
        {
            try
            {
                using (ApplicationUnit unit = new ApplicationUnit())
                {
                    Client client = unit.Clients.GetAll()
                        .FirstOrDefault(u => u.Id == portfolio.Client.Id);

                    Portfolio hasPortfolio = unit.Portfolios.GetAll()
                        .FirstOrDefault(u => u.Name.ToLower() == portfolio.Name.ToLower());

                    if (hasPortfolio == null)
                    {
                        portfolio.Client = client;
                        unit.Portfolios.Add(portfolio);
                        unit.SaveChanges();
                        return portfolio;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public IEnumerable<Portfolio> GetPortfolios()
        {
            try
            {
                List<Portfolio> returnPortfolio = new List<Portfolio>();
                using (ApplicationUnit unit = new ApplicationUnit())
                {
                    IEnumerable<Portfolio> portfolios = unit.Portfolios.GetAll()
                        .Include(c => c.Client).Include(f => f.Facilities).ToList();
                    foreach (var portfolio in portfolios)
                    {
                        returnPortfolio.Add(new Portfolio
                        {
                            Client = portfolio.Client,
                            Id = portfolio.Id,
                            Name = portfolio.Name
                        });
                    }

                    return returnPortfolio;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
