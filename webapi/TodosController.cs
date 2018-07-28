using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Todo.Data.Models;
using webapi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace webapi
{
    [Route("api/[controller]")]
    public class TodosController : ControllerBase
    {
        private readonly IRepo<Todos> _context;

        public TodosController(IRepo<Todos> context)
        {
            _context = context;
        }
        // GET: api/values
        [HttpGet]
        public IEnumerable<Todos> Get()
        {
            return _context.GetAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var todo = _context.Get(id);
            if (todo == null)
                return NotFound();
            return Ok(todo);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post(Todos value)
        {
            try
            {
                _context.Update(value);
                return Ok();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Todos item)
        {
            try
            {
                item.ID = id;
                _context.Add(item);
                return Ok();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var item = _context.Get(id);
                if (item == null)
                    throw new Exception("Item not found");
                _context.Delete(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
