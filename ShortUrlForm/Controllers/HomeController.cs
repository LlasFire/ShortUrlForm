using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataLayer.UrlModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;

namespace ShortUrlForm.Controllers
{
    public class HomeController : Controller
    {
        private readonly FormContext context;

        private readonly DateTime date;

        public HomeController(FormContext context)
        {
            this.context = context;
            date = DateTime.Now;
        }

        public async Task<IActionResult> Index()
        {
            return View(await context.Form.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LongUrl")]CreateForm url)
        {
            UrlForm form = await context.Form.FirstOrDefaultAsync(p => p.LongUrl == url.LongUrl);

            if (form != null)
            {
            }
            else if(ModelState.IsValid)
            {
                UrlForm newForm = GenerateUrl(url.LongUrl);

                context.Form.Add(newForm);
                await context.SaveChangesAsync();
                
            }
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                UrlForm form = await context.Form.FirstOrDefaultAsync(p => p.ID == id);
                if (form != null)
                    return View(form);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UrlForm url)
        {
            url.ShortUrl = GenerateShortURL();

            context.Form.Update(url);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                UrlForm form = await context.Form.FirstOrDefaultAsync(p => p.ID == id);
                if (form != null)
                    return View(form);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                UrlForm form = await context.Form.FirstOrDefaultAsync(p => p.ID == id);
                if (form != null)
                {
                    context.Form.Remove(form);
                    await context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }

        //fetch request increment count of clicks
        [HttpPost]
        public async Task<IActionResult> CounterClickLink(int? id)
        {
            UrlForm form = await context.Form.FirstOrDefaultAsync(p => p.ID == id);

            if (form != null)
            {
                form.CountOfClics ++;

                context.Form.Update(form);
                await context.SaveChangesAsync();

            }

            return RedirectToAction("Index");

        }

        //check our URL before add to database
        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> CheckURL(string url)
        {
            UrlForm form = await context.Form.FirstOrDefaultAsync(p => p.LongUrl == url);

            if (form != null)
                return Json(data: $"This URL: {url} already exists.");
            return Json(true);
        }

        //generate new URL
        public UrlForm GenerateUrl(string longUrl)
        {
            UrlForm url = new UrlForm()
            {
                LongUrl = longUrl,
                ShortUrl = GenerateShortURL(),
                CreationDate = date,
                CountOfClics = 0
            };

            return url;
        }

        //method for create short links
        public string GenerateShortURL()
        {
            int length = 5;
            char[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz123456789".ToCharArray();

            Random rand = new Random();
            StringBuilder url = new StringBuilder(length);

            url.Append($"{this.Request.Scheme}://{this.Request.Host}/");

            int position = 0;

            for (int i = 0; i < length; i++)
            {
                position = rand.Next(0, letters.Length - 1);
                url.Append(letters[position]);
            }
            
            return url.ToString();
        }
    }
}
