public class BotMessage
{
  public string Title { get; private set; }
  public string Description {get; private set; }

  private BotMessage(
    string title, 
    string description
  ) {
    Title = title;
    Description = description;
  }

  // List of messages

  public class GenericInfo
  {
    public static BotMessage Greet
    {
      get
      {
        return new BotMessage(
          null,
          "👋 Hi!"
        );
      }
    }
  }

  public class GenericError
  {
    public static BotMessage InsufficientPermissions
    {
      get
      {
        return new BotMessage(
          null,
          "❌ User does not have sufficient permissions to run this command."
        );
      }
    }
  }
}