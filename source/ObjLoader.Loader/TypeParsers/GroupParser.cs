using ObjLoader.Loader.Common;
using ObjLoader.Loader.Data;

namespace ObjLoader.Loader.TypeParsers
{
    public class GroupParser : IGroupParser
    {
        private readonly IGroupDataStore _groupDataStore;

        public GroupParser(IGroupDataStore groupDataStore)
        {
            _groupDataStore = groupDataStore;
        }

        public bool CanParse(string keyword)
        {
            return keyword.EqualsInvariantCultureIgnoreCase("g");
        }

        public void Parse(string line)
        {
            string[] parts = line.Split(new[] {' '}, 2);

            _groupDataStore.PushGroup(parts[1]);
        }
    }
}