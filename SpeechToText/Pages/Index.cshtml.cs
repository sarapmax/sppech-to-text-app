using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SpeechToText.models;
using SpeechToText.Models;
using SpeechToText.Services;
using SpeechToText.Services.Interfaces;

namespace SpeechToText.Pages
{
    public class IndexModel : PageModel
    {
        private SpeechToTextContext _context;
        private ITextToSpeech _textToSpeech;

        public string SubscriptionKey { get; set; } = "483f98c4ef9040db9cd9813bd1ba2062";
        public string Token;

        public IndexModel(SpeechToTextContext context, ITextToSpeech textToSpeech)
        {
            _context = context;
            _textToSpeech = textToSpeech;
        }

        public void OnGet()
        {
            var ttsAuth = new TTSAuthentication(SubscriptionKey);

            Token = ttsAuth.GetAccessToken();
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

        public async Task<ActionResult> OnGetTranslate(string content, string token)
        {
            var waveBytes = _textToSpeech.TranslateText(token, SubscriptionKey, content);

            return File(await Task.Run(() => waveBytes), "audio/mpeg", "voice.mp3");
        }
    }
}
