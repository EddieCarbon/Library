﻿using Microsoft.AspNetCore.Identity;

namespace Library.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Navigation Property
        public ICollection<Loan> Loans { get; set; }
    }
    
    public class ApplicationRole : IdentityRole
    {
        
    }
}
