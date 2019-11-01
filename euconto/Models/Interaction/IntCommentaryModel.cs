using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EuConto.Models.Interaction
{
    public class IntCommentaryModel
    {
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string Text { get; set; }
        public string SubInteractionId { get; set; }
        public string CommentarieId { get; set; }
        public bool IsEditable { get; set; }
    }
}
