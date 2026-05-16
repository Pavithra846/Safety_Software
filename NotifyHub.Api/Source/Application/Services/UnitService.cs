using NotifyHub.Api.Source.Application.DTOs;
using NotifyHub.Api.Source.Application.Interface;
using NotifyHub.Api.Source.Domain.Entities;
using NotifyHub.Api.Source.Domain.Enums;
using NotifyHub.Api.Source.Domain.Interfaces;

namespace NotifyHub.Api.Source.Application.Services
{
    public class UnitService
    {
        private readonly IUnitRepository _repo;
        private readonly IStackRepository _stackrepo;
        private readonly INotificationService _notification;

        public UnitService(IUnitRepository repo, IStackRepository stackrepo, INotificationService hub)
        {
            _repo = repo;
            _stackrepo = stackrepo;
            _notification = hub;
        }
        public async Task CreateUnitAsync(CreateUnitDTO dto)
        {
            var unit = new Unit
            {
                UnitID = Guid.NewGuid(),
                IsActive = false,
                IsAvail = false,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                UnitName = dto.UnitName,
                UnitType = dto.UnitType,
            };
            await _repo.CreateAsync(unit);
            var UnitResponse = new UnitResponseDTO
            {
                UnitID = unit.UnitID,
                UnitName = unit.UnitName,
                StkNbr = unit.StkNbr,
                UnitType = unit.UnitType,
                IsActive = unit.IsActive,
                IsAvail = unit.IsAvail,
                CreatedOn = unit.CreatedOn,
                UpdatedOn = unit.UpdatedOn,
            };
            await _notification.SendNotification("UnitCreated", UnitResponse);
        }


        public async Task<List<UnitResponseDTO>> GetAllUnitAsync()
        {
            var units = await _repo.GetAllAsync();
            return units.Select(unit => new UnitResponseDTO
            {
                UnitID = unit.UnitID,
                UnitName = unit.UnitName,
                StkNbr = unit.StkNbr,
                UnitType = unit.UnitType,
                CreatedOn = unit.CreatedOn,
                UpdatedOn = unit.UpdatedOn,
                IsActive = unit.IsActive,
                IsAvail = unit.IsAvail,
                IsFinished = true
            }).ToList();
        }

        public async Task<UnitResponseDTO> GetUnitByIdAsync(Guid id)
        {
            var unit  =  await _repo.GetByIdAsync(id);
            var dto = new UnitResponseDTO
            {
                UnitID = unit.UnitID,
                UnitName = unit.UnitName,
                StkNbr = unit.StkNbr,
                UnitType = unit.UnitType,
                CreatedOn = unit.CreatedOn,
                UpdatedOn = unit.UpdatedOn,
                IsActive = unit.IsActive,
                IsAvail = unit.IsAvail,
                IsFinished = true 
            };
            return dto;
        }
        public async Task UpdateUnitByAsync(Guid id, UpdateUnitDTO unit)
        {
            var existingUnit = await _repo.GetByIdAsync(id);

            if (existingUnit == null)
                throw new Exception("unit not found");

            if (!existingUnit.IsAvail)
                return;

            existingUnit.StkNbr = unit.StkNbr;
            existingUnit.UpdatedOn = DateTime.Now;
            if(unit.IsFinished)
                existingUnit.IsActive = true;

            await _repo.UpdateAsync(existingUnit);
            var UnitResponse = new UnitResponseDTO
            {
                UnitID = existingUnit.UnitID,
                UnitName = existingUnit.UnitName,
                StkNbr = existingUnit.StkNbr,
                UnitType = existingUnit.UnitType,
                IsActive = existingUnit.IsActive,
                IsAvail = existingUnit.IsAvail,
                CreatedOn = existingUnit.CreatedOn,
                UpdatedOn = existingUnit.UpdatedOn,
            };
            await _notification.SendNotification("UnitUpdated", UnitResponse);
        }

    }
}
