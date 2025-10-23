using AutoMapper;
using DemoCtyLamHai.Application.Command.Model;
using DemoCtyLamHai.Infrastructure;
using MediatR;

namespace DemoCtyLamHai.Application.Command.Handler
{
    public class UpdateUserCommand : IRequest<bool>
    {
        public UserUpdateCommandModel UpdateCommand { get; set; } = new();
    }
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly UserRepository _repository;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(UserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if (request?.UpdateCommand == null || string.IsNullOrEmpty(request.UpdateCommand.Id))
            {
                throw new ArgumentException("Dữ liệu cập nhật không hợp lệ hoặc thiếu ID người dùng.", nameof(request));
            }

            var users = _repository.GetAll();

            var existingUser = users.FirstOrDefault(u => u.Id == request.UpdateCommand.Id);

            if (existingUser == null)
                throw new KeyNotFoundException($"Không tìm thấy người dùng với ID: {request.UpdateCommand.Id} để cập nhật.");

            _mapper.Map(request.UpdateCommand, existingUser);

            _repository.SaveAll(users);

            return Task.FromResult(true);
        }
    }
}
