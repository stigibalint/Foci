using Foci.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Foci.Pages
{
    public class AdatokFeltolteseFajlbolModel : PageModel
       
    {
        private readonly IWebHostEnvironment _env;
        private readonly FociDbContext _context;
       
        public AdatokFeltolteseFajlbolModel(IWebHostEnvironment env, FociDbContext context)
        
        {
            _env = env;
            _context = context;
        }
        [BindProperty]
        public IFormFile Feltoltes { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync() 
        {
            if(Feltoltes == null || Feltoltes.Length == 0)
            {
                ModelState.AddModelError("Feltoltes", "Adj meg egy állományt");
                return Page();
            }
            var UploadDirtPath = Path.Combine(_env.ContentRootPath, "uploads");
            var UploadFilePath = Path.Combine(UploadDirtPath, Feltoltes.FileName);
            using(var  stream = new FileStream(UploadFilePath, FileMode.Create))
            {
                await Feltoltes.CopyToAsync(stream);
            }
            StreamReader sr = new StreamReader(UploadFilePath);
            sr.ReadLine();
            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine();
                var elemek = line.Split();
                Meccs UjMeccs = new Meccs();
                UjMeccs.Fordulo = int.Parse(elemek[0]);
                UjMeccs.HazaiVeg = int.Parse(elemek[1]);
                //......
                _context.Meccsek.Add(UjMeccs);
                
            }
            sr.Close();
            _context.SaveChanges();

       //     System.IO.File.Delete(UploadFilePath);



            return Page();
        }
    }
}
