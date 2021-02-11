using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data
{
    public class MockCommanderRepo : ICommanderRepo
    {
        public void CreateCommand(Command command)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCommand(Command command)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Command> GetAppCommands()
        {
            var commands = new List<Command>
            {
                new Command{Id = 0, HowTo = "We can crack", Line = "enigma", Platform = "code n code"},
                new Command{Id = 1, HowTo = "We can crack1", Line = "enigma1", Platform = "code n code_1"},
                new Command{Id = 2, HowTo = "We can crack2", Line = "enigma2", Platform = "code n code_2"},
                new Command{Id = 3, HowTo = "We can crack3", Line = "enigma3", Platform = "code n code_3"},
                new Command{Id = 4, HowTo = "We can crack4", Line = "enigma4", Platform = "code n code_4"}
            };
            return commands;
        }

        public Command GetCommandById(int Id)
        {
            //just hardcode things here itslef for learning. originally we retreive the things from DB.
            return new Command{Id = 0, HowTo = "We can crack", Line = "enigma", Platform = "code n code"};
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCommand(Command command)
        {
            throw new System.NotImplementedException();
        }
    }
}