using MusicStreamServiceApp.DAL.Interfaces;

namespace MusicStreamServiceApp.DAL.Entities
{
    public class MusicPlaylist : IEntity<int>
    {
        public int Id { get; set; }
        public int UserPlaylistId { get; set; }
        public int MusicId { get; set; }

        public virtual Music Music { get; set; }
        public virtual UserPlaylist UserPlaylist { get; set; }
    }
}
