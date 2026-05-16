using NotifyHub.Api.Source.Application.DTOs;
using NotifyHub.Api.Source.Application.Interface;
using NotifyHub.Api.Source.Domain.Entities;
using NotifyHub.Api.Source.Domain.Enums;
using NotifyHub.Api.Source.Domain.Interfaces;
using NotifyHub.Api.Source.Infrastructure.Services;

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

        public async Task<Call> CreateCallAsync(CreateCallDTO dto, Guid currentUserId)
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
            await _notification.CreateNotification("CallCreated", CallResponse, currentUserId);
            await _notification.SendNotification("CallCreated", CallResponse);
            return call;

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
        public async Task UpdateCallByAsync(Guid id, UpdateCallDTO? dto = null, bool markAsFinished = false)
        {
            var existingcall = await _repo.GetByIdAsync(id);

            if (existingcall == null)
                throw new Exception("Invalid Data");

            if (markAsFinished)
            {
                existingcall.Status = CallStatus.Finished;
            }
            else if (dto != null)
            {
                existingcall.Status = dto.Status;
                existingcall.Comments = dto.Comments;
            }

            existingcall.UpdatedDttm = DateTime.Now;

            await _repo.UpdateAsync(existingcall);

            var callResponse = new CallResponseDTO
            {
                CallID = existingcall.CallID,
                CreatedDttm = existingcall.CreatedDttm,
                UpdatedDttm = existingcall.UpdatedDttm,
                Location = existingcall.Location,
                LandMark = existingcall.LandMark,
                Comments = existingcall.Comments,
                Type = existingcall.Type,
                Status = existingcall.Status,
                Name = existingcall.Name
            };

            var eventName = markAsFinished ? "CallFinished" : "CallUpdated";

            await _notification.SendNotification(eventName, callResponse);
        }
    }
}
