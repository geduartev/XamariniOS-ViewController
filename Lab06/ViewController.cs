using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace Lab06
{
    public partial class ViewController : UIViewController
    {
        public List<string> PhoneNumbers = new List<string>();

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            string TranslatedNumber = string.Empty;

            TranslateButton.TouchUpInside += (object sender, EventArgs e) =>
            {
                PhoneNumberText.ResignFirstResponder();

                var Translator = new PhoneTranslator();
                TranslatedNumber = Translator.ToNumber(PhoneNumberText.Text);

                if (string.IsNullOrWhiteSpace(TranslatedNumber))
                {
                    CallButton.SetTitle("Llamar", UIControlState.Normal);
                    CallButton.Enabled = false;
                }
                else
                {
                    CallButton.SetTitle($"Llamar al {TranslatedNumber}", UIControlState.Normal);
                    CallButton.Enabled = true;
                    PhoneNumbers.Add(TranslatedNumber);
                }
            };

            CallButton.TouchUpInside += (object sender, EventArgs e) =>
            {
                PhoneNumberText.ResignFirstResponder();

                var URL = new Foundation.NSUrl($"tel:{TranslatedNumber}");
                if (!UIApplication.SharedApplication.OpenUrl(URL))
                {
                    var Alert = UIAlertController.Create("No soportado",
                        "El esquema 'tel:' no es soportado en este dispositivo",
                        UIAlertControllerStyle.Alert);
                    Alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
                    PresentViewController(Alert, true, null);
                }
            };

            VerifyButton.TouchUpInside += (object sender, EventArgs e) =>
            {
                if (this.Storyboard.InstantiateViewController("ValidatorController") is ValidatorController Controller)
                {
                    this.NavigationController.PushViewController(Controller, true);
                }
            };

            CallHistoryButton.TouchUpInside += (object sender, EventArgs e) =>
            {
                if (this.Storyboard.InstantiateViewController("CallHistoryController") is CallHistoryController Controller)
                {
                    Controller.PhoneNumbers = PhoneNumbers;
                    this.NavigationController.PushViewController(Controller, true);
                }
            };
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}