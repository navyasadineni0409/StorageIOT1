using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Data.Tables;
using AzureIOT.Model;
using Microsoft.AspNetCore.Mvc;
using AzureIOT.Repositories;

namespace AzureIOT.Repositories
{
    public class TableStorageRepository
    {
        static string connectionString = "DefaultEndpointsProtocol=https;AccountName=storagesn230113;AccountKey=hec6ImTmJwRF49x9QhdWRPiMxiy9x+P9q2UPPUdqxCrlaod8YRGqkN9m6qsCvr9oS0alkRgjN9Cg+AStlod6Wg==;EndpointSuffix=core.windows.net";
        public static async Task AddTable(string tableName)
        {
            var data = new TableServiceClient(connectionString);
            var client = data.GetTableClient(tableName);
            await client.CreateIfNotExistsAsync();
        }
        public static async Task<Details> Updatetable(Details employee, string tableName)
        {
            var data = new TableServiceClient(connectionString);
            var client = data.GetTableClient(tableName);
            await client.UpsertEntityAsync(employee);
            return employee;
        }
        public static async Task<Details> GetTableData(string tableName, string partitionKey, string id)
        {
            var data = new TableServiceClient(connectionString);
            var client = data.GetTableClient(tableName);
            var tableData = await client.GetEntityAsync<Details>(partitionKey, id);
            return tableData;
        }
        public static TableClient GetTable(string tableName)
        {
            var data = new TableServiceClient(connectionString);
            var client = data.GetTableClient(tableName);
            return client;
        }
        public static async Task<IEnumerable<Details> >GetAllTableData(string tableName)
        {
              var _tableClient = GetTable(tableName);
        IList<Details> modelList = new List<Details>();
        var data = _tableClient.QueryAsync<Details>(filter: "", maxPerPage: 10);
        await foreach(var rec in data){
            modelList.Add(rec);
         }
           return  modelList;
}

        public static async Task DeleteTableData(string tableName, string department, string id)
        {
            var data = new TableServiceClient(connectionString);
            var client = data.GetTableClient(tableName);
            await client.DeleteEntityAsync(department, id);
            return;
        }
    }
}
