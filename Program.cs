using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Exceptions;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using Newtonsoft.Json;

namespace dragulabot
{
  class Program
  {
    static void Main(string[] args)
    {
      MainAsync().GetAwaiter().GetResult();
      
    }

    internal static async Task MainAsync()
    {
      // Get discord key
      DiscordKeys keys = JsonConvert.DeserializeObject<DiscordKeys>
      (
        File.ReadAllText("config\\appsettings.json")
      );
      string discordKey = keys.discordKey.DragulaKey;

      var discord = new DiscordClient(new DiscordConfiguration()
      {
        Token = discordKey,
        TokenType = TokenType.Bot,
        Intents = DiscordIntents.All,

        // Set up logger
        MinimumLogLevel = Microsoft.Extensions.Logging.LogLevel.Information,
        LogTimestampFormat = "yyyy-MM-dd HH:mm:ss"
      });

      discord.GuildMemberAdded += async (s, e) =>
      {
        // Check if in AiMV server
        if (e.Guild.Id == 1163322018498875468)
        {
          // Get welcome channel
          DiscordChannel chan = e.Guild.GetChannel(1170024061724803183);
          string userid = e.Member.Id.ToString();
          await chan.SendMessageAsync($"Welcome to the Ai Music Videos Community, <@{userid}>!");
        }
      };

      // Load commands
      var commands = discord.UseCommandsNext(new CommandsNextConfiguration()
      {
        StringPrefixes = new[] { "d-" }
      });

      commands.RegisterCommands<General>();
      commands.RegisterCommands<Moderator>();
      
      commands.CommandErrored += CmdErroredHandler;

      await discord.ConnectAsync();
      await Task.Delay(-1);

    }

    static private async Task CmdErroredHandler(CommandsNextExtension _, CommandErrorEventArgs e)
    {
      var failedChecks = ((ChecksFailedException)e.Exception).FailedChecks;

      foreach (var fail in failedChecks)
      {
        if (fail is RequireUserPermissionsAttribute)
        {
          GenericEmbed em = new GenericEmbed();
          await e.Context.Channel.SendMessageAsync(
            em.SendGeneric(BotMessage.GenericError.InsufficientPermissions)
          );
        }
      }
    }

  }
}