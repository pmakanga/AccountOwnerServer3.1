﻿using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class AccountForCreationDto
    {
        [Required(ErrorMessage = "Date created is required")]
        public DateTime DateCreated { get; set; }
        [Required(ErrorMessage = "Account type is required")]
        public string AccountType { get; set; }
        public Guid OwnerId { get; set; }
    }
}
