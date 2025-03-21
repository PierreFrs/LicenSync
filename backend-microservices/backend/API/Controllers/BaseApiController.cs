// <copyright file="BaseApiController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using API.RequestHelpers;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseApiController : ControllerBase
{
    protected async Task<ActionResult> CreatePagedResult<T, TDto, TResult>(
        IGenericService<T, TDto> service,
        ISpecification<T, TResult> spec,
        int pageIndex,
        int pageSize
    )
        where T : BaseEntity
        where TDto : BaseDto
        where TResult : BaseDto
    {
        var items = await service.GetCardListByUserIdAsync<TResult>(spec);
        var count = await service.CountAsync(spec);
        var pagination = new Pagination<TResult>(pageIndex, pageSize, count, items);

        return Ok(pagination);
    }
}
