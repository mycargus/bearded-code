namespace ServerCore
{
    public interface IDataSource
    {
        string GetCategoryList();
        string GetImageList(int a_Index);
        byte[] GetImage(int a_Index);
    }
}