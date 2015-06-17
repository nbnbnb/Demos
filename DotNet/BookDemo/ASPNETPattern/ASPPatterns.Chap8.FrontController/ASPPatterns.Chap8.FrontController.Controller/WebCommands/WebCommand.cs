using ASPPatterns.Chap8.FrontController.Controller.ActionCommands;
using ASPPatterns.Chap8.FrontController.Controller.Navigation;
using ASPPatterns.Chap8.FrontController.Controller.Request;
using ASPPatterns.Chap8.FrontController.Controller.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASPPatterns.Chap8.FrontController.Controller.WebCommands
{
    public class WebCommand : IWebCommand
    {
        private IPageNavigator _navigator;
        private List<IActionCommand> _actionCommands;
        private Route _route;
        private PageDirectory _page;
        public WebCommand(IPageNavigator navigator, List<IActionCommand> actionCommands,
            Route route, PageDirectory page)
        {
            _navigator = navigator;
            _actionCommands = actionCommands;
            _route = route;
            _page = page;
        }

        public bool CanHandle(WebRequest webRequest)
        {
            return _route.Matches(webRequest);
        }
        public void Process(WebRequest webRequest)
        {
            _actionCommands.ForEach(cmd => cmd.Process(webRequest));
            _navigator.NavigateTo(_page);
        }
    }
}