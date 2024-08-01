using MediatR;
using Promocoes.Application.Input.Commands.BusinessContext;
using Promocoes.Application.Input.Repositories.Interfaces;
using Promocoes.Application.Input.Services.Imgur.OAuth2;
using Promocoes.Domain.Entities;
using Promocoes.Domain.Enums;
using Promocoes.Domain.ValueObjects;

namespace Promocoes.Application.Input.Receivers.BusinessReceiver
{
    public class Insert : IRequestHandler<BusinessCommand, State>
    {
        private readonly IWriteBusinessRepository _repository;

        public Insert(IWriteBusinessRepository repository)
        {
            _repository = repository;
        }

        public Task<State> Handle(BusinessCommand request, CancellationToken cancellationToken)
        {
            var auth = ImgurAuthorization.OAuth2();
            var uploadImage = ImgurUploadImage.UploadImage(auth, request.Logo);
            var contacts = new Contacts(request.Email, request.Number, request.Site);
            var category = (ECategory)request.Category;

            var business = new BusinessEntity(request.Name, request.Description,
                uploadImage.Result.Link, request.Location, contacts, category,
                request.Operation, request.GeoData, request.IdUser);

            if(!business.IsValid())
                return Task.FromResult(new State(400, "Erro ao inserir Empresa", business.Notifications));
            
            try 
            {
                _repository.InsertBusiness(business);
                _repository.UpdateUserByBusiness(business.IdUser, business.IdUser);
                return Task.FromResult(new State(200, "Empresa adicionada com sucesso", business));
            }
            catch(Exception ex)
            {
                return Task.FromResult(new State(500, "Erro Interno Do Servidor", ex.Data));
                //return Task.FromResult(new State(500, "Erro Inesperado", null));
            }
        }
    }
}