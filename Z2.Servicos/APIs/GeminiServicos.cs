using GenerativeAI;
using GenerativeAI.Types;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Environment = System.Environment;

namespace Z2.Servicos.APIs
{
    public interface IGeminiServicos
    {
        Task<string> PromptFile(string prompt, string filebase64);
        Task<string> Prompt(string prompt);
    }

    public class GeminiServicos : IGeminiServicos
    {
        private readonly GenerativeModel _model;
        private readonly string key = Environment.GetEnvironmentVariable("GEMINI_API_KEY");
        private readonly string versao = "gemini-2.5-flash";

        public GeminiServicos()
        {
            _model = new GenerativeModel(key, versao);
        }

        public async Task<string> Prompt(string prompt)
        {
            var content = new Content
            {
                Parts = new List<Part>
                {
                    new Part
                    {
                        Text = prompt
                    }
                }
            };
            var request = new GenerateContentRequest(content);
            var result = await _model.GenerateContentAsync(request);
            var resposta = result.Text?.Trim();
            return resposta ?? "Sem resposta";
        }

        public async Task<string> PromptFile(string prompt, string filebase64)
        {
            var content = new Content
            {
                Parts = new List<Part>
                {
                    new Part
                    {
                        Text = prompt
                    },
                    new Part
                    {
                        InlineData = new Blob
                        {
                            MimeType = "image/jpeg",
                            Data = filebase64
                        }
                    }
                }
            };
            var request = new GenerateContentRequest(content);
            var result = await _model.GenerateContentAsync(request);
            var resposta = result.Text?.Trim();
            return resposta ?? "Sem resposta";
        }
    }
}
