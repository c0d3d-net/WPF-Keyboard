/*
 * Author : İbrahim GAZALOĞLU 
 * Date   : 23.03.2019
 * Email  : ibrahimgazaloglu@gmail.com  
 *
 */

using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Gazaloglu.OnScreenKeyboard.Command
{
    class CommandFactory : ICommandFactory
    {
  
        public List<Action> PreCommands { get; set; }
        public List<Action> PostCommands { get; set; }

       public ICommand CreateCommand(Action action, Func<object, bool> canExecute = null, int[] ignorePreCommands = null , int[] ignorePostCommands = null)
        {
            var command = new ActionCommand(action, canExecute, ignorePreCommands, ignorePostCommands)
            {
                PreCommands = this.PreCommands ?? new List<Action>(),
                PostCommands = this.PostCommands ?? new List<Action>()
            };
            
            return command;

        }

        public CommandFactory()
        {
            PreCommands  =  new List<Action>();
            PostCommands =  new List<Action>();
        }


    }
}
