using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTcms.Model
{
    public class dt_feedback
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime AddTime { get; set; }
        public int ManagerId { get; set; }
        public int IsDel { get; set; }
    }
}
