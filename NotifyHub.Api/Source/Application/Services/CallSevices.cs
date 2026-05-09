using NotifyHub.Api.Source.Application.DTOs;
using NotifyHub.Api.Source.Application.Interface;
using NotifyHub.Api.Source.Domain.Entities;
using NotifyHub.Api.Source.Domain.Interfaces;

namespace NotifyHub.Api.Source.Application.Services
{
    public class CallService
    {
        private readonly ICallRepository _repo;
        private readonly INotificationService _notification;

        public CallService(ICallRepository repo, INotificationService hub)
        {
            _repo = repo;
            _notification = hub;
        }

        public async Task CreateCallAsync(CreateCallDTO dto)
        {
            var call = new Call
            {
                CallID = Guid.NewGuid(),
                Location = dto.Location,
                LandMark = dto.LandMark,
                Comments = dto.Comments,
                Type = dto.Type,
                Name = dto.Name,
                Status = dto.Status,
                CreatedDttm = DateTime.Now,
                UpdatedDttm = DateTime.Now
            };

            // Save to DB
            await _repo.CreateCallAsync(call);
            var CallResponse = new CallResponseDTO
            {
                CallID = call.CallID,
                CreatedDttm = call.CreatedDttm,
                UpdatedDttm = call.UpdatedDttm,
                Location = call.Location,
                LandMark = call.LandMark,
                Comments = call.Comments,
                Type = call.Type,
                Status = call.Status,
                Name = call.Name
            };
           // await _notification.SendAsync("CallCreated", CallResponse);

        }
        public async Task<List<CallResponseDTO>> GetAllCalls()
        {
            var calls = await _repo.GetAllAsync();
            return calls.Select(call => new CallResponseDTO
            {
                CallID = call.CallID,
                CreatedDttm = call.CreatedDttm,
                UpdatedDttm = call.UpdatedDttm,
                Location = call.Location,
                LandMark = call.LandMark,
                Comments = call.Comments,
                Type = call.Type,
                Status = call.Status,
                Name = call.Name

            }).ToList();
        }

        public async Task<CallResponseDTO> GetCallByIdAsync(Guid id)
        {
             var call = await _repo.GetByIdAsync(id);
            if (call == null) return null;
            var CallResponse = new CallResponseDTO
            {
                CallID = call.CallID,
                CreatedDttm = call.CreatedDttm,
                UpdatedDttm = call.UpdatedDttm,
                Location = call.Location,
                LandMark = call.LandMark,
                Comments = call.Comments,
                Type = call.Type,
                Status = call.Status,
                Name = call.Name
            };
            return CallResponse;
        }
        public async Task UpdateCallByAsync(Guid id, UpdateCallDTO dto)
        {
            var existingcall = await _repo.GetByIdAsync(id);
            if (existingcall == null)
                throw new Exception("Invalid Data");

            existingcall.Status = dto.Status;
            existingcall.UpdatedDttm = DateTime.Now;
            existingcall.Comments = dto.Comments;

            await _repo.UpdateAsync(existingcall);
        }
    }
}
