using ChatBot.Domain;
using Microsoft.EntityFrameworkCore;

namespace ChatBot.Infrastructure;

public class ChatBotDbContext : DbContext
{
    public ChatBotDbContext(DbContextOptions<ChatBotDbContext> options)
        : base(options)
    { }
    
    public DbSet<Conversation> Conversations { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Conversation>()
            .HasKey(c => c.Id);
        
        modelBuilder.Entity<Conversation>()
            .HasMany(c => c.Messages)
            .WithOne(m => m.Conversation);
        
        modelBuilder.Entity<Conversation>()
            .Navigation(c => c.Messages)
            .AutoInclude();
    }
}