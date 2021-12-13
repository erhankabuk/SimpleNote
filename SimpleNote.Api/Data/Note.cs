using SimpleNote.Api.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleNote.Api.Data
{
    public class Note
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;

     
    }
}
