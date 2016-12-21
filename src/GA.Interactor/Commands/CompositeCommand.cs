using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GitAnalyser.Interactor.Commands
{
    internal class CompositeCommand<T> : ICommand<T>, IEnumerable<ICommand<T>>
    {
        private readonly IEnumerable<ICommand<T>> _commands;

        public CompositeCommand(params ICommand<T>[] commands)
        {
            _commands = commands;
        }

        public T Execute() => _commands.Aggregate(default(T), (x, p) => p.Execute());

        public IEnumerator<ICommand<T>> GetEnumerator() => _commands.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
