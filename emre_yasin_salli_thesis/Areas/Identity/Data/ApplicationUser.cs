using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using emre_yasin_salli_thesis.Models;
using Microsoft.AspNetCore.Identity;

namespace emre_yasin_salli_thesis.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string FirstName { get; set; }

    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string LastName { get; set; }
    public virtual ICollection<ShoppingList> ShoppingLists { get; set; } = new List<ShoppingList>();

}

