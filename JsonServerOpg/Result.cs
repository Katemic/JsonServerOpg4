using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonServerOpg
{
    public class Result
    {
        public string ReturnMessage { get; set; }


        public Result(string retunMesssage)
        {
            ReturnMessage = retunMesssage;
        }

        public Result()
        {
            
        }


    }
}
