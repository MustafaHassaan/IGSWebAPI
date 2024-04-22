using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IGS.Models;
using Microsoft.AspNetCore.Cors;
using System.IO;

namespace IGS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class MediasController : ControllerBase
    {
        private readonly IGSCompanyContext _Con;

        public MediasController(IGSCompanyContext Con)
        {
            _Con = Con;
        }

        [HttpGet("GetMedia")]
        public async Task<ActionResult<IEnumerable<Medias>>> GM()
        {
            List<Medias> Medlist = new List<Medias>();
            var Med = await _Con.Medias.Where(m => m.MedDele <= DateTime.Now).ToListAsync();
            if (Med.Count > 0)
            {
                foreach (var item in Med)
                {
                    Medlist.Add(new Medias
                    {
                        Id = item.Id,
                        MedName = item.MedName,
                        AdvertiserName = item.AdvertiserName,
                        MedDescription = item.MedDescription,
                        MedConteact = item.MedConteact,
                        MedImageAname = item.MedImageAname,
                        MedImageApath = item.MedImageApath,
                        MedImageBname = item.MedImageBname,
                        MedImageBpath = item.MedImageBpath,
                        MedImageCname = item.MedImageCname,
                        MedImageCpath = item.MedImageCpath,
                        MedImageDname = item.MedImageDname,
                        MedImageDpath = item.MedImageDpath,
                        MedImageEname = item.MedImageEname,
                        MedImageEpath = item.MedImageEpath,
                        MedImageFname = item.MedImageFname,
                        MedImageFpath = item.MedImageFpath,
                        MedLcoationname = item.MedLcoationname,
                        MedLcoationlat = item.MedLcoationlat,
                        MedLcoationlon = item.MedLcoationlon,
                    });
                }
                return Medlist;
            }
            else
            {
                return StatusCode(404);
            }
        }
        [HttpPost("UserMedia")]
        public async Task<ActionResult<IEnumerable<Medias>>> GMU(Medias Med)
        {
            var us = await _Con.Users.Where(u => u.UserPhone == Med.UserPhone).FirstOrDefaultAsync();
            if (us != null)
            {
                var med = await _Con.Medias.Where(m => m.UserId == us.Id).ToListAsync();
                List<Medias> Medlist = new List<Medias>();
                foreach (var item in med)
                {
                    Medlist.Add(new Medias
                    {
                        Id = item.Id,
                        MedName = item.MedName,
                        AdvertiserName = item.AdvertiserName,
                        MedDescription = item.MedDescription,
                        MedConteact = item.MedConteact,
                        MedImageAname = item.MedImageAname,
                        MedImageApath = item.MedImageApath,
                        MedImageBname = item.MedImageBname,
                        MedImageBpath = item.MedImageBpath,
                        MedImageCname = item.MedImageCname,
                        MedImageCpath = item.MedImageCpath,
                        MedImageDname = item.MedImageDname,
                        MedImageDpath = item.MedImageDpath,
                        MedImageEname = item.MedImageEname,
                        MedImageEpath = item.MedImageEpath,
                        MedImageFname = item.MedImageFname,
                        MedImageFpath = item.MedImageFpath,
                        MedLcoationname = item.MedLcoationname,
                        MedLcoationlat = item.MedLcoationlat,
                        MedLcoationlon = item.MedLcoationlon,
                        MedDate = item.MedDate,
                        MedPrice = item.MedPrice
                    });
                }
                return Medlist;
            }
            return NotFound();
        }

        [HttpPost("Categories")]
        public async Task<ActionResult<IEnumerable<CategoryDetailes>>> GCDId(Categories Cat)
        {
            var CL = await _Con.Categories.Where(x => x.CategoryName == Cat.CategoryName).FirstOrDefaultAsync();
            if (CL != null)
            {
                var CD = await _Con.CategoryDetailes.Where(c => c.CatId == CL.Id).ToListAsync();
                List<CategoryDetailes> Catlist = new List<CategoryDetailes>();
                foreach (var item in CD)
                {
                    Catlist.Add(new CategoryDetailes
                    {
                        Id = item.Id,
                        DetailesName = item.DetailesName,
                    });
                }
                return Catlist;
            }
            return NotFound();
            //return CD;
        }

        [HttpPost("Categoryitems")]
        public async Task<ActionResult<IEnumerable<Medias>>> GCDOM(Categories Cat)
        {
            var CL = await _Con.Categories.Where(x => x.CategoryName == Cat.CategoryName).FirstOrDefaultAsync();
            if (CL != null)
            {
                var Medcat = await _Con.Medias.Where(c => c.CatId == CL.Id).ToListAsync();
                List<Medias> Medlist = new List<Medias>();
                foreach (var item in Medcat)
                {
                    Medlist.Add(new Medias
                    {
                        Id = item.Id,
                        MedName = item.MedName,
                        AdvertiserName = item.AdvertiserName,
                        MedDescription = item.MedDescription,
                        MedConteact = item.MedConteact,
                        MedImageAname = item.MedImageAname,
                        MedImageApath = item.MedImageApath,
                        MedImageBname = item.MedImageBname,
                        MedImageBpath = item.MedImageBpath,
                        MedImageCname = item.MedImageCname,
                        MedImageCpath = item.MedImageCpath,
                        MedImageDname = item.MedImageDname,
                        MedImageDpath = item.MedImageDpath,
                        MedImageEname = item.MedImageEname,
                        MedImageEpath = item.MedImageEpath,
                        MedImageFname = item.MedImageFname,
                        MedImageFpath = item.MedImageFpath,
                        MedLcoationname = item.MedLcoationname,
                        MedLcoationlat = item.MedLcoationlat,
                        MedLcoationlon = item.MedLcoationlon,
                        MedPrice = item.MedPrice
                    });
                }
                return Medlist;
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Medias>>> GetMedias()
        {
            var Med = await _Con.Medias.ToListAsync();
            return Med;
        }

        // GET: api/Medias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Medias>> GetMedias(int id)
        {
            var medias = await _Con.Medias.FindAsync(id);

            if (medias == null)
            {
                return NotFound();
            }

            return medias;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedias(int id, Medias medias)
        {
            if (id != medias.Id)
            {
                return BadRequest();
            }

            _Con.Entry(medias).State = EntityState.Modified;

            try
            {
                await _Con.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MediasExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Medias>> PostMedias(Medias Med)
        {
            var user = await _Con.Users.Where(u => u.UserPhone == Med.UserPhone).FirstOrDefaultAsync();
            Med.UserId = user.Id;
            var category = await _Con.Categories.Where(c => c.CategoryName == Med.CategoryName).FirstOrDefaultAsync();
            Med.CatId = category.Id;
            var Catdetailes = await _Con.CategoryDetailes.Where(c => c.DetailesName == Med.DetailesName).FirstOrDefaultAsync();
            Med.CatdetId = Catdetailes.Id;
            _Con.Medias.Add(Med);
            await _Con.SaveChangesAsync();
            return StatusCode(200);
            //return CreatedAtAction("GetMedias", new { id = medias.Id }, medias);
        }

        // DELETE: api/Medias/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Medias>> DeleteMedias(int id)
        {
            var medias = await _Con.Medias.FindAsync(id);
            if (medias == null)
            {
                return NotFound();
            }

            _Con.Medias.Remove(medias);
            await _Con.SaveChangesAsync();

            return medias;
        }

        private bool MediasExists(int id)
        {
            return _Con.Medias.Any(e => e.Id == id);
        }
    }
}
