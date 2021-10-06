using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopBridge.Service.Interfaces;
using ShopBridgeAPI.Helper;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ShopBridgeAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        // private ShopbridgedbContext _db;
        IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        //[Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation("GetProducts", "Retrieve Product List")]
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAll()
        {
            try
            {

                var data = _productService.GetProducts();
                return Ok(data);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        //GET:api/product/{12}
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation("GetProductsByID", "Retrieve Product By ID")]
        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            try
            {
                var prod = _productService.GetProduct(id);
                if (prod is null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                else
                {
                    return Ok(prod);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet]
        public ActionResult<PagedCollectionResponse<Product>> GetFilter([FromQuery] SampleFilterModel filter)
        {

            //Filtering logic  
            Func<SampleFilterModel, IEnumerable<Product>> filterData = (filterModel) =>
            {
                return _productService.GetProducts().Where(p => p.Name.StartsWith(filterModel.Term ?? String.Empty))
                .Skip((filterModel.Page - 1) * filter.Limit)
                .Take(filterModel.Limit);
            };

            //Get the data for the current page  
            var result = new PagedCollectionResponse<Product>();
            result.Items = filterData(filter);

            //Get next page URL string  
            SampleFilterModel nextFilter = filter.Clone() as SampleFilterModel;
            nextFilter.Page += 1;
            String nextUrl = filterData(nextFilter).Count() <= 0 ? null : this.Url.Action("GetFilter", null, nextFilter, Request.Scheme);

            //Get previous page URL string  
            SampleFilterModel previousFilter = filter.Clone() as SampleFilterModel;
            previousFilter.Page -= 1;
            String previousUrl = previousFilter.Page <= 0 ? null : this.Url.Action("GetFilter", null, previousFilter, Request.Scheme);

            result.NextPage = !String.IsNullOrWhiteSpace(nextUrl) ? new Uri(nextUrl) : null;
            result.PreviousPage = !String.IsNullOrWhiteSpace(previousUrl) ? new Uri(previousUrl) : null;

            return result;

        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public IActionResult Add(Product model)
        {
           //Pending to Validate and based on that retrun Message.
            
            try
            {
                _productService.AddProduct(model);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("Update Product", "Update Product By ID")]
        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] Product model)  //parameter binding
        {
            try
            {
                if (id != model.ProductId)
                    return BadRequest();

                _productService.UpdateProduct(model);

                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[SwaggerOperation("Mofidy Product Name", "Update Product Name By ID")]
        //[HttpPatch]
        //public IActionResult Modify(Product model)
        //{
        //    try
        //    {
        //        Product data = _db.Products.Find(model.ProductId);
        //        data.Name = model.Name;
        //        _db.SaveChanges();
        //        return StatusCode(StatusCodes.Status200OK);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //}

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation("DeleteProductsByID", "Delete Product By ID")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Product model = _productService.GetProduct(id);
                if (model != null)
                {
                    _productService.DeleteProduct(id);
                   
                    return StatusCode(StatusCodes.Status200OK);
                }
                return StatusCode(StatusCodes.Status304NotModified);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }


    }
}
