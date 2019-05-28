using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SpeechToText.Models;

namespace SpeechToText.models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new SpeechToTextContext(
                serviceProvider.GetRequiredService<DbContextOptions<SpeechToTextContext>>()))
            {
                if (context.Speech.Any())
                {
                    return;
                }

                context.Speech.AddRange(
                    new Speech
                    {
                        RequestText = "What are you doing now?",
                        ResponseText = "I'm watching TV."
                    },

                    new Speech
                    {
                        RequestText = "What are you watching?",
                        ResponseText = "I'm watching Friends. What are you doing?"
                    },

                    new Speech
                    {
                        RequestText = "I'm doing my homework, but I really need to take a break.",
                        ResponseText = "You want to do something?"
                    },

                    new Speech
                    {
                        RequestText = "Yes. But I shouldn't. I got to finish my assignment now.",
                        ResponseText = "Alright. Call me later then."
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
