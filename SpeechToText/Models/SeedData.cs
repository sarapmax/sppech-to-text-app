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
                        RequestText = "Good afternoon. TBH Network Solutions.",
                        ResponseText = "Yeah, hi. My system is down and I need to speak with a technician. "
                    },

                    new Speech
                    {
                        RequestText = "Oh, okay. Let me gather some information and see if we can help. What is your first name? ",
                        ResponseText = "Maria."
                    },

                    new Speech
                    {
                        RequestText = "And your last name; would you spell it for me please?",
                        ResponseText = "It’s C-H-A-M-B- E-R-S, Chambers."
                    },

                    new Speech
                    {
                        RequestText = "Okay. And your company name? ",
                        ResponseText = "I’m with GoldStar Environmental. "
                    },

                    new Speech
                    {
                        RequestText = "Okay. And your company name? ",
                        ResponseText = "I’m with GoldStar Environmental. "
                    },

                    new Speech
                    {
                        RequestText = "Okay. And your callback number? ",
                        ResponseText = "610-265-1715."
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
