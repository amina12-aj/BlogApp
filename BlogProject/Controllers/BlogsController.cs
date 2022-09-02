using BlogProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BlogProject.Controllers
{
    [Authorize]
    public class BlogsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BlogsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Blogs
        [AllowAnonymous]
        public async Task<IActionResult> Index(DateTime PublishedDate)
        {

            return _context.Blogs != null ?
                        View(await _context.Blogs.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Blogs'  is null.");
        }

        // GET: BlogsByDate
        public async Task<ActionResult<IEnumerable<Blog>>> Get()
        {
            var sortedArticles = _context.Blogs.OrderBy(x => x.PublishedDate)
         .ThenBy(x => x.PublishedDate.Date);       
            var sorted = sortedArticles.ToListAsync();
            return View(await sorted);
        }

        // GET: Blogs/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {

            var blog = await _context.Blogs
                .FirstOrDefaultAsync(m => m.ArticleId == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // GET: Blogs/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Create([Bind("ArticleId,Title,Description")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }

        // GET: Blogs/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || _context.Blogs == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
        }

        // POST: Blogs/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("ArticleId,Title,Description")] Blog blog)
        {
            if (id != blog.ArticleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogExists(blog.ArticleId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }

        // GET: Blogs/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Blogs == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                .FirstOrDefaultAsync(m => m.ArticleId == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Blogs == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Blogs'  is null.");
            }
            var blog = await _context.Blogs.FindAsync(id);
            if (blog != null)
            {
                _context.Blogs.Remove(blog);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogExists(int id)
        {
            return (_context.Blogs?.Any(e => e.ArticleId == id)).GetValueOrDefault();
        }
    }
}
