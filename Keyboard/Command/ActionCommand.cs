﻿/*
 * Author : İbrahim GAZALOĞLU 
 * Date   : 23.03.2019
 * Email  : ibrahimgazaloglu@gmail.com  
 *
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace WPFTouchscreenKeyboard.Command
{
    internal class ActionCommand : ICommand
    {
        private readonly Action _action;
        private readonly Func<object, bool> _canExecute;
        private readonly int[] _ignorePreCommands;
        private readonly int[] _ignorePostCommands;

        public List<Action> PreCommands { get; set; }
        public List<Action> PostCommands { get; set; }


        public ActionCommand(Action action, Func<object, bool> canExecute,int[] ignorePreCommands,int[] ignorePostCommands)
        {
            _canExecute = canExecute ?? (p => true);
            _action = action;

            _ignorePostCommands = ignorePostCommands;
            _ignorePreCommands = ignorePreCommands;
        }

        public bool CanExecute(object parameter)
        {

            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            if (_action == null)
                return;

            //Pre commands
            if (PreCommands.Count > 0)
            {
                foreach (var preCommand in PreCommands)
                {
                    if (_ignorePreCommands == null || !_ignorePreCommands.Where((i, index) => index == PreCommands.IndexOf(preCommand)).Any())
                        preCommand();
                }
            }

            //Actual Command
            _action();


            //Post commands
            if (PostCommands.Count > 0)
            {
                foreach (var postCommand in PostCommands)
                {
                    if(_ignorePostCommands == null || !_ignorePostCommands.Where((i, index) => index == PostCommands.IndexOf(postCommand)).Any())
                        postCommand();
                }
            }



        }

        public event EventHandler CanExecuteChanged;
    }
}
