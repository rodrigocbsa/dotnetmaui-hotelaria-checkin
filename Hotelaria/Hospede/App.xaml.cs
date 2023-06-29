using System.Diagnostics;

namespace Hospede
{
    public partial class App : Application
    {
        public App()
        {
            try
            {
                InitializeComponent();

                MainPage = new AppShell();
            }catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            
        }
    }
}