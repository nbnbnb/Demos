using ASPPatterns.Chap8.FrontController.Controller.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASPPatterns.Chap8.FrontController.Controller.WebCommands
{
    public interface IWebCommandRegistry
    {
        IWebCommand GetCommandFor(WebRequest webRequest);
    }
}
