using DomenicoPieShop_Empty.Models;
using Microsoft.AspNetCore.Components;

namespace DomenicoPieShop_Empty.Pages.App
{
    public partial class PieCard
    {
        [Parameter]
        public Pie? Pie { get; set; }
    }
}
