using Foundation;
using System;
using UIKit;

namespace Lab06
{
    public partial class ValidatorController : UIViewController
    {
        public ValidatorController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            VerifyButton.TouchUpInside += (object sender, EventArgs e) =>
            {
                Validate(EmailText.Text, PasswordText.Text);
            };
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        async private void Validate(string email, string password)
        {
            var Client = new SALLab06.ServiceClient();
            var result = await Client.ValidateAsync(email, password, this);

            var Alert = UIAlertController.Create("Resultado ",
                $"{result.Status}\n{result.FullName}\n{result.Token}",
                UIAlertControllerStyle.Alert);

            Alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));

            PresentViewController(Alert, true, null);
        }
    }
}