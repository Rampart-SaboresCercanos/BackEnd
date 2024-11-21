using BackEnd.Orders.Domain.Model.Aggregates;
using BackEnd.Orders.Domain.Repositories;
using BackEnd.Shared.Infrastructure.Persistence.EFC.Configuration;
using BackEnd.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Orders.Infrastructure.Persistence.EFC.Repositories;

public class OrderRepository(AppDbContext context): BaseRepository<Order>(context), IOrderRepository
{
    public async Task<IEnumerable<Order>> FindAllAsync()
    {
        return await Context.Set<Order>().ToListAsync();
    }

    public async Task<IEnumerable<Order>> FindByCustomerIdAsync(int customerId)
    {
        return await Context.Set<Order>()
            .Where(order => order.customerId == customerId)
            .ToListAsync();
    }


    public async Task<Order?> FindByOrderIdAsync(int orderId)
    {
        return await Context.Set<Order>().FirstOrDefaultAsync(f => f.Id == orderId );
    }

    public async Task<IEnumerable<Order>> FindByStatusAsync(string status)
    {
        return await Context.Set<Order>()
            .Where(order => order.status == status)
            .ToListAsync();
    }

    public async Task UpdateAsync(Order order)
    {
        Context.Set<Order>().Update(order);
        await Context.SaveChangesAsync();
    }

    public async Task RemoveAsync(Order order)
    {
        Context.Set<Order>().Remove(order);
        await Context.SaveChangesAsync();
    }
}