using System;
using System.Text;
using System.IO;
using ServerCore;

namespace ImageServer
{
    public class ImageDbStringLiteral : IDataSource
    {
        public string GetCategoryList()
        {
            StringBuilder list = new StringBuilder();

            list.Append("<CategoryList>");
            list.Append(AddCategory("School", 1));
            list.Append(AddCategory("Work", 20));
            list.Append(AddCategory("Childhood", 7));
            list.Append(AddCategory("Kids", 111));
            list.Append(AddCategory("Wedding", 5));
            list.Append("</CategoryList>");

            return list.ToString();
        }

        private static string AddCategory(string a_Name, int a_Index)
        {
            return String.Format("<Category name=\"{0}\" index=\"{1}\"/>", a_Name, a_Index);
        }

        public string GetImageList(int a_Index)
        {
            StringBuilder list = new StringBuilder();

            list.Append("<ImageList>");
            list.Append(AddImage("image1", 12));
            list.Append(AddImage("image2", 15));
            list.Append(AddImage("image3", 1));
            list.Append(AddImage("image4", 56));
            list.Append(AddImage("image5", 3));
            list.Append("</ImageList>");

            return list.ToString();
        }

        public byte[] GetImage(int a_Index)
        {
            return File.ReadAllBytes("C:\\Users\\mycargus\\Desktop\\facebook_btn.png");
        }

        private static string AddImage(string a_Name, int a_Index)
        {
            return String.Format("<Item name=\"{0}\" index=\"{1}\"/>", a_Name, a_Index);
        }

        public void Bind()
        {
            
        }
    }
}