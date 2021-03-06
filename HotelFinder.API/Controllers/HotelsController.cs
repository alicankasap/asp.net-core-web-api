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
        public async Task<IActionResult> Get()
        {
            var hotels = await _hotelService.GetAllHotels();
            return Ok(hotels);
        }

        /// <summary>
        /// Get Hotel By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{id}")] // api/hotels/gethotelbyid/2
        public async Task<IActionResult> GetHotelById(int id)
        {
            var hotel = await _hotelService.GetHoteById(id);
            if (hotel != null)
            {
                return Ok(hotel);
            }
            return NotFound();
        }

        /// <summary>
        /// Get Hotel By Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{name}")] // api/hotels/gethotelbyname/doubletree
        public async Task<IActionResult> GetHotelByName(string name)
        {
            var hotel = await _hotelService.GetHotelByName(name);
            if (hotel!=null)
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
        [Route("[action]")]
        public async Task<IActionResult> CreateHotel([FromBody] Hotel hotel)
        {
            var createdHotel = await _hotelService.CreateHotel(hotel);
            return Ok(createdHotel);
        }

        /// <summary>
        /// Update the Hotel
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateHotel([FromBody] Hotel hotel)
        {
            if (await _hotelService.GetHoteById(hotel.Id)!=null)
            {
                return Ok(await _hotelService.UpdateHotel(hotel)); 
            }
            return NotFound();
            
        }

        /// <summary>
        /// Delete the Hotel
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            if (await _hotelService.GetHoteById(id) != null)
            {
                await _hotelService.DeleteHotel(id);
                return Ok();
            }
            return NotFound();
        }
    }
}
