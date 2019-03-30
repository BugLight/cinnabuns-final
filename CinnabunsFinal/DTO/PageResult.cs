using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinnabunsFinal.DTO
{
    public class PageResult<T>
    {
        public int TotalCount { get; set; }
        public List<T> Data { get; set; }
    }
}
