using BimApi.Models;
using System.Collections.Concurrent;

namespace BimApi.Repositories
{
    public class BimElementRepository
    {
        private readonly ConcurrentDictionary<string, BimElement> _elements = new();

        public IEnumerable<BimElement> GetAll() => _elements.Values;

        public BimElement? Get(string ifcGuid) =>
            _elements.TryGetValue(ifcGuid, out var element) ? element : null;

        public bool Add(BimElement element) =>
            _elements.TryAdd(element.IfcGuid, element);

        public bool UpdateProgress(string ifcGuid, int progressPercent)
        {
            if (!_elements.TryGetValue(ifcGuid, out var existing))
                return false;

            existing.ProgressPercent = progressPercent;
            _elements[ifcGuid] = existing;
            return true;
        }

        //public bool Delete(string ifcGuid)
        //{
        //    return _elements.TryRemove(ifcGuid, out var bimElement);
        //}
    }
}
