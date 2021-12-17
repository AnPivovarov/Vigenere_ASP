using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Vigenere_ASP.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Vigenere_ASP.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHostEnvironment _environment;

        public IndexModel(ILogger<IndexModel> logger, IHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }

        //private string fullPath = AppDomain.CurrentDomain.BaseDirectory.ToString() + "Files";

        [BindProperty(SupportsGet = true)]
        public VigenereModel VigenereModel { get; set; }

        public static string InputText { get; set; } = "";

        public string Result { get; set; }

        private static string SavedResult { get; set; } = "";

        public string Key { get; set; }

        public string[] Modes = new[] { "Расшифровать", "Зашифровать" };

        public string Mode { get; set; }

        [BindProperty]
        public IFormFile InputFile { get; set; }

        public string OutFileName { get; set; }

        public string OutFileExt { get; set; }

        public void OnGet()
        {
            VigenereModel.InputText = InputText;
            InputText = "";
        }

        public IActionResult OnPostUpload()
        {
            if (InputFile != null)
            {
                string ext = Path.GetExtension(InputFile.FileName);
                var file = Path.Combine(_environment.ContentRootPath, "Files", "input" + ext);

                if (ext == ".txt")
                {
                    InputText = TxtProcessing(file);
                }
                else if (ext == ".docx")
                {
                    InputText = DocxProcessing(file);
                }


            }

            return RedirectToPage("Index");
        }

        public void OnPostCalc()
        {
            Key = VigenereModel.Key;
            if (string.IsNullOrEmpty(Key))
            {
                Key = "скорпион";
            }
            Mode = VigenereModel.Mode;
            if (Mode == Modes[0])
            {
                Result = Vigenere.VigenereDecrypt(VigenereModel.InputText, Key);
                if (string.IsNullOrEmpty(Result))
                {
                    Result = "";
                }
            }
            else if (Mode == Modes[1])
            {
                Result = Vigenere.VigenereEncrypt(VigenereModel.InputText, Key);
                if (string.IsNullOrEmpty(Result))
                {
                    Result = "";
                }
            }
            VigenereModel.OutputText = Result;
            SavedResult = Result;
        }

        public ActionResult OnPostDownload()
        {
            Aspose.Words.Document doc = new Aspose.Words.Document();
            Aspose.Words.DocumentBuilder builder = new Aspose.Words.DocumentBuilder(doc);
            builder.Write(SavedResult);

            OutFileName = VigenereModel.OutFileName;

            if (string.IsNullOrEmpty(OutFileName))
            {
                OutFileName = "output";
            }
            OutFileExt = VigenereModel.OutFileExt;

            if (OutFileExt == ".txt")
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    doc.Save(ms, Aspose.Words.SaveFormat.Text);
                    return File(ms.ToArray(), "application/pdf", OutFileName +  ".txt");
                }
            }
            else if (OutFileExt == ".docx")
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    doc.Save(ms, Aspose.Words.SaveFormat.Docx);
                    return File(ms.ToArray(), "application/pdf", OutFileName + ".docx");
                }
            }

            return Page();
        }

        public string TxtProcessing(string file)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            string processingResult = "";

            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                InputFile.CopyTo(fileStream);
            }

            using (var stream = new StreamReader(file))
            {
                processingResult = stream.ReadToEnd();
            }

            bool isEncodingRight = false;
            foreach (char c in processingResult)
            {
                if (Vigenere.letters.Contains(c))
                {
                    isEncodingRight = true;
                }
            }

            if (isEncodingRight == false)
            {
                using (var stream = new StreamReader(file, Encoding.GetEncoding("windows-1251")))
                {

                    processingResult = stream.ReadToEnd();
                }
            }

            return processingResult;
        }

        public string DocxProcessing(string file)
        {
            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                InputFile.CopyTo(fileStream);
            }

            string processingResult = "";
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Open(file, false))
            {
                Body body = wordDocument.MainDocumentPart.Document.Body;
                processingResult = body.InnerText.ToString();
            }
            return processingResult;
        }
    }


}
