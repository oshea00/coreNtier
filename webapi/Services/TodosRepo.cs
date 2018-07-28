using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Data.Models;

namespace webapi.Services
{
    public interface IRepo<T>
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Add(T item);
        void Delete(T id);
        void Update(T item);
    }

    public class TodosRepo : IRepo<Todos>
    {
        TodosContext _context;
        public TodosRepo(TodosContext context)
        {
            _context = context;
        }

        public IEnumerable<Todos> GetAll()
        {
            return _context.Todos.ToList();
        }

        public Todos Get(int id)
        {
            return _context.Find<Todos>(id);
        }

        public void Add(Todos todo)
        {
            _context.Add(todo);
            _context.SaveChanges();
        }

        public void Delete(Todos item)
        {
            _context.Remove(item);
            _context.SaveChanges();
        }

        public void Update(Todos item)
        {
            _context.Update(item);
            _context.SaveChanges();
        }
    }
}
