using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
namespace MyAspBlog.Model
{
    public class TypeInfo:BaseId
    {
        [SugarColumn(ColumnDataType ="nvarchar(12)")]
        public string Type { get; set; }
    }
}
