/*
 * Author : İbrahim GAZALOĞLU 
 * Date   : 23.03.2019
 * Email  : ibrahimgazaloglu@gmail.com  
 *
 */

using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace WPFTouchscreenKeyboard.Command
{
    interface ICommandFactory
    {
        List<Action> PreCommands { get; set; }
        List<Action> PostCommands { get; set; }

       ICommand CreateCommand(Action action, Func<object, bool> canExecute = null, int[] ignorePreCommands = null,int[] ignorePostCommands = null);

    }
}
