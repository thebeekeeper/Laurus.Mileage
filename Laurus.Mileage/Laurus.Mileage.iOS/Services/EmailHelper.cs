using Foundation;
using Laurus.Mileage.iOS.Services;
using Laurus.Mileage.Services;
using MessageUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(EmailHelper))]
namespace Laurus.Mileage.iOS.Services
{
   public class EmailHelper : IEmailHelper
   {
      public void SendEmail(string attachment)
      {
         MFMailComposeViewController mailController;
         if (MFMailComposeViewController.CanSendMail)
         {
            mailController = new MFMailComposeViewController();
            //mailController.SetToRecipients(new string[] { "thebeekeeper@gmail.com" });
                mailController.SetSubject("Mileage Report");
            mailController.SetMessageBody("Mileage report exported from MileageApp", false);

            var fileHelper = new FileHelper();
            var stream = fileHelper.ReadFileContents(attachment);
            mailController.AddAttachmentData(NSData.FromStream(stream), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "expense report.xlsx");

            mailController.Finished += (object sender, MFComposeResultEventArgs e) => 
            {
               // can set pass in an action to call when done
               //if (completed != null)
               //   completed(e.Result == MFMailComposeResult.Sent);
               e.Controller.DismissViewController(true, null);

                    // is this the right place to close this? not sure if it copies the stream over before this
                stream.Close();
            };

            // http://stackoverflow.com/questions/24136464/access-viewcontroller-in-dependencyservice-to-present-mfmailcomposeviewcontrolle
            //var rootController = ((AppDelegate)(UIApplication.SharedApplication.Delegate)).Window.RootViewController.ChildViewControllers[0].ChildViewControllers[1].ChildViewControllers[0];
            //var navcontroller = rootController as UINavigationController;
            //if (navcontroller != null)
            //   rootController = navcontroller.VisibleViewController;
            //rootController.PresentViewController(mailController, true, null);
            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(mailController, true, null);
         }
      }
   }
}