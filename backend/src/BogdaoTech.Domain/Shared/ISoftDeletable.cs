namespace BogdaoTech.Domain.Shared
{
    public interface ISoftDeletable
    {
        void Delete();
        void Restore();
    }
}
