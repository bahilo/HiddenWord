using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HiddenWordWpf.Classes
{
    public class CustomCommands
    {
        public static readonly RoutedUICommand Exit = new RoutedUICommand
                        (
                                "toto",
                                "Exit",
                                typeof(CustomCommands),
                                new InputGestureCollection()
                                {
                                        new KeyGesture(Key.F4, ModifierKeys.Alt,"Exit")
                                }
                        );

        public static readonly RoutedUICommand Start = new RoutedUICommand
                        (
                                "Start",
                                "Start",
                                typeof(CustomCommands),
                                new InputGestureCollection()
                                {
                                        new KeyGesture(Key.F5)
                                }
                        );

        public static readonly RoutedUICommand MaxTry = new RoutedUICommand
                        (
                                "Max try",
                                "MaxTry",
                                typeof(CustomCommands),
                                new InputGestureCollection()
                                {
                                        new KeyGesture(Key.M, ModifierKeys.Alt)
                                }
                        );

        public static readonly RoutedUICommand UserSelect = new RoutedUICommand
                        (
                                "Select",
                                "UserSelect",
                                typeof(CustomCommands),
                                new InputGestureCollection()
                                {
                                        new KeyGesture(Key.S, ModifierKeys.Alt)
                                }
                        );

        public static readonly RoutedUICommand UserCreate = new RoutedUICommand
                        (
                                "Create",
                                "UserCreate",
                                typeof(CustomCommands),
                                new InputGestureCollection()
                                {
                                        new KeyGesture(Key.C, ModifierKeys.Alt)
                                }
                        );

        public static readonly RoutedUICommand Word = new RoutedUICommand
                        (
                                "Word",
                                "Word",
                                typeof(CustomCommands),
                                new InputGestureCollection()
                                {
                                        new KeyGesture(Key.W, ModifierKeys.Alt)
                                }
                        );

        public static readonly RoutedUICommand Statistics = new RoutedUICommand
                        (
                                "Statistics",
                                "Statistics",
                                typeof(CustomCommands),
                                new InputGestureCollection()
                                {
                                        new KeyGesture(Key.F2)
                                }
                        );
        
    }
}
