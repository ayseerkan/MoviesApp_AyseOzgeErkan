using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DAL;

namespace BLL.Models
{
    public class RoleModel
    {
        public Role Record { get; set; }

        [DisplayName("Role Name")]
        public string Name => Record.Name;
    }
}
