using Microsoft.EntityFrameworkCore;
using NotifyHub.Api.Source.Application.DTOs;
using NotifyHub.Api.Source.Domain.Entities;
using NotifyHub.Api.Source.Domain.Enums;
using NotifyHub.Api.Source.Domain.Interfaces;
using NotifyHub.Api.Source.Infrastructure.Data;

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

        public async Task<StackResponseDto> CreateStackAsync(CreateStackDto dto)
        {
            var call = await _Callrepo.GetByIdAsync(dto.CallID);
            if (call == null)
                throw new Exception("Invalid CallID");

            var stack = new Stack
            {
                StackID = Guid.NewGuid(),
                CallID = dto.CallID,
                Location = call.Location,
                Status = 0, //Stack Created
                CreatedDttm = DateTime.Now,
                UpdatedDttm = DateTime.Now
            };


            int attempts = 0;

            while (attempts < 3)
            {
                try
                {
                    int currentYear = DateTime.Now.Year;

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
            await _notification.SendAsync("StackCreated", stack);
            return new StackResponseDto
            {
                StackID = stack.StackID,
                CallID = stack.CallID,
                StkNbr = stack.StkNbr,
                Location = stack.Location,
                Status = stack.Status,
                CreatedDttm = stack.CreatedDttm,
                UpdatedDttm = stack.UpdatedDttm
            };
        }
        public async Task<List<StackResponseDto>> GetAllStackAsync()
        {
            var stacks = await _repo.GetAllAsync();
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
            var stack = await _repo.GetByIdAsync(id);
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
        public async Task UpdateStackByAsync(Guid id, UpdateStackDto dto)
        {
                var existingStack = await _repo.GetByIdAsync(id);

                if (existingStack == null)
                    throw new Exception("Invalid Data");
            if (dto.Status == StackStatus.Finished)
            {
                await _repo.DeleteAsync(existingStack);
            }
            else
            {
                existingStack.Location = dto.Location;
                existingStack.Status = dto.Status;
                existingStack.UpdatedDttm = DateTime.Now;

                await _repo.UpdateAsync(existingStack);
            }
               
        }

    }
}
