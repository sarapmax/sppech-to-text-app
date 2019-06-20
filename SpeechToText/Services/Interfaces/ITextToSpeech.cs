using System.Threading.Tasks;

namespace SpeechToText.Services.Interfaces
{
    public interface ITextToSpeech
    {
        Task<byte[]> TranslateText(string token, string key, string content);
    }
}
