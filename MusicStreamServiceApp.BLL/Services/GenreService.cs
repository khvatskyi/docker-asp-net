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
    public class GenreService : SetOfFields, IGenreService
    {
        public GenreService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }

        public async Task AddGenreAsync(GenreDTO genreDTO)
        {
            var genre = mapper.Map<Genre>(genreDTO);

            await unitOfWork.GenreRepository.Add(genre);
        }

        public async Task DeleteGenreAsync(int Id)
        {
            var genre = await unitOfWork.GenreRepository.Get(Id);

            await unitOfWork.GenreRepository.Delete(genre);
        }

        public async Task<GenreDTO> GetGenreAsync(int Id)
        {
            var genre = await unitOfWork.GenreRepository.Get(Id);

            return mapper.Map<GenreDTO>(genre);
        }

        public async Task<GenreDTO> GetGenreAsync(string Name)
        {
            var genre = await unitOfWork.GenreRepository.GetByName(Name);

            return mapper.Map<GenreDTO>(genre);
        }

        public async Task<IEnumerable<GenreDTO>> GetAllGenresAsync()
        {
            var genreList = await unitOfWork.GenreRepository.GetAll();

            return mapper.Map<IEnumerable<GenreDTO>>(genreList);
        }

        public async Task UpdateGenreAsync(GenreDTO genreDTO)
        {
            var genre = mapper.Map<Genre>(genreDTO);

            await unitOfWork.GenreRepository.Update(genre);
        }

        public async Task<bool> IsAnyGenreDefinedAsync(int Id)
        {
            return await unitOfWork.GenreRepository.Any(Id);
        }
    }
}
