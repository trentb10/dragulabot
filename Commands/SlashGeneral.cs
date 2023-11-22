using DSharpPlus.SlashCommands;

public class SlashGeneral : ApplicationCommandModule
{
  static public Random r = new Random();

  [SlashCommand("ping", "Test Dragula's latency in ms")]
  public async Task SlashPing(InteractionContext ctx)
  {
    // Get latency
    int latency = ctx.Client.Ping;
    
    // Show results
    PingEmbed em = new PingEmbed();

    await ctx.CreateResponseAsync(em.SendPing(latency.ToString()));
  }

  [SlashCommand("random", "Post a random music video from the Ai Music Videos YouTube channel")]
  public async Task SlashRandom(InteractionContext ctx)
  {
    // Populate list of videos
    List<string> videos = new List<string>();

    videos.Add("https://www.youtube.com/watch?v=dKgZj1R1I1A");
    videos.Add("https://www.youtube.com/watch?v=794rosnY5jw");
    videos.Add("https://www.youtube.com/watch?v=fqWGHh3upHg");
    videos.Add("https://www.youtube.com/watch?v=AEUu3heIJNY");
    videos.Add("https://www.youtube.com/watch?v=5ErWHgsxZcM");
    videos.Add("https://www.youtube.com/watch?v=uZHayUg3wRI");
    videos.Add("https://www.youtube.com/watch?v=qKwo-siqpFQ");

    // Choose video
    int showVideo = r.Next(0, videos.Count);
    
    // Post video
    await ctx.CreateResponseAsync(videos[showVideo]);
  }
}