using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Filters;

public class NotFoundFilter<T> : IAsyncActionFilter where T : BaseEntity
{

    private readonly IService<T> _service;

    public NotFoundFilter(IService<T> service)
    {
        _service = service;
    }
    // Bir filter constructor'ında parametre alıyor ise, Controllerda kullanırken [ServiceFilter] üzerinden kullanmak lazım
    // Aynı zaman filter'da belirttiğimiz tipi de ( biz Product alıyoruz ) bunu da program.cs'te eklemiş olmak lazım
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
       var idValue = context.ActionArguments.Values.FirstOrDefault();

        // eğer ,id gelmez ise next ile deavm et 
        if (idValue == null)
        {
            await next.Invoke();
            return;
        }

        var id = (int)idValue;

        var anyEntity = await _service.AnyAsync(x=>x.Id == id);

        if (anyEntity)
        {
            await next.Invoke();
            return;
        }

        context.Result = new NotFoundObjectResult(CustomResponseDto<NoContentDto>.Fail(400, $"{typeof(T).Name}({id}) not found"));


    }
}
