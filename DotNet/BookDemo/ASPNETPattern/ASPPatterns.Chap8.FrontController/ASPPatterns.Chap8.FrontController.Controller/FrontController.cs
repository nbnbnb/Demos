using ASPPatterns.Chap8.FrontController.Controller.Request;
using ASPPatterns.Chap8.FrontController.Controller.WebCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASPPatterns.Chap8.FrontController.Controller
{
    public class FrontController
    {
        IWebCommandRegistry _webCommandRegistry;
        public FrontController(IWebCommandRegistry webCommandRegistry)
        {
            _webCommandRegistry = webCommandRegistry;
        }
        public void Handle(WebRequest request)
        {
            _webCommandRegistry.GetCommandFor(request).Process(request);
        }
    }
}
