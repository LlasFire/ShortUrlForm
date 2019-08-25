using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace DataLayer.UrlModel
{
    //only for action Create
    public class CreateForm
    {
        [Display(Name = "Your URL")]
        [Required(ErrorMessage = "No URL given")]
        [Url(ErrorMessage = "The URL is incorrect")]
        [Remote(action: "CheckURL", controller: "Home")]
        public string LongUrl { get; set; }
    }
}
