using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IGS.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;

namespace IGS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class CategoriesController : ControllerBase
    {
        private readonly IGSCompanyContext _Con;
        public CategoriesController(IGSCompanyContext Con, IWebHostEnvironment IWH)
        {
            _Con = Con;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categories>>> GetCategories()
        {
            return await _Con.Categories.ToListAsync();
           
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Categories>> GetCategories(int id)
        {
            var categories = await _Con.Categories.FindAsync(id);

            if (categories == null)
            {
                return NotFound();
            }

            return categories;
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategories(Categories Cat)
        {
            var Category = _Con.Categories.Where(x => x.Id == Cat.Id).FirstOrDefault();
            var userid = _Con.Users.Where(x => x.UserPhone == Cat.UserPhone).FirstOrDefault();
            if (Category == null)
            {
                return BadRequest();
            }
            else
            {
                Category.CategoryName = Cat.CategoryName;
                Category.UserId = userid.Id;
                try
                {
                    if (Cat.CategoryImageName != null)
                    {
                        Category.CategoryImageName = Cat.CategoryImageName;
                    }
                    if (Cat.CategoryImagePath != null)
                    {
                        Category.CategoryImagePath = Cat.CategoryImagePath;
                    }
                    _Con.Entry(Category).State = EntityState.Modified;
                    await _Con.SaveChangesAsync();
                    return StatusCode(201);
                }
                catch (Exception ex)
                {
                    return StatusCode(504);
                }
            }
        }
        [HttpPost]
        public async Task<ActionResult<Categories>> PostCategories(Categories categories)
        {
            var userid = await _Con.Users.Where(u => u.UserPhone == categories.UserPhone).FirstOrDefaultAsync();
            categories.UserId = userid.Id;
            _Con.Categories.Add(categories);
            await _Con.SaveChangesAsync();
            return StatusCode(201);
            //return CreatedAtAction("GetCategories", new { id = categories.Id }, categories);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Categories>> DeleteCategories(int id)
        {
            var categories = await _Con.Categories.FindAsync(id);
            if (categories == null)
            {
                return NotFound();
            }
            _Con.Categories.Remove(categories);
            await _Con.SaveChangesAsync();
            return StatusCode(201);
        }
        public static Bitmap ByteToImage(byte[] img)
        {
            using (MemoryStream Ms = new MemoryStream())
            {
                Ms.Write(img, 0, img.Length);
                Ms.Seek(0, SeekOrigin.Begin);
                Bitmap Bm = new Bitmap(Ms);
                return Bm;
            }
        }
        private bool CategoriesExists(int id)
        {
            return _Con.Categories.Any(e => e.Id == id);
        }
    }
}
