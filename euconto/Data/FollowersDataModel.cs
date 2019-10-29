using EuConto.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EuConto.Data
{
    public class FollowersDataModel
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public virtual ApplicationUserModel User { get; set; }

        public virtual List<ApplicationUserModel> Followers { get; set; }
    }
}