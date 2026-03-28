using Microsoft.EntityFrameworkCore;
using NotifyHub.Api.Source.Domain.Entities;
using NotifyHub.Api.Source.Domain.Interfaces;
using NotifyHub.Api.Source.Infrastructure.Data;

namespace NotifyHub.Api.Source.Infrastructure.Repositories
{
    public class CallRepository:ICallRepository
    {
        private readonly AppDbContext _context;

        public CallRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateCallAsync(Call call)
        {
            await _context.Calls.AddAsync(call);
            await _context.SaveChangesAsync();
        }
       
        public async Task<List<Call>> GetAllAsync()
        {
            return await _context.Calls.ToListAsync();
        }
        public async Task<Call> GetByIdAsync(Guid callId)
        {
            return await _context.Calls.FindAsync(callId);
        }
        public async Task UpdateAsync(Call call)
        {
            var existingCall = await _context.Calls.FindAsync(call.CallID);

            if (existingCall == null)
            {
                throw new Exception("Call not found");
            }
            existingCall.Location = call.Location;
            existingCall.LandMark = call.LandMark;
            existingCall.Type = call.Type;
            existingCall.Name = call.Name;
            existingCall.Comments = call.Comments;
            existingCall.UpdatedDttm = DateTime.Now;
            existingCall.Status = call.Status;

            await _context.SaveChangesAsync();
        }
    }
}
