using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.AddressHandlers;

public class CreateAddressCommandHandler
{
    private readonly IRepository<Address> _repository;

    public CreateAddressCommandHandler(IRepository<Address> repository)
    {
        _repository = repository;
    }

    public async Task Handle(CreateAddressCommand createAddressComand)
    {
        await _repository.CreateAsync(new Address
        {
            City = createAddressComand.City,
            Detail1 = createAddressComand.Detail1,
            District = createAddressComand.District,
            UserId = createAddressComand.UserId,
            Country = createAddressComand.Country,
            Description = createAddressComand.Description,
            Detail2 = createAddressComand.Detail2,
            Email = createAddressComand.Email,
            Name = createAddressComand.Name,
            Surname = createAddressComand.Surname,
            Phone = createAddressComand.Phone,
            ZipCode = createAddressComand.ZipCode

        });
    }
}
