using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EFRelationships.Data
{
    public class QestionAnswerDataContextFactory : IDesignTimeDbContextFactory<QuestionAnswerDBContext>
    {
        public QuestionAnswerDBContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), $"..{Path.DirectorySeparatorChar}EFRelationships.Web"))
               .AddJsonFile("appsettings.json")
               .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true).Build();

            return new QuestionAnswerDBContext(config.GetConnectionString("ConStr"));
        }
    }
}
