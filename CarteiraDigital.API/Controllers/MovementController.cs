using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarteiraDigital.Domain.Models;
using CarteiraDigital.Domain.Models.Interfaces;
using CarteiraDigital.Domain.Models.Movement;
using CarteiraDigital.Domain.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarteiraDigital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MovementController : ControllerBase
    {
        private readonly IMovementService _movementService;
        public MovementController(IMovementService movementService)
        {
            _movementService = movementService;
        }

        [HttpPost, Route("History")]
        public async Task<IResult<HistoryModel>> History(FilterMovementModel filter)
        {
            return await _movementService.History(filter);
        }
    }
}
