using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Text;

namespace DerivativesCalculator
{
    public class MyUserNamePasswordValidator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            Console.Write("\nValidating username, {0}, and password, {1} ... ", userName, password);
            if ((string.Compare(userName, "don", true) != 0) || (string.Compare(password, "hall", false) != 0))
            {
                throw new SecurityTokenException("Unknown user.");
            }
            Console.Write("Done: Credentials accepted. \n");
        }
    }
}
