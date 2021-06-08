using ObjLoader.Loader.Data;
using ObjLoader.Loader.Data.DataStore;
using ObjLoader.Loader.TypeParsers.Interfaces;

namespace ObjLoader.Loader.TypeParsers
{
    public class GroupParser : TypeParserBase, IGroupParser
    {
        private readonly IGroupDataStore _groupDataStore;

        public GroupParser(IGroupDataStore groupDataStore)
        {
            _groupDataStore = groupDataStore;
        }

        protected override string Keyword
        {
            get { return "g"; }
        }

        public override void Parse(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
                line = "default";

            _groupDataStore.PushGroup(line);
        }
    }
}