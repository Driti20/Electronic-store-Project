﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Electronics_Store.Areas.Admin.ViewModels
{
    public class EditUserViewModel
    {
        public EditUserViewModel()
        {
            Roles = new List<string>();
        }

        public string Id { get; set; }
        
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public List<string> Roles { get; set; }
    }
}
