using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementDomain.Entity
{
    public class Notification
    {
        [Key]
        public Guid Id { get; set; }  
        public string Type { get; set; }
        public string Message { get; set; }    
        public string Timestamp { get; set; }
        public string Status { get; set; }  

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}
