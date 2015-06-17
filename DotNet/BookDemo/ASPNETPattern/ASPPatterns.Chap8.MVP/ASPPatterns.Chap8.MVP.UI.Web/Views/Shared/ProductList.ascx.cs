using ASPPatterns.Chap8.MVP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPPatterns.Chap8.MVP.UI.Web.Views.Shared
{
    public partial class ProductList : System.Web.UI.UserControl
    {
        public void SetProductsToDisplay(IEnumerable<ASPPatterns.Chap8.MVP.Model.Product> products)
        {
            this.rptProducts.DataSource = products;
            this.rptProducts.DataBind();
        }
    }
}