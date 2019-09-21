namespace DEV.API.App.Domain.Core.Command
{
    public class CommandResponse
    {
        public CommandResponse(bool success = false)
        {
            Success = success;
        }

        public bool Success { get; private set; }
    }
}
