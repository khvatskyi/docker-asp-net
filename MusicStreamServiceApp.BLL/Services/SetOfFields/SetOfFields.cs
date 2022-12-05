using AutoMapper;
using MusicStreamServiceApp.DAL.Entities;
using MusicStreamServiceApp.DAL.Interfaces;

namespace MusicStreamServiceApp.BLL.Services
{
    public abstract class SetOfFields
    {
        protected readonly IMapper mapper;
        protected readonly IUnitOfWork unitOfWork;
        protected SetOfFields(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
    }
}
