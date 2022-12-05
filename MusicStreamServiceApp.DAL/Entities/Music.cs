using System;
using System.Collections.Generic;
using MusicStreamServiceApp.DAL.Interfaces;

namespace MusicStreamServiceApp.DAL.Entities
{
    public partial class Music : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int? Year { get; set; }
        public int? AlbumId { get; set; }
        public string PhotoPath { get; set; }
        public string FilePath { get; set; }
        public Album Album { get; set; }
        public virtual ICollection<MusicPlaylist> MusicPlaylists { get; set; }
        public virtual ICollection<MusicGenre> MusicGenres { get; set; }
    }
}
