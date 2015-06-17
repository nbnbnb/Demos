using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Models
{
    public class UserInfoModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.Model == null)
            {

                return new UserInfo
                {
                    UserName = "ZhangJin",
                    Age = 27,
                    UserAddress = new Address
                    {
                        Province = "HuBei",
                        City = "WuHan"
                    }
                };
            }
            else
            {
                return bindingContext.Model;
            }
        }
    }
}