using ASPPatterns.Chap7.Library.Services.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASPPatterns.Chap7.Library.Services.Messages
{
    public class FindBookTitlesResponse : ResponseBase
    {
        public IEnumerable<BookTitleView> BookTitles { get; set; }
    }
}
