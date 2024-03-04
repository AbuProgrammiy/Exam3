using BookStore.Api.Attributes;
using BookStore.Application.Abstractions.IServices;
using BookStore.Domain.Entities.DTOs;       // BookDTO ishlashi uchun
using BookStore.Domain.Entities.Enums;
using BookStore.Domain.Entities.Models;     // Book ishlashi uchun
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;             // ControllerBase ishlashi uchun

namespace BookStore.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class cBookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public cBookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        [IdentityFilter(Permissions.Create)]
        public string Create(BookDTO bookDTO)
        {
            return _bookService.Create(bookDTO);
        }

        [HttpGet]
        [IdentityFilter(Permissions.Get)]
        public IEnumerable<Book> GetAll()
        {
            return _bookService.GetAll();
        }

        [HttpGet]
        [IdentityFilter(Permissions.Get)]
        public Book GetByName(string Name)
        {
            return _bookService.GetByName(Name);
        }

        [HttpPut]
        [IdentityFilter(Permissions.Update)]
        public string Update(int id,BookDTO bookDTO)
        {
            return _bookService.Update(id, bookDTO);
        }

        [HttpDelete]
        [IdentityFilter(Permissions.Delete)]
        public string Delete(int id)
        {
            return _bookService.Delete(id);
        }
    }
}
