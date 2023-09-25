using AuthSystem.Areas.Identity.Data;
using AuthSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using PdfSharp.Pdf;
using iTextSharp.text.pdf.codec;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;


namespace AuthSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            this._userManager = userManager;
        }

        public IActionResult Index()
        {
            ViewData["UserID"] = _userManager.GetUserId(this.User);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        //[ValidateInput(false)]
        public FileResult Export(string ExportData)
        {
            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                StringReader reader = new StringReader(ExportData);
                // Create a new PDF document with custom settings
                Document PdfFile = new Document(PageSize.A4, 20f, 20f, 20f, 20f); // Adjust margins as needed
                PdfWriter writer = PdfWriter.GetInstance(PdfFile, stream);
                PdfFile.Open();
                // Load and add logo to the PDF document
                string logoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "logo2.png");
                Image logoImage = Image.GetInstance(logoPath);
                PdfFile.Add(logoImage);
                // Setting Font and Text Formatting
                BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, BaseFont.NOT_EMBEDDED);
                Font customFont = new Font(baseFont, 8f, Font.NORMAL);
                // Add a title to the PDF document
                Font titleFont = new Font(Font.FontFamily.HELVETICA, 14f, Font.BOLD);
                Paragraph title = new Paragraph("Order List", titleFont);
                title.Alignment = Element.ALIGN_CENTER;
                title.SpacingAfter = 8f;
                PdfFile.Add(title);
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, PdfFile, reader);
                PdfFile.AddAuthor("Transportes DJEN, Ld");
                PdfFile.AddTitle("Order List");
                PdfFile.Close();
                return File(stream.ToArray(), "application/pdf", "OrderList.pdf");
            }
        }
    }
}