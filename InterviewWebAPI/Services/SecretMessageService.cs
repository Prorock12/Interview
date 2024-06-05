namespace InterviewWebAPI.Services
{
    public interface ISecretMessageService
    {
        public string SecretMessage();
    }
    public class SecretMessageService: ISecretMessageService
    {
        public string SecretMessage()
        {
            return "Welcome to the Hell";
        }
    }
}
