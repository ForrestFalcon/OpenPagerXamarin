using System;
using System.IO;
using OpenPager.Models;
using OpenPager.Services;
using Xamarin.Forms;
using OpenPager.Views;
using SQLite;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace OpenPager
{
	public partial class App : Application
	{
	    private static SQLiteAsyncConnection database;

        public static SQLiteAsyncConnection Database
	    {
	        get
	        {
	            if (database == null)
	            {
	                database = new SQLiteAsyncConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "database.db3"));
	            }
	            return database;
	        }
	    }

        public App ()
		{
            DependencyService.Register<IDataStore<Operation>, OperationDataStore>();

            InitializeComponent();
			MainPage = new NavigationPage(new OperationsPage());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
