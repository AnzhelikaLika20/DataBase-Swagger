namespace DataBaseLibrary;

public class DataAccessLayer : IDataAccessLayer
{
    public static DataBase _dataBase = new();
    public static DataAccessLayer _accessLayer = new();
    public DataAccessLayer(){ }
    /// <summary>
    /// Getting a list of all products purchased by the buyer with the longest name.
    /// </summary>
    /// <param name="dataBase">Database that supports Good, Sale, Shop and Buyer tables.</param>
    /// <returns>A list of all products purchased by the buyer with the longest name.</returns>
    public IEnumerable<Good> GetAllGoodsOfLongestNameBuyer(IDataBase dataBase)
    {
        var sales = dataBase.GetTable<Sale>();
        var goods = dataBase.GetTable<Good>();
        var buyers = dataBase.GetTable<Buyer>();

        var listGoods = from good in goods
            where (from sale in sales
                where (sale.BuyerId == (from buyer in buyers
                    where buyer.Name.Length == (buyers.Max(x => x.Name.Length))
                    orderby buyer.Name descending
                    select buyer).ToList()[0].Id)
                select sale.GoodId).Contains(good.Id)
            select good;

        return listGoods;
    }
    /// <summary>
    /// Getting the name of the category of the most expensive product.
    /// </summary>
    /// <param name="dataBase">Database that supports Good, Sale, Shop and Buyer tables.</param>
    /// <returns>The name of the category of the most expensive product.</returns>
    public string? GetMostExpensiveGoodCategory(IDataBase dataBase)
    {
        var goods = dataBase.GetTable<Good>();

        var category = (from good in goods
            where good.Price == goods.Max(x => x.Price)
            select good.Category).First();

        return category;
    }
    /// <summary>
    /// Getting the name of the city where the least money was spent.
    /// </summary>
    /// <param name="dataBase">Database that supports Good, Sale, Shop and Buyer tables.</param>
    /// <returns>The name of the city where the least money was spent.</returns>
    public string? GetMinimumSalesCity(IDataBase dataBase)
    {
        var sales = dataBase.GetTable<Sale>();
        var goods = dataBase.GetTable<Good>();
        var shops = dataBase.GetTable<Shop>();

        var sumValuesByShopId = from sale in sales
            group sale.GoodCount * (from good in goods
                where good.Id == sale.GoodId
                select good.Price).SingleOrDefault() by sale.ShopId
            into shopIdAndSum
            select new { shopIdAndSum.Key, sum = shopIdAndSum.Sum() };

        var cityWithMinValue = (from shop in shops
            from sumValue in sumValuesByShopId
            where sumValue.Key == shop.Id
            group sumValue.sum by shop.City
            into cityWithValues
            select new { cityWithValues.Key, value = cityWithValues.Sum() }).MinBy(x => x.value);

        return cityWithMinValue!.Key;
    }
    /// <summary>
    /// Getting a list of buyers who bought the most popular product.
    /// </summary>
    /// <param name="dataBase">Database that supports Good, Sale, Shop and Buyer tables.</param>
    /// <returns>A list of buyers who bought the most popular product.</returns>
    public IEnumerable<Buyer> GetMostPopularGoodBuyers(IDataBase dataBase)
    {
        var sales = dataBase.GetTable<Sale>();
        var buyers = dataBase.GetTable<Buyer>();

        var goodCount = from groupedGoods in (from sale in sales
                group sale.GoodCount by sale.GoodId)
            select (groupedGoods.Key, groupedGoods.Sum());

        var people = from buyer in buyers
            where (from sale in sales
                where sale.GoodId == (from count in goodCount
                    where count.Item2 == goodCount.Max(x => x.Item2)
                    select count.Key).First()
                select sale.BuyerId).Contains(buyer.Id)
            select buyer;

        return people;
    }
    /// <summary>
    /// Determining the number of stores for each country and getting the smallest value.
    /// </summary>
    /// <param name="dataBase">Database that supports Good, Sale, Shop and Buyer tables.</param>
    /// <returns>The number of stores for each country and getting the smallest value.</returns>
    public int GetMinimumNumberOfShopsInCountry(IDataBase dataBase)
    {
        var shops = dataBase.GetTable<Shop>();

        var minCountOfShops = (from countryAndShops in (from shop in shops
                group shop by shop.Country)
            select (countryAndShops.Key, countryAndShops.Count())).Min(x => x.Item2);

        return minCountOfShops;
    }
    /// <summary>
    /// Getting a list of purchases made by customers in all cities other than their city of residence.
    /// </summary>
    /// <param name="dataBase">Database that supports Good, Sale, Shop and Buyer tables.</param>
    /// <returns>A list of purchases made by customers in all cities other than their city of residence.</returns>
    public IEnumerable<Sale> GetOtherCitySales(IDataBase dataBase)
    {
        var sales = dataBase.GetTable<Sale>();
        var shops = dataBase.GetTable<Shop>();
        var buyers = dataBase.GetTable<Buyer>();

        var salesList = from sale in sales
            where (from shop in shops
                where shop.Id == sale.ShopId
                select shop.City).SingleOrDefault() != (from buyer in buyers
                where buyer.Id == sale.BuyerId
                select buyer.City).SingleOrDefault()
            select sale;


        return salesList;
    }
    /// <summary>
    /// Getting the total cost of purchases made by all customers.
    /// </summary>
    /// <param name="dataBase">Database that supports Good, Sale, Shop and Buyer tables.</param>
    /// <returns>The total cost of purchases made by all customers.</returns>
    public long GetTotalSalesValue(IDataBase dataBase)
    {
        var sales = dataBase.GetTable<Sale>();
        var goods = dataBase.GetTable<Good>();

        var totalSumSales = (from sale in sales
            select sale.GoodCount * (from good in goods
                where good.Id == sale.GoodId
                select good.Price).SingleOrDefault()).Sum();

        return totalSumSales;
    }
}