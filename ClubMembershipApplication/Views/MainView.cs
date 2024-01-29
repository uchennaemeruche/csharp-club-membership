using System;
using ClubMembershipApplication.FieldValidators;

namespace ClubMembershipApplication.Views
{
    public class MainView : IView
    {
        public IFieldValidator? FieldValidator => null;

        IView _registerView;
        IView _loginView;

        public MainView(IView registerView, IView loginView)
        {
            _registerView = registerView;
            _loginView = loginView;
        }

        public void RunView()
        {
            CommonOutputText.WriteMainHeading();

            Console.WriteLine("Please press 'l' to login or 'r' to register new user");
            ConsoleKey option = Console.ReadKey().Key;

            if(option == ConsoleKey.R)
            {

                RunRegistrationView();
                RunLoginView();

            }
            else if(option == ConsoleKey.L) {
                RunLoginView();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Goodbye");
                Console.ReadKey();
            }

        }

        private void RunRegistrationView()
        {
            _registerView.RunView();
        }

        private void RunLoginView()
        {
            _loginView.RunView();
        }


    }
}

