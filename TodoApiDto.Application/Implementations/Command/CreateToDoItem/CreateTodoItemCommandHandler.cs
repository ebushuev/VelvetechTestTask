using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using TodoApiDto.Application.Interfaces;
using TodoApiDto.Domain.Entities;

namespace TodoApiDto.Application.Implementations.Command.CreateToDoItem
{
    public class CreateTodoItemCommandHandler: IRequestHandler<CreateTodoItemCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly ITodoApiDtoDbContext _context;
        private readonly ILogger<CreateTodoItemCommandHandler> _logger;
        public CreateTodoItemCommandHandler(IMapper mapper, ITodoApiDtoDbContext context,
            ILogger<CreateTodoItemCommandHandler> logger)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }
        public async Task<Guid> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<TodoItem>(request);
            try
            {
                await _context.Items.AddAsync(item);
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation($"Item added to database.");
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error creating element: {ex.Message}");
            }
            return item.Id;
        }
    }
}