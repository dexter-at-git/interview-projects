using System;
using System.Threading.Tasks;
using BeerManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeerManager.Controllers
{
    [Route("api/[controller]")]
    public class BeerManagerController : Controller
    {
        private readonly IBeerManagerProxyService _beerManagerProxyService;

        public BeerManagerController(IBeerManagerProxyService beerManagerProxyService)
        {
            _beerManagerProxyService = beerManagerProxyService;
        }

        [AllowAnonymous]
        [Route("beers")]
        [HttpGet]
        public async Task<IActionResult> GetBeers(string order, bool reverse, string name, int page)
        {
            try
            {
                var beerList = await _beerManagerProxyService.GetBeers(order, reverse, name, page);
                return this.Ok(beerList);
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, ex.Message);
            }
        }

    }
}
