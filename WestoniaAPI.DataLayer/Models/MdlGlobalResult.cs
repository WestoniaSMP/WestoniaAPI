using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WestoniaAPI.DataLayer.Models
{
    public class GlobalResultModel<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";
        public T? Content { get; set; }

        public GlobalResultModel(bool success, string msg = "")
        {
            Success = success;
            Message = msg;
        }

        public GlobalResultModel(T? content = default, string msg = "")
        {
            Success = true;
            Message = msg;
            Content = content;
        }
    }
}
