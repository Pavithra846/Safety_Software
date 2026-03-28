using NotifyHub.Api.Source.Domain.Entities;
using NotifyHub.Api.Source.Domain.Interfaces;

namespace NotifyHub.Api.Source.Application.Services
{
    public class UnitService
    {
        private readonly IUnitRepository _repo;
        private readonly INotificationService _notification;

        public UnitService(IUnitRepository repo, INotificationService hub)
        {
            _repo = repo;
            _notification = hub;
        }
        public async Task CreateUnitAsync(Unit unit)
        {
            if (unit.UnitID == Guid.Empty)
                unit.UnitID = Guid.NewGuid();

            if (unit.CreatedOn == DateTime.MinValue)
                unit.CreatedOn = DateTime.UtcNow;

            unit.UpdatedOn = DateTime.UtcNow;
            unit.IsActive = false;
            unit.IsAvail = false;

            //TODO: need logic

            await _repo.CreateAsync(unit);
            await _notification.SendAsync("UnitCreated", unit);
        }

        public async Task<List<Unit>> GetAllUnitAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Unit> GetUnitByIdAsync(Guid id)
        {
            return await _repo.GetByIdAsync(id);
        }
        public async Task UpdateUnitByAsync(Unit unit)
        {
            var existingUnit = await _repo.GetByIdAsync(unit.UnitID);

            if (existingUnit == null)
                throw new Exception("unit not found");

            existingUnit.StkNbr = unit.StkNbr;
            existingUnit.UnitType = unit.UnitType;
            existingUnit.UpdatedOn = DateTime.Now;
            if(unit.IsFinished)
                existingUnit.IsActive = true;

            await _repo.UpdateAsync();
        }

    }
}
