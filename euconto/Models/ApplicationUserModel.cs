using EuConto.Data;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EuConto.Models
{
    public class ApplicationUserModel : IdentityUser
    {
        public List<StoryDataModel> Storys { get; set; }

        [Required]
        [MaxLength(256)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(2048)]
        public string Bio { get; set; }
    }
}