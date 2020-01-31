namespace MVC.Email.EmailSettings
{
    public interface IEmailConfiguration
    {
        string PopPassword { get; set; }
        int PopPort { get; set; }
        string PopServer { get; set; }
        string PopUsername { get; set; }
    }
}