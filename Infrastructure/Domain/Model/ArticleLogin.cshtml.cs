using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Manalacdave.MidtermExam.Infrastructure.Domain.Model
{
    public class ArticleLogin
    {
        public Guid? Id { get; set; }
        public Guid? UserId { get; set; }
        public string? Type { get; set; } //Editorial, News
        public string? Key { get; set; }
        public string? Value { get; set; }
    }

}