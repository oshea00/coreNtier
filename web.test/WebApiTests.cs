using NUnit.Framework;
using Todo.Data.Models;
using webapi.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using webapi.Services;
using webapi;
using Microsoft.AspNetCore.Mvc;

namespace Tests
{
    public class WebApiTests
    {
        DbContextOptions<TodosContext> _options;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<TodosContext>()
                            .UseInMemoryDatabase(databaseName: "todos")
                            .Options;
            // Run the test against one instance of the context
            using (var context = new TodosContext(_options))
            {
                var service = new TodosRepo(context);
                foreach (var item in service.GetAll())
                {
                    service.Delete(item);
                }
                service.Add(new Todos { ID = 1, Description = "Test todo", IsComplete = false });
            }
        }

        [Test]
        public void canAddTodos()
        {
            using (var context = new TodosContext(_options))
            {
                var todos = new TodosRepo(context).GetAll().ToList();
                Assert.AreEqual(1, todos.Count());
                Assert.AreEqual("Test todo", todos[0].Description);
            }
        }

        [Test]
        public void canGetListfromController()
        {
            using (var context = new TodosContext(_options))
            {
                var c = new TodosController(new TodosRepo(context));
                var list = c.Get();
                Assert.AreEqual(1, list.Count());
            }
        }

        [Test]
        public void canAddTodo()
        {
            using (var context = new TodosContext(_options))
            {
                var c = new TodosController(new TodosRepo(context));
                var list = c.Get();
                var newid = list.Last().ID + 1;
                var newitem = new Todos { ID = newid, Description = $"New item {newid}", IsComplete = false };
                c.Put(newid, newitem);
                int newcount = c.Get().Count();
                Assert.AreEqual(newid, newcount);
            }
        }

        [Test]
        public void canUpdateTodo()
        {
            using (var context = new TodosContext(_options))
            {
                var c = new TodosController(new TodosRepo(context));
                var viewitem = (OkObjectResult) c.Get(1);
                var item = (Todos)viewitem.Value;
                item.Description = "updated item";
                c.Post(item);
                var updatedview = (OkObjectResult)c.Get(1);
                var updatedDesc = ((Todos)updatedview.Value).Description;
                Assert.AreEqual(item.Description, updatedDesc);
            }
        }

        [Test]
        public void CanDeleteTodo()
        {
            using (var context = new TodosContext(_options))
            {
                var c = new TodosController(new TodosRepo(context));
                var viewitem = c.Delete(2);
                var item = c.Get(2);
                Assert.True(item is NotFoundResult);
            }
        }

        [Test]
        public void AlreadyDeletedReturnsNotFoundObjectResult()
        {
            using (var context = new TodosContext(_options))
            {
                var c = new TodosController(new TodosRepo(context));
                var item = c.Delete(2);
                Assert.True(item is NotFoundObjectResult);
            }
        }
    }
}
