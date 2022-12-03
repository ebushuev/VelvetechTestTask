using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApiDTO.BL.Interfaces;
using TodoApiDTO.DAL.Interfaces;

namespace TodoApiDTO.BL {
    public class ServiceTodoItemDTO : IService<TodoItemDTO> {

        private IRepository<TodoItem> _repository;
        private readonly ILogger<ServiceTodoItemDTO> _logger;
        private readonly IMapper _mapperToDTO;
        private readonly IMapper _mapperToItem;

        public ServiceTodoItemDTO( IUnitOfWork unitOfWork, ILogger<ServiceTodoItemDTO> logger ) {
            _logger = logger;
            _repository = unitOfWork.TodoItems;
            var config = new MapperConfiguration ( cfg => cfg.CreateMap<TodoItem, TodoItemDTO> ()
            .ForMember ( "Id", opt => opt.MapFrom ( src => src.Id ) )
            .ForMember ( "Name", opt => opt.MapFrom ( src => src.Name ) )
            .ForMember ( "IsComplete", opt => opt.MapFrom ( src => src.IsComplete ) ) );
            _mapperToDTO = new Mapper ( config );
            config = new MapperConfiguration ( cfg => cfg.CreateMap<TodoItemDTO, TodoItem> ()
            .ForMember ( "Id", opt => opt.MapFrom ( src => src.Id ) )
            .ForMember ( "Name", opt => opt.MapFrom ( src => src.Name ) )
            .ForMember ( "IsComplete", opt => opt.MapFrom ( src => src.IsComplete ) ) );
            _mapperToItem = new Mapper ( config );
        }

        public IEnumerable<TodoItemDTO> GetAll() {
            return _mapperToDTO.Map<IEnumerable<TodoItemDTO>> ( _repository.GetAll () );
        }

        public async Task<ActionResult<TodoItemDTO>> Get( long id ) {
            return _mapperToDTO.Map<TodoItemDTO> ( await _repository.GetAsync ( id ) );
        }

        public async Task<int> Update( long id, TodoItemDTO itemDTO ) {
            if(id != itemDTO.Id) {
                return 400;
            }

            _repository.Update ( _mapperToItem.Map<TodoItem> ( itemDTO ) );

            try {
                await _repository.SaveChangesAsync ();
            }
            catch(DbUpdateConcurrencyException ex) when(!IsExists ( id )) {
                _logger.LogError ( $"Exception: {ex}" );
                return 404;
            }
            return 204;
        }

        public async Task<int> Create( TodoItemDTO itemDTO ) {
            try {
                await _repository.CreateAsync ( _mapperToItem.Map<TodoItem> ( itemDTO ) );
                await _repository.SaveChangesAsync ();
            }
            catch(DbUpdateConcurrencyException ex) when(!IsExists ( itemDTO.Id )) {
                _logger.LogError ( $"Exception: {ex}" );
                return 404;
            }
            catch(DbException ex) {
                _logger.LogError ( $"Exception: {ex}" );
                return 400;
            }
            return 201;
        }

        public async Task<int> Delete( long id ) {
            var todoItem = await _repository.GetAsync ( id );

            if(todoItem == null) {
                return 404;
            }
            _repository.Delete ( todoItem );
            try {
                await _repository.SaveChangesAsync ();
            }
            catch(DbUpdateConcurrencyException ex) when(!IsExists ( id )) {
                _logger.LogError ( $"Exception: {ex}" );
                return 404;
            }

            return 204;
        }

        public bool IsExists( long id ) {
            return !( _repository.GetAsync ( id ) is null );
        }

    }
}
