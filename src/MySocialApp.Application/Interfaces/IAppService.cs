namespace MySocialApp.Application;

public interface IAppService< TResponse, TReadResponse, TCreateRequest, TUpdateRequest, TFilterRequest>
{
    Task<Guid> Add(TCreateRequest request);
    Task Update(Guid id, TUpdateRequest request);
    Task Remove(Guid id);
    Task<TResponse> Read(Guid id);
    Task<PaginationResponse<TReadResponse>> Read(FilterRequest<TFilterRequest> request);
}
