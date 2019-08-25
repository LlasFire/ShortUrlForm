using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace DataLayer.UrlModel
{
    //main model class
    public class UrlForm
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Your URL")]
        [Required(ErrorMessage = "No URL given")]
        [Url(ErrorMessage = "The URL is incorrect")]
        public string LongUrl { get; set; }

        [Display(Name = "New short URL")]
        public string ShortUrl { get; set; }

        [Display(Name = "Creation date")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Count of clics")]
        public int CountOfClics { get; set; }

    }
}
