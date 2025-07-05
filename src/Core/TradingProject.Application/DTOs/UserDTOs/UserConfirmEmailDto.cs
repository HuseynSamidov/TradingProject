using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingProject.Application.DTOs.UserDTOs
{
    public class UseronfirmEmailDto
    {
        public string UserId { get; set; } = null!;
        public string Token { get; set; } = null!;
    }

}
