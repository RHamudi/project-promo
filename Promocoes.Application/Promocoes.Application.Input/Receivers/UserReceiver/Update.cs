using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OneOf;
using Promocoes.Application.Input.Commands.UserContext;
using Promocoes.Application.Input.Repositories.Interfaces;
using Promocoes.Application.Output.DTOs;
using Promocoes.Application.Output.Interfaces;
using Promocoes.Domain.Entities;
using Promocoes.Errors;
using Promocoes.Errors.Exceptions.infra.output.User;

namespace Promocoes.Application.Input.Receivers.UserReceiver
{
    public class Update : IRequestHandler<UpdateUserCommand, OneOf<UpdateUserCommand, AppError>>
    {
        private readonly IWriteUserRepository _repository;
        private readonly IReadUserRepository _readRepository;
        private readonly IMapper _mapper;

        public Update(IWriteUserRepository repository, IReadUserRepository readRepository, IMapper mapper)
        {
            _readRepository = readRepository;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<OneOf<UpdateUserCommand, AppError>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userById = _readRepository.GetUserById(request.IdUser);

            if (userById.IsT1)
                return userById.AsT1;


            UpdateUserCommand userUpdated = new() {
                IdUser = request.IdUser,
                Name = request.Name != userById.AsT0.Nome & request.Name != null ? request.Name :  userById.AsT0.Nome, 
                Email = request.Email != userById.AsT0.Email & request.Email != null ? request.Email : userById.AsT0.Email,
                Password = request.Password != userById.AsT0.Senha  & request.Password != null ? request.Password : userById.AsT0.Senha,
                IdBusiness = request.IdBusiness != userById.AsT0.IdEmpresa & request.IdBusiness != null ? request.IdBusiness : userById.AsT0.IdEmpresa 
            };

            
            var update = _repository.UpdateUser(userUpdated);
            if (update.IsT0)
                return update.AsT0;

            return update.AsT1;
            
        }
    }
}