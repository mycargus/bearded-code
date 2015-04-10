namespace ServerCore
{
    public abstract class Database : IDataSource
    {
        public abstract string GetCategoryList();
        public abstract string GetImageList(int a_Index);
        public abstract byte[] GetImage(int a_Index);
    }
}