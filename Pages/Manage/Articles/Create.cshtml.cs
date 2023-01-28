using Manalacdave.MidtermExam.Infrastructure.Domain;
using Manalacdave.MidtermExam.Infrastructure.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace Manalacdave.MidtermExam.Pages.Manage.Columns
{
    public class Create : PageModel
    {
        private ILogger<Index> _logger;
        private DefaultDbContext _context;

        [BindProperty]
        public ViewModel View { get; set; }

        public Create(DefaultDbContext context, ILogger<Index> logger)
        {
            _logger = logger;
            _context = context;
            View = View ?? new ViewModel();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(View.Name))
            {
                ModelState.AddModelError("", "Role name cannot be blank.");
                return Page();
            }

            if (string.IsNullOrEmpty(View.Description))
            {
                ModelState.AddModelError("", "Description name cannot be blank.");
                return Page();
            }

            Columns columns = new Columns()
            {
                Id = Guid.NewGuid(),
                Name = View.Name,
                Description = View.Description,
                Abbreviation = View.Abbreviation
            };

            _context?.Columns?.Add(columns);
            _context?.SaveChanges();

            return RedirectPermanent("~/manage/columns");
        }

        public class ViewModel : Columns
        {

        }
    }
}