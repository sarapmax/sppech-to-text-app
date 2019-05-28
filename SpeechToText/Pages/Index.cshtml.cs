using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SpeechToText.Models;

namespace SpeechToText.Pages
{
    public class IndexModel : PageModel
    {
        private SpeechToTextContext _context;

        public IndexModel(SpeechToTextContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
        }

        public JsonResult OnGetSpeechResponse(string speechRequest)
        {
            var response = _context.Speech.FromSql(@"SELECT s.ID, s.RequestText, s.ResponseText, ft.Rank
                                                    FROM Speech s
                                                    INNER JOIN FREETEXTTABLE (Speech, RequestText, {0}) ft
                                                    ON (s.ID = ft.[Key])
                                                    ORDER BY ft.Rank DESC", speechRequest).ToList();

            return new JsonResult(response);
        }
    }
}
