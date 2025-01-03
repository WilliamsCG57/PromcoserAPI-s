﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PromcoserDOMAIN.Core.Entities;
using PromcoserDOMAIN.Data;

namespace PromcoserAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParteDiarioController : ControllerBase
    {
        private readonly PromcoserDbContext _context;

        public ParteDiarioController(PromcoserDbContext context)
        {
            _context = context;
        }

        
        [HttpGet("GetAllActive/{idPersonal}")]
        public async Task<IActionResult> GetAllActive(int idPersonal)
        {
            var entidadesActivas = await _context.ParteDiario
                .Where(r => r.Estado && r.IdPersonalNavigation.IdPersonal == idPersonal)
                .Include(p => p.IdLugarTrabajoNavigation)
                .Include(p => p.IdMaquinariaNavigation)
                .Include(p => p.IdPersonalNavigation)
                .Include(p => p.IdClienteNavigation)
                .Select(p => new
                {
                    p.IdParteDiario,
                    p.IdMaquinariaNavigation.Placa,
                    personal = p.IdPersonalNavigation.Nombre + " " + p.IdPersonalNavigation.Apellido,
                    p.IdLugarTrabajoNavigation.Descripcion,
                    p.IdClienteNavigation.RazonSocial,
                    p.Finalizado,
                    p.Fecha,
                    p.HorometroInicio,
                    p.HorometroFinal,
                    p.CantidadPetroleo,
                    p.CantidadAceite,
                    p.Estado
                })
                .ToListAsync();

            return Ok(entidadesActivas);
        }

        
        [HttpGet("GetAllActiveConfirmed/{idPersonal}")]
        public async Task<IActionResult> GetAllActiveConfirmed(int idPersonal)
        {
            var entidadesActivas = await _context.ParteDiario
                .Where(r => r.Estado && r.IdPersonalNavigation.IdPersonal == idPersonal && r.Finalizado)
                .Include(p => p.IdLugarTrabajoNavigation)
                .Include(p => p.IdMaquinariaNavigation)
                .Include(p => p.IdPersonalNavigation)
                .Include(p => p.IdClienteNavigation)
                .Select(p => new
                {
                    p.IdParteDiario,
                    p.IdMaquinariaNavigation.Placa,
                    personal = p.IdPersonalNavigation.Nombre + " " + p.IdPersonalNavigation.Apellido,
                    p.IdLugarTrabajoNavigation.Descripcion,
                    p.IdClienteNavigation.RazonSocial,
                    p.Finalizado,
                    p.Fecha,
                    p.HorometroInicio,
                    p.HorometroFinal,
                    p.CantidadPetroleo,
                    p.CantidadAceite,
                    p.Estado
                })
                .ToListAsync();

            return Ok(entidadesActivas);
        }

        [HttpGet("GetAllActivePending/{idPersonal}")]
        public async Task<IActionResult> GetAllActivePending(int idPersonal)
        {
            var entidadesActivas = await _context.ParteDiario
                .Where(r => r.Estado && r.IdPersonalNavigation.IdPersonal == idPersonal && !r.Finalizado)
                .Include(p => p.IdLugarTrabajoNavigation)
                .Include(p => p.IdMaquinariaNavigation)
                .Include(p => p.IdPersonalNavigation)
                .Include(p => p.IdClienteNavigation)
                .Select(p => new
                {
                    p.IdParteDiario,
                    p.IdMaquinariaNavigation.Placa,
                    personal = p.IdPersonalNavigation.Nombre + " " + p.IdPersonalNavigation.Apellido,
                    p.IdLugarTrabajoNavigation.Descripcion,
                    p.IdClienteNavigation.RazonSocial,
                    p.Finalizado,
                    p.Fecha,
                    p.HorometroInicio,
                    p.HorometroFinal,
                    p.CantidadPetroleo,
                    p.CantidadAceite,
                    p.Estado
                })
                .ToListAsync();

            return Ok(entidadesActivas);
        }

       
        [HttpGet("GetAllActive")]
        public async Task<IActionResult> GetAllActive()
        {
            var entidadesActivas = await _context.ParteDiario
                .Where(r => r.Estado)
                .Include(p => p.IdLugarTrabajoNavigation)
                .Include(p => p.IdMaquinariaNavigation)
                .Include(p => p.IdPersonalNavigation)
                .Include(p => p.IdClienteNavigation)
                .Select(p => new
                {
                    p.IdParteDiario,
                    p.IdMaquinariaNavigation.Placa,
                    personal = p.IdPersonalNavigation.Nombre + " " + p.IdPersonalNavigation.Apellido,
                    p.IdLugarTrabajoNavigation.Descripcion,
                    p.IdClienteNavigation.RazonSocial,
                    p.Finalizado,
                    p.Fecha,
                    p.HorometroInicio,
                    p.HorometroFinal,
                    p.CantidadPetroleo,
                    p.CantidadAceite,
                    p.IdCliente,
                    p.IdMaquinaria,
                    p.IdPersonal,
                    p.IdLugarTrabajo,
                    p.Estado
                })
                .ToListAsync();

            return Ok(entidadesActivas);
        }

       
        [HttpGet("GetAllInactive")]
        public async Task<IActionResult> GetAllInactive()
        {
            var entidadesInactivas = await _context.ParteDiario
                .Where(r => !r.Estado)
                .Include(p => p.IdLugarTrabajoNavigation)
                .Include(p => p.IdMaquinariaNavigation)
                .Include(p => p.IdPersonalNavigation)
                .Include(p => p.IdClienteNavigation)
                .Select(p => new
                {
                    p.IdParteDiario,
                    p.IdMaquinariaNavigation.Placa,
                    personal = p.IdPersonalNavigation.Nombre + p.IdPersonalNavigation.Apellido,
                    p.IdLugarTrabajoNavigation.Descripcion,
                    p.IdClienteNavigation.RazonSocial,
                    p.Finalizado,
                    p.Fecha,
                    p.HorometroInicio,
                    p.HorometroFinal,
                    p.CantidadPetroleo,
                    p.CantidadAceite,
                    p.Estado
                })
                .ToListAsync();

            return Ok(entidadesInactivas);
        }

        
        [HttpPost("Create")]
        public async Task<ActionResult<ParteDiario>> PostEntidad([FromBody] ParteDiarioDTO dto)
        {
            var entidad = new ParteDiario
            {
                IdCliente = dto.IdCliente,
                IdPersonal = dto.IdPersonal,
                IdLugarTrabajo = dto.IdLugarTrabajo,
                IdMaquinaria = dto.IdMaquinaria,
                Finalizado = dto.Finalizado,
                Fecha = dto.Fecha,
                HorometroInicio = dto.HorometroInicio,
                HorometroFinal = dto.HorometroFinal,
                CantidadPetroleo = dto.CantidadPetroleo,
                CantidadAceite = dto.CantidadAceite,
                Estado = dto.Estado
            };

            _context.ParteDiario.Add(entidad);
            await _context.SaveChangesAsync();
            CreatedAtAction(nameof(GetAllActive), new { id = entidad.IdParteDiario }, entidad);

            return Ok();
        }

       
        [HttpPut("Update")]
        public async Task<IActionResult> PutEntidad(ParteDiarioUpdateDTO entidadDTO)
        {
            var parteDiarioExistente = await _context.ParteDiario
                .FirstOrDefaultAsync(m => m.IdParteDiario == entidadDTO.IdParteDiario);

            if (parteDiarioExistente == null)
            {
                return NotFound();
            }

            parteDiarioExistente.Finalizado = entidadDTO.Finalizado;
            parteDiarioExistente.Fecha = entidadDTO.Fecha;
            parteDiarioExistente.HorometroInicio = entidadDTO.HorometroInicio;
            parteDiarioExistente.HorometroFinal = entidadDTO.HorometroFinal;
            parteDiarioExistente.CantidadPetroleo = entidadDTO.CantidadPetroleo;
            parteDiarioExistente.CantidadAceite = entidadDTO.CantidadAceite;
            parteDiarioExistente.Estado = entidadDTO.Estado;
            parteDiarioExistente.IdCliente = entidadDTO.IdCliente;
            parteDiarioExistente.IdPersonal = entidadDTO.IdPersonal;
            parteDiarioExistente.IdLugarTrabajo = entidadDTO.IdLugarTrabajo;
            parteDiarioExistente.IdMaquinaria = entidadDTO.IdMaquinaria;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        [HttpPut("Confirm")]
        public async Task<IActionResult> ConfirmEntidad(ParteDiarioConfirmDTO entidadDTO)
        {
            var parteDiarioExistente = await _context.ParteDiario
                .FirstOrDefaultAsync(m => m.IdParteDiario == entidadDTO.IdParteDiario);

            if (parteDiarioExistente == null)
            {
                return NotFound();
            }

            parteDiarioExistente.Finalizado = true;
            parteDiarioExistente.HorometroFinal = entidadDTO.HorometroFinal;
            parteDiarioExistente.CantidadPetroleo = entidadDTO.CantidadPetroleo;
            parteDiarioExistente.CantidadAceite = entidadDTO.CantidadAceite;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }


        [HttpPut("Activate/{id}")]
        public async Task<IActionResult> PutActEstadoEntidad(int id)
        {
            var entidad = await _context.ParteDiario.FindAsync(id);

            if (entidad == null)
            {
                return NotFound();
            }
            entidad.Estado = true;

            _context.Entry(entidad).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        
        [HttpPut("Deactivate/{id}")]
        public async Task<IActionResult> PutDesEstadoEntidad(int id)
        {
            var entidad = await _context.ParteDiario.FindAsync(id);

            if (entidad == null)
            {
                return NotFound();
            }
            entidad.Estado = false;

            _context.Entry(entidad).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ParteDiarioExists(int id)
        {
            return _context.ParteDiario.Any(e => e.IdParteDiario == id);
        }
    }
}
