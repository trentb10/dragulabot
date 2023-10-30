using System.Text;
using DSharpPlus.Entities;

public class Embed
{
  public DiscordEmbedBuilder BuildEmbed(
    DiscordEmbedBuilder embed
  ) {
    embed.Build();

    return embed;
  }
}

public class GenericEmbed : Embed
{
  public DiscordEmbedBuilder SendGeneric(BotMessage msg)
  {
    DiscordEmbedBuilder embed = new DiscordEmbedBuilder
    {
      Description = msg.Description
    };

    return BuildEmbed(embed);
  }
}

public class PingEmbed : Embed
{
  public DiscordEmbedBuilder SendPing
  (
    string latency
  ){
    DiscordEmbedBuilder embed = new DiscordEmbedBuilder
    {
      Description = $"🛎️ Latency: **{latency}**ms"
    };

    return BuildEmbed(embed);
  }
}

public class SayEmbed : Embed
{
  public DiscordEmbedBuilder SendSayEmbed
  (
    string title,
    string description
  ){
    DiscordEmbedBuilder embed = new DiscordEmbedBuilder
    {
      Title = title,
      Description = description
    };

    return BuildEmbed(embed);
  }
}