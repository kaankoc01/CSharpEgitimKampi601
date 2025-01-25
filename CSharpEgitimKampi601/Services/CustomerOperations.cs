using System.Collections.Generic;
using CSharpEgitimKampi601.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CSharpEgitimKampi601.Services
{
    public class CustomerOperations
    {
        public void AddCustomer(Customer customer)
        {
            var collection = new MongoDbConnection();
            var customersCollection = collection.GetCustomersCollection();

            var document = new BsonDocument
            {
                {"CustomerName",customer.CustomerName},
                {"CustomerSurname",customer.CustomerSurname},
                {"CustomerCity",customer.CustomerCity},
                {"CustomerBalance",customer.CustomerBalance},
                {"CustomerShoppingCount",customer.CustomerShoppingCount}
            };
            customersCollection.InsertOne(document);
        }

        public List<Customer> GetAllCustomers()
        {
            var connection = new MongoDbConnection(); //veritabanı bağlantsı    
            var customersCollection = connection.GetCustomersCollection(); // customers collectiona bağlandı.
            var customers = customersCollection.Find(new BsonDocument()).ToList();
            var customerList = new List<Customer>();
            foreach (var customer in customers)
            {
                customerList.Add(new Customer
                {
                    CustomerId = customer["_id"].ToString(),
                    CustomerName = customer["CustomerName"].ToString(),
                    CustomerSurname = customer["CustomerSurname"].ToString(),
                    CustomerCity = customer["CustomerCity"].ToString(),
                    CustomerBalance = customer["CustomerBalance"].ToString(),
                    CustomerShoppingCount = customer["CustomerShoppingCount"].AsInt32
                });
            }
            return customerList;
        }

        public void DeleteCustomer(string id)
        {
            var connection = new MongoDbConnection();
            var customersCollection = connection.GetCustomersCollection();
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
            customersCollection.DeleteOne(filter);

        }

        public void UpdateCustomer(Customer customer)
        {
            var connection = new MongoDbConnection();
            var customersCollection = connection.GetCustomersCollection();
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(customer.CustomerId));
            var updatedValue = Builders<BsonDocument>.Update
                .Set("CustomerName", customer.CustomerName)
                .Set("CustomerSurname", customer.CustomerSurname)
                .Set("CustomerCity", customer.CustomerCity)
                .Set("CustomerBalance", customer.CustomerBalance)
                .Set("CustomerShoppingCount", customer.CustomerShoppingCount);
            customersCollection.UpdateOne(filter, updatedValue);

        }

        public Customer GetCustomerById(string id)
        {
            var connection = new MongoDbConnection();
            var customersCollection = connection.GetCustomersCollection();
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
            var customer = customersCollection.Find(filter).FirstOrDefault();
            return new Customer
            {
                CustomerId = customer["_id"].ToString(),
                CustomerName = customer["CustomerName"].ToString(),
                CustomerSurname = customer["CustomerSurname"].ToString(),
                CustomerCity = customer["CustomerCity"].ToString(),
                CustomerBalance = customer["CustomerBalance"].ToString(),
                CustomerShoppingCount = customer["CustomerShoppingCount"].AsInt32
            };
        }
    }
}

