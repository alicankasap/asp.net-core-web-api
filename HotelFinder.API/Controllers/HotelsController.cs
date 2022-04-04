using HotelFinder.Business.Abstract;
using HotelFinder.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelFinder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private IHotelService _hotelService;
        public HotelsController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        /// <summary>
        /// Get All Hotels
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var hotels = _hotelService.GetAllHotels();
            return Ok(hotels);
        }

        /// <summary>
        /// Get Hotel By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var hotel = _hotelService.GetHoteById(id);
            if (hotel != null)
            {
                return Ok(hotel);
            }
            return NotFound();
        }

        /// <summary>
        /// Create a Hotel
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody] Hotel hotel)
        {
            var createdHotel = _hotelService.CreateHotel(hotel);
            return CreatedAtAction("Get", new { id = createdHotel.Id }, createdHotel);
        }

        /// <summary>
        /// Update the Hotel
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put([FromBody] Hotel hotel)
        {
            if (_hotelService.GetHoteById(hotel.Id)!=null)
            {
                return Ok(_hotelService.UpdateHotel(hotel)); 
            }
            return NotFound();
            
        }

        /// <summary>
        /// Delete the Hotel
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_hotelService.GetHoteById(id) != null)
            {
                _hotelService.DeleteHotel(id);
                return Ok();
            }
            return NotFound();
        }
    }
}
