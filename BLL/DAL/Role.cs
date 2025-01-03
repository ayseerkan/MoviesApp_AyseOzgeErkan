﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DAL
{
    public class Role
    {
        public int Id { get; set; }

        [Required]
        [StringLength(5)]
        public string Name { get; set; }

        // Navigation Property
        public List<User> Users { get; set; } = new List<User>();
    }
}
