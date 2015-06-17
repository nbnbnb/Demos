using MVC4.Extensions.ModelExtensions;
using MvcAppModelValidation.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcAppModelValidation.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            ModelValidatorProvider validatorProvider =
                new DataErrorInfoModelValidatorProvider();

            var res = GetVlidators(typeof(Contact), validatorProvider).ToList();
            return View(res);
        }

        // 只保留 Contact 类型和Address 属性上的 AlawysFailsAttribte
        // Address 类型上的也要去掉
        // GET: /Home/Index2
        public ActionResult Index2()
        {
            Address address = new Address
            {
                Province = "江苏",
                City = "苏州",
                District = "工业园区",
                Street = "星湖街 328 号"
            };

            Contact contact = new Contact
            {
                Name = "张三",
                PhoneNo = "123456789",
                EmailAddress = "zhangsan@gmail.com",
                Address = address
            };

            ModelMetadata modelMetadata = ModelMetadataProviders.Current
                .GetMetadataForType(() => contact, typeof(Contact));

            ModelValidator validator = ModelValidator.GetModelValidator(modelMetadata, base.ControllerContext);
            return View(validator.Validate(contact));
        }

        // 验证 Model 绑定过程中国对 ModelError 的设置
        // 记得加上 AlawysFailsAttribte 特性
        // GET: /Home/Index3
        public ActionResult Index3()
        {
            Address address = new Address
            {
                Province = "江苏",
                City = "苏州",
                District = "工业园区",
                Street = "星湖街 328 号"
            };

            Contact contact = new Contact
            {
                Name = "张三",
                PhoneNo = "123456789",
                EmailAddress = "zhangsan@gmail.com",
                Address = address
            };

            return View(contact);
        }

        [HttpPost]
        public ActionResult Index3(Contact contact)
        {
            return View(contact);
        }

        public ActionResult Index4()
        {
            return View(new Contact());
        }

        [HttpPost]
        public ActionResult Index4([ModelBinder(typeof(CustomModelBinder))] Contact contact)
        {
            return View(contact);
        }

        private IEnumerable<ModelValidator> GetVlidators(Type dataType,
            ModelValidatorProvider validatorProvider)
        {
            ModelMetadata metadata = ModelMetadataProviders.Current
                .GetMetadataForType(() => new Contact(), typeof(Contact));

            // 获取类型上的 Validator
            var validators = validatorProvider.GetValidators(metadata, base.ControllerContext).ToList();

            foreach (var validator in validators)
            {
                yield return validator;
            }

            // 获取属性上的 Validator
            foreach (var propertyMetadata in metadata.Properties)
            {
                validators = validatorProvider.GetValidators(propertyMetadata, base.ControllerContext).ToList();
                foreach (var validator in validators)
                {
                    yield return validator;
                }
            }
        }

    }
}
