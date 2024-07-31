﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;

namespace Medcard.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedcardController : ControllerBase
    {
        private readonly IRepository _repository; 

        public MedcardController(IRepository repository)
        {
            _repository = repository;
        }

        
        [HttpGet("GET")]
        public async Task<IActionResult> GET()
        {
            var medcard = await _repository.GetAsync();

            return new JsonResult(medcard);
        }
        [HttpGet("GET/{id}")]
        public async Task<IActionResult> GETID(Guid id)
        {
            var medcard = await _repository.GetByIdAsync(id);

            return new JsonResult(medcard);
        }

        [HttpPost("CREATE")]
        public async Task<IActionResult> CREATE(
                                                    string name, string phone,
                                                    string petName, int chipNumber,
                                                    int petAge, string petBreed,
                                                    string petDrugs = "Пока что здесь пусто!",
                                                    string petTreatment = "Пока что здесь пусто!")
        {
            try
            {
                var medcard = await _repository.CreateAsync(name, phone, petName, chipNumber, petAge, petBreed, petDrugs, petTreatment);

                return new JsonResult(medcard);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to create medcards: {ex.Message}");
            }
        }
        [HttpPost("UPDATE")]
        public async Task<IActionResult> UPDATE(Guid id,
                                                    string name, string phone,
                                                    string petName, int chipNumber,
                                                    int petAge, string petBreed,
                                                    string petDrugs ,
                                                    string petTreatment)
        {
            try
            {
                var medcard = await _repository.UpdateAsync(id,name,phone,petName,chipNumber,petAge,petBreed,petDrugs,petTreatment);

                return new JsonResult(medcard);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to create medcards: {ex.Message}");
            }
        }
        [HttpPost("DELETE")]
        public async Task<IActionResult> DELETE(Guid id)
        {
            var deletedMedcard = await _repository.DeleteAsync(id);

            if (deletedMedcard != null)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }


    }
}
