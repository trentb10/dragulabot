using System.Text;
using System.Web;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Newtonsoft.Json;

public class General : BaseCommandModule
{
  [Command("greet")]
  public async Task DGreet(CommandContext ctx)
  {
    GenericEmbed em = new GenericEmbed();
    await ctx.Channel.SendMessageAsync(
      em.SendGeneric(BotMessage.GenericInfo.Greet)
    );
  }

  [Command("ping")]
  public async Task DPing(CommandContext ctx)
  {
    // Get latency
    int latency = ctx.Client.Ping;
    
    // Show results
    PingEmbed em = new PingEmbed();

    await ctx.Channel.SendMessageAsync(em.SendPing(latency.ToString()));
  }
}