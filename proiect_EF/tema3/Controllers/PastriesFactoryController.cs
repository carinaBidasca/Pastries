using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PastriesCommon.Entities;
using PastriesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tema3.Models;
using tema3.Utils;
using AuthorizeAttribute = tema3.Models.AuthorizeAttribute;
namespace tema3.Controllers
{
    //CRUD operations for factories
    [Route("api/factories")]
    [ApiController]
    public class PastriesFactoryController : ControllerBase
    {
        private IPastriesFactoryService _pastriesFactoryService;

        public PastriesFactoryController(IPastriesFactoryService pastries)
        {
            _pastriesFactoryService = pastries;
        }

        /// <summary>
        /// get by id
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(PastriesFactoryModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute] int? id)
        {
            if (!id.HasValue)
                return BadRequest("Id cannot be empty");

            var result = await _pastriesFactoryService.GetByIdAsync((int)id);
            if (result == null)
                return NotFound();

            return Ok(result.ToModel());
        }

        /// <summary>
        /// get all 
        /// </summary>
        [HttpGet]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(List<PastriesFactoryModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] int? pageNumber = null, [FromQuery] int? pageSize = null)
        {

            if (!pageNumber.HasValue) pageNumber = 1;
            if (!pageSize.HasValue) pageSize = 10;
            var results = await _pastriesFactoryService.GetAsync(pageNumber.Value, pageSize.Value);
            if (!results.Any())
                return NoContent();

            return Ok(results.Select(x => x.ToModel()));
        }


        /// <summary>
        /// add factory
        /// </summary>
        [HttpPost]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(PastriesFactoryModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> Add([FromBody] PastriesFactoryModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _pastriesFactoryService.AddAsync(model.ToDto(null));

            return Ok(result.ToModel());
        }

        /// <summary>
        /// update with put
        /// </summary>
        [HttpPut]
        [Route("{id}")]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] PastriesFactoryModel model)
        {
            if (id == null)
                return BadRequest("Id cannot be empty");

            if (id != model.Id)
                return BadRequest("Ids do not match");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _pastriesFactoryService.UpdateAsync(model.ToDto(id));
            if (await result)
                return Ok();

            return StatusCode(StatusCodes.Status500InternalServerError);
        }


        /// <summary>
        ///delete by id
        /// </summary>
        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (id == null)
                return BadRequest("Id cannot be empty");

            var result = _pastriesFactoryService.RemoveAsync(id);
            if (await result)
                return Ok();

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        /// <summary>
        /// add product to factory-id din ruta
        /// </summary>
        [HttpPost]
        [Authorize]
        [Route("{factoryId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProductModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddProductToFactory([FromRoute] int factoryId, [FromBody] ProductModel product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _pastriesFactoryService.AddProductToFactory(factoryId, product.ToDto(product.Id));//ii dau id ul

            return Ok(result.ToModel());
        }

    }
}
