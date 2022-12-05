using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MusicStreamServiceApp.BLL.Interfaces.IServices;
using MusicStreamServiceApp.DAL.Interfaces;
using MusicStreamServiceApp.DAL.Entities;
using System.Linq;
using AutoMapper;
using MusicStreamServiceApp.BLL.DTOs;

namespace MusicStreamServiceApp.BLL.Services
{
    public class MusicService : SetOfFields, IMusicService
    {
        public MusicService(IUnitOfWork unitOfWork,
            IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }

        public async Task<bool> IsAnyMusicDefinedAsync(int Id)
        {
            return await unitOfWork.MusicRepository.Any(Id);
        }

        public async Task AddMusicAsync(MusicCUDTO musicCreateDTO)
        {
            await CheckGenreAsync(musicCreateDTO.Genre);
            var music = mapper.Map<Music>(musicCreateDTO);
            music.AlbumId = await CheckAlbumAsync(musicCreateDTO.Album, musicCreateDTO.Author);
            await unitOfWork.MusicRepository.Add(music);
            music = await unitOfWork.MusicRepository.Get(music.Name, music.Author, music.Year);
            await AddMusicGenreRecordAsync(music.Id, musicCreateDTO.Genre);
        }

        public async Task DeleteMusicAsync(MusicViewDTO musicDTO)
        {
            var music = await unitOfWork.MusicRepository.Get(musicDTO.Name, musicDTO.Author, musicDTO.Year);
            await unitOfWork.MusicRepository.Delete(music);
        }

        public async Task<IEnumerable<MusicViewDTO>> GetAllMusicAsync()
        {
            IEnumerable<Music> musics = await unitOfWork.MusicRepository.GetAll();
            return mapper.Map<IEnumerable<MusicViewDTO>>(musics);
        }

        public async Task<IEnumerable<MusicViewDTO>> GetMusicByAlbumAsync(int AlbumId)
        {
            var musicList = await unitOfWork.MusicRepository.GetByAlbumId(AlbumId);
            return mapper.Map<IEnumerable<MusicViewDTO>>(musicList);
        }

        public async Task<IEnumerable<MusicViewDTO>> GetMusicByNameAsync(string Name)
        {
            var musicList = await unitOfWork.MusicRepository.GetByName(Name);
            return mapper.Map<List<MusicViewDTO>>(musicList);
        }

        public async Task<MusicViewDTO> GetMusicForViewAsync(int Id)
        {
            var music = await unitOfWork.MusicRepository.Get(Id);
            return mapper.Map<MusicViewDTO>(music);
        }

        public async Task<MusicCUDTO> GetMusicForUpdateAsync(int Id)
        {
            var music = await unitOfWork.MusicRepository.Get(Id);
            return await ToFillMusicCUDTORecordsAsync(music);
        }

        public async Task<MusicCUDTO> GetMusicForUpdateAsync(string Name, string Author, int? Year)
        {
            var music = await unitOfWork.MusicRepository.Get(Name, Author, Year);
            return await ToFillMusicCUDTORecordsAsync(music);
        }

        public async Task UpdateMusicAsync(int Id, MusicCUDTO musicDTO)
        {
            await CheckGenreAsync(musicDTO.Genre);
            var music = mapper.Map<Music>(musicDTO);
            music.Id = Id;
            music.AlbumId = await CheckAlbumAsync(musicDTO.Album, musicDTO.Author);
            await unitOfWork.MusicRepository.Update(music);
            await AddMusicGenreRecordAsync(music.Id, musicDTO.Genre);
        }

        private async Task<MusicCUDTO> ToFillMusicCUDTORecordsAsync(Music music)
        {
            var musicDTO = mapper.Map<MusicCUDTO>(music);
            if (musicDTO == null)
            {
                return musicDTO;
            }

            if (music.AlbumId != null)
            {
                var album = await unitOfWork.AlbumRepository.Get(music.AlbumId.Value);

                musicDTO.Album = album.Name;
            }
            else
            {
                musicDTO.Album = "";
            }
            music.MusicGenres = await unitOfWork.MusicGenreRepository.GetByMusicId(music.Id);
            if (music.MusicGenres != null)
            {
                foreach (var item in music.MusicGenres)
                {
                    item.Genre = await unitOfWork.GenreRepository.Get(item.GenreId);
                }

                int i = 0;
                foreach (var item in music.MusicGenres)
                {
                    if (i < music.MusicGenres.Count() - 1)
                    {
                        musicDTO.Genre += item.Genre.Name + ", ";
                    }
                    else
                    {
                        musicDTO.Genre += item.Genre.Name;
                    }
                    i++;
                }
            }
            if (musicDTO.Genre == null)
            {
                musicDTO.Genre = "";
            }
            return musicDTO;
        }
        private async Task<int?> CheckAlbumAsync(string AlbumName, string AuthorName)
        {
            if (!string.IsNullOrWhiteSpace(AlbumName))
            {
                var album = await unitOfWork.AlbumRepository.Get(AlbumName, AuthorName);
                if (album == null)
                {
                    throw new Exception("Album not found");
                }
                return album.Id;
            }
            return null;
        }
        private async Task CheckGenreAsync(string GenreName)
        {
            var genre = await unitOfWork.GenreRepository.GetByName(GenreName);
            if (genre == null)
            {
                throw new Exception("Incorrect genre name");
            }
        }
        private async Task AddMusicGenreRecordAsync(int musicId, string genreName)
        {
            var musicgenreList = await unitOfWork.MusicGenreRepository.GetByMusicId(musicId);
            foreach(var musicGenre in musicgenreList)
            {
                await unitOfWork.MusicGenreRepository.Delete(musicGenre);
            }
            var genre = await unitOfWork.GenreRepository.GetByName(genreName);
            var mg = new MusicGenre
            {
                MusicId = musicId,
                GenreId = genre.Id,
            };
            await unitOfWork.MusicGenreRepository.Add(mg);
        }
    }
}
