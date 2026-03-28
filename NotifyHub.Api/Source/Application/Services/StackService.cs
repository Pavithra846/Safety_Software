using Microsoft.EntityFrameworkCore;
using NotifyHub.Api.Source.Domain.Entities;
using NotifyHub.Api.Source.Domain.Interfaces;
using NotifyHub.Api.Source.Infrastructure.Data;

namespace NotifyHub.Api.Source.Application.Services
{
    public class StackService
    {
        private readonly IStackRepository _repo;
        private readonly ICallRepository _Callrepo;
        private readonly INotificationService _notification;
        public StackService(IStackRepository repo,ICallRepository callrepo, INotificationService notification)
        {
            _repo = repo;
            _Callrepo = callrepo;
            _notification = notification;
        }

        public async Task CreateStackAsync(Stack stack)
        {
            if (stack == null)
                throw new Exception("Stack cannot be null");

            if (stack.CallID == Guid.Empty)
                throw new Exception("CallID is required");

            if (string.IsNullOrWhiteSpace(stack.Location))
                throw new Exception("Location is required");
            #region Stack Number
            int currentYear = DateTime.Now.Year;

            var maxNumber = await _repo.GetMaxStackNumberForYearAsync(currentYear);

            int nextSequence;

            if (maxNumber == 0)
            {
                nextSequence = 1;
            }
            else
            {
                // Extract last 4 digits
                int lastSequence = int.Parse(maxNumber.ToString().Substring(4));
                nextSequence = lastSequence + 1;
            }

            // Format: 20260001
            stack.StkNbr = int.Parse($"{currentYear}{nextSequence:D4}");
            #endregion 
            var call = await _Callrepo.GetByIdAsync(stack.CallID);

            if (call == null)
                throw new Exception("Invalid CallID");

            // Get location from Call
            string location = call.Location;
            stack.StackID = Guid.NewGuid();        // generate ID
            stack.CreatedDttm = DateTime.Now;
            stack.UpdatedDttm = DateTime.Now;

            await _repo.CreateAsync(stack);
            await _notification.SendAsync("StackCreated", stack);
        }
       
        public async Task<List<Stack>> GetAllStackAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Stack> GetStackByIdAsync(Guid id)
        {
            return await _repo.GetByIdAsync(id);
        }
        public async Task UpdateStackByAsync(Stack stack)
        {
            var existingStack = await _repo.GetByIdAsync(stack.StackID);

            if (existingStack == null)
                throw new Exception("Stack not found");

            existingStack.Location = stack.Location;
            existingStack.Status = stack.Status;
            existingStack.UpdatedDttm = DateTime.Now;

            await _repo.SaveAsync();
        }
    }
}
