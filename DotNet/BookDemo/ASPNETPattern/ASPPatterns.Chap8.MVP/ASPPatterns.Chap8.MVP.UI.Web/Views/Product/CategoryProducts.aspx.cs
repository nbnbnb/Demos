using ASPPatterns.Chap8.MVP.Presentation;
using ASPPatterns.Chap8.MVP.UI.Web.Views.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ninject;
using ASPPatterns.Chap8.MVP.UI.Web.App_Start;
using ASPPatterns.Chap8.MVP.Model;

namespace ASPPatterns.Chap8.MVP.UI.Web.Views.Product
{
    public partial class CategoryProducts : System.Web.UI.Page, ICategoryProductsView
    {
        private ICategoryProductsPresenter _presenter;
        protected void Page_Init(object sender, EventArgs e)
        {
            _presenter = new CategoryProductsPresenter(this, NinjectWebCommon.CurrentKernel.Get<ProductService>());
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _presenter.Display();
        }

        public Model.Category Category
        {
            set { litCategoryName.Text = value.Name; }
        }

        public int CategoryId
        {
            get { return int.Parse(Request.QueryString["CategoryId"]); }
        }


        public IEnumerable<Model.Product> CategoryProductList
        {
            set { this.plCategoryProducts.SetProductsToDisplay(value); }
        }

        public IEnumerable<Model.Category> CategoryList
        {
            set
            {
                Shop shopMasterPage = (Shop)Page.Master;
                shopMasterPage.CategoryListControl.SetCategoriesToDisplay(value);
            }
        }
    }
}