using FreshMvvm;
using Notes.Mobile.Api;
using Notes.Mobile.Forms.Notes;
using PropertyChanged;
using Xamarin.Forms;

namespace Notes.Mobile.Forms.Login
{
    [ImplementPropertyChanged]
    public class LoginPageModel : FreshBasePageModel
    {
        private IAccountService _accountService;

        public LoginPageModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public string AccountName { get; set; }
        public string Password { get; set; }

        public Command Login
        {
            get
            {
                return new Command(async () =>
                {
                    var result = await _accountService.Login(AccountName, Password);
                    await CoreMethods.PopPageModel();
                });
            }
        }
    }
}