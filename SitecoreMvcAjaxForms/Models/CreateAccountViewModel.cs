
namespace SitecoreMvcAjaxForms.Models
{
    public class CreateAccountViewModel : ViewModelBase
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}