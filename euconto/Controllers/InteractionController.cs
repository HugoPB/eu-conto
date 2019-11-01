using EuConto.Data;
using EuConto.Models.Interaction;
using EuConto.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EuConto.Controllers
{
    public class InteractionController : Controller
    {
        protected ApplicationDbContext _context;
        protected UserServices _userServices;

        public InteractionController(ApplicationDbContext context,
            UserServices userServices)
        {
            _context = context;
            _userServices = userServices;
        }

        public async Task<IActionResult> SaveInteraction(string interactionid, string commentarytext)
        {
            var UserLogged = await _userServices.GetByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var Interaction = await _context.Interactions
                                        .Include(x => x.Comentaries)
                                        .FirstAsync(x => x.Id == interactionid);

            var subInt = new SubInteractionDataModel();
            var newInt = new InteractionDataModel
            {
                Likes = new List<LikesDataModel>(),
                Comentaries = new List<ComentaryDataModel>()
            };

            subInt.Interaction = newInt;

            ComentaryDataModel NewCommentarie = new ComentaryDataModel
            {
                Text = commentarytext,
                User = UserLogged,
                SubInteraction = subInt
            };

            Interaction.Comentaries.Add(NewCommentarie);

            await _context.SaveChangesAsync();

            return Json(new { CommentarieId = NewCommentarie.Id, UserId = UserLogged.Id, UserName = UserLogged.UserName, Text = NewCommentarie.Text });
        }

        public async Task<IActionResult> DeleteInteraction(string commentarieid)
        {
            var commentarie = await _context.Comentaries.FirstAsync(x => x.Id == commentarieid);

            _context.Remove(commentarie);

            await _context.SaveChangesAsync();

            return Json(new { sucess = true });
        }

        public async Task<IActionResult> InteractionById(string interactionid)
        {
            var UserLoggedId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var Interaction = await _context.Interactions
                                        .Include(x => x.Comentaries)
                                        .ThenInclude(x => x.SubInteraction)
                                        .ThenInclude(x => x.Interaction)
                                        .ThenInclude(x => x.Comentaries)
                                        .Include(x => x.Comentaries)
                                        .ThenInclude(x => x.User)
                                        .FirstAsync(x => x.Id == interactionid);

            var IntComent = new List<IntCommentaryModel>();

            foreach (var Coment in Interaction.Comentaries)
            {
                IntComent.Add(new IntCommentaryModel
                {
                    Text = Coment.Text,
                    SubInteractionId = Coment.SubInteraction.Interaction.Comentaries.Count > 0 ? Coment.SubInteraction.Interaction.Id : "",
                    UserName = Coment.User.UserName,
                    UserId = Coment.User.Id,
                    CommentarieId = Coment.Id,
                    IsEditable = Coment.User.Id == UserLoggedId ? true : false
                });
            }

            return Json(new { Interactions = IntComent });
        }
    }
}
