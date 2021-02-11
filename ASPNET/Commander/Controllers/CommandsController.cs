using System.Collections.Generic;
using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers
{
    [Route("api/commands")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommanderRepo _repo;
        private readonly IMapper _mapper;

        public CommandsController(ICommanderRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        //GET api/commands
        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDtos>> GetAllCommands()
        {
            var commands = _repo.GetAppCommands();
            return Ok(_mapper.Map<IEnumerable<CommandReadDtos>>(commands));
        }

        //GET api/commands/{id}
        [HttpGet("{id}", Name = "GetCommandById")]
        public ActionResult<CommandReadDtos> GetCommandById(int id)
        {
            var command = _repo.GetCommandById(id);

            if(command != null)
            {
                return Ok(_mapper.Map<CommandReadDtos>(command));
            }
            return NotFound();
        }

        //POST api/commnands
        [HttpPost]
        public ActionResult <CommandReadDtos> CreateCommand(CommandCreateDtos cmd)
        {
            var commandModel = _mapper.Map<Command>(cmd);
            _repo.CreateCommand(commandModel);
            _repo.SaveChanges();
            var commandReadDto = _mapper.Map<CommandReadDtos>(commandModel);
            // return Ok(commandReadDto);
            return CreatedAtRoute(nameof(GetCommandById), new { Id = commandReadDto.Id}, commandReadDto);
        }

        //PUT api/commands/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDtos commandUpdateDtos)
        {
            var commandModelFromRepo = _repo.GetCommandById(id);

            if(commandModelFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(commandUpdateDtos, commandModelFromRepo);

            _repo.UpdateCommand(commandModelFromRepo);

            _repo.SaveChanges();

            return NoContent();
        }

        //PATCH api/commands/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDtos> patchDoc)
        {
            var commandModelFromRepo = _repo.GetCommandById(id);
            if(commandModelFromRepo == null)
            {
                return NotFound();
            }

            var commandToPatch = _mapper.Map<CommandUpdateDtos>(commandModelFromRepo);
            patchDoc.ApplyTo(commandToPatch, ModelState);

            if(!TryValidateModel(commandToPatch))
            {
                return ValidationProblem();
            }

            _mapper.Map(commandToPatch, commandModelFromRepo);

            _repo.UpdateCommand(commandModelFromRepo);
            _repo.SaveChanges();

            return NoContent();
        }

        //DELETE api/commands/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var commandModelFromRepo = _repo.GetCommandById(id);
            if(commandModelFromRepo == null)
            {
                return NotFound();
            }
            _repo.DeleteCommand(commandModelFromRepo);
            _repo.SaveChanges();
            return NoContent();
        }
    }
}