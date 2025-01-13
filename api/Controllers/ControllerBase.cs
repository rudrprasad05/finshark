using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.middleware;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    public class BaseController: ControllerBase
    {
        protected async Task WriteLog(string message)
        {
            await Log.Write(message);
        }
    }
}