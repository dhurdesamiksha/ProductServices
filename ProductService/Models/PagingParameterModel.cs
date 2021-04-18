using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductService.Models
{
    public class PagingParameterModel
    {
        const int maxPageSize = 5;
        public int pageNumber { get; set; }
        public int _pageSize { get; set; } = 5;
        public int pageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}