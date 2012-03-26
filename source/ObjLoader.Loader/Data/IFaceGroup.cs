namespace ObjLoader.Loader.Data
{
    public interface IFaceGroup
    {
        Face GetFace(int i);
        void AddFace(Face face);
    }
}