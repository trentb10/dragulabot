using System.Text;
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
    if (message != "")
    {
      await ctx.Channel.SendMessageAsync(message);
    } 
    else
    {
      // just discard message mayb
    }
  }
}