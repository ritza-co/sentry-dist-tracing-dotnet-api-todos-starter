using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;

namespace todo_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;
        private readonly ILogger<TodoController> _logger;

        public TodoController(TodoContext context, ILogger<TodoController> logger)
        {
            _context = context;
             _logger = logger;
        }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
    {  
        try
        {
            using var timeoutCts = new CancellationTokenSource(TimeSpan.FromSeconds(1));
            await Task.Delay(120000, timeoutCts.Token);
            var result = await _context.TodoItems.ToListAsync(timeoutCts.Token);

            // this works
            //  var result = await _context.TodoItems.ToListAsync();
            
            return result;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

        [HttpGet("{id}")]
        public ActionResult<TodoItem> GetTodoItem(int id)
        {
            var todoItem = _context.TodoItems.Find(id);
            if (todoItem == null)
                return NotFound();
            return todoItem;
        }

        [HttpPost]
        public ActionResult<TodoItem> CreateTodoItem(TodoItem item)
        {
            _context.TodoItems.Add(item);
            _context.SaveChanges();
            return item;
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTodoItem(int id, TodoItem item)
        {
            var existing = _context.TodoItems.Find(id);
            if (existing == null)
                return NotFound();

            existing.Title = item.Title;
            existing.IsDone = item.IsDone;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTodoItem(int id)
        {
            var todoItem = _context.TodoItems.Find(id);
            if (todoItem == null)
                return NotFound();

            _context.TodoItems.Remove(todoItem);
            _context.SaveChanges();
            return NoContent();
        }
    }
}