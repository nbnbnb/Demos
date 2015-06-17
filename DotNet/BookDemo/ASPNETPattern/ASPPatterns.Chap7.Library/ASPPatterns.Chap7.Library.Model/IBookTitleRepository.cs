using ASPPatterns.Chap7.Library.Infrastructure.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASPPatterns.Chap7.Library.Model
{
    public interface IBookTitleRepository
    {
        void Add(BookTitle book);
        void Remove(BookTitle book);
        void Save(BookTitle book);
        BookTitle FindBy(string ISBN);
        IEnumerable<BookTitle> FindAll();
        IEnumerable<BookTitle> FindAll(int index, int count);
        IEnumerable<BookTitle> FindBy(Query query);
        IEnumerable<BookTitle> FindBy(Query query, int index, int count);
    }
}
