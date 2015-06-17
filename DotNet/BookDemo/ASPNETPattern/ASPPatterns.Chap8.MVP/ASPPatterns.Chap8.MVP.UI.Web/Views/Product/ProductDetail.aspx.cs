using ASPPatterns.Chap8.MVP.Model;
using ASPPatterns.Chap8.MVP.Presentation;
using ASPPatterns.Chap8.MVP.Presentation.Basket;
using ASPPatterns.Chap8.MVP.Presentation.Navigation;
using ASPPatterns.Chap8.MVP.UI.Web.Views.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ninject;
using ASPPatterns.Chap8.MVP.UI.Web.App_Start;

namespace ASPPatterns.Chap8.MVP.UI.Web.Views.Product
{
    public partial class ProductDetail : System.Web.UI.Page, IProductDetailView
    {

        private IProductDetailPresenter _presenter;

        protected void Page_Init(object sender, EventArgs e)
        {
            _presenter = new ProductDetailPresenter(this,
                NinjectWebCommon.CurrentKernel.Get<ProductService>(),
                NinjectWebCommon.CurrentKernel.Get<IBasket>(),
                NinjectWebCommon.CurrentKernel.Get<IPageNavigator>());
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _presenter.Display();
        }

        public int ProductId
        {
            get { return int.Parse(Request.QueryString["ProductId"]); }
        }

        public string Name
        {
            set { this.litName.Text = value; }
        }

        public decimal Price
        {
            set { this.litPrice.Text = String.Format("{0:C}", value); }
        }

        public string Description
        {
            set { this.litDescription.Text = value; }
        }

        public IEnumerable<Model.Category> CategoryList
        {
            set
            {
                Shop shopMasterPage = (Shop)Page.Master;
                shopMasterPage.CategoryListControl.SetCategoriesToDisplay(value);
            }
        }

        protected void btnAddToBasket_Click(object sender, EventArgs e)
        {
            _presenter.AddProductToBasketAndShowBasketPage();
        }
    }
}