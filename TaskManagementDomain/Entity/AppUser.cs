using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementDomain.Entity
{
    public class AppUser: IdentityUser
    {
        
        public string FullName { get; set; }     
        public ICollection<MyTask> Tasks { get; set; } 
        public ICollection<Notification> Notifications { get; set; }
    }
}
