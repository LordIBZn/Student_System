﻿using Microsoft.AspNetCore.Identity;

namespace Student_System.Models
{
    public class Student : IdentityUser
    {
        public int StudentId { get; set; }
    }
}
