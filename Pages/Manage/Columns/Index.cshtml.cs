using Manalacdave.MidtermExam.Infrastructure.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Manalacdave.MidtermExam.Infrastructure.Domain.Models;
using System.Linq;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using Microsoft.Extensions.Options;
using Manalacdave.MidtermExam.Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Manalacdave.MidtermExam.Infrastructure.Domain.Model;

namespace Manalacdave.MidtermExam.Pages.Manage.Columns
{
    [Authorize(Columns = "Admin")]
    public class Index : PageModel
    {
        private ILogger<Index> _logger;
        private DefaultDbContext _context;

        [BindProperty]
        public ViewModel View { get; set; }

        public Index(DefaultDbContext context, ILogger<Index> logger)
        {
            _logger = logger;
            _context = context;
            View = View ?? new ViewModel();
        }

        public IActionResult OnGet(int? pageIndex = 1, int? pageSize = 10, string? sortBy = "", SortOrder sortOrder = SortOrder.Ascending, string? keyword = "")
        {
            var skip = (int)((pageIndex - 1) * pageSize);

            var query = _context.Columns.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(a =>
                            a.Name != null && a.Name.ToLower().Contains(keyword.ToLower())
                        || a.Description != null && a.Description.ToLower().Contains(keyword.ToLower())
                        || a.Abbreviation != null && a.Abbreviation.ToLower().Contains(keyword.ToLower())
                );
            }

            var totalColumns = query.Count();

            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy.ToLower() == "name" && sortOrder == SortOrder.Ascending)
                {
                    query = query.OrderBy(a => a.Name);
                }
                else if (sortBy.ToLower() == "name" && sortOrder == SortOrder.Descending)
                {
                    query = query.OrderByDescending(a => a.Name);
                }
                else if (sortBy.ToLower() == "description" && sortOrder == SortOrder.Ascending)
                {
                    query = query.OrderBy(a => a.Description);
                }
                else if (sortBy.ToLower() == "description" && sortOrder == SortOrder.Descending)
                {
                    query = query.OrderByDescending(a => a.Description);
                }
                else if (sortBy.ToLower() == "abbreviation" && sortOrder == SortOrder.Ascending)
                {
                    query = _context.Roles.OrderBy(a => a.Abbreviation);
                }
                else if (sortBy.ToLower() == "abbreviation" && sortOrder == SortOrder.Descending)
                {
                    query = query.OrderByDescending(a => a.Abbreviation);
                }
            }

            var columns = query
                            .Skip(skip)
                            .Take((int)pageSize)
                            .ToList();

            View.Columns = new Paged<Role>()
            {
                Items = columns,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalRows = totalRs,
                SortBy = sortBy,
                SortOrder = sortOrder,
                Keyword = keyword
            };

            return Page();
        }

        public class ViewModel
        {
            public Paged<Column>? Columns { get; set; }
        }
    }
}