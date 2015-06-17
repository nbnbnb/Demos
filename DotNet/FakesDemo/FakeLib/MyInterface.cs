using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FakeLib
{
    public class MyInterface : IEnumerable,IInfo
    {

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }


        public string YouName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
