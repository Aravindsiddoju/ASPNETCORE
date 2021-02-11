using System;
using System.Collections.Generic;
using System.Linq;
using Commander.Models;

namespace Commander.Data
{
    public class SqlCommanderRepo : ICommanderRepo
    {
        private readonly CommanderContext _context;

        public SqlCommanderRepo(CommanderContext context)
        {
            _context = context;
        }

        public void CreateCommand(Command command)
        {
            _context.Add(command);
        }

        public void DeleteCommand(Command command)
        {
            if(command == null)
            {
                throw new ArgumentNullException();
            }
            _context.Commands.Remove(command);
        }

        public IEnumerable<Command> GetAppCommands()
        {
            return _context.Commands.ToList();
        }

        public Command GetCommandById(int Id)
        {
            return _context.Commands.FirstOrDefault(x => x.Id == Id);
        }

        public bool SaveChanges()
        {
           return (_context.SaveChanges() >= 0);
        }

        public void UpdateCommand(Command command)
        {
            //Nothing
        }
    }
}