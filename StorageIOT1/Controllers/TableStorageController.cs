using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Data.Tables;
using AzureIOT.Model;
using AzureIOT.Repositories;

namespace AzureIOT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableStorageController : ControllerBase
    {
        [HttpPost("AddTable")]
        public async Task<string> AddTable(string tableName)
        {
            await TableStorageRepository.AddTable(tableName);
            return null;
        }
        [HttpPost("UpdateTable")]
        public async Task<Details> Updatetable(Details employee,string tableName)
        {
            employee.PartitionKey = tableName;
            string Id = Guid.NewGuid().ToString();
            employee.Id = Id;
            employee.RowKey = Id;
            employee.Timestamp = DateTime.Now;
            var data = await TableStorageRepository.Updatetable(employee, tableName);
            return data;
        }
        [HttpGet("GetTableData")]
        public async Task<Details> GetTableData(string tableName,string partitionKey, string id)
        {
            var data = await TableStorageRepository.GetTableData(tableName, partitionKey, id);
            return data;
        }

        [HttpGet("GetTable")]
        public TableClient GetTable(string tableName)
        {
            var data = TableStorageRepository.GetTable(tableName);
            return data;
        }
        [HttpGet("GetAllTableData")]
        public async Task<IEnumerable<Details>> GetAllTableData(string tableName)
        {
            var data = await TableStorageRepository.GetAllTableData(tableName);
            return data;
        }


        [HttpDelete("DeleteTableData")]
        public async Task DeleteTableData(string tableName, string partitionKey, string id)
        {
            await TableStorageRepository.DeleteTableData(tableName, partitionKey, id);
            return;
        }


    }
}
