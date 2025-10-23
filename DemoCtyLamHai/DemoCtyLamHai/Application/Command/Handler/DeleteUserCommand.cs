using DemoCtyLamHai.Infrastructure;
using MediatR;

namespace DemoCtyLamHai.Application.Command.Handler
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public string Id { get; set; }
    }
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly UserRepository _repository;

        public DeleteUserCommandHandler(UserRepository repository)
        {
            _repository = repository;
        }
        public Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {

            if (string.IsNullOrEmpty(request?.Id))

                throw new ArgumentException("ID người dùng không được để trống.", nameof(request));

            var users = _repository.GetAll();

            var userToRemove = users.FirstOrDefault(u => u.Id == request.Id);

            if (userToRemove == null)

                throw new KeyNotFoundException($"Không tìm thấy người dùng với ID: {request.Id} để xóa.");

            users.Remove(userToRemove);


            _repository.SaveAll(users);

            return Task.FromResult(true);
        }
    }
}
