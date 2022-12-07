using System.Windows.Controls;

namespace TP2_14E_A2022.Pages
{
    public static class PageExtensions
    {
        public static void GoToMenuPage(this Page page)
        {
            GoToPage<PageMenu>(page);
        }

        public static void GoToConnectionPage(this Page page)
        {
            GoToPage<PageConnection>(page);
        }

        public static void GoToPage<TPage>(this Page page) where TPage : Page, new()
        {
            TPage newPage = new TPage();
            page.NavigationService.Navigate(newPage);
        }
    }
}
