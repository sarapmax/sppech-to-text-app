using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SpeechToText.Models;
using SpeechToText.models;

namespace SpeechToText.Pages
{
    public class exampleModel : PageModel
    {
        private readonly SpeechToText.Models.SpeechToTextContext _context;

        public exampleModel(SpeechToText.Models.SpeechToTextContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Speech Speech { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Speech.Add(Speech);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}