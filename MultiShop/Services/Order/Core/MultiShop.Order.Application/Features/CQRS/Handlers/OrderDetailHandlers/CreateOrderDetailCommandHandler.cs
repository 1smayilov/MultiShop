﻿using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommand;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers;

public class CreateOrderDetailCommandHandler
{
    private readonly IRepository<OrderDetail> _repository;

    public CreateOrderDetailCommandHandler(IRepository<OrderDetail> repository)
    {
        _repository = repository;
    }

    public async Task Handle(CreateOrderDetailCommand command)
    {
        await _repository.CreateAsync(new OrderDetail
        {
            OrderingId = command.OrderingId,
            ProductAmount = command.ProductAmount,
            ProductName = command.ProductName,
            ProductPrice = command.ProductPrice,
            ProductId = command.ProductId,
            ProductTotalPrice = command.ProductTotalPrice
        });
    }
}
