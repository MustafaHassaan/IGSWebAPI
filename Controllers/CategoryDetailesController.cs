using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IGS.Models;
using Microsoft.AspNetCore.Cors;

namespace IGS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class CategoryDetailesController : ControllerBase
    {
        private readonly IGSCompanyContext _Con;
        public CategoryDetailesController(IGSCompanyContext Con)
        {
            _Con = Con;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDetailes>>> GetCategoryDetailes()
        {
            return await _Con.CategoryDetailes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDetailes>> GetCategoryDetailes(int id)
        {
            var categoryDetailes = await _Con.CategoryDetailes.FindAsync(id);

            if (categoryDetailes == null)
            {
                return NotFound();
            }

            return categoryDetailes;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoryDetailes(CategoryDetailes CD)
        {
            var Cat = _Con.Categories.Where(c => c.CategoryName == CD.CategoryName).FirstOrDefault();
            var CatDet = _Con.CategoryDetailes.Where(x => x.Id == CD.Id).FirstOrDefault();
            if (CatDet == null && Cat == null)
            {
                return BadRequest();
            }
            CatDet.CatId = Cat.Id;
            CatDet.DetailesName = CD.DetailesName;
            _Con.Entry(CatDet).State = EntityState.Modified;
            await _Con.SaveChangesAsync();
            return StatusCode(201);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDetailes>> PostCategoryDetailes(CategoryDetailes CD)
        {
            var CatId = await _Con.Categories.Where(C => C.CategoryName == CD.CategoryName).FirstOrDefaultAsync();
            if (CatId != null)
            {
                CD.CatId = CatId.Id;
            }
            _Con.CategoryDetailes.Add(CD);
            await _Con.SaveChangesAsync();
            return StatusCode(201);
            //return CreatedAtAction("GetCategoryDetailes", new { id = categoryDetailes.Id }, categoryDetailes);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoryDetailes>> DeleteCategoryDetailes(int id)
        {
            var categoryDetailes = await _Con.CategoryDetailes.FindAsync(id);
            if (categoryDetailes == null)
            {
                return NotFound();
            }

            _Con.CategoryDetailes.Remove(categoryDetailes);
            await _Con.SaveChangesAsync();
            return categoryDetailes;
        }

        private bool CategoryDetailesExists(int id)
        {
            return _Con.CategoryDetailes.Any(e => e.Id == id);
        }
    }
}
