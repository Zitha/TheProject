using System;
using System.Web.Mvc;
using TheProject.ReportWebApplication.Models;
using TheProject.ReportWebApplication.Services;

namespace TheProject.ReportWebApplication.Controllers
{
    [HandleError]
    public class BaseController : Controller
    {


        /// <summary>
        ///     Log exception to database and redirect user to error view 
        /// </summary>
        /// <param name="exceptionContext">
        ///     The exception context.
        /// </param>
        /// <returns>
        ///     Error View
        /// </returns>
        protected override void OnException(ExceptionContext filterContext)
        {
            ErrorService errorService = new ErrorService();
            ErrorLog errorLog = new ErrorLog()
            {
                ErrorMessage = filterContext.Exception.Message,
                Source = filterContext.Exception.Source,
                Date = DateTime.Now
            };
            errorService.Log(errorLog);
            base.OnException(filterContext);
        }

    }
}