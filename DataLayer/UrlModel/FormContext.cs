using Microsoft.EntityFrameworkCore;


namespace DataLayer.UrlModel
{
    public class FormContext: DbContext
    {
        public DbSet<UrlForm> Form { get; set; }

        public FormContext(DbContextOptions<FormContext> options) : base(options)
        {
            Database.EnsureCreated();
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // root@127.0.0.1:3306
            optionsBuilder.UseMySQL("Server=127.0.0.1;Uid=shortenerapp;Pwd=ShortenerApp-123;Database=UrlForm;SslMode=none");
        }

    }
}
