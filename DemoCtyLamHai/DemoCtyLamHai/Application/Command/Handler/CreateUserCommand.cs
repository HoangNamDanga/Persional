using AutoMapper;
using DemoCtyLamHai.Application.Command.Model;
using DemoCtyLamHai.Application.Query.Model;
using DemoCtyLamHai.Domain;
using DemoCtyLamHai.Infrastructure;
using MediatR;

namespace DemoCtyLamHai.Application.Command.Handler
{
    public class CreateUserCommand : IRequest<bool>
    {
        public UserCreateCommandModel UserCreate { get; set; } = new();
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, bool>
    {
        private readonly UserRepository _repository;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(UserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public  Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            if (request == null || request.UserCreate == null)
                throw new Exception("Dữ liệu không hợp lệ!");

            var users = _repository.GetAll();

            var newUser = _mapper.Map<User>(request.UserCreate);
            newUser.Id = Guid.NewGuid().ToString();

            users.Add(newUser);
            _repository.SaveAll(users);

            return Task.FromResult(true);
        }
    }

}
