using System;
using Domain.Models;
using Repository.Repositories;

namespace Service.Service.Interfaces
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;

        public BlogService(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }


        public async Task Create(Blog blog)
        {
            await _blogRepository.Create(blog);
        }

        public async Task Delete(Blog blog)
        {
            await _blogRepository.Delete(blog);
        }

        public async Task Edit(int id, Blog blog)
        {
            var existBlog = await _blogRepository.GetById(id);

            existBlog.BackgroundImage = blog.BackgroundImage;
            existBlog.Comments = blog.Comments;
            existBlog.Content = blog.Content;
            existBlog.Image = blog.Image;
            existBlog.Title = blog.Title;
            existBlog.Description = blog.Description;

            await _blogRepository.Edit(existBlog);
        }

        public async Task<IEnumerable<Blog>> GetAll()
        {
            return await _blogRepository.GetGetAllWithIncludes();
        }

        public async Task<Blog> GetById(int id)
        {
            return await _blogRepository.GetBlogByIdWithComment(id);
        }
    }
}

