using AutoMapper;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using TodoApiDto.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace TodoApiDto.Application.Implementations.Command.UpdateToDoItem
{
    public class UpdateToDoItemCommandHandler : IRequestHandler<UpdateToDoItemCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly ITodoApiDtoDbContext _context;
        private readonly ILogger<UpdateToDoItemCommandHandler> _logger;

        public UpdateToDoItemCommandHandler(IMapper mapper, ITodoApiDtoDbContext context, 
            ILogger<UpdateToDoItemCommandHandler> logger)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }
        public async Task<Unit> Handle(UpdateToDoItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _context.Items.FirstOrDefaultAsync(x => x.Id == request.Id);
            if(item != null)
            {
                try
                {
                    _mapper.Map(request, item);
                    await _context.SaveChangesAsync(cancellationToken);

                    _logger.LogInformation($"Element with id : {request.Id} updated");
                }
                catch(Exception ex)
                {
                    _logger.LogError($"Error updating element with id:{request.Id}: {ex.Message}");
                }
            }
            return Unit.Value;
        }
    }
}