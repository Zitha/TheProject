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
                    Portfolio hasPortfolio = unit.Portfolios.GetAll()
                        .FirstOrDefault(u => u.Name.ToLower() == portfolio.Name.ToLower());

                    if (hasPortfolio == null)
                    {
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
                using (ApplicationUnit unit = new ApplicationUnit())
                {
                    IEnumerable<Portfolio> portfolios = unit.Portfolios.GetAll()
                        .Include(c=>c.Client).Include(f => f.Facilities).ToList();

                    return portfolios;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
