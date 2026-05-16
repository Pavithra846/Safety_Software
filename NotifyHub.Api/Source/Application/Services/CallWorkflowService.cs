using NotifyHub.Api.Source.Application.DTOs;

namespace NotifyHub.Api.Source.Application.Services
{
    public class CallWorkflowService
    {
        private readonly CallService _callService;
        private readonly StackService _stackService;

        public CallWorkflowService(
            CallService callService,
            StackService stackService)
        {
            _callService = callService;
            _stackService = stackService;
        }

        public async Task CreateCallWithStackAsync(Guid callId)
        {   
            await _stackService.CreateStackAsync(callId);
        }

        public async Task FinishStackAndCallAsync(Guid callId, bool markAsFinished)
        {
            await _callService.UpdateCallByAsync(callId, markAsFinished: markAsFinished);
            
        }
    }
}
