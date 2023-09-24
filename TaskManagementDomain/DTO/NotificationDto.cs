using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementDomain.DTO
{
    public class NotificationDto
    {
        public string Type { get; set; }
        public string Message { get; set; }
        public string Timestamp { get; set; }
        public string UserId { get; set; }

    }
}
