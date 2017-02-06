using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Medico.Model;
using Medico.Repository.Interfaces;
using Medico.Web.Filters;

namespace Medico.Web.Controllers
{
     [AuthorizeRedirect(Roles = "Publisher")]
    public class IssueController : Controller
     {
         private IIssueRepository _issueRepository;

         public IssueController(IIssueRepository issueRepository)
         {
             _issueRepository = issueRepository;
         }

         public ActionResult List(int id)
         {
             List<Issue> issueList = _issueRepository.GetIssuesofJournal(id);
             var journals = Mapper.Map<List<Issue>, List<IssueViewModel>>(issueList);
             return View(journals);
         }   
     }
}