using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MusicStreamServiceApp.BLL.Interfaces.IServices;
using MusicStreamServiceApp.DAL.Interfaces;
using MusicStreamServiceApp.DAL.Entities;
using MusicStreamServiceApp.BLL.DTOs;
using AutoMapper;

namespace MusicStreamServiceApp.BLL.Services
{
    public class AlbumService : SetOfFields, IAlbumService
    {
        public AlbumService(IUnitOfWork unitOfWork, IMapper mapper)
           : base(unitOfWork, mapper)
        {
        }

        public async Task AddAlbumAsync(AlbumDTO albumDTO)
        {
            var album = mapper.Map<Album>(albumDTO);

            await unitOfWork.AlbumRepository.Add(album);
        }

        public async Task DeleteAlbumAsync(int Id)
        {
            var album = await unitOfWork.AlbumRepository.Get(Id);

            await unitOfWork.AlbumRepository.Delete(album);
        }

        public async Task<AlbumDTO> GetAlbumAsync(int Id)
        {
            var album = await unitOfWork.AlbumRepository.Get(Id);

            return mapper.Map<AlbumDTO>(album);
        }

        public async Task<AlbumDTO> GetAlbumAsync(string Name, string Author)
        {
            var album = await unitOfWork.AlbumRepository.Get(Name, Author);

            return mapper.Map<AlbumDTO>(album);
        }

        public async Task<IEnumerable<AlbumDTO>> GetAllAlbumsAsync()
        {
            var albums = await unitOfWork.AlbumRepository.GetAll();

            return mapper.Map<IEnumerable<AlbumDTO>>(albums);
        }

        public async Task<IEnumerable<AlbumDTO>> GetAuthorAlbumsAsync(string Author)
        {
            if (Author == null)
            {
                throw new Exception("Author's name is null");
            }

            var albums = await unitOfWork.AlbumRepository.GetAuthorAlbums(Author);

            if(albums == null)
            {
                throw new Exception("Not found");
            }

            return mapper.Map<IEnumerable<AlbumDTO>>(albums);
        }

        public async Task<bool> IsAnyAlbumDefinedAsync(int Id)
        {
            return await unitOfWork.AlbumRepository.Any(Id);
        }

        public async Task UpdateAlbumAsync(AlbumDTO albumDTO)
        {
            var album = mapper.Map<Album>(albumDTO);

            await unitOfWork.AlbumRepository.Update(album);
        }
    }
}
