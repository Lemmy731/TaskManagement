using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementDomain.Entity
{
    public class MyTask
    {
        [Key]
        public Guid Id { get; set; }    
        public string Title { get; set; }   
        public string Description { get; set; } 
        public string Priority { get; set; }    
        public DateTime DueDate { get; set; }
        public string Status { get; set; }

        [ForeignKey(nameof(Project))]
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}
