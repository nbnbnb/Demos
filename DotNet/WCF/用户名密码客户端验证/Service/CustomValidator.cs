using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Linq;
using System.Text;

namespace Service
{
    public class CustomValidator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            // 如果验证不通过，则直接抛出异常接口
            // 验证通过，则不做任何处理
        }
    }
}
