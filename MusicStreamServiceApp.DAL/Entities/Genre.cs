using System.Collections.Generic;
using MusicStreamServiceApp.DAL.Interfaces;

namespace MusicStreamServiceApp.DAL.Entities
{
    public class Genre : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<MusicGenre> MusicGenres { get; set; }
    }
}
