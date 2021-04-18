using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using ProductDataAccess;
using ProductService.Models;

namespace ProductService.Controllers
{
    public class ProductsController : ApiController
    {
        ProductDBEntities _context;
        public ProductsController()
        {
            _context = new ProductDBEntities();
        }

        [HttpGet]
        public IEnumerable<Product> GetProducts([FromUri] PagingParameterModel pagingParmeterModel)
        {
            var source = _context.Products.ToList();
            int count = source.Count();
            int CurrentPage = pagingParmeterModel.pageNumber;
            int PageSize = pagingParmeterModel.pageSize;
            int TotalCount = count;
            int TotalPages = (int)Math.Ceiling(count / (double)PageSize);
            var items = source.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
            var previousPage = CurrentPage > 1 ? "Yes" : "No";
            var nextPage = CurrentPage < TotalPages ? "Yes" : "No";
            var paginationMetadata = new
            {
                totalCount = TotalCount,
                pageSize = PageSize,
                currentPage = CurrentPage,
                totalPages = TotalPages,
                previousPage,
                nextPage
            };

            HttpContext.Current.Response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));
            return items;
        }
        [HttpGet]
        public HttpResponseMessage GetProductsById(int id)
        {
            var entity = _context.Products.FirstOrDefault(p => p.ID == id);
            //int entityCount;
            if (entity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            else
            {
                Response res = new Response();
                res.error = 1;
                res.errorMessage = "Product with Id = " + id.ToString() + " not found.";
                //res.count = 0;
                string response = ExtensionMethod.ToJSON(res);
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, response);
                //return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product with Id = " + id.ToString() + "not found.");
            }
        }
        [HttpPost]
        public HttpResponseMessage Post([FromBody] Product product)
        {
            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                var message = Request.CreateResponse(HttpStatusCode.Created, product);
                message.Headers.Location = new Uri(Request.RequestUri + product.ID.ToString());
                return message;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var entity = _context.Products.FirstOrDefault(p => p.ID == id);
                if (entity == null)
                {
                    Response res = new Response();
                    res.error = 1;
                    res.errorMessage = "Product with id = " + id.ToString() + " not found to update.";
                    res.count = 0;
                    string response = ExtensionMethod.ToJSON(res);
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, response);
                    //return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product with id = " + id.ToString() + "not found.");
                }
                else
                {
                    _context.Products.Remove(entity);
                    _context.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody] Product product)
        {
            try
            {
                var entity = _context.Products.FirstOrDefault(p => p.ID == id);
                if (entity == null)
                {
                    Response res = new Response();
                    res.error = 1;
                    res.errorMessage = "Product with Id =" + id.ToString() + " not found.";
                    res.count = 0;
                    string response = ExtensionMethod.ToJSON(res);
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, response);
                    //return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product with Id =" + id.ToString() + "not found to update.");
                }
                else
                {
                    entity.Name = product.Name;
                    entity.Description = product.Description;
                    entity.Price = product.Price;
                    _context.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
