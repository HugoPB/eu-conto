using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EuConto.Models.Components
{
    public class SpotlightUsersModel
    {
        public List<SpotlightUsers> SpotlightUsers = new List<SpotlightUsers>();
    }

    public class SpotlightUsers
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Bio { get; set; }
    }
}
