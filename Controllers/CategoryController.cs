using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sheldy.Models;
using Sheldy.Category;
using Microsoft.AspNetCore.Authorization;

namespace Sheldy.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        CategoryList categoryList;
        
        public CategoryController()
        {
            categoryList = new CategoryList();
        }
        [Authorize]
        [HttpPost("addCategory")]
        public CategoryTask addCategory(CategoryTask category)
        {
            
            if(category.ParentId==0)
            {
                category.ParentId = null;
            }
            category=categoryList.AddCategory(category);
            return category;
        }
        [HttpGet("getCategory")]
        public IEnumerable<CategoryTask> getCategory(int id)
        {
            if (id == 0)
                return categoryList.GetCategoryListTop();
            else
                return categoryList.GetCategoryList(id);

        }
        [HttpGet("removeCategory")]
        public IActionResult removeCategory(int id)
        {
            try
            {
                categoryList.remove(id);
                return Ok();
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
