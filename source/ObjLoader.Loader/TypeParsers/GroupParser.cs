using ObjLoader.Loader.Data;

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
            string[] parts = line.Split(new[] {' '}, 2);

            _groupDataStore.PushGroup(parts[1]);
        }
    }
}