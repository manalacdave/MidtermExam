using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations.Schema;

namespace Manalacdave.MidtermExam.Infrastructure.Domain.Model
{
    public class Articles
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? EmailAddress { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Gender? Gender { get; set; }
        public Guid? RoleId { get; set; }

        [ForeignKey("ArticlesId")]
        public Articles? Articles { get; set; }
    }

    public enum Type
    {
        Editorial = 1,
        News = 2
    }
}