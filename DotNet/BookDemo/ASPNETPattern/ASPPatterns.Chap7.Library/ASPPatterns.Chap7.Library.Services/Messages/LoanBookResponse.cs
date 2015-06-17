using ASPPatterns.Chap7.Library.Services.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASPPatterns.Chap7.Library.Services.Messages
{
    public class LoanBookResponse : ResponseBase
    {
        public LoanView Loan { get; set; }
    }
}
