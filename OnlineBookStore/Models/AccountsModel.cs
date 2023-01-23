using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineBookStore.Models
{
    public class User
    {

        public int Id { get; set; }

        [Index(IsUnique = true)]
        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required, StringLength(50)]
        public string Email { get; set; }

        [Required, StringLength(50)]
        public string Password { get; set; }

    }


    public class Role
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string RoleName { get; set; }
    }


    public class UserRolesMapping
    {
        public int Id { get; set; }
        public User User { get; set; }

        //Foreign Key
        [Column(Order = 1)]
        public int UserId { get; set; }


        public Role Role { get; set; }
        //foreign key
        [Column(Order = 2)]
        [Index("IX_UserId_RoleId", 1, IsUnique = true)]

        public int RoleId { get; set; }
    }

    public class RegisterViewModel
    {
        [Required, StringLength(50)]
        [RegularExpression("^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$", ErrorMessage = "Invalid Email Id")]
        public string EmailId { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required, StringLength(50)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password and Confirm Password doesn't match")]
        public string ConfirmPassword { get; set; }


        [Display(Name = "Roles")]
        public int RoleId { get; set; }

        public List<Role> Roles { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required, StringLength(50)]
        public string Password { get; set; }
    }


    public class UsersAndRoles
    {
        public string Username { get; set; }
        public string RoleName { get; set; }
    }

    public class UserRoleViewModel
    {
        [Display(Name = "Username(s)")]
        public int UserId { get; set; }
        [Display(Name = "Roles")]
        public int RoleId { get; set; }
        public List<User> Users { get; set; }
        public List<Role> Roles { get; set; }

    }
}