using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleNote.Api.Data;
using SimpleNote.Api.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleNote.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public NoteController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IEnumerable<Note>> GetNotes()
        {

            return await _context.Notes.OrderByDescending(x => x.Time).ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> GetNote(int id)
        {
            Note note = await _context.Notes.FindAsync(id);
            if (note == null)
            {
                return NotFound();
            }

            return note;
        }
        [HttpPost]
        public async Task<ActionResult<Note>> PostNotes(PostNoteDto dto)
        {
            if (ModelState.IsValid)
            {
                Note note = new Note() { Title = dto.Title, Content = dto.Content };
                _context.Add(note);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetNote), new { id = note.Id }, note);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]

        public async Task<ActionResult<Note>> PutNote(int id, PustNoteDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                Note note = await _context.Notes.FindAsync(id);
                if (note == null)
                {
                    return NotFound();
                }
                note.Title = dto.Title;
                note.Content = dto.Content;
                note.Time = DateTime.Now;
                await _context.SaveChangesAsync();
                return note;
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Note>> DeleteNote(int id)
        {
            Note note = _context.Notes.Find(id);
            if (note == null)
            {
                return BadRequest(ModelState);
            }
            _context.Remove(note);
            await _context.SaveChangesAsync();
            return NoContent();
        }














    }
}
