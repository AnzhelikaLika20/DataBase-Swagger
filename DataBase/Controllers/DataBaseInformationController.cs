using DataBaseLibrary;
using Microsoft.AspNetCore.Mvc;

namespace DataBase.Controllers;

public class DataBaseInformationController : Controller
{
    /// <summary>
        /// Getting a list of all products purchased by the buyer with the longest name.
        /// </summary>
        /// <returns>A list of all products purchased by the buyer with the longest name.</returns>
        [HttpGet("GetAllGoodsOfLongestNameBuyer")]
        public IActionResult GetAllGoodsOfLongestNameBuyer()
        {
            try
            {
                return Ok(DataAccessLayer._accessLayer.GetAllGoodsOfLongestNameBuyer(DataAccessLayer._dataBase));
            }
            catch
            {
                return Ok(Enumerable.Empty<Good>());
            }
        }
        /// <summary>
        /// Getting the name of the category of the most expensive product.
        /// </summary>
        /// <returns>The name of the category of the most expensive product.</returns>
        [HttpGet("GetMostExpensiveGoodCategory")]
        public IActionResult GetMostExpensiveGoodCategory()
        {
            try
            {
                return Ok(DataAccessLayer._accessLayer.GetMostExpensiveGoodCategory(DataAccessLayer._dataBase));
            }
            catch
            {
                return Ok(String.Empty);
            }
        }
        /// <summary>
        /// Getting the name of the city where the least money was spent.
        /// </summary>
        /// <returns>The name of the city where the least money was spent.</returns>
        [HttpGet("GetMinimumSalesCity")]
        public IActionResult GetMinimumSalesCity()
        {
            try
            {
                return Ok(DataAccessLayer._accessLayer.GetMinimumSalesCity(DataAccessLayer._dataBase));
            }
            catch
            {
                return Ok(String.Empty);
            }
        }
        /// <summary>
        /// Getting a list of buyers who bought the most popular product.
        /// </summary>
        /// <returns>A list of buyers who bought the most popular product.</returns>
        [HttpGet("GetMostPopularGoodBuyers")]
        public IActionResult GetMostPopularGoodBuyers()
        {
            try
            {
                return Ok(DataAccessLayer._accessLayer.GetMostPopularGoodBuyers(DataAccessLayer._dataBase));
            }
            catch
            {
                return Ok(Enumerable.Empty<Buyer>());
            }
        }
        /// <summary>
        /// Determining the number of stores for each country and getting the smallest value.
        /// </summary>
        /// <returns>The number of stores for each country and getting the smallest value.</returns>
        [HttpGet("GetMinimumNumberOfShopsInCountry")]
        public IActionResult GetMinimumNumberOfShopsInCountry()
        {
            try
            {
                return Ok(DataAccessLayer._accessLayer.GetMinimumNumberOfShopsInCountry(DataAccessLayer._dataBase));
            }
            catch
            {
                return Ok(0);
            }
        }
        /// <summary>
        /// Getting a list of purchases made by customers in all cities other than their city of residence.
        /// </summary>
        /// <returns>A list of purchases made by customers in all cities other than their city of residence.</returns>
        [HttpGet("GetOtherCitySales")]
        public IActionResult GetOtherCitySales()
        {
            try
            {
                return Ok(DataAccessLayer._accessLayer.GetOtherCitySales(DataAccessLayer._dataBase));
            }
            catch
            {
                return Ok(Enumerable.Empty<Sale>());
            }
        }
        /// <summary>
        /// Getting the total cost of purchases made by all customers.
        /// </summary>
        /// <returns>The total cost of purchases made by all customers.</returns>
        [HttpGet("GetTotalSalesValue")]
        public IActionResult GetTotalSalesValue()
        {
            try
            {
                return Ok(DataAccessLayer._accessLayer.GetTotalSalesValue(DataAccessLayer._dataBase));
            }
            catch
            {
                return Ok(0);
            }
        }
}