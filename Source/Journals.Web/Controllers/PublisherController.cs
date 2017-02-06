using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using AutoMapper;
using Medico.Model;
using Medico.Repository.Interfaces;
using Medico.Web.Filters;
using Medico.Web.Helpers;

namespace Medico.Web.Controllers
{
    /// <summary>
    /// The publisher controller.
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    [AuthorizeRedirect(Roles = "Publisher")]
    public class PublisherController : Controller
    {
        /// <summary>
        /// The journal repository
        /// </summary>
        private IJournalRepository _journalRepository;

        /// <summary>
        /// The membership service
        /// </summary>
        private IStaticMembershipService _membershipService;

        /// <summary>
        /// The issue repository
        /// </summary>
        private IIssueRepository _issueRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublisherController"/> class.
        /// </summary>
        /// <param name="journalRepo">The journal repo.</param>
        /// <param name="membershipService">The membership service.</param>
        /// <param name="issueRepository">The issue repository.</param>
        public PublisherController(IJournalRepository journalRepo, IStaticMembershipService membershipService, IIssueRepository issueRepository)
        {
            _journalRepository = journalRepo;
            _membershipService = membershipService;
            _issueRepository = issueRepository;
        }

        public ActionResult Index()
        {
            var userId = (int)_membershipService.GetUser().ProviderUserKey;

            List<Journal> allJournals = _journalRepository.GetAllJournals(userId);
            var journals = Mapper.Map<List<Journal>, List<JournalViewModel>>(allJournals);
            return View(journals);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult GetFile(int Id)
        {
            Journal j = _journalRepository.GetJournalById(Id);
            if (j == null)
                throw new System.Web.Http.HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));

            return File(j.Content, j.ContentType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(JournalViewModel journal)
        {
            if (ModelState.IsValid)
            {
                var newJournal = Mapper.Map<JournalViewModel, Journal>(journal);
                JournalHelper.PopulateFile(journal.File, newJournal);

                newJournal.UserId = (int)_membershipService.GetUser().ProviderUserKey;

                var opStatus = _journalRepository.AddJournal(newJournal);
                if (!opStatus.Status)
                    throw new System.Web.Http.HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));

                return RedirectToAction("Index");
            }
            else
                return View(journal);
        }

        public ActionResult Delete(int Id)
        {
            var selectedJournal = _journalRepository.GetJournalById(Id);
            var journal = Mapper.Map<Journal, JournalViewModel>(selectedJournal);
            return View(journal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(JournalViewModel journal)
        {
            var selectedJournal = Mapper.Map<JournalViewModel, Journal>(journal);

            var opStatus = _journalRepository.DeleteJournal(selectedJournal);
            if (!opStatus.Status)
                throw new System.Web.Http.HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int Id)
        {
            var journal = _journalRepository.GetJournalById(Id);

            var selectedJournal = Mapper.Map<Journal, JournalUpdateViewModel>(journal);

            return View(selectedJournal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(JournalUpdateViewModel journal)
        {
            if (ModelState.IsValid)
            {
                var selectedJournal = Mapper.Map<JournalUpdateViewModel, Journal>(journal);
                JournalHelper.PopulateFile(journal.File, selectedJournal);

                var opStatus = _journalRepository.UpdateJournal(selectedJournal);
                if (!opStatus.Status)
                    throw new System.Web.Http.HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));

                return RedirectToAction("Index");
            }
            else
                return View(journal);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
        }

        /// <summary>
        /// Issues the list.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ActionResult IssueList(int id)
        {
            List<Issue> issueList = _issueRepository.GetIssuesofJournal(id);
            var issueViewModelList = new List<IssueViewModel>();
            foreach (var issue in issueList)
            {
                issueViewModelList.Add(new IssueViewModel()
                {
                    JournalId = issue.JournalId,
                    Id = issue.Id,
                    Text = issue.Text,
                    FileName = issue.FileName
                });
            }
            this.Session.Add("JournalId", id);
            return View(issueViewModelList);
        }

        /// <summary>
        /// Creates the issue.
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateIssue()
        {
            return View();
        }

        /// <summary>
        /// Creates the issue.
        /// </summary>
        /// <param name="issue">The issue.</param>
        /// <returns></returns>
        /// <exception cref="System.Web.Http.HttpResponseException"></exception>
        /// <exception cref="HttpResponseMessage"></exception>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateIssue(IssueViewModel issue)
        {
            if (ModelState.IsValid)
            {
                var newIssue = new Issue()
                {
                    Text = issue.Text,
                    FileName = issue.FileName,
                    JournalId = Convert.ToInt32(this.Session["JournalId"]),
                    CreationDate = DateTime.Now
                };
                IssueHelper.PopulateFile(issue.File, newIssue);

                var opStatus = _issueRepository.AddIssue(newIssue);
                if (!opStatus.Status)
                    throw new System.Web.Http.HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));

                return RedirectToAction("IssueList");
            }
            else
                return View(issue);
        }
    }
}