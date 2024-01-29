// See https://aka.ms/new-console-template for more information


using ClubMembershipApplication;
using ClubMembershipApplication.Views;

IView mainView = Factory.GetMainViewObject();

mainView.RunView();


Console.ReadKey();
