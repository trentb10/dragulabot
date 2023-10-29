using System.Text;
using System.Collections.Generic;
using System.Web;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Newtonsoft.Json;

// These commands may be run by users with Moderator roles only

public class Moderator : BaseCommandModule
{
  [Command("say"), RequireUserPermissions(DSharpPlus.Permissions.ModerateMembers)]
  public async Task DSay(CommandContext ctx, string channel, [RemainingText] string message = "")
  {
    if (channel != "")
    {
      try
      {
        // First detect if the channel is actually a channel
        string[] filter = new string[] { "<", ">", "#" };
        foreach (string c in filter)
        {
          channel = channel.Replace(c, string.Empty);
        }
        DiscordChannel chan = await ctx.Client.GetChannelAsync(Convert.ToUInt64(channel));
        
        // Valid channel, post message in designated channel
        await chan.SendMessageAsync(message);
      }
      catch
      {
        await ctx.Channel.SendMessageAsync($"{channel} {message}");
      }
    } 
    else
    {
      // just discard message mayb
    }
  }
}