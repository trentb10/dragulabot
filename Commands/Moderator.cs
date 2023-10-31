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
  public async Task DSay(CommandContext ctx, [RemainingText] string message = "")
  {
    // Check if message isn't empty
    if (message != "")
    {
      // Delete sent message
      await ctx.Message.DeleteAsync();
      // Check if a channel to post to was provided

      // Probably a Channel
      // ------------------
      if (message.StartsWith("<#"))
      {
        // Extract channel id
        string channel = message.Substring(0, message.IndexOf(">") + 1);

        // Trim channel from message
        string trimmedMessage = message.Replace(channel, string.Empty).Trim();
        
        // Get channel id
        string[] filter = new string[] { "<", ">", "#"};
        foreach (string c in filter)
        {
          channel = channel.Replace(c, string.Empty);
        }

        // Attempt to send message into channel
        try
        {
          DiscordChannel chan = await ctx.Client.GetChannelAsync(Convert.ToUInt64(channel));

          // Valid channel, post to designated channel
          await SayToChannel (chan, trimmedMessage);
        }
        // Okay, so it's not a channel, or something went wrong, just post the message string
        catch
        {
          await Say(ctx, message);
        }
      }
      // Not a Channel
      // -------------
      else
      {
        await Say(ctx, message);
      }
    }
    else
    {
      // do something, or just ignore
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

  #region Non-Command methods
  public async Task Say(CommandContext ctx, string message)
  {
    await ctx.Channel.SendMessageAsync(message);
  }

  public async Task SayToChannel(DiscordChannel chan, string message)
  {
    await chan.SendMessageAsync(message);
  }
  #endregion
}