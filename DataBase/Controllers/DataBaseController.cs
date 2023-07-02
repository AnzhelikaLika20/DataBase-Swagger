using DataBaseLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Xml.Linq;

namespace DataBase.Controllers
{
    [Route("/api/")]
    public class DataBaseController : Controller
    {

        public DataBaseController(IDataBase database)
        {
        }
        /// <summary>
        /// Enter Good(Sale/Buyer/Shop) to create a table of type Good(Sale/Buyer/Shop).
        /// </summary>
        /// <param name="type">Type of the table.</param>
        /// <returns>Created table of type T.</returns>
        [HttpPost("CreateTable{type}")]
        public IActionResult CreateTable(string type)
        {
            try
            {
                if (type == "Good")
                    DataAccessLayer._dataBase.CreateTable<Good>();
                else if (type == "Buyer")
                    DataAccessLayer._dataBase.CreateTable<Buyer>();
                else if (type == "Sale")
                    DataAccessLayer._dataBase.CreateTable<Sale>();
                else if (type == "Shop")
                    DataAccessLayer._dataBase.CreateTable<Shop>();
                else
                    return BadRequest("Incorrect input");
                return Ok();
            }
            catch (DataBaseException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Adding a good to the database.
        /// </summary>
        /// <param name="name">Name of a good.</param>
        /// <param name="shopId">The id of a shop.</param>
        /// <param name="category">The category of a good.</param>
        /// <param name="price">The price of a good.</param>
        /// <returns>An added object of type Good</returns>
        [HttpPost("AddGood/{name}/{shopId}/{category}/{price}")]
        public IActionResult AddGood(string name, int shopId, string category, long price)
        {
            try
            {
                Good good = new Good(name, shopId, category, price);
                DataAccessLayer._dataBase.InsertInto(() => good);
                return Ok(good);
            }
            catch (DataBaseException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Adding a buyer to the database.
        /// </summary>
        /// <param name="name">The Name of a buyer.</param>
        /// <param name="surname">The surname of the buyer.</param>
        /// <param name="city">The buyer's city of residence.</param>
        /// <param name="country">The buyer's country of residence.</param>
        /// <returns>An added object of type buyer.</returns>
        [HttpPost("AddBuyer{name}/{surname}/{city}/{country}")]
        public IActionResult AddBuyer(string name, string surname, string city, string country)
        {
            try
            {
                Buyer buyer = new Buyer(name, surname, city, country);
                DataAccessLayer._dataBase.InsertInto(() => buyer);
                return Ok(buyer);
            }
            catch (DataBaseException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Adding a sale to the database.
        /// </summary>
        /// <param name="buyerId">The id of a buyer.</param>
        /// <param name="shopId">The id of a shop.</param>
        /// <param name="goodId">The id of a good.</param>
        /// <param name="goodCount"></param>
        /// <returns>An added object of type Sale.</returns>
        [HttpPost("AddSale{buyerId}/{shopId}/{goodId}/{goodCount}")]
        public IActionResult AddSale(int buyerId, int shopId, int goodId, long goodCount)
        {
            try
            {
                Sale sale = new Sale(buyerId, shopId, goodId, goodCount);
                DataAccessLayer._dataBase.InsertInto(() => sale);
                return Ok(sale);
            }
            catch (DataBaseException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Adding a shop to the database.
        /// </summary>
        /// <param name="name">The name of a shop.</param>
        /// <param name="city">The city of a shop.</param>
        /// <param name="country">The country of a shop.</param>
        /// <returns>An added object of type Shop.</returns>
        [HttpPost("AddShop{name}/{city}/{country}")]
        public IActionResult AddShop(string name, string city, string country)
        {
            try
            {
                Shop shop = new Shop(name, city, country);
                DataAccessLayer._dataBase.InsertInto(() => shop);
                return Ok(shop);
            }
            catch (DataBaseException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Enter Good(Sale/Buyer/Shop) to get a table of type Good(Sale/Buyer/Shop).
        /// </summary>
        /// <param name="type">A type of a table.</param>
        /// <returns>The table of type T.</returns>
        [HttpGet("Get{type}")]
        public IActionResult GetTable(string type)
        {
            try
            {
                if (type == "Good")
                    return Ok(DataAccessLayer._dataBase.GetTable<Good>());
                if (type == "Buyer")
                    return Ok(DataAccessLayer._dataBase.GetTable<Buyer>());
                if (type == "Sale")
                    return Ok(DataAccessLayer._dataBase.GetTable<Sale>());
                if (type == "Shop")
                    return Ok(DataAccessLayer._dataBase.GetTable<Shop>());
                return BadRequest("Incorrect input");
            }
            catch (DataBaseException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Serializing a table of type T to a file with the path location.
        /// </summary>
        /// <param name="type">A type of a table.</param>
        /// <param name="path">A path of file for serialized data.</param>
        /// <returns>Verdict of serialization.</returns>
        [HttpGet("SerializeTable{type}/{path}")]
        public IActionResult SerializeTable(string type, string path)
        {
            try
            {
                if (type == "Good")
                {
                    DataAccessLayer._dataBase.Serialize<Good>(path);
                    return Ok("JSON-serialization completed successfully!");
                }
                if (type == "Shop")
                {
                    DataAccessLayer._dataBase.Serialize<Shop>(path);
                    return Ok("JSON-serialization completed successfully!");
                }
                if (type == "Sale")
                {
                    DataAccessLayer._dataBase.Serialize<Sale>(path);
                    return Ok("JSON-serialization completed successfully!");
                }
                if (type == "Buyer")
                {
                    DataAccessLayer._dataBase.Serialize<Buyer>(path);
                    return Ok("JSON-serialization completed successfully!");
                }
                return BadRequest("Incorrect input");
            }
            catch (DataBaseException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Deserializing a table with type T to from the file with the path location.
        /// </summary>
        /// <param name="type">A type of a table.</param>
        /// <param name="path">A path of file with json presentation of data.</param>
        /// <returns>Verdict of deserialization.</returns>
        [HttpPut("DeserializeTable{type}/{path}")]
        public IActionResult DeserializeTable(string type, string path)
        {
            try
            {
                if (type == "Good")
                {
                    DataAccessLayer._dataBase.Deserialize<Good>(path);
                    return Ok("JSON-deserialization completed successfully!");
                }
                if (type == "Shop")
                {
                    DataAccessLayer._dataBase.Deserialize<Shop>(path);
                    return Ok("JSON-deserialization completed successfully!");
                }
                if (type == "Sale")
                {
                    DataAccessLayer._dataBase.Deserialize<Sale>(path);
                    return Ok("JSON-deserialization completed successfully!");
                }
                if (type == "Buyer")
                {
                    DataAccessLayer._dataBase.Deserialize<Buyer>(path);
                    return Ok("JSON-deserialization completed successfully!");
                }
                return BadRequest("Incorrect input");
            }
            catch (DataBaseException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
