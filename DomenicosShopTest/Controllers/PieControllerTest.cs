using DomenicoPieShop_Empty.Controllers;
using DomenicoPieShop_Empty.ViewModels;
using DomenicosShopTest.Mocks;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomenicosShopTest.Controllers
{
    public  class PieControllerTest
    {
        [Fact]//diventa un metodo uni test
        public void ReturnAllPies()
        {
            //arrange
            var pieRepMock = RepositoryMocks.GetPieRepository();
            var catRepoMock = RepositoryMocks.GetCategoryRepository();
            var pieController = new PieController(pieRepMock.Object, catRepoMock.Object);

            var result = pieController.List("");

            //assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var pieListViewModel = Assert.IsAssignableFrom<PieListViewModel>(viewResult.ViewData.Model);
            Assert.Equal(10, pieListViewModel.Pies.Count());
        }
    }
}
