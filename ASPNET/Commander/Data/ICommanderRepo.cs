using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data
{
    public interface ICommanderRepo
    {
        bool SaveChanges(); 
        IEnumerable<Command> GetAppCommands();
        Command GetCommandById(int Id);
        void CreateCommand(Command command);
        void UpdateCommand(Command command);
        void DeleteCommand(Command command);
    }
}