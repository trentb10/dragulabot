using DSharpPlus.SlashCommands;

public class SlashGeneral : ApplicationCommandModule
{
  [SlashCommand("ping", "Test Dragula's latency in ms")]
  public async Task SlashPing(InteractionContext ctx)
  {
    // Get latency
    int latency = ctx.Client.Ping;
    
    // Show results
    PingEmbed em = new PingEmbed();

    await ctx.CreateResponseAsync(em.SendPing(latency.ToString()));
  }
}