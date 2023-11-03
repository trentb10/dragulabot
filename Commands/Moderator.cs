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
  public async Task DSay(CommandContext ctx, [RemainingText] string input = "")
  {
    // Check if message isn't empty
    if (input != "")
    {
      // Delete sent message
      await ctx.Message.DeleteAsync();
      // Check if a channel to post to was provided

      // Probably a Channel
      // ------------------
      if (input.StartsWith("<#"))
      {
        // Extract channel id
        string channel = input.Substring(0, input.IndexOf(">") + 1);

        // Trim channel from message
        string trimmedMessage = input.Replace(channel, string.Empty).Trim();

        // Get channel id
        string[] filter = new string[] { "<", ">", "#" };
        foreach (string c in filter)
        {
          channel = channel.Replace(c, string.Empty);
        }

        // Attempt to send message into channel
        try
        {
          DiscordChannel chan = await ctx.Client.GetChannelAsync(Convert.ToUInt64(channel));

          // Valid channel, post to designated channel
          await SayToChannel(chan, trimmedMessage);
        }
        // Okay, so it's not a channel, or something went wrong, just post the message string
        catch
        {
          await Say(ctx, input);
        }
      }
      // Not a Channel
      // -------------
      else
      {
        await Say(ctx, input);
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
    string channelParam = "--channel";
    string titleParam = "--title";
    string descriptionParam = "--content";

    // Get embed title
    string title = input.Substring(input.IndexOf(titleParam) + titleParam.Length).Trim();
    title = title.Substring(0, title.IndexOf(descriptionParam)).Trim();

    // Get embed content
    string description = input.Substring(input.IndexOf(descriptionParam) + descriptionParam.Length).Trim();

    // Get channel, if provided
    if (input.Substring(0, input.IndexOf(' ')) == channelParam)
    {
      // Extract channel id
      string channel = input.Substring(input.IndexOf(channelParam), input.IndexOf(">") + 1).Trim();
      Console.WriteLine(channel);
      channel = channel.Substring(input.IndexOf("<")).Trim();
      Console.WriteLine(channel);

      // Get channel id
      string[] filter = new string[] { "<", ">", "#" };
      foreach (string c in filter)
      {
        channel = channel.Replace(c, string.Empty);
      }

      // Attempt to send message into channel
      try
      {
        DiscordChannel chan = await ctx.Client.GetChannelAsync(Convert.ToUInt64(channel));

        // Valid channel, post to designated channel
        await SayEmbedToChannel(chan, title, description);
      }
      // Okay, so it's not a channel, or something went wrong, just post the message string
      catch
      {
        await SayEmbed(ctx, title, description);
      }
    }
    else
    {
    // Delete sent message
    await ctx.Message.DeleteAsync();

    // Send embed
    await SayEmbed(ctx, title, description);

    }
  }

  [Command("editsay"), RequireUserPermissions(DSharpPlus.Permissions.ModerateMembers)]
  public async Task DGetMessage(CommandContext ctx, [RemainingText] string input = "")
  {
    // https://discord.com/channels/1163322018498875468/1166546788845633586/1169797821449388083
    ulong channelId = Convert.ToUInt64(1166546788845633586);
    ulong messageId = Convert.ToUInt64(1169797821449388083);

    DiscordChannel chan = ctx.Channel.Guild.GetChannel(channelId);
    DiscordMessage msg = await chan.GetMessageAsync(messageId);
    Console.WriteLine(msg);
    Console.WriteLine(chan);
    
    await msg.ModifyAsync("edit me again pls");

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

  public async Task SayEmbed(CommandContext ctx, string title, string description)
  {
    SayEmbed em = new SayEmbed();
    await ctx.Channel.SendMessageAsync(em.SendSayEmbed(title, description));
  }

  public async Task SayEmbedToChannel(DiscordChannel chan, string title, string description)
  {
    SayEmbed em = new SayEmbed();
    await chan.SendMessageAsync(em.SendSayEmbed(title, description));
  }
  #endregion
}