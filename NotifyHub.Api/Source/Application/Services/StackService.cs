using Microsoft.EntityFrameworkCore;
using NotifyHub.Api.Source.Application.DTOs;
using NotifyHub.Api.Source.Application.Interface;
using NotifyHub.Api.Source.Domain.Entities;
using NotifyHub.Api.Source.Domain.Enums;
using NotifyHub.Api.Source.Domain.Interfaces;

namespace NotifyHub.Api.Source.Application.Services
{
    public class StackService
    {
        private readonly IStackRepository _repo;
        private readonly ICallRepository _Callrepo;
        private readonly INotificationService _notification;
        public StackService(IStackRepository repo, ICallRepository callrepo, INotificationService notification)
        {
            _repo = repo;
            _Callrepo = callrepo;   
            _notification = notification;
        }

        public async Task CreateStackAsync(Guid callID)
        {
            var call = await _Callrepo.GetByIdAsync(callID);
            if (call == null)
                throw new KeyNotFoundException("Invalid CallId");

            var stack = new Stack
            {
                StackID = Guid.NewGuid(),
                CallID = callID,
                Location = call.Location,
                Status = 0, //Stack Created
                CreatedDttm = DateTime.UtcNow,
                UpdatedDttm = DateTime.UtcNow
            };


            int attempts = 0;

            while (attempts < 3)
            {
                try
                {
                    int currentYear = DateTime.UtcNow.Year;

                    var maxNumber = await _repo.GetMaxStackNumberForYearAsync(currentYear);

                    int nextSequence = maxNumber == 0
                        ? 1
                        : int.Parse(maxNumber.ToString().Substring(4)) + 1;

                    stack.StkNbr = int.Parse($"{currentYear}{nextSequence:D4}");

                    await _repo.CreateAsync(stack);
                    break;
                }
                catch (DbUpdateException)
                {
                    attempts++;

                    if (attempts >= 3)
                        throw new Exception("Failed to generate unique Stack Number.");
                }
            }
            var StackResponse = new StackResponseDto
            {
                StackID = stack.StackID,
                CallID = stack.CallID,
                StkNbr = stack.StkNbr,
                Location = stack.Location,
                Status = stack.Status,
                CreatedDttm = stack.CreatedDttm,
                UpdatedDttm = stack.UpdatedDttm
            };
            await _notification.SendNotification("StackCreated", StackResponse);
        }
        public async Task<List<StackResponseDto>> GetAllStackAsync()
        {
            var stacks = await _repo.GetAllAsync();
            if (stacks == null)
                throw new KeyNotFoundException("Stack Not Found");
            return stacks.Select(stack => new StackResponseDto
            {
                StackID = stack.StackID,
                CallID = stack.CallID,
                StkNbr = stack.StkNbr,
                Location = stack.Location,
                Status = stack.Status,
                CreatedDttm = stack.CreatedDttm,
                UpdatedDttm = stack.UpdatedDttm
            }).ToList();


        }

        public async Task<StackResponseDto> GetStackByIdAsync(Guid id)
        {
            var stack = await _repo.GetByStackIdAsync(id);
            if (stack == null)
                throw new KeyNotFoundException("Stack Not Found");
            var dto = new StackResponseDto
            {
                StackID = stack.StackID,
                CallID = stack.CallID,
                StkNbr = stack.StkNbr,
                Location = stack.Location,
                Status = stack.Status,
                CreatedDttm = stack.CreatedDttm,
                UpdatedDttm = stack.UpdatedDttm
            };
            return dto;

        }
        public async Task<(bool, Guid CallID)> UpdateStackByAsync(Guid id, UpdateStackDto dto)
        {
                var existingStack = await _repo.GetByStackIdAsync(id);

                if (existingStack == null)
                    throw new KeyNotFoundException("Stack Not Found");

            var StackResponse = new StackResponseDto
            {
                StackID = existingStack.StackID,
                CallID = existingStack.CallID,
                StkNbr = existingStack.StkNbr,
                Location = existingStack.Location,
                Status = existingStack.Status,
                CreatedDttm = existingStack.CreatedDttm,
                UpdatedDttm = existingStack.UpdatedDttm
            };
            if (dto.Status == StackStatus.Finished)
            {
                await _repo.DeleteAsync(existingStack);
                await _notification.SendNotification("StackFinished", StackResponse);
                bool hasStacks = await _repo.HasStacksByCallIdAsync(existingStack.CallID);
                return (hasStacks, existingStack.CallID);
            }
            else
            {
                existingStack.Location = dto.Location;
                existingStack.Status = dto.Status;
                existingStack.UpdatedDttm = DateTime.UtcNow;

                await _repo.UpdateAsync(existingStack);
                await _notification.SendNotification("StackUpdated", StackResponse);
            }
            return (false, existingStack.CallID);


        }

    }
}
