using AutoMapper;
using DemoCtyLamHai.Application.Query.Model;
using DemoCtyLamHai.Infrastructure;
using MediatR;

namespace DemoCtyLamHai.Application.Query.Handler
{
    public class GetAllUserQuery : IRequest<List<UserQuery>>
    {
    }

    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, List<UserQuery>>
    {
        private readonly UserRepository _repository;
        private readonly IMapper _mapper;

        public GetAllUserQueryHandler(UserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public  Task<List<UserQuery>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
               
                var users = _repository.GetAll();

                
                var userQueries = _mapper.Map<List<UserQuery>>(users);

                
                return userQueries;
            }, cancellationToken); 
        }
    }
}
