using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
namespace MyAspBlog.Model
{
    public class AuthorInfo :BaseId
    {
        [SugarColumn(ColumnDataType ="nvarchar(16)")]
        public string Name { get; set; }

        [SugarColumn(ColumnDataType = "nvarchar(30)")]
        public string UserName { get; set; }

        [SugarColumn(ColumnDataType = "nvarchar(128)")]
        public string UserPassWd { get; set; }
    }
}
