using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductService
{
    public class Response
    {
        public object[] result;
        public int error;
        public string errorMessage;
        public int count;
    }
}