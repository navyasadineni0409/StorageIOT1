using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using AzureIOT.Repositories;
using System.Collections;

namespace AzureIOT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueueController : ControllerBase
    {
        [HttpPost("AddQueue")]
        public async Task<string> AddQueue(string queueName)
        {
            await QueueRepository.CreateQueue(queueName);
            return null;
        }
        [HttpPost("InsertMessage")]
        public async Task<string> InsertMessage(string queueName, string msg)
        {
            await QueueRepository.InsertMessage(queueName, msg);
            return null;
        }
        [HttpGet("PeekMessage")]
        public PeekedMessage[] PeekMessage(string queueName, int peekValue)
        {
            var data = QueueRepository.PeekMessage(queueName, peekValue);
            return data;
        }
        [HttpPut("UpdateMessage")]
        public async Task<string> UpdateMessage(string queueName, string msg)
        {
            await QueueRepository.UpdateMessage(queueName, msg);
            return null;
        }
        [HttpPut("DequeueMessage")]
        public async Task<string> DequeueMessage(string queueName)
        {
            await QueueRepository.DequeueMessage(queueName);
            return null;
        }
        [HttpDelete("DeleteQueue")]
        public async Task<string> DeleteQueue(string queueName)
        {
            await QueueRepository.DeleteQueue(queueName);
            return null;
        }


    }
}
