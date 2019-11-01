using EuConto.Data;
using EuConto.Models;
using EuConto.Models.Story;
using EuConto.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace EuConto.Controllers
{
    [Authorize]
    public class StoryController : Controller
    {
        protected ApplicationDbContext _context;
        protected UserManager<ApplicationUserModel> _userManager;
        protected UserServices _userServices;

        public StoryController(ApplicationDbContext context,
            UserManager<ApplicationUserModel> userManager,
            UserServices userServices)
        {
            _context = context;
            _userManager = userManager;
            _userServices = userServices;
        }

        #region User interactions with Story

        public IActionResult UserStorys()
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var UserLogged = _userManager.FindByIdAsync(UserId).Result;
            List<StoryModel> Storys = new List<StoryModel>();

            var DBStorys = _context.Storys.Include(x => x.User)
                                            .Include(x => x.Interaction)
                                                .ThenInclude(x => x.Likes)
                                                    .ThenInclude(x => x.User)
                                            .Include(x => x.Interaction.Comentaries);

            foreach (var DBStory in DBStorys.Where(x => x.User.Id == UserId))
            {
                Storys.Add(new StoryModel
                {
                    Id = DBStory.Id,
                    Title = DBStory.Title,
                    Description = DBStory.Description,
                    Gender = DBStory.Gender,
                    Published = DBStory.Published,
                    DtLastPublish = DBStory.DtLastPublish,
                    Likes = DBStory.Interaction.Likes.Count,
                    Comentaries = DBStory.Interaction.Comentaries.Count,
                    Liked = DBStory.Interaction.Likes.Find(x => x.User.Id == UserId) != null ? true : false,
                    InteractionId = DBStory.Interaction.Id,
                    UserId = UserId,
                    UserName = UserLogged.UserName
                });
            }

            return View(Storys);
        }

        public IActionResult UserStoryCreateEdit(string storyId)
        {
            StoryModel Story = new StoryModel();


            if(storyId != "0")
            {
                var DBStorys = _context.Storys.Include(x => x.User);

                foreach (var DBStory in DBStorys.Where(x => x.User.Id == User.FindFirstValue(ClaimTypes.NameIdentifier) && x.Id == storyId))
                {
                    Story.Id = DBStory.Id;
                    Story.Title = DBStory.Title;
                    Story.Description = DBStory.Description;
                    Story.Gender = DBStory.Gender;
                    Story.Published = DBStory.Published;
                }
            }

            return View(Story);
        }

        [HttpPost]
        public IActionResult UserStoryCreateEdit(StoryModel Story)
        {
            if (ModelState.IsValid)
            {
                if (Story.Id != null)
                {
                    var DBStorys = _context.Storys.Include(x => x.User);

                    foreach (var DBStory in DBStorys.Where(x => x.User.Id == User.FindFirstValue(ClaimTypes.NameIdentifier) && x.Id == Story.Id))
                    {
                        DBStory.Title = Story.Title;
                        DBStory.Description = Story.Description;
                        DBStory.Gender = Story.Gender;
                        if (Story.Published != DBStory.Published)
                        {
                            DBStory.Published = Story.Published;
                            DBStory.DtLastPublish = DateTime.Now;
                        }
                    }
                }
                else
                {
                    var NewInteraction = new InteractionDataModel();
                    NewInteraction.Likes = new List<LikesDataModel>();
                    NewInteraction.Comentaries = new List<ComentaryDataModel>();

                    _context.Storys.Add(new StoryDataModel
                    {
                        Title = Story.Title,
                        Description = Story.Description,
                        Gender = Story.Gender,
                        User = _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)).Result,
                        Published = Story.Published,
                        DtLastPublish = Story.Published ? DateTime.Now : new DateTime(),
                        DtCreation = DateTime.Now,
                        Interaction = NewInteraction
                    });
                }

                _context.SaveChanges();

                return RedirectToAction("UserStorys");
            }

            return View();            
        }

        public IActionResult UserStoryDelete(string storyId)
        {
            if (storyId != "0")
            {
                _context.Remove(_context.Storys.Where(x => x.User.Id == User.FindFirstValue(ClaimTypes.NameIdentifier) && x.Id == storyId).FirstOrDefault());

                _context.SaveChanges();
            }

            return RedirectToAction("UserStorys");
        }

        public IActionResult UserStoryChapters(string storyId = "0")
        {
            ChapterModel StoryChapter = new ChapterModel();
            var UserLogged = _userServices.GetByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)).Result;

            if (storyId != "0")
            {
                var Story = _context.Storys.Include(x => x.User)
                                            .Include(x => x.Chapters)
                                                .ThenInclude(x => x.Interaction)
                                                    .ThenInclude(x => x.Likes)
                                                        .ThenInclude(x => x.User)
                                            .Include(x => x.Chapters)
                                                .ThenInclude(x => x.Interaction)
                                                    .ThenInclude(x => x.Comentaries)
                                            .Where(x => x.Id == storyId);

                StoryChapter.IsEditable = Story.FirstOrDefault(x => x.User.Id == User.FindFirstValue(ClaimTypes.NameIdentifier) && x.Id == storyId) != null ? true : false;

                foreach (var DBStory in Story)
                {
                    StoryChapter.Story = new StoryModel{
                        Id = DBStory.Id,
                        Title = DBStory.Title,
                        Description = DBStory.Description,
                        Gender = DBStory.Gender
                    };

                    if(DBStory.Chapters != null)
                    {
                        StoryChapter.Chapters = new List<StoryChapters>();

                        foreach (var ChapterStory in DBStory.Chapters)
                        {
                            if(StoryChapter.IsEditable || (!StoryChapter.IsEditable && ChapterStory.Published))
                            {
                                StoryChapter.Chapters.Add(new StoryChapters
                                {
                                    Id = ChapterStory.Id,
                                    Seq = ChapterStory.Seq,
                                    Title = ChapterStory.Title,
                                    Description = ChapterStory.Description,
                                    Published = ChapterStory.Published,
                                    DtLastPublish = ChapterStory.DtLastPublish,
                                    Likes = ChapterStory.Interaction.Likes.Count,
                                    Comentaries = ChapterStory.Interaction.Comentaries.Count,
                                    Liked = ChapterStory.Interaction.Likes.Find(x => x.User.Id == User.FindFirstValue(ClaimTypes.NameIdentifier)) != null ? true : false,
                                    InteractionId = ChapterStory.Interaction.Id,
                                    UserName = UserLogged.UserName,
                                    UserId = UserLogged.Id
                                });
                            }

                            StoryChapter.Chapters.OrderByDescending(x => x.Seq);
                        }
                    }
                }
            }
            
            return View(StoryChapter);
        }

        public IActionResult UserStoryChapterSectionsCreateEdit(string storyId, string chapterId)
        {
            var Model = new ChapterSectionModel();

            var Story = _context.Storys.Where(x => x.User.Id == User.FindFirstValue(ClaimTypes.NameIdentifier) && x.Id == storyId);

            foreach (var DBStory in Story)
            {
                Model.Story = new StoryModel
                {
                    Id = DBStory.Id,
                    Title = DBStory.Title,
                    Description = DBStory.Description,
                    Gender = DBStory.Gender
                };
            }

            var DBChapterP = _context.Chapters.Include(x => x.Sections);
            var Chapter = DBChapterP.Where(x => x.Id == chapterId);

            foreach (var DBChapter in Chapter)
            {
                Model.Chapter = new StoryChapters
                {
                    Id = DBChapter.Id,
                    Seq = DBChapter.Seq,
                    Title = DBChapter.Title,
                    Description = DBChapter.Description,
                    Published = DBChapter.Published
                };

                Model.Sections = new List<SectionModel>();

                foreach (var Section in DBChapter.Sections)
                {
                    Model.Sections.Add(new SectionModel
                    {
                        Id = Section.Id,
                        Seq = Section.Seq,
                        Text = Section.Text == null ? "" : Section.Text
                    });
                }

                Model.Sections = Model.Sections.OrderBy(x => x.Seq).ToList();
            }

            if(Model.Chapter == null)
            {
                Model.Chapter = new StoryChapters
                {
                    Seq = 1,
                    Title = "",
                    Description = "",
                    Published = false
                };
            }

            if (Model.Sections == null)
            {
                Model.Sections = new List<SectionModel>();
                Model.Sections.Add(new SectionModel
                {
                    Seq = 1,
                    Text = ""
                });
            }

            return View(Model);
        }

        [HttpPost]
        public IActionResult UserStoryChapterSectionsCreateEdit(string storyId, StoryChapters Chapter, List<SectionModel> Sections)
        {
            var DBStorys = _context.Storys.Include(x => x.User);
            var DBStorysChapter = _context.Storys.Include(x => x.Chapters);

            var Story = DBStorysChapter.Where(x => x.User.Id == User.FindFirstValue(ClaimTypes.NameIdentifier) && x.Id == storyId).First();

            var NextSeq = 0;
            if (Story.Chapters.Count > 1)
                NextSeq = Story.Chapters.Last().Seq + 1;

            if (Chapter.Id == "novo")
            {
                if(Story.Chapters == null)
                {
                    Story.Chapters = new List<ChapterDataModel>();
                }

                var NewInteraction = new InteractionDataModel();
                NewInteraction.Likes = new List<LikesDataModel>();
                NewInteraction.Comentaries = new List<ComentaryDataModel>();

                Story.Chapters.Add(new ChapterDataModel
                {
                    Seq = NextSeq,
                    Title = Chapter.Title,
                    Description = Chapter.Description,
                    Published = Chapter.Published,
                    DtLastPublish = Chapter.Published ? DateTime.Now : new DateTime(),
                    Sections = UpdateSections(Sections),
                    Interaction = NewInteraction
                });
            }
            else
            {
                var DBStoryChapter = _context.Chapters.Include(x => x.Sections);
                var StoryChapter = DBStoryChapter.Where(x => x.Id == Chapter.Id).First();

                StoryChapter.Seq = Chapter.Seq;
                StoryChapter.Title = Chapter.Title;
                StoryChapter.Description = Chapter.Description;
                StoryChapter.Sections.Clear();
                StoryChapter.Sections = UpdateSections(Sections);
                if (Chapter.Published != StoryChapter.Published)
                {
                    StoryChapter.Published = Chapter.Published;
                    StoryChapter.DtLastPublish = DateTime.Now;
                }
            }

            _context.SaveChanges();

            return Json(new { ChapterId = Story.Chapters.Where(x => x.Seq == Chapter.Seq).First().Id, SectionsSaved = Sections });
        }

        public IActionResult UserStoryChapterSectionsRead(string storyId, string chapterId)
        {
            var Model = new ChapterSectionModel();

            var Story = _context.Storys.Where(x => x.Id == storyId);

            foreach (var DBStory in Story)
            {
                Model.Story = new StoryModel
                {
                    Id = DBStory.Id,
                    Title = DBStory.Title,
                    Description = DBStory.Description,
                    Gender = DBStory.Gender
                };
            }

            var DBChapterP = _context.Chapters.Include(x => x.Sections);
            var Chapter = DBChapterP.Where(x => x.Id == chapterId);

            foreach (var DBChapter in Chapter)
            {
                Model.Chapter = new StoryChapters
                {
                    Id = DBChapter.Id,
                    Seq = DBChapter.Seq,
                    Title = DBChapter.Title,
                    Description = DBChapter.Description
                };

                Model.Sections = new List<SectionModel>();

                foreach (var Section in DBChapter.Sections)
                {
                    Model.Sections.Add(new SectionModel
                    {
                        Id = Section.Id,
                        Seq = Section.Seq,
                        Text = Section.Text == null ? "" : Section.Text
                    });
                }

                Model.Sections = Model.Sections.OrderBy(x => x.Seq).ToList();
            }

            if (Model.Chapter == null)
            {
                Model.Chapter = new StoryChapters
                {
                    Seq = 1,
                    Title = "",
                    Description = ""
                };
            }

            if (Model.Sections == null)
            {
                Model.Sections = new List<SectionModel>();
                Model.Sections.Add(new SectionModel
                {
                    Seq = 1,
                    Text = ""
                });
            }

            return View(Model);
        }

        private List<SectionDataModel> UpdateSections(List<SectionModel> Sections)
        {
            List<SectionDataModel> ret = new List<SectionDataModel>();

            foreach(SectionModel Section in Sections)
            {
                ret.Add(new SectionDataModel
                {
                    Seq = Section.Seq,
                    Text = Section.Text
                });
            }

            return ret;
        }

        public IActionResult UserStoryChapterSectionsDelete(string storyId, string chapterId)
        {
            var Story = _context.Storys.Where(x => x.User.Id == User.FindFirstValue(ClaimTypes.NameIdentifier) && x.Id == storyId).FirstOrDefault();
            
            var Chapter = _context.Chapters.Where(x => x.Id == chapterId && x.Story.Id == Story.Id).FirstOrDefault();

            _context.Remove(Chapter);

            _context.SaveChanges();
            
            return RedirectToRoute("UserStoryChapters", new { storyId = storyId });
        }

        #endregion

        #region Story interactions of application

        #endregion
    }
}