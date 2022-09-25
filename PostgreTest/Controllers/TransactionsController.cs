using System.Transactions;

using Microsoft.AspNetCore.Mvc;
using PostgreTest.Data;
using PostgreTest.Data.Entities;
using PostgreTest.ViewModels;

[ApiController]
[Route("[controller]")]
public class TransactionsController: ControllerBase {
    private readonly TransactionsDbContext _db;

    public TransactionsController(TransactionsDbContext context)
    {
        _db = context;        
    }



    [HttpGet]
    [Route("getProductsByHighPrice/{price:decimal}")]
    /*https://localhost:7252/Transactions/getProductsByHighPrice/35.0*/
    public ActionResult<IEnumerable<Product>> GetProductsByHighPrice(decimal? price){
        var response = _db.Products.Where(product => product.Price <= price && product.Price != 0);
        return response.ToList();
    }

    [HttpGet]
    [Route("clientsOnOneStreet")]
    /*https://localhost:7252/Transactions/clientsOnOneStreet*/
    public ActionResult<IEnumerable<Client>> GetClientsByStreet([FromForm]StreetViewModel addressModel){

        var addresses = _db.LegalAddresses.Where(a=> a.Country == addressModel.Country
                            && a.City == addressModel.City 
                            && a.Street == addressModel.Street).ToList();
        var clients = new List<Client>();

        foreach(var i in addresses){
            clients.Add(_db.Clients.Where(client => client.Id == i.ClientId).FirstOrDefault());
        }

        return Ok(clients);
    }

    [HttpGet]
    [Route("transactions")]
    /*https://localhost:7252/Transactions/transactions*/
    public ActionResult<IEnumerable<TransactionsViewModel>> GetTransactions(){

        var result = new System.Collections.Generic.List<TransactionsViewModel>();

        
        var collection  = _db.Transactions.ToList();
        foreach(var i in collection){
            
            //Шукаємо ім'я клієнта

            var tmpValue = new TransactionsViewModel();
            tmpValue.ClientName = _db.Clients.Where(cl => cl.Id == i.ClientId).FirstOrDefault().Name;
            
            //Шукаємо продукт за для визначення ціни за одиницю  товару
            
            var product = _db.Products.Where(t => t.Id == i.ProductId).FirstOrDefault();
        
            tmpValue.ProductName = product.Name;
            
            var productPriceByOneUnit = product.Price;
            
            tmpValue.DateTransaction = i.Date;
            tmpValue.Price = i.CountOfProduct * productPriceByOneUnit;

            result.Add(tmpValue);
        }
        result = result.OrderByDescending(c => c.DateTransaction).ToList();

        return Ok(result);
    }

    [HttpGet]
    [Route("productWithMaxPrice")]
    /*https://localhost:7252/Transactions/productWithMaxPrice*/
    public ActionResult GetProductWithMaxPrice(){
        var maxPrice = _db.Products.Max(product => product.Price);
        var response = _db.Products.Where(p=> p.Price == maxPrice).FirstOrDefault();
        return Ok(response);
    }



}