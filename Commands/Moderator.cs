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
    // Delete sent message
    await ctx.Message.DeleteAsync();

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
      catch // First argument is not a channel or failed to find channel, just post message
      {
        await ctx.Channel.SendMessageAsync($"{channel} {message}");
      }
    } 
    else
    {
      // just discard message mayb
    }
  }

  [Command("sayembed"), RequireUserPermissions(DSharpPlus.Permissions.ModerateMembers)]
  public async Task DSayEmbed(CommandContext ctx, [RemainingText] string input = "")
  {
    // Delete sent message
    await ctx.Message.DeleteAsync();
    
    string titleParam = "--title";
    string descriptionParam = "--content";

    // Get embed title
    string title = input.Substring(input.IndexOf(titleParam) + titleParam.Length);
    title = title.Substring(0, title.IndexOf(descriptionParam)).Trim();

    // Get embed content
    string description = input.Substring(input.IndexOf(descriptionParam) + descriptionParam.Length).Trim();

    // Send embed
    SayEmbed em = new SayEmbed();
    await ctx.Channel.SendMessageAsync(em.SendSayEmbed(title, description));
  }
}