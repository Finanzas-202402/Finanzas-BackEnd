using System.Net.Mime;
using Finanzas_BackEnd.Bills.Domain.Model.Aggregates;
using Finanzas_BackEnd.Bills.Domain.Model.Commands;
using Finanzas_BackEnd.Bills.Domain.Model.Queries;
using Finanzas_BackEnd.Bills.Domain.Model.ValueObjects;
using Finanzas_BackEnd.Bills.Domain.Services;
using Finanzas_BackEnd.Bills.Interfaces.REST.Resources;
using Finanzas_BackEnd.Bills.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace Finanzas_BackEnd.Bills.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class BillsController(IBillCommandService billCommandService, IBillQueryService billQueryService)
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateBill(CreateBillResource resource)
    {
        var createBillCommand = CreateBillCommandFromResourceAssembler.ToCommandFromResource(resource);
        var bill = await billCommandService.Handle(createBillCommand);
        if (bill is null) return BadRequest();
        var billResource = BillResourceFromEntityAssembler.ToResourceFromEntity(bill);
        return CreatedAtAction(nameof(GetBillById), new { billId = billResource.Id }, billResource);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllBills()
    {
        var getAllBillsQuery = new GetAllBillsQuery();
        var bills = await billQueryService.Handle(getAllBillsQuery);
        var billResources = bills.Select(BillResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(billResources);
    }
    
    [HttpGet("{billId:int}")]
    public async Task<IActionResult> GetBillById(int billId)
    {
        var getBillByIdQuery = new GetBillByIdQuery(billId);
        var bill = await billQueryService.Handle(getBillByIdQuery);
        if (bill == null) return NotFound();
        var billResource = BillResourceFromEntityAssembler.ToResourceFromEntity(bill);
        return Ok(billResource);
    }
    
    [HttpGet("by-user/{userId:int}")]
    public async Task<IActionResult> GetBillsByUserId(int userId)
    {
        var getBillsByUserIdQuery = new GetBillByUserIdQuery(userId);
        var bills = await billQueryService.Handle(getBillsByUserIdQuery);
        var billResources = bills.Select(BillResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(billResources);
    }

    [HttpGet("by-email/{userEmail}")]
    public async Task<IActionResult> GetBillsByUserEmail(string userEmail)
    {
        var getBillsByUserEmailQuery = new GetBillByUserEmailQuery(userEmail);
        var bills = await billQueryService.Handle(getBillsByUserEmailQuery);
        var billResources = bills.Select(BillResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(billResources);
    }

    [HttpGet("by-cancelled/{userId:int}/{cancelled:bool}")]
    public async Task<IActionResult> GetBillsByUserIdAndCancelled(int userId, bool cancelled)
    {
        var getBillsByUserIdAndCancelledQuery = new GetBillByUserIdAndCancelledQuery(userId, cancelled);
        var bills = await billQueryService.Handle(getBillsByUserIdAndCancelledQuery);
        var billResources = bills.Select(BillResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(billResources);
    }
    
    [HttpDelete("{billId:int}")]
    public async Task<IActionResult> DeleteBill(int billId)
    {
        var getBillByIdQuery = new GetBillByIdQuery(billId);
        var bill = await billQueryService.Handle(getBillByIdQuery);
        if (bill is null) return BadRequest();
        await billCommandService.Handle(new DeleteBillCommand(billId));
        return Ok(new { message = "Bill successfully deleted." });
    }
    
    [HttpPut("{billId:int}")]
    public async Task<IActionResult> UpdateBill(int billId, UpdateBillResource resource)
    {
        if (billId != resource.Id)
        {
            return BadRequest("Bill ID mismatch.");
        }

        var updateBillCommand = UpdateBillCommandFromResourceAssembler.ToCommandFromResource(resource);
        var bill = await billCommandService.Handle(updateBillCommand);
        if (bill is null) return NotFound();
        var billResource = BillResourceFromEntityAssembler.ToResourceFromEntity(bill);
        return Ok(billResource);
    }
    
    // Bill Value calculation logic
    private static decimal CalculateBillValue(Bill bill)
    {
        decimal m = 1;
        decimal n = 1;
        decimal billValue = 0;

        switch (bill.RateTime)
        {
            case ERateTime.Fortnightly: m = 15;
                break;
            case ERateTime.Monthly: m = 30;
                break;
            case ERateTime.Bimonthly: m = 60;
                break;
            case ERateTime.Quarterly: m = 90;
                break;
            case ERateTime.FourMonthly: m = 120;
                break;
            case ERateTime.Semiannual: m = 180;
                break;
            case ERateTime.Annual: m = 360;
                break;
            default:
                throw new ArgumentException("Invalid rate time.");
        }

        switch (bill.Capitalization)
        {
            case ECapitalization.Diary: n = 1;
                break;
            case ECapitalization.Fortnightly: n = 15;
                break;
            case ECapitalization.Monthly: n = 30;
                break;
            case ECapitalization.Bimonthly: n = 60;
                break;
            case ECapitalization.Quarterly: n = 90;
                break;
            case ECapitalization.FourMonthly: n = 120;
                break;
            case ECapitalization.Semiannual: n = 180;
                break;
            case ECapitalization.Annual: n = 360;
                break;
            default:
                throw new ArgumentException("Invalid capitalization.");
        }

        if (bill.RateType == ERateType.Nominal)
        {
            billValue = bill.BillValue / (decimal)Math.Pow((double)(1 + ((bill.RateValue / 100) / (m / n))),
                ((bill.EndDate.Date - bill.StartDate.Date).Days) / (double)n);
        }
        else if (bill.RateType == ERateType.Effective)
        {
            billValue = bill.BillValue / (decimal)Math.Pow((double)(1 + (bill.RateValue / 100)),
                ((bill.EndDate.Date - bill.StartDate.Date).Days) / (double)m);
        }
        else
        {
            throw new ArgumentException("Invalid rate type.");
        }

        return billValue; 
    }
    // End of Bill Value calculation logic
    
    [HttpGet("get-value/{billId:int}")]
    public async Task<IActionResult> GetBillValueById(int billId)
    {
        var getBillByIdQuery = new GetBillByIdQuery(billId);
        var bill = await billQueryService.Handle(getBillByIdQuery);
        if (bill == null) return NotFound();
        if (bill.Cancelled == false) return BadRequest("Bill is not cancelled yet.");

        var billValue = CalculateBillValue(bill);

        return Ok(billValue);
    }

    [HttpGet("get-eac/{billId:int}")]
    public async Task<IActionResult> GetBillEacById(int billId)
    {
        var getBillByIdQuery = new GetBillByIdQuery(billId);
        var bill = await billQueryService.Handle(getBillByIdQuery);
        if (bill == null) return NotFound();
        if (bill.Cancelled == false) return BadRequest("Bill is not cancelled yet.");

        // EAC calculation logic
        
        var billValue = CalculateBillValue(bill);
        var eac = (((decimal)Math.Pow((double)(bill.BillValue / billValue),
            (360 / (double)((bill.ExpirationDate.Date - bill.EndDate.Date).Days)))) - 1) * 100;
        
        // End of EAC calculation logic

        return Ok(eac);
    }
}