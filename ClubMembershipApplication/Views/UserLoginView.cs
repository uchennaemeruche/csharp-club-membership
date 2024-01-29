using System;
using ClubMembershipApplication.Data;
using ClubMembershipApplication.FieldValidators;
using ClubMembershipApplication.Models;

namespace ClubMembershipApplication.Views
{
    public class UserLoginView : IView

    {

        ILogin _loginUser;

        public IFieldValidator FieldValidator => null;

        public UserLoginView(ILogin login)
        {
            _loginUser = login;

        }

        public void RunView()
        {
            CommonOutputText.WriteMainHeading();

            CommonOutputText.WriteRLoginHeading();

            Console.Write("Please enter your email address: ");
            string emailAddress = Console.ReadLine()!;

            Console.Write("Please enter your password:");
            string password = Console.ReadLine()!;

            User user = _loginUser.Login(emailAddress, password);

            if(user != null)
            {
                WelcomeUserView welcomeUserView = new WelcomeUserView(user);
                welcomeUserView.RunView();
            }
            else
            {
                Console.Clear();
                CommonOutputFormat.ChangeFontColor(FontTheme.Danger);
                Console.WriteLine("Invalid login credential");
                CommonOutputFormat.ChangeFontColor(FontTheme.Default);
                Console.ReadKey();
            }
        }
    }
}

