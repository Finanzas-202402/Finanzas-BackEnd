using Finanzas_BackEnd.Bills.Domain.Model.Aggregates;
using Finanzas_BackEnd.Bills.Domain.Model.Commands;
using Finanzas_BackEnd.Bills.Domain.Repositories;
using Finanzas_BackEnd.Bills.Domain.Services;
using Finanzas_BackEnd.Shared.Domain.Repositories;

namespace Finanzas_BackEnd.Bills.Application.Internal.CommandServices;

public class BillCommandService(IBillRepository billRepository, IUnitOfWork unitOfWork) : IBillCommandService
{
    public async Task<Bill?> Handle(CreateBillCommand command)
    {
        var bill = new Bill(command);
        try
        {
            await billRepository.AddAsync(bill);
            await unitOfWork.CompleteAsync();
            return bill;
        } catch (Exception e)
        {
            Console.WriteLine($"An error occurred while creating the bill: {e.Message}");
            return null;
        }
    }

    public async Task<Bill?> Handle(UpdateBillCommand command)
    {
        var bill = await billRepository.FindByIdAsync(command.Id);
        if (bill == null)
        {
            Console.WriteLine($"Bill with ID {command.Id} not found.");
            return null;
        }

        var newBill = new Bill(command);

        try
        {
            billRepository.Remove(bill);
            await billRepository.AddAsync(newBill);
            await unitOfWork.CompleteAsync();
            return newBill;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while updating the bill: {e.Message}");
            return null;
        }
    }

    public async Task<bool> Handle(DeleteBillCommand command)
    {
        var bill = await billRepository.FindByIdAsync(command.Id);
        if (bill == null)
        {
            Console.WriteLine($"Bill with ID {command.Id} not found.");
            return false;
        }
        
        try
        {
            billRepository.Remove(bill);
            await unitOfWork.CompleteAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while deleting the bill: {e.Message}");
            return false;
        }
    }
}