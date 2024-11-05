using AutoMapper;
using MySocialApp.Domain;
using MySocialApp.Infrastructure.Exceptions;
using System.Linq.Expressions;

namespace MySocialApp.Application;

internal abstract class AppService<TEntity, TReponse, TReadResponse, TCreateRequest, TUpdateRequest, TFilterRequest>
    : IAppService<TReponse, TReadResponse, TCreateRequest, TUpdateRequest, TFilterRequest>
    where TEntity : Entity
{
    private readonly IService<TEntity> _service;
    private readonly IRepository<TEntity> _repository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public AppService(IService<TEntity> service,
        IRepository<TEntity> repository,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _service = service;
        _repository = repository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Add(TCreateRequest request)
    {
        var entity = _mapper.Map<TEntity>(request);
        entity = await _service.Add(entity);
        await _unitOfWork.Commit();
        return entity.Id;
    }

    public async Task<TReponse> Read(Guid id)
    {
        var entity = await ReadEntity(id);
        return _mapper.Map<TReponse>(entity);
    }

    public async Task<PaginationResponse<TReadResponse>> Read(FilterRequest<TFilterRequest> request)
    {
        var filter = GetFilter(request);
        var orderBy = GetSort(request.Ordenation.OrderBy);
        var result = await _repository.Get(filter, orderBy, request.Ordenation.IsDescending(), request.Page, request.PageSize);
        var entityMaping = _mapper.Map<IEnumerable<TReadResponse>>(result.Item1);
        var response = new PaginationResponse<TReadResponse>(entityMaping, result.Item2);
        return response;
    }

    public async Task Remove(Guid id)
    {
        var entity = await ReadEntity(id);
        await _service.Remove(entity);
        await _unitOfWork.Commit();
    }

    public async Task Update(Guid id, TUpdateRequest request)
    {
        var entity = await ReadEntity(id);
        _mapper.Map(request, entity);
        await _service.Update(entity);
        await _unitOfWork.Commit();
    }

    protected async virtual Task<TEntity> ReadEntity(Guid id)
    {
        var entity = await _repository.Get(id);
        if (entity is null)
            throw new NotFoundException("Entity not found");

        return entity;
    }


    public abstract Expression<Func<TEntity, bool>> GetFilter(FilterRequest<TFilterRequest> request);
    public abstract Expression<Func<TEntity, object>> GetSort(string sortBy);
}
