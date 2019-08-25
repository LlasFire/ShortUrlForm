using System.Threading.Tasks;
using DataLayer.UrlModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ShortUrlForm.Controllers
{
    public class ShortUrlController : Controller
    {
        private readonly FormContext context;

        public ShortUrlController(FormContext context)
        {
            this.context = context;
        }

        //redirecting method for ours short links
        [HttpGet]
        public async Task<IActionResult> Path()
        {
            string link = $"{this.Request.Scheme}://{this.Request.Host}{Request.Path}";

            UrlForm form = await context.Form.FirstOrDefaultAsync(p => p.ShortUrl == link);

            if (form != null)
            {
                return Redirect(form.LongUrl);
            }
            else
                return NotFound();
        }
    }
}