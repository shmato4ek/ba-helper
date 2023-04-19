using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.DAL.Entities
{
    public class RefreshToken
    {
        private const int DAYS_TO_EXPIRE = 1;
        public RefreshToken()
        {
            ExpiresAt = DateTime.UtcNow.AddDays(DAYS_TO_EXPIRE);
            CreatedAt = DateTime.UtcNow;
        }
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
