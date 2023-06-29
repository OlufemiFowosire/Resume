using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Text;

namespace Bitmacs.Function
{
    public static class GetVisitorCount
    {

        [FunctionName("GetVisitorCount")]
        public static HttpResponseMessage Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req,
            [CosmosDB(databaseName: "resume-db", collectionName: "count-container", ConnectionStringSetting = "cosmosDBConnection", Id = "1", PartitionKey = "1")] Counter counter,
            [CosmosDB(databaseName: "resume-db", collectionName: "count-container", ConnectionStringSetting = "cosmosDBConnection", Id = "1", PartitionKey = "1")] out Counter updatedCounter,
            ILogger log
        ){
            // To update the site visit count number
            updatedCounter = counter;
            updatedCounter.Count += 1;
            
            log.LogInformation("C# HTTP trigger function processed a request.");

            var jsonToReturn = JsonConvert.SerializeObject(counter);
            
            return new HttpResponseMessage(HttpStatusCode.OK){
                Content = new StringContent(jsonToReturn, Encoding.UTF8, "application/json")
            };
        }
    }
}
