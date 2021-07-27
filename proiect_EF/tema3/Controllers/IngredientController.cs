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
    //CRUD operations for ingredients
    [Route("api/ingredients")]
    [ApiController]
   // [ApiExplorerSettings(GroupName = "v1")]
    public class IngredientController : ControllerBase
    {
        private IIngredientServiceAsync _ingredientService;

        public IngredientController(IIngredientServiceAsync ingredientService)
        {
            _ingredientService = ingredientService;
        }


        /// <summary>
        /// Get the ingredient by the given id.
        /// </summary>
        /// <param name="id">GUID id of the ingredient.</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /1
        ///     Authorization: "Bearer [Token]"
        ///
        /// </remarks>
        /// <returns>The ingredient with the given id.</returns>
        /// <response code="400">If id is empty.</response>      
        /// <response code="404">If ingredient does not exist.</response>      
        /// <response code="200">Returns the ingredient.</response>   
        [HttpGet]
        [Route("{id}")]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IngredientModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute] int? id)
        {//hasvalue in loc de id==null,merge pt int? ca tip
            if (!id.HasValue)
                return BadRequest("Id cannot be empty");

            var result = await _ingredientService.GetByIdAsync((int)id);
            if (result == null)
                return NotFound();

            return Ok(result.ToModel());
        }


        /// <summary>
        /// Get all existing ingredients. The results will be paged
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(List<IngredientModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] int? pageNumber = null, [FromQuery] int? pageSize = null)
        {
           
            if (!pageNumber.HasValue) pageNumber = 1;
            if (!pageSize.HasValue) pageSize = 10;
            var results =await  _ingredientService.GetAsync(pageNumber.Value, pageSize.Value);
            if (!results.Any())
                return NoContent();

            return Ok(results.Select(x => x.ToModel()));
        }


        /// <summary>
        /// Add an ingredient in the system.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IngredientModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> Add([FromBody] IngredientModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var context = (User)HttpContext.Items["User"];
            var result =await  _ingredientService.AddAsync(model.ToDto(null,context.Id));

            return Ok(result.ToModel());
        }


        /// <summary>
        /// Update an existing ingredient.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{id}")]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] IngredientModel model)
        {
            if (id == null)
                return BadRequest("Id cannot be empty");

            if (id != model.Id)
                return BadRequest("Ids do not match");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var result =  _ingredientService.UpdateAsync(model.ToDto(id));
            if (await result)
                return Ok();

            return StatusCode(StatusCodes.Status500InternalServerError);
        }


        /// <summary>
        /// Delete an existing ingredient.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

            var result = _ingredientService.RemoveAsync(id);
            if (await result)
                return Ok();

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        
        /// <summary>
        ///get by quantity
        /// </summary>
        /// <param name="quantity"></param>
        /// <returns></returns>
        [HttpGet("quantity")]
        [Route("{quantity/nou}")]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public  async Task<IActionResult> GetByQuantityAsync([FromRoute]int? quantity)
        {
            if (!quantity.HasValue)
                return BadRequest("quantity cannot be null or a negative number");
            var result = await _ingredientService.GetByQuantityAsync((int)quantity);
            if (!result.Any())
                return NoContent();

            return Ok(result.Select(x => x.ToModel()));
        }
        
        /// <summary>
        /// add product to ingredient
        /// </summary>
        /// <param name="ingredientId">id ingredient</param>
        /// <param name="product">produs</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("{ingredientId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProductModel), StatusCodes.Status200OK)]
        public async  Task<IActionResult> AddProductToIngredient([FromRoute]int ingredientId,[FromBody] ProductModel product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _ingredientService.AddProductToIngredient(ingredientId, product.ToDto(product.Id));//ii dau id ul

            return Ok(result.ToModel());
        }

    }
}
