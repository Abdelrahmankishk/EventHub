using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EventHub
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("EventHub — EF Core Console App");
            using AppDbContext db = new();

            await db.Database.MigrateAsync();
        }
    }
}
