using DomenicoPieShop_Empty.Models;

namespace DomenicoPieShop_Empty.ViewModels
{
    public class HomeViewModel
    {
        public HomeViewModel(IEnumerable<Pie> piesOfTheWeek)
        {
            PiesOfTheWeek = piesOfTheWeek;
        }

        public IEnumerable<Pie> PiesOfTheWeek { get; }

    }
}
