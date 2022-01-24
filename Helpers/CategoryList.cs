using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sheldy.Models;
namespace Sheldy.Category
{
    public class CategoryList
    {
        public List<CategoryTask> categoryList;
        SheldyContext db;
        public CategoryList()
        {
            db = new SheldyContext();
            categoryList = db.CategoryTasks.ToList();
        }
        public CategoryTask AddCategory(CategoryTask newCategory)
        {
            CategoryTask parentCategory = db.CategoryTasks.First(p => p.Id == newCategory.ParentId);
            newCategory.Level =parentCategory.Level+1;
            db.CategoryTasks.Add(newCategory);
            db.SaveChanges();
            categoryList = db.CategoryTasks.ToList();
            return db.CategoryTasks.FirstOrDefault(p => p.Name == newCategory.Name && p.ParentId == newCategory.ParentId);
        }
        public IEnumerable<CategoryTask> GetCategoryList(int id)
        {
            return db.CategoryTasks.Where(p => p.ParentId == id).ToArray();
        }
        public IEnumerable<CategoryTask> GetCategoryListTop()
        {
            return db.CategoryTasks.Where(p => p.Level == 1).ToArray();
        }
        public void remove( int id)
        {
            CategoryTask task=db.CategoryTasks.FirstOrDefault(p => p.Id==id);
            db.CategoryTasks.Remove(task);
            db.SaveChanges();
        }
        //public void RemoveCategory(int id)
        //{

        //}
    }
}
