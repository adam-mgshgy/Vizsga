using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoveYourBody.WebAPI.Controllers
{
    public static class Extension
    {
        public static ActionResult Run(this ControllerBase controller, Func<ActionResult> function)
        {
            try
            {
                return function();
            }
            catch (Exception ex)
            {
                //WriteLog(ex);
                return controller.BadRequest(new
                {
#if DEBUG
                    ErrorMessage = ex.Message,
                    StackTrace = ex.StackTrace
#else
                    ErrorMessage = "Váratlan hiba"
#endif
                });
            }
        }
    }
}
