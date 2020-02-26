using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Blog;
using server.DB;

namespace CheckOut.Repository
{
    public class BlogRepository : IRepository<Blog.Blog>
    {
        private IDatabase<Blog.Blog> _db;
        public BlogRepository(IDatabase<Blog.Blog> db)
        {
            _db = db;
        }

        public Blog.Blog Create(Blog.Blog entity)
        {
            string id = _db.Create(entity);
            entity.Id = id;
            return entity;
        }

        public async Task<Blog.Blog> GetByIdAsync(string id)
        {
            return await _db.GetByIdAsync(id);
        }

        public Blog.Blog Update(Blog.Blog entity)
        {
            return _db.Update(entity, entity.Id);
        }

        public string Delete(string id)
        {
            return _db.Delete(id);
        }

        public async Task<IEnumerable<Blog.Blog>> ListAllAsync()
        {
            return await _db.ListAllAsync();
        }

    }
}
